using System;
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

            IRestRequest uploadTokenRequest = Client.Builder.PostCreateFileUpload(apiFileUploadRequest);
            ApiUploadToken token = Client.Executor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);

            PlainFileKey plainFileKey = CreateFileKey();
            EncryptedUpload(new Uri(token.UploadUrl), ref plainFileKey);

            EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
            ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(FileUploadRequest);
            apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);

            IRestRequest completeFileUploadRequest =
                Client.Builder.PutCompleteFileUpload(new Uri(token.UploadUrl).PathAndQuery, apiCompleteFileUpload);
            ApiNode resultNode = Client.Executor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
            Node publicResultNode = NodeMapper.FromApiNode(resultNode);
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

        private void EncryptedUpload(Uri uploadUrl, ref PlainFileKey plainFileKey) {
            FileEncryptionCipher cipher;
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

        private string ProcessEncryptedChunk(Uri uploadUrl, byte[] buffer, long uploadedByteCount, int bytesRead, FileEncryptionCipher cipher,
            bool isFinalBlock, int sendTry = 1) {
            #region Encryption part

            EncryptedDataContainer encryptedContainer;
            if (isFinalBlock) {
                encryptedContainer = cipher.DoFinal();
            } else {
                byte[] plainChunkBytes = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, plainChunkBytes, 0, bytesRead);
                encryptedContainer = cipher.ProcessBytes(new PlainDataContainer(plainChunkBytes));
            }

            byte[] encryptedChunkBytes = encryptedContainer.Content;

            #endregion

            ApiUploadChunkResult chunkResult =
                UploadChunkWebClient(uploadUrl, encryptedChunkBytes, uploadedByteCount, encryptedChunkBytes.Length);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, encryptedChunkBytes, encryptedChunkBytes.Length)) {
                if (sendTry <= 3) {
                    return ProcessEncryptedChunk(uploadUrl, buffer, uploadedByteCount, bytesRead, cipher, isFinalBlock, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }

            return isFinalBlock ? Convert.ToBase64String(encryptedContainer.Tag) : null;
        }
    }
}