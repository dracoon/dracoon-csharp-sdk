using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class EncFileUpload : FileUpload {
        private readonly UserPublicKey _userPublicKey;

        public EncFileUpload(IInternalDracoonClient client, string actionId, FileUploadRequest request, Stream input, UserPublicKey publicKey,
            long fileSize) : base(client, actionId, request, input, fileSize) {
            _userPublicKey = publicKey;

            LogTag = nameof(EncFileUpload);
        }

        protected override Node StartUpload() {
            NotifyStarted(ActionId);
            ApiCreateFileUpload apiFileUploadRequest = FileMapper.ToApiCreateFileUpload(FileUploadRequest);

            try {
                apiFileUploadRequest.UseS3 = CheckUseS3();
            } catch (DracoonApiException apiException) {
                DracoonClient.Log.Warn(LogTag, "S3 direct upload is not possible.", apiException);
            }

            IRestRequest uploadTokenRequest = Client.Builder.PostCreateFileUpload(apiFileUploadRequest);
            UploadToken = Client.Executor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);

            Node publicResultNode;
            PlainFileKey plainFileKey = CreatePreferredPlainFileKey();
            ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(FileUploadRequest);
            if (apiFileUploadRequest.UseS3.HasValue && apiFileUploadRequest.UseS3.Value) {
                List<ApiS3FileUploadPart> s3Parts = EncryptedS3Upload(ref plainFileKey);
                EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
                apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);
                apiCompleteFileUpload.Parts = s3Parts;
                IRestRequest completeFileUploadRequest = Client.Builder.PutCompleteS3FileUpload(UploadToken.UploadId, apiCompleteFileUpload);
                Client.Executor.DoSyncApiCall<VoidResponse>(completeFileUploadRequest, RequestType.PutCompleteS3Upload);
                publicResultNode = NodeMapper.FromApiNode(S3Finished());
            } else {
                EncryptedUpload(ref plainFileKey);
                EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
                apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);
                IRestRequest completeFileUploadRequest =
                    Client.Builder.PutCompleteFileUpload(new Uri(UploadToken.UploadUrl).PathAndQuery, apiCompleteFileUpload);
                ApiNode resultNode = Client.Executor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
                publicResultNode = NodeMapper.FromApiNode(resultNode);
            }

            NotifyFinished(ActionId, publicResultNode);
            return publicResultNode;
        }

        private PlainFileKey CreatePreferredPlainFileKey() {
            try {
                return Crypto.Sdk.Crypto.GenerateFileKey(CryptoHelper.DeterminePlainFileKeyVersion(_userPublicKey.Version));
            } catch (CryptoException ce) {
                string message = "Creation of file key for upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        private EncryptedFileKey EncryptFileKey(PlainFileKey plainFileKey) {
            try {
                return Crypto.Sdk.Crypto.EncryptFileKey(plainFileKey, _userPublicKey);
            } catch (CryptoException ce) {
                string message = "Encryption of file key for upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(LogTag, message);
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
            DracoonClient.Log.Debug(LogTag, "Uploading file [" + FileUploadRequest.Name + "] in encrypted proxied way.");
            FileEncryptionCipher cipher;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileEncryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                string message = "Creation of encryption engine for encrypted upload " + ActionId + " failed!";
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }

            try {
                long uploadedByteCount = 0;
                byte[] buffer = new byte[DracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = InputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    EncryptedDataContainer encryptedContainer = EncryptChunk(cipher, bytesRead, buffer, false);
                    ProcessEncryptedChunk(new Uri(UploadToken.UploadUrl), encryptedContainer, uploadedByteCount, cipher, false);
                    uploadedByteCount += encryptedContainer.Content.Length;
                }

                EncryptedDataContainer finalEncryptedContainer = EncryptChunk(cipher, bytesRead, buffer, true);
                plainFileKey.Tag = ProcessEncryptedChunk(new Uri(UploadToken.UploadUrl), finalEncryptedContainer, uploadedByteCount, cipher, true);
                uploadedByteCount += finalEncryptedContainer.Content.Length;
                if (LastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, uploadedByteCount, OptionalFileSize);
                }
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                string message = "Read from stream failed!";
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer?.Stop();
            }
        }

        private string ProcessEncryptedChunk(Uri uploadUrl, EncryptedDataContainer encryptedContainer, long uploadedByteCount, FileEncryptionCipher cipher,
            bool isFinalBlock, int sendTry = 1) {
            ApiUploadChunkResult chunkResult =
                UploadChunkWebClient(uploadUrl, encryptedContainer.Content, uploadedByteCount, encryptedContainer.Content.Length);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, encryptedContainer.Content, encryptedContainer.Content.Length)) {
                if (sendTry <= 3) {
                    return ProcessEncryptedChunk(uploadUrl, encryptedContainer, uploadedByteCount, cipher, isFinalBlock, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }

            return isFinalBlock ? Convert.ToBase64String(encryptedContainer.Tag) : null;
        }

        #endregion

        #region S3 upload

        private List<ApiS3FileUploadPart> EncryptedS3Upload(ref PlainFileKey plainFileKey) {
            DracoonClient.Log.Debug(LogTag, "Uploading file [" + FileUploadRequest.Name + "] via encrypted s3 direct upload.");
            FileEncryptionCipher cipher;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileEncryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                DracoonClient.Log.Debug(LogTag, "Creation of encryption engine for encrypted upload " + ActionId + " failed!");
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }

            try {
                int chunkSize = DefineS3ChunkSize();
                int s3UrlBatchSize = DefineS3BatchSize(chunkSize);
                long uploadedByteCount = 0;
                byte[] buffer = new byte[chunkSize];
                int bytesRead, offset = 0;

                while ((bytesRead = InputStream.Read(buffer, offset, buffer.Length - offset)) >= 0) {
                    int nextByte = InputStream.ReadByte(); // Check if further bytes are available
                    byte[] chunk;

                    EncryptedDataContainer container = EncryptChunk(cipher, bytesRead + offset, buffer, false);
                    if (nextByte == -1) {
                        // It is the last block
                        EncryptedDataContainer finalContainer = EncryptChunk(cipher, bytesRead + offset, buffer, true);
                        plainFileKey.Tag = Convert.ToBase64String(finalContainer.Tag);
                        chunk = new byte[container.Content.Length + finalContainer.Content.Length];
                        Buffer.BlockCopy(finalContainer.Content, 0, chunk, container.Content.Length, finalContainer.Content.Length);
                    } else {
                        chunk = new byte[container.Content.Length];
                    }

                    Buffer.BlockCopy(container.Content, 0, chunk, 0, container.Content.Length);

                    if (chunk.Length < chunkSize) {
                        S3Urls = RequestS3Urls(S3Parts.Count + 1, 1, chunk.Length);
                    } else if (S3Urls.Count == 0) {
                        S3Urls = RequestS3Urls(S3Parts.Count + 1, s3UrlBatchSize, chunkSize);
                    }

                    string partETag = UploadS3ChunkWebClient(S3Urls.Dequeue(), chunk, uploadedByteCount);
                    S3Parts.Add(new ApiS3FileUploadPart() {
                        PartEtag = partETag,
                        PartNumber = S3Parts.Count + 1
                    });
                    uploadedByteCount += chunk.Length;

                    if (nextByte != -1) {
                        // Do it every time if the current block isn't the last
                        Buffer.SetByte(buffer, 0, (byte)nextByte);
                        offset = 1;
                    } else {
                        break;
                    }
                }

                if (LastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, uploadedByteCount, OptionalFileSize);
                }

                return S3Parts;
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                string message = "Read from stream failed!";
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer?.Stop();
            }
        }

        #endregion
    }
}