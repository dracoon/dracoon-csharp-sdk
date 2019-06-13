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

        protected List<IFileUploadCallback> callbacks = new List<IFileUploadCallback>();
        protected DracoonClient dracoonClient;
        protected string actionId;
        protected Thread runningThread = null;
        protected Stream inputStream;
        protected Stopwatch progressReportTimer;
        protected bool isInterrupted = false;
        protected FileUploadRequest fileUploadRequest;
        protected long optionalFileSize;
        protected bool preferS3;
        protected long lastNotifiedProgressValue = 0;

        public FileUpload(DracoonClient client, string actionId, FileUploadRequest request, Stream input, long fileSize, bool preferS3) {
            dracoonClient = client;
            this.actionId = actionId;
            this.preferS3 = preferS3;
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
            bool useS3 = false;
            try {
                useS3 = CheckUseS3();
                apiFileUploadRequest.UseS3 = useS3;
            } catch (DracoonApiException apiException) {
                dracoonClient.Log.Warn(LOGTAG, "S3 direct upload is not possible.", apiException);
            }

            RestRequest uploadTokenRequest = dracoonClient.RequestBuilder.PostCreateFileUpload(apiFileUploadRequest);
            ApiUploadToken token = dracoonClient.RequestExecutor.DoSyncApiCall<ApiUploadToken>(uploadTokenRequest, RequestType.PostUploadToken);
            Node publicResultNode = null;
            if (useS3) {
                Dictionary<int, string> s3Parts = UploadS3(token.UploadId);
                ApiCompleteFileUpload apiCompleteFileUpload = FileMapper.ToApiCompleteFileUpload(fileUploadRequest);
                apiCompleteFileUpload.PartNumber = s3Parts.Keys.ToList();
                apiCompleteFileUpload.PartEtags = s3Parts.Values.ToList();
                RestRequest completeFileUploadRequest = dracoonClient.RequestBuilder.PutCompleteS3FileUpload(token.UploadId, apiCompleteFileUpload);
                dynamic res = dracoonClient.RequestExecutor.DoSyncApiCall<dynamic>(completeFileUploadRequest, RequestType.PutCompleteS3Upload);
            } else {
                Upload(new Uri(token.UploadUrl));
                RestRequest completeFileUploadRequest = dracoonClient.RequestBuilder.PutCompleteFileUpload(new Uri(token.UploadUrl).PathAndQuery, FileMapper.ToApiCompleteFileUpload(fileUploadRequest));
                ApiNode resultNode = dracoonClient.RequestExecutor.DoSyncApiCall<ApiNode>(completeFileUploadRequest, RequestType.PutCompleteUpload);
                publicResultNode = NodeMapper.FromApiNode(resultNode);
            }
            NotifyFinished(actionId, publicResultNode);
            return publicResultNode;
        }

        private void Upload(Uri uploadUrl) {
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

        #region S3-Support

        protected bool CheckUseS3() {
            dracoonClient.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiS3UploadPossible);
            RestRequest generalSettingsRequest = dracoonClient.RequestBuilder.GetGeneralSettings();
            ApiGeneralSettings apiGeneralSettings = dracoonClient.RequestExecutor.DoSyncApiCall<ApiGeneralSettings>(generalSettingsRequest, RequestType.GetGeneralSettings);
            if (apiGeneralSettings.UseS3Storage) {
                if (preferS3) {
                    return true;
                } else {
                    RestRequest infrastructureSettingsRequest = dracoonClient.RequestBuilder.GetInfrastructureSettings();
                    ApiInfrastructureSettings apiInfrastructureSettings = dracoonClient.RequestExecutor.DoSyncApiCall<ApiInfrastructureSettings>(infrastructureSettingsRequest, RequestType.GetInfrastructureSettings);
                    if (apiInfrastructureSettings.S3EnforceDirectUpload) {
                        return true;
                    }
                }
            }
            return false;
        }

        private Dictionary<int, string> UploadS3(string uploadId) {
            dracoonClient.Log.Debug(LOGTAG, "Uploading file [" + fileUploadRequest.Name + "] via s3 direct upload.");
            try {
                progressReportTimer = Stopwatch.StartNew();
                int chunkSize = dracoonClient.HttpConfig.ChunkSize;
                int s3UrlBatchSize = 5;
                if (chunkSize < S3_MINIMUM_CHUNKSIZE) {
                    dracoonClient.Log.Debug(LOGTAG, "FYI: The defined chunk size [" + dracoonClient.HttpConfig.ChunkSize +
                        "] is lower than the minimum chunk size of s3 direct upload [" + S3_MINIMUM_CHUNKSIZE +
                        "]. Therefore the minimum s3 direct upload chunk size will be used.");
                    chunkSize = S3_MINIMUM_CHUNKSIZE;
                }
                if (optionalFileSize > 0) {
                    if (optionalFileSize > chunkSize) {
                        s3UrlBatchSize = (int)Math.Ceiling((double)optionalFileSize / chunkSize);
                        if (s3UrlBatchSize > S3_URL_BATCH) {
                            s3UrlBatchSize = S3_URL_BATCH;
                        }
                    }
                }
                // TODO if filesize is known, optimize chunk size

                // <int = part number, string = ETag> 
                Dictionary<int, string> s3Parts = new Dictionary<int, string>();
                Queue<Uri> s3Urls = new Queue<Uri>();

                long uploadedByteCount = 0;
                byte[] buffer = new byte[chunkSize];
                int bytesRead = 0;
                while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0) {
                    if (s3Urls.Count == 0) {
                        s3Urls = RequestS3Urls(uploadId, s3Parts.Count + 1, s3UrlBatchSize, chunkSize);
                    }
                    string partETag = ProcessS3Chunk(s3Urls.Dequeue(), buffer, ref uploadedByteCount, bytesRead);
                    s3Parts.Add(s3Parts.Count + 1, partETag);
                }
                if (lastNotifiedProgressValue != uploadedByteCount) { // Notify 100 percent progress
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

        private string ProcessS3Chunk(Uri uploadUrl, byte[] buffer, ref long uploadedByteCount, int bytesRead) {
            using (WebClient requestClient = dracoonClient.RequestBuilder.ProvideS3ChunkUploadWebClient(bytesRead)) {
                long previousBytesSentValue = uploadedByteCount;
                long currentUploadedByteCount = 0;
                requestClient.UploadProgressChanged += (o, progressEvent) => {
                    currentUploadedByteCount += progressEvent.BytesSent;
                    if (progressReportTimer.ElapsedMilliseconds > PROGRESS_UPDATE_INTERVAL && progressEvent.BytesSent > 0) {
                        lastNotifiedProgressValue = currentUploadedByteCount + previousBytesSentValue;
                        NotifyProgress(actionId, lastNotifiedProgressValue, optionalFileSize);
                        progressReportTimer.Restart();
                    }
                };
                byte[] result = dracoonClient.RequestExecutor.ExecuteWebClientChunkUpload(requestClient, uploadUrl, buffer, RequestType.PostUploadS3Chunk, runningThread);
            }
            return "";
        }

        private Queue<Uri> RequestS3Urls(string uploadId, int firstPartNumber, int count, long chunkSize) {
            ApiGetS3Urls getS3UrlParams = new ApiGetS3Urls() {
                Size = chunkSize,
                FirstPartNumber = firstPartNumber,
                LastPartNumber = firstPartNumber + count
            };
            RestRequest s3UrlRequest = dracoonClient.RequestBuilder.PostGetS3Urls(uploadId, getS3UrlParams);
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