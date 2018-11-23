using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonRequestExecuter {

        internal enum RequestType {
            GetServerVersion, GetServerTime,
            SetUserKeyPair, GetCustomerAccount, GetUserAccount, GetUserKeyPair, DeleteUserKeyPair,
            GetNodes, GetNode, PostRoom, PostFolder, PutFolder, PutRoom, PutEnableRoomEncryption, PutFile, DeleteNodes,
            PostDownloadToken, GetFileKey, PostUploadToken, PutCompleteUpload, PostUploadChunk,
            GetDownloadChunk, PostCopyNodes, PostMoveNodes, GetSearchNodes, GetMissingFileKeys, PostMissingFileKeys,
            PostCreateDownloadShare, DeleteDownloadShare, GetDownloadShares, PostCreateUploadShare, DeleteUploadShare,
            GetUploadShares, PostFavorite, DeleteFavorite, GetAuthenticatedPing, PostOAuthToken, PostOAuthRefresh, GetGeneralSettings, GetInfrastructureSettings, GetDefaultsSettings,
            GetRecycleBin, DeleteRecycleBin, GetPreviousVersions, GetPreviousVersion, PostRestoreNodeVersion, DeletePreviousVersions
        }

        private static readonly string LOGTAG = typeof(DracoonRequestExecuter).Name;
        private DracoonClient dracoonClient;
        private bool isServerVersionCompatible = false;
        private string[] remoteRestApiVersion;

        internal DracoonRequestExecuter(DracoonClient client) {
            dracoonClient = client;
        }


        public void CheckApiServerVersion(string minVersionForCheck = ApiConfig.MinimumApiVersion) {
            if (isServerVersionCompatible && minVersionForCheck == ApiConfig.MinimumApiVersion) {
                return;
            }
            if (remoteRestApiVersion == null) {
                ApiServerVersion serverVersion = DoSyncApiCall<ApiServerVersion>(dracoonClient.RequestBuilder.GetServerVersion(), RequestType.GetServerVersion);
                remoteRestApiVersion = Regex.Split(serverVersion.RestApiVersion, "\\.");
            }
            string[] minVersion = Regex.Split(minVersionForCheck, "\\.");
            for (int iterate = 0; iterate < 3; iterate++) {
                int remoteVersionPart = int.Parse(remoteRestApiVersion[iterate]);
                int minVersionPart = int.Parse(minVersion[iterate]);
                if (remoteVersionPart > minVersionPart) {
                    break;
                } else if (remoteVersionPart < minVersionPart) {
                    throw new DracoonApiException(DracoonApiCode.API_VERSION_NOT_SUPPORTED);
                }
            }
            if (minVersionForCheck == ApiConfig.MinimumApiVersion) {
                isServerVersionCompatible = true;
            }
        }

        public T DoSyncApiCall<T>(RestRequest request, RequestType requestType, int authTry = 0) where T : class, new() {
            RestClient client = new RestClient(dracoonClient.ServerUri) {
                UserAgent = dracoonClient.HttpConfig.UserAgent,
            };
            if (dracoonClient.HttpConfig.WebProxy != null) {
                client.Proxy = dracoonClient.HttpConfig.WebProxy;
            }
            IRestResponse response = client.Execute(request);
            if (response.ErrorException != null && response.ErrorException is WebException we) { // It's an HTTP exception
                dracoonClient.ApiErrorParser.ParseError(we, requestType);
            }
            if (!response.IsSuccessful) { // It's an API exception
                try {
                    dracoonClient.ApiErrorParser.ParseError(response, requestType);
                } catch (DracoonApiException apiError) {
                    if (apiError.ErrorCode == DracoonApiCode.AUTH_UNAUTHORIZED && authTry < 3) {
                        dracoonClient.Log.Debug(LOGTAG, "Retry the refresh of the access token in " + authTry * 1000 + " millis again.");
                        Thread.Sleep(1000 * authTry);
                        dracoonClient.OAuthClient.RefreshAccessToken();
                        foreach (Parameter cur in request.Parameters) {
                            if (cur.Name == ApiConfig.AuthorizationHeader) {
                                cur.Value = dracoonClient.OAuthClient.BuildAuthString();
                            }
                        }
                        return DoSyncApiCall<T>(request, requestType, authTry + 1);
                    } else {
                        throw apiError;
                    }
                }
            }
            if (typeof(T) == typeof(VoidResponse)) {
                return new VoidResponse() as T;
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        public byte[] ExecuteWebClientChunkDownload(WebClient requestClient, Uri target, Thread asyncThread = null, int sendTry = 0) {
            byte[] response = null;
            try {
                Task<byte[]> responseTask = requestClient.DownloadDataTaskAsync(target);
                response = responseTask.Result;
            } catch (AggregateException ae) {
                if (ae.InnerException is WebException we) {
                    if (we.Status == WebExceptionStatus.SecureChannelFailure) {
                        string message = "Server SSL handshake failed!";
                        dracoonClient.Log.Error(LOGTAG, message, we);
                        throw new DracoonNetInsecureException(message, we);
                    } else if (we.Status == WebExceptionStatus.RequestCanceled) {
                        throw new ThreadInterruptedException();
                    } else if (we.Status == WebExceptionStatus.ProtocolError) {
                        dracoonClient.ApiErrorParser.ParseError(we, RequestType.GetDownloadChunk);
                    } else {
                        string message = "Server communication failed!";
                        dracoonClient.Log.Debug(LOGTAG, message);
                        if (dracoonClient.HttpConfig.RetryEnabled && sendTry < 3) {
                            dracoonClient.Log.Debug(LOGTAG, "Retry the request in " + sendTry * 1000 + " millis again.");
                            Thread.Sleep(1000 * sendTry);
                            ExecuteWebClientChunkDownload(requestClient, target, asyncThread, sendTry + 1);
                        } else {
                            if (asyncThread != null && asyncThread.ThreadState == ThreadState.Aborted) {
                                throw new ThreadInterruptedException();
                            } else {
                                if (we.Status == WebExceptionStatus.RequestCanceled) {
                                    throw new ThreadInterruptedException();
                                }
                                dracoonClient.ApiErrorParser.ParseError(we, RequestType.GetDownloadChunk);
                            }
                        }
                    }
                }
            }
            return response;
        }

        public byte[] ExecuteWebClientChunkUpload(WebClient requestClient, Uri target, byte[] multipartFormatedChunk, Thread asyncThread = null, int sendTry = 0) {
            byte[] response = null;
            try {
                Task<byte[]> responseTask = requestClient.UploadDataTaskAsync(target, "POST", multipartFormatedChunk);
                response = responseTask.Result;
            } catch (AggregateException ae) {
                if (ae.InnerException is WebException we) {
                    if (we.Status == WebExceptionStatus.SecureChannelFailure) {
                        string message = "Server SSL handshake failed!";
                        dracoonClient.Log.Error(LOGTAG, message, we);
                        throw new DracoonNetInsecureException(message, we);
                    } else if (we.Status == WebExceptionStatus.RequestCanceled) {
                        throw new ThreadInterruptedException();
                    } else if (we.Status == WebExceptionStatus.ProtocolError) {
                        dracoonClient.ApiErrorParser.ParseError(we, RequestType.PostUploadChunk);
                    } else {
                        string message = "Server communication failed!";
                        dracoonClient.Log.Debug(LOGTAG, message);
                        if (dracoonClient.HttpConfig.RetryEnabled && sendTry < 3) {
                            dracoonClient.Log.Debug(LOGTAG, "Retry the request in " + sendTry * 1000 + " millis again.");
                            Thread.Sleep(1000 * sendTry);
                            ExecuteWebClientChunkUpload(requestClient, target, multipartFormatedChunk, asyncThread, sendTry + 1);
                        } else {
                            if (asyncThread != null && asyncThread.ThreadState == ThreadState.Aborted) {
                                throw new ThreadInterruptedException();
                            } else {
                                if (we.Status == WebExceptionStatus.RequestCanceled) {
                                    throw new ThreadInterruptedException();
                                }
                                dracoonClient.ApiErrorParser.ParseError(we, RequestType.PostUploadChunk);
                            }
                        }
                    }
                }
            }
            return response;
        }

    }
}
