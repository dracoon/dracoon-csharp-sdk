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
        private readonly object LockObject = new object();
        private readonly int MAX_S3_POLLING_INTERVAL = 4000; // 4 sec

        protected string LogTag = nameof(FileUpload);

        protected readonly long PROGRESS_UPDATE_INTERVAL = 500;
        protected readonly int S3_MINIMUM_CHUNKSIZE = 5 * 1024 * 1024; // 5 MB minimum chunksize for s3 upload
        protected readonly int S3_URL_BATCH = 5;

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
        protected ApiUploadToken UploadToken;

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
                RunSync();
            }

            RunningThread = new Thread(Child);
            RunningThread.Start();
        }

        public Node RunSync() {
            try {
                return StartUpload();
            } catch (DracoonException de) {
                NotifyFailed(ActionId, de);
                throw;
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

            try {
                apiFileUploadRequest.UseS3 = CheckUseS3();
            } catch (DracoonApiException apiException) {
                DracoonClient.Log.Warn(LogTag, "S3 direct upload is not possible.", apiException);
            }

            IRestRequest uploadTokenRequest = Client.Builder.PostCreateFileUpload(apiFileUploadRequest);
            UploadToken = Client.Executor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);
            Node publicResultNode;
            if (apiFileUploadRequest.UseS3.HasValue && apiFileUploadRequest.UseS3.Value) {
                List<ApiS3FileUploadPart> s3Parts = UploadS3();
                ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(FileUploadRequest);
                apiCompleteFileUpload.Parts = s3Parts;
                apiCompleteFileUpload.IsPrioritisedVirusScan = FileUploadRequest.IsPrioritisedVirusScan;
                IRestRequest completeFileUploadRequest = Client.Builder.PutCompleteS3FileUpload(UploadToken.UploadId, apiCompleteFileUpload);
                Client.Executor.DoSyncApiCall<VoidResponse>(completeFileUploadRequest, RequestType.PutCompleteS3Upload);
                publicResultNode = NodeMapper.FromApiNode(S3Finished());
            } else {
                Upload();
                IRestRequest completeFileUploadRequest = Client.Builder.PutCompleteFileUpload(new Uri(UploadToken.UploadUrl).PathAndQuery,
                    FileMapper.ToApiCompleteFileUpload(FileUploadRequest));
                ApiNode resultNode = Client.Executor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
                publicResultNode = NodeMapper.FromApiNode(resultNode);
            }

            NotifyFinished(ActionId, publicResultNode);
            return publicResultNode;
        }

        #region Normal upload

        private void Upload() {
            DracoonClient.Log.Debug(LogTag, "Uploading file [" + FileUploadRequest.Name + "] in proxied way.");
            try {
                long uploadedByteCount = 0;
                byte[] buffer = new byte[DracoonClient.HttpConfig.ChunkSize];
                int bytesRead = 0;
                while ((bytesRead = InputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    ProcessChunk(new Uri(UploadToken.UploadUrl), buffer, uploadedByteCount, bytesRead);
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
                DracoonClient.Log.Debug(LogTag, message);
                throw new DracoonFileIOException(message, ioe);
            } finally {
                ProgressReportTimer?.Stop();
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
                requestClient.UploadProgressChanged += (sender, e) => {
                    lock (LockObject) {
                        long increaseWithoutHeader = e.BytesSent - headerLength;
                        if (ProgressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL && increaseWithoutHeader > 0) {
                            LastNotifiedProgressValue = currentUploadedByteCount + increaseWithoutHeader;
                            NotifyProgress(ActionId, LastNotifiedProgressValue, OptionalFileSize);
                            ProgressReportTimer.Restart();
                        }
                    }
                };
                ProgressReportTimer = Stopwatch.StartNew();
                byte[] chunkUploadResultBytes = Client.Executor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, multipartFormatedChunkData,
                    RequestType.PostUploadChunk, RunningThread);
                ApiUploadChunkResult chunkUploadResult =
                    JsonConvert.DeserializeObject<ApiUploadChunkResult>(ApiConfig.ENCODING.GetString(chunkUploadResultBytes));
                return chunkUploadResult;
            }
        }

        #endregion

        #region S3 upload

        protected bool CheckUseS3() {
            Client.Executor.CheckApiServerVersion();
            IRestRequest generalSettingsRequest = Client.Builder.GetGeneralSettings();
            ApiGeneralSettings apiGeneralSettings =
                Client.Executor.DoSyncApiCall<ApiGeneralSettings>(generalSettingsRequest, RequestType.GetGeneralSettings);
            return apiGeneralSettings.UseS3Storage;
        }

        protected ApiNode S3Finished() {
            int currentInterval = 500;
            Stopwatch s3Polling = Stopwatch.StartNew();
            while (true) {
                if (s3Polling.ElapsedMilliseconds >= currentInterval) {
                    IRestRequest request = Client.Builder.GetS3Status(UploadToken.UploadId);
                    ApiS3Status status = Client.Executor.DoSyncApiCall<ApiS3Status>(request, RequestType.GetS3Status);
                    switch (status.Status) {
                        case "done":
                            s3Polling.Stop();
                            return status.Node;
                        case "transfer":
                            break;
                        case "finishing":
                            break;
                        case "error":
                            s3Polling.Stop();
                            DracoonErrorParser.ParseError(status.ErrorInfo, RequestType.GetS3Status);
                            throw new DracoonApiException(DracoonApiCode.SERVER_S3_UPLOAD_COMPLETION_FAILED);
                        default:
                            s3Polling.Stop();
                            throw new DracoonApiException(DracoonApiCode.SERVER_S3_UPLOAD_COMPLETION_FAILED);
                    }

                    if (currentInterval < MAX_S3_POLLING_INTERVAL) {
                        currentInterval *= 2;
                    }

                    s3Polling.Restart();
                }
            }
        }

        protected int DefineS3BatchSize(int chunkSize) {
            if (OptionalFileSize <= 0) {
                return S3_URL_BATCH;
            }

            double divided = (double)OptionalFileSize / chunkSize;
            double floored = Math.Floor(divided);
            int fileDependentBatchSize = (int)floored;

            return fileDependentBatchSize < S3_URL_BATCH ? fileDependentBatchSize : S3_URL_BATCH;
        }

        protected int DefineS3ChunkSize() {
            if (DracoonClient.HttpConfig.ChunkSize < S3_MINIMUM_CHUNKSIZE) {
                DracoonClient.Log.Debug(LogTag,
                    "FYI: The defined chunk size [" + DracoonClient.HttpConfig.ChunkSize +
                    "] is lower than the minimum chunk size of s3 direct upload [" + S3_MINIMUM_CHUNKSIZE +
                    "]. Therefore the minimum s3 direct upload chunk size will be used.");
                return S3_MINIMUM_CHUNKSIZE;
            }

            return DracoonClient.HttpConfig.ChunkSize;
        }

        private List<ApiS3FileUploadPart> UploadS3() {
            DracoonClient.Log.Debug(LogTag, "Uploading file [" + FileUploadRequest.Name + "] via s3 direct upload.");
            try {
                int chunkSize = DefineS3ChunkSize();
                int s3UrlBatchSize = DefineS3BatchSize(chunkSize);

                List<ApiS3FileUploadPart> S3Parts = new List<ApiS3FileUploadPart>();
                Queue<Uri> S3Urls = new Queue<Uri>();

                long uploadedByteCount = 0;
                byte[] buffer = new byte[chunkSize];
                int bytesRead;
                while ((bytesRead = InputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    if (bytesRead < chunkSize) {
                        S3Urls = RequestS3Urls(S3Parts.Count + 1, 1, bytesRead);
                    } else if (S3Urls.Count == 0) {
                        S3Urls = RequestS3Urls(S3Parts.Count + 1, s3UrlBatchSize, chunkSize);
                    }

                    byte[] chunk = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, chunk, 0, bytesRead);
                    string partETag = UploadS3ChunkWebClient(S3Urls.Dequeue(), chunk, uploadedByteCount);
                    S3Parts.Add(new ApiS3FileUploadPart {
                        PartEtag = partETag,
                        PartNumber = S3Parts.Count + 1
                    });
                    uploadedByteCount += chunk.Length;
                }

                if(S3Parts.Count == 0) { // if it was an empty file we have to put an empty part to s3 so that we put the empty file info to our api
                    DracoonClient.Log.Debug(LogTag, "The file [" + FileUploadRequest.Name + "] was an empty file. Therefore an empty part is uploaded to s3 now.");
                    S3Urls = RequestS3Urls(S3Parts.Count + 1, 1, 0);
                    string partETag = UploadS3ChunkWebClient(S3Urls.Dequeue(), new byte[0], 0);
                    S3Parts.Add(new ApiS3FileUploadPart {
                        PartEtag = partETag,
                        PartNumber = S3Parts.Count + 1
                    });
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

        protected string UploadS3ChunkWebClient(Uri uploadUrl, byte[] chunk, long uploadedByteCount) {
            using (WebClient requestClient = Client.Builder.ProvideS3ChunkUploadWebClient()) {
                requestClient.UploadProgressChanged += (sender, e) => {
                    lock (LockObject) {
                        if (ProgressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL) {
                            LastNotifiedProgressValue = e.BytesSent + uploadedByteCount;
                            NotifyProgress(ActionId, LastNotifiedProgressValue, OptionalFileSize);
                            ProgressReportTimer.Restart();
                        }
                    }
                };
                ProgressReportTimer = Stopwatch.StartNew();
                byte[] result =
                    Client.Executor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, chunk, RequestType.PutUploadS3Chunk, RunningThread);
                return ApiConfig.ENCODING.GetString(result);
            }
        }

        protected Queue<Uri> RequestS3Urls(int firstPartNumber, int count, long chunkSize) {
            ApiGetS3Urls getS3UrlParams = new ApiGetS3Urls() {
                Size = chunkSize,
                FirstPartNumber = firstPartNumber,
                LastPartNumber = firstPartNumber + count - 1
            };
            IRestRequest s3UrlRequest = Client.Builder.PostGetS3Urls(UploadToken.UploadId, getS3UrlParams);
            List<ApiS3Url> s3UrlsResult = Client.Executor.DoSyncApiCall<ApiS3Urls>(s3UrlRequest, RequestType.PostGetS3Urls).Urls;

            Queue<Uri> newS3UrlQueue = new Queue<Uri>(s3UrlsResult.Count);
            foreach (ApiS3Url currentS3Url in s3UrlsResult) {
                newS3UrlQueue.Enqueue(new Uri(currentS3Url.Url));
            }

            return newS3UrlQueue;
        }

        #endregion

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