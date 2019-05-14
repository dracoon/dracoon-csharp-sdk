using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal {
    internal class FileDownload {

        private static readonly string LOGTAG = typeof(FileDownload).Name;

        protected readonly long PROGRESS_UPDATE_INTERVAL = 250;

        protected List<IFileDownloadCallback> callbacks = new List<IFileDownloadCallback>();
        protected DracoonClient dracoonClient;
        protected Node associatedNode;
        protected string actionId;
        protected Thread runningThread = null;
        protected Stream outputStream;
        protected Stopwatch progressReportTimer;
        protected bool isInterrupted = false;
        protected long lastNotifiedProgressValue = 0;

        public FileDownload(DracoonClient client, string actionId, Node nodeToDownload, Stream output) {
            dracoonClient = client;
            outputStream = output;
            this.actionId = actionId;
            associatedNode = nodeToDownload;
        }

        public void AddFileDownloadCallback(IFileDownloadCallback callback) {
            if (callback != null) {
                callbacks.Add(callback);
            }
        }

        public void RemoveFileDownloadCallback(IFileDownloadCallback callback) {
            if (callback != null) {
                callbacks.Remove(callback);
            }
        }

        public void RunAsync() {
            ThreadStart child = new ThreadStart(() => {
                try {
                    StartDownload();
                } catch (DracoonException de) {
                    NotifyFailed(actionId, de);
                } catch (ThreadAbortException) {
                    NotifyCanceled(actionId);
                } catch (ThreadInterruptedException) {
                    NotifyCanceled(actionId);
                }
            });
            runningThread = new Thread(child);
            runningThread.Start();
        }

        public void RunSync() {
            try {
                StartDownload();
            } catch (DracoonException de) {
                NotifyFailed(actionId, de);
                throw de;
            } catch (ThreadAbortException) {
                NotifyCanceled(actionId);
            } catch (ThreadInterruptedException) {
                NotifyCanceled(actionId);
            }
        }

        public void CancelDownload() {
            if (runningThread != null && runningThread.IsAlive) {
                isInterrupted = true;
                runningThread.Abort();
            }
        }

        protected virtual void StartDownload() {
            NotifyStarted(actionId);
            RestRequest downloadTokenRequest = dracoonClient.RequestBuilder.PostFileDownload(associatedNode.Id);
            ApiDownloadToken token = dracoonClient.RequestExecutor.DoSyncApiCall<ApiDownloadToken>(downloadTokenRequest, DracoonRequestExecuter.RequestType.PostDownloadToken);
            Download(new Uri(token.DownloadUrl));
            NotifyFinished(actionId);
        }

        private void Download(Uri downloadUri) {
            try {
                progressReportTimer = Stopwatch.StartNew();
                long downloadedByteCount = 0;
                while (downloadedByteCount < associatedNode.Size.GetValueOrDefault(0)) {
                    byte[] chunk = DownloadChunk(downloadUri, ref downloadedByteCount, associatedNode.Size.GetValueOrDefault(0));
                    outputStream.Write(chunk, 0, chunk.Length);
                }
                if (lastNotifiedProgressValue != downloadedByteCount) { // Notify 100 percent progress
                    NotifyProgress(actionId, downloadedByteCount, associatedNode.Size.GetValueOrDefault(0));
                }
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

        protected byte[] DownloadChunk(Uri downloadUri, ref long downloadedByteCount, long totalSize) {
            byte[] chunkDownloadedResultBytes;
            long requestCount = totalSize - downloadedByteCount;
            if (requestCount > dracoonClient.HttpConfig.ChunkSize) {
                requestCount = dracoonClient.HttpConfig.ChunkSize;
            }

            using (WebClient requestClient = dracoonClient.RequestBuilder.ProvideChunkDownloadWebClient(downloadedByteCount, requestCount)) {
                long currentDownloadedByteCount = downloadedByteCount;
                long previousBytesReceivedValue = 0;
                requestClient.DownloadProgressChanged += (o, progessEvent) => {
                    previousBytesReceivedValue = progessEvent.BytesReceived;
                    if (progressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL) {
                        NotifyProgress(actionId, currentDownloadedByteCount + previousBytesReceivedValue, totalSize);
                        lastNotifiedProgressValue = currentDownloadedByteCount + previousBytesReceivedValue;
                        progressReportTimer.Restart();
                    }
                };
                chunkDownloadedResultBytes = dracoonClient.RequestExecutor.ExecuteWebClientDownload(requestClient, downloadUri, RequestType.GetDownloadChunk, runningThread);
                downloadedByteCount += previousBytesReceivedValue;
            }
            return chunkDownloadedResultBytes;
        }

        #region Callback helper functions

        protected void NotifyStarted(string actionId) {
            callbacks.ForEach(currentCallback => currentCallback.OnStarted(actionId));
        }

        protected void NotifyProgress(string actionId, long bytesDone, long bytesTotal) {
            callbacks.ForEach(currentCallback => currentCallback.OnRunning(actionId, bytesDone, bytesTotal));
        }

        protected void NotifyFinished(string actionId) {
            callbacks.ForEach(currentCallback => currentCallback.OnFinished(actionId));
        }

        protected void NotifyCanceled(string actionId) {
            callbacks.ForEach(currentCallback => currentCallback.OnCanceled(actionId));
        }

        protected void NotifyFailed(string id, DracoonException de) {
            callbacks.ForEach(currentCallback => currentCallback.OnFailed(actionId, de));
        }

        #endregion
    }
}
