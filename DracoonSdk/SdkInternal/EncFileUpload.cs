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

namespace Dracoon.Sdk.SdkInternal {
    internal class EncFileUpload : FileUpload {

        private UserPublicKey userPublicKey;

        public EncFileUpload(DracoonClient client, string actionId, FileUploadRequest request, Stream input, UserPublicKey publicKey, long fileSize) : base(client, actionId, request, input, fileSize) {
            userPublicKey = publicKey;

            LOGTAG = typeof(EncFileUpload).Name;
        }

        protected override Node StartUpload() {
            NotifyStarted(actionId);
            ApiCreateFileUpload apiFileUploadRequest = FileMapper.ToApiCreateFileUpload(fileUploadRequest);
            if (apiFileUploadRequest.Classification == null) {
                try {
                    dracoonClient.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiUseHomeDefaultClassificationMinApiVersion);
                } catch (DracoonApiException) {
                    apiFileUploadRequest.Classification = 1;
                }
            }

            RestRequest uploadTokenRequest = dracoonClient.RequestBuilder.PostCreateFileUpload(apiFileUploadRequest);
            ApiUploadToken token = dracoonClient.RequestExecutor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, DracoonRequestExecuter.RequestType.PostUploadToken);

            PlainFileKey plainFileKey = CreateFileKey();
            EncryptedUpload(new Uri(token.UploadUrl), ref plainFileKey);

            EncryptedFileKey encryptedFileKey = EncryptFileKey(plainFileKey);
            ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(fileUploadRequest);
            apiCompleteFileUpload.FileKey = FileMapper.ToApiFileKey(encryptedFileKey);

            RestRequest completeFileUploadRequest = dracoonClient.RequestBuilder.PutCompleteFileUpload(new Uri(token.UploadUrl).PathAndQuery, apiCompleteFileUpload);
            ApiNode resultNode = dracoonClient.RequestExecutor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, DracoonRequestExecuter.RequestType.PutCompleteUpload);
            Node publicResultNode = NodeMapper.FromApiNode(resultNode);
            NotifyFinished(actionId, publicResultNode);
            return publicResultNode;
        }

        private PlainFileKey CreateFileKey() {
            try {
                return Crypto.Sdk.Crypto.GenerateFileKey(userPublicKey.Version);
            } catch (CryptoException ce) {
                string message = "Creation of file key for upload " + actionId + " failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        private EncryptedFileKey EncryptFileKey(PlainFileKey plainFileKey) {
            try {
                return Crypto.Sdk.Crypto.EncryptFileKey(plainFileKey, userPublicKey);
            } catch (CryptoException ce) {
                string message = "Encryption of file key for upload " + actionId + " failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        private void EncryptedUpload(Uri uploadUrl, ref PlainFileKey plainFileKey) {
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
                long uploadedByteCount = 0;
                byte[] buffer = new byte[dracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    ProcessEncryptedChunk(uploadUrl, buffer, ref uploadedByteCount, bytesRead, cipher, false);
                }
                plainFileKey.Tag = ProcessEncryptedChunk(uploadUrl, buffer, ref uploadedByteCount, bytesRead, cipher, true);
                if (lastNotifiedProgressValue != uploadedByteCount) { // Notify 100 percent progress
                    NotifyProgress(actionId, uploadedByteCount, optionalFileSize);
                }
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

        private string ProcessEncryptedChunk(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount, int bytesRead, FileEncryptionCipher cipher, bool isFinalBlock, int sendTry = 1) {
            #region Encryption part
            EncryptedDataContainer encryptedContainer = null;
            if (isFinalBlock) {
                encryptedContainer = cipher.DoFinal();
            } else {
                byte[] plainChunkBytes = new byte[bytesRead];
                Buffer.BlockCopy(buffer, 0, plainChunkBytes, 0, bytesRead);
                encryptedContainer = cipher.ProcessBytes(new PlainDataContainer(plainChunkBytes));
            }
            byte[] encryptedChunkBytes = encryptedContainer.Content;
            #endregion

            ApiUploadChunkResult chunkResult = UploadChunkWebClient(uploadUrl, encryptedChunkBytes, ref uploadedByteCount, encryptedChunkBytes.Length);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, encryptedChunkBytes, encryptedChunkBytes.Length)) {
                if (sendTry <= 3) {
                    ProcessEncryptedChunk(uploadUrl, buffer, ref uploadedByteCount, bytesRead, cipher, isFinalBlock, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }
            return isFinalBlock ? Convert.ToBase64String(encryptedContainer.Tag) : null;
        }
    }
}
