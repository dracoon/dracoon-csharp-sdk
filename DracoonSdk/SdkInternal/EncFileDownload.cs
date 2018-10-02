using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Dracoon.Sdk.SdkInternal {
    internal class EncFileDownload : FileDownload {

        private static readonly string LOGTAG = typeof(EncFileDownload).Name;
        private UserPrivateKey userPrivateKey;

        public EncFileDownload(DracoonClient client, string actionId, Node nodeToDownload, Stream output, UserPrivateKey privateKey) : base(client, actionId, nodeToDownload, output) {
            userPrivateKey = privateKey;
        }

        protected override void StartDownload() {
            NotifyStarted(actionId);
            RestRequest downloadTokenRequest = dracoonClient.RequestBuilder.PostFileDownload(associatedNode.Id);
            ApiDownloadToken token = dracoonClient.RequestExecutor.DoSyncApiCall<ApiDownloadToken>(downloadTokenRequest, DracoonRequestExecuter.RequestType.PostDownloadToken);
            EncryptedFileKey encryptedFileKey = dracoonClient.NodesImpl.GetEncryptedFileKey(associatedNode.Id);
            EncryptedDownload(new Uri(token.DownloadUrl), DecryptFileKey(encryptedFileKey));
            NotifyFinished(actionId);
        }

        private PlainFileKey DecryptFileKey(EncryptedFileKey encryptedFileKey) {
            try {
                return Crypto.Sdk.Crypto.DecryptFileKey(encryptedFileKey, userPrivateKey, dracoonClient.EncryptionPassword);
            } catch (CryptoException ce) {
                string message = "Decryption of file key for encrypted download " + actionId + " failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        private void EncryptedDownload(Uri downloadUri, PlainFileKey plainFileKey) {
            FileDecryptionCipher cipher = null;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileDecryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                string message = "Creation of decryption engine for encrypted download " + actionId + " failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
            try {
                progressReportTimer = Stopwatch.StartNew();
                long downloadedByteCount = 0;
                while (downloadedByteCount < associatedNode.Size.GetValueOrDefault(0)) {
                    byte[] chunk = DownloadChunk(downloadUri, ref downloadedByteCount, associatedNode.Size.GetValueOrDefault(0));
                    Debug.WriteLine("bytesDownloaded " + downloadedByteCount);
                    EncryptedDataContainer encryptedContainer = new EncryptedDataContainer(chunk, null);
                    PlainDataContainer plainContainer = cipher.ProcessBytes(encryptedContainer);
                    outputStream.Write(plainContainer.Content, 0, plainContainer.Content.Length);
                }
                Debug.WriteLine("bytesDownloaded after final " + downloadedByteCount);
                byte[] encryptionTag = Convert.FromBase64String(plainFileKey.Tag);
                EncryptedDataContainer tagContainer = new EncryptedDataContainer(null, encryptionTag);
                PlainDataContainer finalContainer = cipher.DoFinal(tagContainer);
                outputStream.Write(finalContainer.Content, 0, finalContainer.Content.Length);
                if (lastNotifiedProgressValue != downloadedByteCount) { // Notify 100 percent progress
                    NotifyProgress(actionId, downloadedByteCount, associatedNode.Size.GetValueOrDefault(0));
                }
            } catch (CryptoException ce) {
                string message = "Decryption of file failed while downloading!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonFileIOException(message, ce);
            } catch (IOException ioe) {
                if (isInterrupted) {
                    throw new ThreadInterruptedException();
                }
                string message = "Write to stream failed!";
                dracoonClient.Log.Debug(LOGTAG, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                progressReportTimer.Stop();
            }
        }
    }
}
