using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using RestSharp;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class EncFileUpload : FileUpload {
        private readonly UserPublicKey _userPublicKey;

        public EncFileUpload(IInternalDracoonClient client, string actionId, FileUploadRequest request, Stream input, UserPublicKey publicKey, long fileSize) :
            base(client, actionId, request, input, fileSize) {
            _userPublicKey = publicKey;

            Logtag = nameof(EncFileUpload);
        }

        protected override Node StartUpload() {
            NotifyStarted(ActionId);
            ApiCreateFileUpload apiFileUploadRequest = FileMapper.ToApiCreateFileUpload(FileUploadRequest);
            if (apiFileUploadRequest.Classification == null) {
                try {
                    Client.Executor.CheckApiServerVersion(ApiConfig.ApiUseHomeDefaultClassificationMinApiVersion);
                } catch (DracoonApiException) {
                    apiFileUploadRequest.Classification = 1;
                }
            }
            try {
                apiFileUploadRequest.UseS3 = CheckUseS3();
            } catch (DracoonApiException apiException) {
                dracoonClient.Log.Warn(LOGTAG, "S3 direct upload is not possible.", apiException);
            }
            IRestRequest uploadTokenRequest = Client.Builder.PostCreateFileUpload(apiFileUploadRequest);
            ApiUploadToken token = Client.Executor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);

            Node publicResultNode = null;
            PlainFileKey plainFileKey = CreateFileKey();
            ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(fileUploadRequest);
            if (apiFileUploadRequest.UseS3.HasValue && apiFileUploadRequest.UseS3.Value) {
                List<ApiS3FileUploadPart> s3Parts = EncryptedS3Upload(ref plainFileKey);
                EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
                apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);
                apiCompleteFileUpload.Parts = s3Parts;
                RestRequest completeFileUploadRequest =
                    dracoonClient.RequestBuilder.PutCompleteS3FileUpload(uploadToken.UploadId, apiCompleteFileUpload);
                dracoonClient.RequestExecutor.DoSyncApiCall<VoidResponse>(completeFileUploadRequest,
                    DracoonRequestExecuter.RequestType.PutCompleteS3Upload);
                publicResultNode = NodeMapper.FromApiNode(S3Finished());
            } else {
                EncryptedUpload(ref plainFileKey);
                EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
                apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);
                RestRequest completeFileUploadRequest =
                    dracoonClient.RequestBuilder.PutCompleteFileUpload(new Uri(uploadToken.UploadUrl).PathAndQuery, apiCompleteFileUpload);
                ApiNode resultNode =
                    dracoonClient.RequestExecutor.DoSyncApiCall<ApiNode>(completeFileUploadRequest,
                        DracoonRequestExecuter.RequestType.PutCompleteUpload);
                publicResultNode = NodeMapper.FromApiNode(resultNode);
            }

            NotifyFinished(ActionId, publicResultNode);
            return publicResultNode;
        }

        private PlainFileKey CreateFileKey() {
            try {
                return Crypto.Sdk.Crypto.GenerateFileKey(_userPublicKey.Version);
            } catch (CryptoException ce) {
                string message = "Creation of file key for upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        private EncryptedFileKey EncryptFileKey(PlainFileKey plainFileKey) {
            try {
                return Crypto.Sdk.Crypto.EncryptFileKey(plainFileKey, _userPublicKey);
            } catch (CryptoException ce) {
                string message = "Encryption of file key for upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        private EncryptedDataContainer EncryptChunk(FileEncryptionCipher cipher, int byteCount, byte[] chunk, bool isFinalBlock) {
            if (isFinalBlock) {
                return cipher.DoFinal();
            }

            byte[] plainChunkBytes = new byte[byteCount];
            Buffer.BlockCopy(chunk, 0, plainChunkBytes, 0, byteCount);
            return cipher.ProcessBytes(new PlainDataContainer(plainChunkBytes));
        }

        #region Normal upload

        private void EncryptedUpload(ref PlainFileKey plainFileKey) {
            dracoonClient.Log.Debug(LOGTAG, "Uploading file [" + fileUploadRequest.Name + "] in encrypted proxied way.");
            FileEncryptionCipher cipher = null;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileEncryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                string message = "Creation of encryption engine for encrypted upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }

            try {
                ProgressReportTimer = Stopwatch.StartNew();
                long uploadedByteCount = 0;
                byte[] buffer = new byte[DracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = InputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    ProcessEncryptedChunk(uploadUrl, buffer, uploadedByteCount, bytesRead, cipher, false);
                    uploadedByteCount += bytesRead;
                }

                plainFileKey.Tag = ProcessEncryptedChunk(uploadUrl, buffer, uploadedByteCount, bytesRead, cipher, true);
                if (LastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, uploadedByteCount, OptionalFileSize);
                }
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                string message = "Read from stream failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer.Stop();
            }
        }

        private string ProcessEncryptedChunk(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount, int bytesRead, FileEncryptionCipher cipher,
            bool isFinalBlock, int sendTry = 1) {
            EncryptedDataContainer encryptedContainer = EncryptChunk(cipher, bytesRead, buffer, isFinalBlock);

            ApiUploadChunkResult chunkResult =
                UploadChunkWebClient(uploadUrl, encryptedContainer.Content, ref uploadedByteCount, encryptedContainer.Content.Length);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, encryptedContainer.Content, encryptedContainer.Content.Length)) {
                if (sendTry <= 3) {
                    return ProcessEncryptedChunk(uploadUrl, buffer, uploadedByteCount, bytesRead, cipher, isFinalBlock, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }

            return isFinalBlock ? Convert.ToBase64String(encryptedContainer.Tag) : null;
        }

        #endregion

        #region S3 upload

        private List<ApiS3FileUploadPart> EncryptedS3Upload(ref PlainFileKey plainFileKey) {
            dracoonClient.Log.Debug(LOGTAG, "Uploading file [" + fileUploadRequest.Name + "] via encrypted s3 direct upload.");
            FileEncryptionCipher cipher = null;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileEncryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                string message = "Creation of encryption engine for encrypted upload " + actionId + " failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }

            try {
                progressReportTimer = Stopwatch.StartNew();

                int chunkSize = DefineS3ChunkSize();
                int s3UrlBatchSize = DefineS3BatchSize(chunkSize);
                List<ApiS3FileUploadPart> s3Parts = new List<ApiS3FileUploadPart>();
                Queue<Uri> s3Urls = new Queue<Uri>();
                long uploadedByteCount = 0;
                byte[] buffer = new byte[chunkSize];
                int bytesRead = 0;
                int offset = 0;

                while ((bytesRead = inputStream.Read(buffer, offset, buffer.Length - offset)) >= 0) {
                    int nextByte = inputStream.ReadByte(); // Check if further bytes are available
                    int chunkByteCount = 0;

                    EncryptedDataContainer container = EncryptChunk(cipher, bytesRead + offset, buffer, false);
                    chunkByteCount = container.Content.Length;
                    if (nextByte == -1) {
                        // It is the last block
                        EncryptedDataContainer finalContainer = EncryptChunk(cipher, bytesRead + offset, buffer, true);
                        chunkByteCount += finalContainer.Content.Length;
                        plainFileKey.Tag = Convert.ToBase64String(finalContainer.Tag);
                        buffer = new byte[chunkByteCount];
                        Buffer.BlockCopy(container.Content, 0, buffer, 0, container.Content.Length);
                        Buffer.BlockCopy(finalContainer.Content, 0, buffer, container.Content.Length, finalContainer.Content.Length);
                    } else {
                        buffer = new byte[chunkByteCount];
                        Buffer.BlockCopy(container.Content, 0, buffer, 0, container.Content.Length);
                    }

                    if (chunkByteCount < chunkSize) {
                        s3Urls = RequestS3Urls(s3Parts.Count + 1, 1, chunkByteCount);
                    } else if (s3Urls.Count == 0) {
                        s3Urls = RequestS3Urls(s3Parts.Count + 1, s3UrlBatchSize, chunkSize);
                    }

                    string partETag = UploadS3ChunkWebClient(s3Urls.Dequeue(), buffer, ref uploadedByteCount);
                    s3Parts.Add(new ApiS3FileUploadPart() {
                        PartEtag = partETag,
                        PartNumber = s3Parts.Count + 1
                    });

                    if (nextByte != -1) {
                        // Do it every time if the current block isn't the last
                        Buffer.SetByte(buffer, 0, (byte) nextByte);
                        offset = 1;
                    } else {
                        break;
                    }
                }

                if (lastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(actionId, uploadedByteCount, optionalFileSize);
                }

                return s3Parts;
            } catch (IOException ioe) {
                if (isInterrupted) {
                    throw new ThreadInterruptedException();
                }

                string message = "Read from stream failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                progressReportTimer.Stop();
            }
        }

        #endregion
    }
}