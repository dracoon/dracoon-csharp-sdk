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

namespace Dracoon.Sdk.SdkInternal {
    internal class FileUpload {
        protected string LOGTAG;
        
        protected readonly long PROGRESS_UPDATE_INTERVAL = 250;

        protected List<IFileUploadCallback> callbacks = new List<IFileUploadCallback>();
        protected DracoonClient dracoonClient;
        protected string actionId;
        protected Thread runningThread = null;
        protected Stream inputStream;
        protected Stopwatch progressReportTimer;
        protected bool isInterrupted = false;
        protected FileUploadRequest fileUploadRequest;
        protected long optionalFileSize;
        protected long lastNotifiedProgressValue = 0;

        public FileUpload(DracoonClient client, string actionId, FileUploadRequest request, Stream input, long fileSize) {
            dracoonClient = client;
            this.actionId = actionId;
            inputStream = input;
            fileUploadRequest = request;
            optionalFileSize = fileSize;

            LOGTAG = typeof(FileUpload).Name;
        }

        public void AddFileUploadCallback(IFileUploadCallback callback) {
            if (callback != null) {
                callbacks.Add(callback);
            }
        }

        public void RemoveFileUploadCallback(IFileUploadCallback callback) {
            if (callback != null) {
                callbacks.Remove(callback);
            }
        }

        public void RunAsync() {
            ThreadStart child = new ThreadStart(() => {
                try {
                    StartUpload();
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

        public Node RunSync() {
            try {
                return StartUpload();
            } catch (DracoonException de) {
                NotifyFailed(actionId, de);
                throw de;
            } catch (ThreadAbortException) {
                NotifyCanceled(actionId);
                return null;
            } catch (ThreadInterruptedException) {
                NotifyCanceled(actionId);
                return null;
            }
        }

        public void CancelUpload() {
            if (runningThread != null && runningThread.IsAlive) {
                isInterrupted = true;
                runningThread.Abort();
            }
        }

        protected virtual Node StartUpload() {
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
            Upload(new Uri(token.UploadUrl));
            RestRequest completeFileUploadRequest = dracoonClient.RequestBuilder.PutCompleteFileUpload(new Uri(token.UploadUrl).PathAndQuery, FileMapper.ToApiCompleteFileUpload(fileUploadRequest));
            ApiNode resultNode = dracoonClient.RequestExecutor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, DracoonRequestExecuter.RequestType.PutCompleteUpload);
            Node publicResultNode = NodeMapper.FromApiNode(resultNode);
            NotifyFinished(actionId, publicResultNode);
            return publicResultNode;
        }

        private void Upload(Uri uploadUrl) {
            try {
                progressReportTimer = Stopwatch.StartNew();
                long uploadedByteCount = 0;
                byte[] buffer = new byte[dracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    ProcessChunk(uploadUrl, buffer, ref uploadedByteCount, bytesRead);
                }
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

        private void ProcessChunk(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount, int bytesRead, int sendTry = 1) {
            ApiUploadChunkResult chunkResult = UploadChunkWebClient(uploadUrl, buffer, ref uploadedByteCount, bytesRead);
            if (!FileHash.CompareFileHashes(chunkResult.Hash, buffer, bytesRead)) {
                if (sendTry <= 3) {
                    ProcessChunk(uploadUrl, buffer, ref uploadedByteCount, bytesRead, sendTry + 1);
                } else {
                    throw new DracoonNetIOException("The uploaded chunk hash and local chunk hash are not equal!");
                }
            }
        }

        protected ApiUploadChunkResult UploadChunkWebClient(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount, int bytesRead) {
            #region Build multipart
            string formDataBoundary = "---------------------------" + Guid.NewGuid();
            byte[] packageHeader = ApiConfig.encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n", formDataBoundary, "file", fileUploadRequest.Name));
            byte[] packageFooter = ApiConfig.encoding.GetBytes(string.Format("\r\n--" + formDataBoundary + "--"));
            byte[] multipartFormatedChunkData = new byte[packageHeader.Length + packageFooter.Length + bytesRead];
            Buffer.BlockCopy(packageHeader, 0, multipartFormatedChunkData, 0, packageHeader.Length);
            Buffer.BlockCopy(buffer, 0, multipartFormatedChunkData, packageHeader.Length, bytesRead);
            Buffer.BlockCopy(packageFooter, 0, multipartFormatedChunkData, packageHeader.Length + bytesRead, packageFooter.Length);
            #endregion

            using (WebClient requestClient = dracoonClient.RequestBuilder.ProvideChunkUploadWebClient(bytesRead, uploadedByteCount, formDataBoundary, optionalFileSize == -1 ? "*" : optionalFileSize.ToString())) {
                long currentUploadedByteCount = uploadedByteCount;
                long previousBytesSentValue = 0;
                requestClient.UploadProgressChanged += (o, progressEvent) => {
                    previousBytesSentValue = progressEvent.BytesSent;
                    if (progressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL) {
                        NotifyProgress(actionId, currentUploadedByteCount + previousBytesSentValue, optionalFileSize);
                        lastNotifiedProgressValue = currentUploadedByteCount + previousBytesSentValue;
                        progressReportTimer.Restart();
                    }
                };
                byte[] chunkUploadResultBytes = dracoonClient.RequestExecutor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, multipartFormatedChunkData, runningThread);
                ApiUploadChunkResult chunkUploadResult = JsonConvert.DeserializeObject<ApiUploadChunkResult>(ApiConfig.encoding.GetString(chunkUploadResultBytes));
                uploadedByteCount += previousBytesSentValue - packageFooter.LongLength - packageHeader.LongLength;
                return chunkUploadResult;
            }
        }

        #region Callback helper functions

        protected void NotifyStarted(string actionId) {
            callbacks.ForEach(currentCallback => currentCallback.OnStarted(actionId));
        }

        protected void NotifyProgress(string actionId, long bytesDone, long bytesTotal) {
            callbacks.ForEach(currentCallback => currentCallback.OnRunning(actionId, bytesDone, bytesTotal));
        }

        protected void NotifyFinished(string actionId, Node result) {
            callbacks.ForEach(currentCallback => currentCallback.OnFinished(actionId, result));
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
