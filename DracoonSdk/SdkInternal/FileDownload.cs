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
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class FileDownload {
        protected string LogTag = nameof(FileDownload);

        protected const long ProgressUpdateInterval = 250;

        protected List<IFileDownloadCallback> Callbacks = new List<IFileDownloadCallback>();
        protected IInternalDracoonClient Client;
        protected Node AssociatedNode;
        protected string ActionId;
        internal Thread RunningThread;
        protected Stream OutputStream;
        protected Stopwatch ProgressReportTimer;
        protected bool IsInterrupted;
        protected long LastNotifiedProgressValue;

        public FileDownload(IInternalDracoonClient client, string actionId, Node nodeToDownload, Stream output) {
            Client = client;
            OutputStream = output;
            ActionId = actionId;
            AssociatedNode = nodeToDownload;
        }

        public void AddFileDownloadCallback(IFileDownloadCallback callback) {
            if (callback != null) {
                Callbacks.Add(callback);
            }
        }

        public void RunAsync() {
            void Child() {
                try {
                    StartDownload();
                } catch (DracoonException de) {
                    NotifyFailed(ActionId, de);
                } catch (ThreadAbortException) {
                    NotifyCanceled(ActionId);
                } catch (ThreadInterruptedException) {
                    NotifyCanceled(ActionId);
                }
            }

            RunningThread = new Thread(Child);
            RunningThread.Start();
        }

        public void RunSync() {
            try {
                StartDownload();
            } catch (DracoonException de) {
                NotifyFailed(ActionId, de);
                throw;
            } catch (ThreadAbortException) {
                NotifyCanceled(ActionId);
            } catch (ThreadInterruptedException) {
                NotifyCanceled(ActionId);
            }
        }

        public void CancelDownload() {
            if (RunningThread != null && RunningThread.IsAlive) {
                IsInterrupted = true;
                RunningThread.Abort();
            }
        }

        protected virtual void StartDownload() {
            NotifyStarted(ActionId);
            RestRequest downloadTokenRequest = Client.Builder.PostFileDownload(AssociatedNode.Id);
            ApiDownloadToken token = Client.Executor.DoSyncApiCall<ApiDownloadToken>(downloadTokenRequest, RequestType.PostDownloadToken);
            Download(new Uri(token.DownloadUrl));
            NotifyFinished(ActionId);
        }

        private void Download(Uri downloadUri) {
            try {
                ProgressReportTimer = Stopwatch.StartNew();
                long downloadedByteCount = 0;
                while (downloadedByteCount < AssociatedNode.Size.GetValueOrDefault(0)) {
                    byte[] chunk = DownloadChunk(downloadUri, downloadedByteCount, AssociatedNode.Size.GetValueOrDefault(0));
                    OutputStream.Write(chunk, 0, chunk.Length);
                    downloadedByteCount += chunk.Length;
                }

                if (LastNotifiedProgressValue != downloadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, downloadedByteCount, AssociatedNode.Size.GetValueOrDefault(0));
                }
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                const string message = "Write to stream failed!";
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer.Stop();
            }
        }

        protected byte[] DownloadChunk(Uri downloadUri, long downloadedByteCount, long totalSize) {
            byte[] chunkDownloadedResultBytes;
            long requestCount = totalSize - downloadedByteCount;
            if (requestCount > DracoonClient.HttpConfig.ChunkSize) {
                requestCount = DracoonClient.HttpConfig.ChunkSize;
            }

            using (WebClient requestClient = Client.Builder.ProvideChunkDownloadWebClient(downloadedByteCount, requestCount)) {
                long currentDownloadedByteCount = downloadedByteCount;
                requestClient.DownloadProgressChanged += (o, progessEvent) => {
                    lock (ProgressReportTimer) {
                        if (ProgressReportTimer.ElapsedMilliseconds > ProgressUpdateInterval) {
                            NotifyProgress(ActionId, currentDownloadedByteCount + progessEvent.BytesReceived, totalSize);
                            LastNotifiedProgressValue = currentDownloadedByteCount + progessEvent.BytesReceived;
                            ProgressReportTimer.Restart();
                        }
                    }
                };
                chunkDownloadedResultBytes =
                    Client.Executor.ExecuteWebClientDownload(requestClient, downloadUri, RequestType.GetDownloadChunk, RunningThread);
            }

            return chunkDownloadedResultBytes;
        }

        #region Callback helper functions

        protected void NotifyStarted(string actionId) {
            Callbacks.ForEach(currentCallback => currentCallback.OnStarted(actionId));
        }

        protected void NotifyProgress(string actionId, long bytesDone, long bytesTotal) {
            Callbacks.ForEach(currentCallback => currentCallback.OnRunning(actionId, bytesDone, bytesTotal));
        }

        protected void NotifyFinished(string actionId) {
            Callbacks.ForEach(currentCallback => currentCallback.OnFinished(actionId));
        }

        protected void NotifyCanceled(string actionId) {
            Callbacks.ForEach(currentCallback => currentCallback.OnCanceled(actionId));
        }

        protected void NotifyFailed(string id, DracoonException de) {
            Callbacks.ForEach(currentCallback => currentCallback.OnFailed(ActionId, de));
        }

        #endregion
    }
}