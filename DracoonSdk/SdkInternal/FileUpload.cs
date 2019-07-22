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
using System.Linq;
using System.Net;
using System.Threading;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class FileUpload {
        protected string Logtag = nameof(FileUpload);

        protected readonly long PROGRESS_UPDATE_INTERVAL = 500;
        protected readonly int S3_MINIMUM_CHUNKSIZE = 5 * 1024 * 1024; // 5 MB minimum chunksize for s3 upload
        protected readonly int S3_URL_BATCH = 5;
        private readonly int MAX_S3_POLLING_INTERVAL = 4000; // 4 sec

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
        protected ApiUploadToken uploadToken;

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

            try {
                apiFileUploadRequest.UseS3 = CheckUseS3();
            } catch (DracoonApiException apiException) {
                dracoonClient.Log.Warn(LOGTAG, "S3 direct upload is not possible.", apiException);
            }

            RestRequest uploadTokenRequest = dracoonClient.RequestBuilder.PostCreateFileUpload(apiFileUploadRequest);
            uploadToken = dracoonClient.RequestExecutor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);
            Node publicResultNode = null;
            if (apiFileUploadRequest.UseS3.HasValue && apiFileUploadRequest.UseS3.Value) {
                List<ApiS3FileUploadPart> s3Parts = UploadS3();
                ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(fileUploadRequest);
                apiCompleteFileUpload.Parts = s3Parts;
                RestRequest completeFileUploadRequest =
                    dracoonClient.RequestBuilder.PutCompleteS3FileUpload(uploadToken.UploadId, apiCompleteFileUpload);
                dracoonClient.RequestExecutor.DoSyncApiCall<dynamic>(completeFileUploadRequest, RequestType.PutCompleteS3Upload);
                publicResultNode = NodeMapper.FromApiNode(S3Finished());
            } else {
                Upload();
                RestRequest completeFileUploadRequest =
                    dracoonClient.RequestBuilder.PutCompleteFileUpload(new Uri(uploadToken.UploadUrl).PathAndQuery,
                        FileMapper.ToApiCompleteFileUpload(fileUploadRequest));
                ApiNode resultNode = dracoonClient.RequestExecutor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
                publicResultNode = NodeMapper.FromApiNode(resultNode);
            }

            NotifyFinished(actionId, publicResultNode);
            return publicResultNode;
        }

        #region Normal upload

        private void Upload() {
            dracoonClient.Log.Debug(LOGTAG, "Uploading file [" + fileUploadRequest.Name + "] in proxied way.");
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

        #endregion

        #region S3 upload

        protected bool CheckUseS3() {
            dracoonClient.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiS3UploadPossible);
            RestRequest generalSettingsRequest = dracoonClient.RequestBuilder.GetGeneralSettings();
            ApiGeneralSettings apiGeneralSettings =
                dracoonClient.RequestExecutor.DoSyncApiCall<ApiGeneralSettings>(generalSettingsRequest, RequestType.GetGeneralSettings);
            if (apiGeneralSettings.UseS3Storage) {
                return true;
            }

            return false;
        }

        protected ApiNode S3Finished() {
            int currentInterval = 500;
            Stopwatch s3Polling = Stopwatch.StartNew();
            while (true) {
                if (s3Polling.ElapsedMilliseconds >= currentInterval) {
                    RestRequest request = dracoonClient.RequestBuilder.GetS3Status(uploadToken.UploadId);
                    ApiS3Status status = dracoonClient.RequestExecutor.DoSyncApiCall<ApiS3Status>(request, RequestType.GetS3Status);
                    if (status.Status == "done") {
                        s3Polling.Stop();
                        return status.Node;
                    }

                    if (status.Status == "error") {
                        s3Polling.Stop();
                        throw new DracoonApiException();
                    }

                    if (currentInterval < MAX_S3_POLLING_INTERVAL) {
                        currentInterval *= 2;
                    }

                    s3Polling.Restart();
                }
            }
        }

        protected int DefineS3BatchSize(int chunkSize) {
            int fileDependentBatchSize = (int) Math.Ceiling((double) optionalFileSize / chunkSize);
            if (fileDependentBatchSize == 0) {
                return 1;
            }

            if (fileDependentBatchSize <= S3_URL_BATCH) {
                return fileDependentBatchSize;
            }

            return S3_URL_BATCH;
        }

        protected int DefineS3ChunkSize() {
            if (dracoonClient.HttpConfig.ChunkSize < S3_MINIMUM_CHUNKSIZE) {
                dracoonClient.Log.Debug(LOGTAG,
                    "FYI: The defined chunk size [" + dracoonClient.HttpConfig.ChunkSize +
                    "] is lower than the minimum chunk size of s3 direct upload [" + S3_MINIMUM_CHUNKSIZE +
                    "]. Therefore the minimum s3 direct upload chunk size will be used.");
                return S3_MINIMUM_CHUNKSIZE;
            }

            return dracoonClient.HttpConfig.ChunkSize;
        }

        private List<ApiS3FileUploadPart> UploadS3() {
            dracoonClient.Log.Debug(LOGTAG, "Uploading file [" + fileUploadRequest.Name + "] via s3 direct upload.");
            try {
                progressReportTimer = Stopwatch.StartNew();
                int chunkSize = DefineS3ChunkSize();
                int s3UrlBatchSize = DefineS3BatchSize(chunkSize);

                List<ApiS3FileUploadPart> s3Parts = new List<ApiS3FileUploadPart>();
                Queue<Uri> s3Urls = new Queue<Uri>();

                long uploadedByteCount = 0;
                byte[] buffer = new byte[chunkSize];
                int bytesRead = 0;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    if (bytesRead < chunkSize) {
                        s3Urls = RequestS3Urls(s3Parts.Count + 1, 1, bytesRead);
                    } else if (s3Urls.Count == 0) {
                        s3Urls = RequestS3Urls(s3Parts.Count + 1, s3UrlBatchSize, chunkSize);
                    }

                    byte[] chunk = new byte[bytesRead];
                    Buffer.BlockCopy(buffer, 0, chunk, 0, bytesRead);
                    string partETag = UploadS3ChunkWebClient(s3Urls.Dequeue(), chunk, ref uploadedByteCount);
                    s3Parts.Add(new ApiS3FileUploadPart() {
                        PartEtag = partETag,
                        PartNumber = s3Parts.Count + 1
                    });
                }

                if (lastNotifiedProgressValue != uploadedByteCount) {
                    // Notify 100 percent progress
                    NotifyProgress(actionId, uploadedByteCount, optionalFileSize);
                }

                return s3Parts;
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

        protected string UploadS3ChunkWebClient(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount) {
            using (WebClient requestClient = dracoonClient.RequestBuilder.ProvideS3ChunkUploadWebClient()) {
                long previousBytesSentValue = uploadedByteCount;
                long currentUploadedByteCount = 0;
                requestClient.UploadProgressChanged += (o, progressEvent) => {
                    currentUploadedByteCount += progressEvent.BytesSent - currentUploadedByteCount;
                    if (progressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL && progressEvent.BytesSent > 0) {
                        lastNotifiedProgressValue = currentUploadedByteCount + previousBytesSentValue;
                        NotifyProgress(actionId, lastNotifiedProgressValue, optionalFileSize);
                        progressReportTimer.Restart();
                    }
                };

                byte[] result =
                    dracoonClient.RequestExecutor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, buffer, RequestType.PutUploadS3Chunk,
                        runningThread);
                uploadedByteCount += currentUploadedByteCount;
                return ApiConfig.encoding.GetString(result);
            }
        }

        protected Queue<Uri> RequestS3Urls(int firstPartNumber, int count, long chunkSize) {
            ApiGetS3Urls getS3UrlParams = new ApiGetS3Urls() {
                Size = chunkSize,
                FirstPartNumber = firstPartNumber,
                LastPartNumber = firstPartNumber + count - 1
            };
            RestRequest s3UrlRequest = dracoonClient.RequestBuilder.PostGetS3Urls(uploadToken.UploadId, getS3UrlParams);
            List<ApiS3Url> s3UrlsResult = dracoonClient.RequestExecutor.DoSyncApiCall<ApiS3Urls>(s3UrlRequest, RequestType.PostGetS3Urls).Urls;

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