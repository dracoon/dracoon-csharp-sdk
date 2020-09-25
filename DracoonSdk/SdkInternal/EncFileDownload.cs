using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class EncFileDownload : FileDownload {

        public EncFileDownload(IInternalDracoonClient client, string actionId, Node nodeToDownload, Stream output) : base(
            client, actionId, nodeToDownload, output) {

            Logtag = nameof(EncFileDownload);
        }

        protected override void StartDownload() {
            NotifyStarted(ActionId);
            IRestRequest downloadTokenRequest = Client.Builder.PostFileDownload(AssociatedNode.Id);
            ApiDownloadToken token = Client.Executor.DoSyncApiCall<ApiDownloadToken>(downloadTokenRequest, RequestType.PostDownloadToken);
            EncryptedFileKey encryptedFileKey = Client.NodesImpl.GetEncryptedFileKey(AssociatedNode.Id);
            UserKeyPair keyPair = Client.AccountImpl.GetAndCheckUserKeyPair(CryptoHelper.DetermineUserKeyPairVersion(encryptedFileKey.Version));
            EncryptedDownload(new Uri(token.DownloadUrl), DecryptFileKey(encryptedFileKey, keyPair.UserPrivateKey));
            NotifyFinished(ActionId);
        }

        private PlainFileKey DecryptFileKey(EncryptedFileKey encryptedFileKey, UserPrivateKey userPrivateKey) {
            try {
                return Crypto.Sdk.Crypto.DecryptFileKey(encryptedFileKey, userPrivateKey, Client.EncryptionPassword);
            } catch (CryptoException ce) {
                string message = "Decryption of file key for encrypted download " + ActionId + " failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        private void EncryptedDownload(Uri downloadUri, PlainFileKey plainFileKey) {
            FileDecryptionCipher cipher;
            try {
                cipher = Crypto.Sdk.Crypto.CreateFileDecryptionCipher(plainFileKey);
            } catch (CryptoException ce) {
                string message = "Creation of decryption engine for encrypted download " + ActionId + " failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }

            try {
                ProgressReportTimer = Stopwatch.StartNew();
                long downloadedByteCount = 0;
                while (downloadedByteCount < AssociatedNode.Size.GetValueOrDefault(0)) {
                    byte[] chunk = DownloadChunk(downloadUri, downloadedByteCount, AssociatedNode.Size.GetValueOrDefault(0));
                    EncryptedDataContainer encryptedContainer = new EncryptedDataContainer(chunk, null);
                    PlainDataContainer plainContainer = cipher.ProcessBytes(encryptedContainer);
                    OutputStream.Write(plainContainer.Content, 0, plainContainer.Content.Length);
                    downloadedByteCount += chunk.Length;
                }

                byte[] encryptionTag = Convert.FromBase64String(plainFileKey.Tag);
                EncryptedDataContainer tagContainer = new EncryptedDataContainer(null, encryptionTag);
                PlainDataContainer finalContainer = cipher.DoFinal(tagContainer);
                OutputStream.Write(finalContainer.Content, 0, finalContainer.Content.Length);
                if (LastNotifiedProgressValue != downloadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, downloadedByteCount, AssociatedNode.Size.GetValueOrDefault(0));
                }
            } catch (CryptoException ce) {
                const string message = "Decryption of file failed while downloading!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonFileIOException(message, ce);
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                const string message = "Write to stream failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer.Stop();
            }
        }
    }
}