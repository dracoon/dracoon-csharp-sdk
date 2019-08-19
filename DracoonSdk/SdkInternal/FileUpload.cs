using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class FileUpload {
        protected string Logtag = nameof(FileUpload);

        protected const long ProgressUpdateInterval = 500;

        protected List<IFileUploadCallback> Callbacks = new List<IFileUploadCallback>();
        protected IInternalDracoonClient Client;
        protected string ActionId;
        internal Thread RunningThread;
        protected Stream InputStream;
        protected Stopwatch ProgressReportTimer;
        protected bool IsInterrupted;
        protected FileUploadRequest FileUploadRequest;
        protected long OptionalFileSize;
        protected long LastNotifiedProgressValue;

        public FileUpload(IInternalDracoonClient client, string actionId, FileUploadRequest request, Stream input, long fileSize) {
            Client = client;
            ActionId = actionId;
            InputStream = input;
            FileUploadRequest = request;
            OptionalFileSize = fileSize;
        }

        public void AddFileUploadCallback(IFileUploadCallback callback) {
            if (callback != null) {
                Callbacks.Add(callback);
            }
        }

        public void RunAsync() {
            void Child() {
                try {
                    StartUpload();
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

        public Node RunSync() {
            try {
                return StartUpload();
            } catch (DracoonException de) {
                NotifyFailed(ActionId, de);
                throw de;
            } catch (ThreadAbortException) {
                NotifyCanceled(ActionId);
                return null;
            } catch (ThreadInterruptedException) {
                NotifyCanceled(ActionId);
                return null;
            }
        }

        public void CancelUpload() {
            if (RunningThread != null && RunningThread.IsAlive) {
                IsInterrupted = true;
                RunningThread.Abort();
            }
        }

        protected virtual Node StartUpload() {
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
            Upload(new Uri(token.UploadUrl));
            IRestRequest completeFileUploadRequest = Client.Builder.PutCompleteFileUpload(new Uri(token.UploadUrl).PathAndQuery,
                FileMapper.ToApiCompleteFileUpload(FileUploadRequest));
            ApiNode resultNode = Client.Executor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
            Node publicResultNode = NodeMapper.FromApiNode(resultNode);
            NotifyFinished(ActionId, publicResultNode);
            return publicResultNode;
        }

        private void Upload(Uri uploadUrl) {
            try {
                ProgressReportTimer = Stopwatch.StartNew();
                long uploadedByteCount = 0;
                byte[] buffer = new byte[DracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = InputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    ProcessChunk(uploadUrl, buffer, uploadedByteCount, bytesRead);
                    uploadedByteCount += bytesRead;
                }

                if (LastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(ActionId, uploadedByteCount, OptionalFileSize);
                }
            } catch (IOException ioe) {
                if (IsInterrupted) {
                    throw new ThreadInterruptedException();
                }

                const string message = "Read from stream failed!";
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer.Stop();
            }
        }

        private void ProcessChunk(Uri uploadUrl, byte[] buffer, long uploadedByteCount, int bytesRead, int sendTry = 1) {
            ApiUploadChunkResult chunkResult = UploadChunkWebClient(uploadUrl, buffer, uploadedByteCount, bytesRead);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, buffer, bytesRead)) {
                if (sendTry <= 3) {
                    ProcessChunk(uploadUrl, buffer, uploadedByteCount, bytesRead, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }
        }

        protected ApiUploadChunkResult UploadChunkWebClient(Uri uploadUrl, byte[] buffer, long uploadedByteCount, int bytesRead) {
            #region Build multipart

            string formDataBoundary = "---------------------------" + Guid.NewGuid();
            byte[] packageHeader = ApiConfig.ENCODING.GetBytes(
                $"--{formDataBoundary}\r\nContent-Disposition: form-data; name=\"{"file"}\"; filename=\"{FileUploadRequest.Name}\"\r\n\r\n");
            byte[] packageFooter = ApiConfig.ENCODING.GetBytes(string.Format("\r\n--" + formDataBoundary + "--"));
            byte[] multipartFormatedChunkData = new byte[packageHeader.Length + packageFooter.Length + bytesRead];
            Buffer.BlockCopy(packageHeader, 0, multipartFormatedChunkData, 0, packageHeader.Length);
            Buffer.BlockCopy(buffer, 0, multipartFormatedChunkData, packageHeader.Length, bytesRead);
            Buffer.BlockCopy(packageFooter, 0, multipartFormatedChunkData, packageHeader.Length + bytesRead, packageFooter.Length);

            #endregion

            long headerLength = packageFooter.LongLength + packageHeader.LongLength;

            using (WebClient requestClient = Client.Builder.ProvideChunkUploadWebClient(bytesRead, uploadedByteCount, formDataBoundary,
                OptionalFileSize == -1 ? "*" : OptionalFileSize.ToString())) {
                long currentUploadedByteCount = uploadedByteCount;
                requestClient.UploadProgressChanged += (o, progressEvent) => {
                    lock (ProgressReportTimer) {
                        long increaseWithoutHeader = (progressEvent.BytesSent - headerLength);
                        if (ProgressReportTimer.ElapsedMilliseconds > ProgressUpdateInterval && increaseWithoutHeader > 0) {
                            LastNotifiedProgressValue = currentUploadedByteCount + increaseWithoutHeader;
                            NotifyProgress(ActionId, LastNotifiedProgressValue, OptionalFileSize);
                            ProgressReportTimer.Restart();
                        }
                    }
                };
                byte[] chunkUploadResultBytes = Client.Executor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, multipartFormatedChunkData,
                    RequestType.PostUploadChunk, RunningThread);
                ApiUploadChunkResult chunkUploadResult =
                    JsonConvert.DeserializeObject<ApiUploadChunkResult>(ApiConfig.ENCODING.GetString(chunkUploadResultBytes));
                return chunkUploadResult;
            }
        }

        #region Callback helper functions

        protected void NotifyStarted(string actionId) {
            Callbacks.ForEach(currentCallback => currentCallback.OnStarted(actionId));
        }

        protected void NotifyProgress(string actionId, long bytesDone, long bytesTotal) {
            Callbacks.ForEach(currentCallback => currentCallback.OnRunning(actionId, bytesDone, bytesTotal));
        }

        protected void NotifyFinished(string actionId, Node result) {
            Callbacks.ForEach(currentCallback => currentCallback.OnFinished(actionId, result));
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