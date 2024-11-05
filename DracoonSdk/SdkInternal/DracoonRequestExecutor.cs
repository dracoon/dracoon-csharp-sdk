﻿using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.OAuth;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonRequestExecutor : IRequestExecutor {
        internal enum RequestType {
            GetServerVersion, GetServerTime, SetUserKeyPair, GetCustomerAccount, GetUserAccount,
            GetUserKeyPair, GetUserKeyPairs, DeleteUserKeyPair, GetUserAvatar, DeleteUserAvatar, PostUserAvatar,
            GetUserProfileAttributes, PutUserProfileAttributes, DeleteUserProfileAttributes,
            GetResourcesAvatar, GetNodes, GetNode, PostRoom, PostFolder,
            PutFolder, PutRoom, PutEnableRoomEncryption, PutFile, DeleteNodes,
            PostDownloadToken, GetFileKey, PostUploadToken, PutCompleteUpload, PutCompleteS3Upload,
            PostUploadChunk, PutUploadS3Chunk, GetDownloadChunk, PostCopyNodes, PostMoveNodes,
            GetSearchNodes, GetMissingFileKeys, PostMissingFileKeys, PostCreateDownloadShare, DeleteDownloadShare,
            PostMailDownloadShare, PostMailUploadShare,
            GetDownloadShares, PostCreateUploadShare, DeleteUploadShare, GetUploadShares, PostFavorite,
            DeleteFavorite, GetAuthenticatedPing, PostOAuthToken, PostOAuthRefresh, GetGeneralSettings,
            GetInfrastructureSettings, GetDefaultsSettings, GetRecycleBin, DeleteRecycleBin, GetPreviousVersions,
            GetPreviousVersion, PostRestoreNodeVersion, DeletePreviousVersions, PostGetS3Urls, GetS3Status, GetPasswordPolicies,
            GetAlgorithms, GetClassificationPolicies, GenerateVirusProtectionInfo, DeleteMaliciousFile, GetDownloadShareSubscriptions,
            GetUploadShareSubscriptions, PostUploadShareSubscription, PostDownloadShareSubscription, DeleteDownloadShareSubscription,
            DeleteUploadShareSubscription
        }

        private const string Logtag = nameof(DracoonRequestExecutor);
        private readonly IOAuth _auth;
        private readonly IInternalDracoonClient _client;
        private bool _isServerVersionCompatible;
        private string[] _apiVersion;

        internal DracoonRequestExecutor(IOAuth auth, IInternalDracoonClient client) {
            _auth = auth;
            _client = client;
        }

        private static bool RequestIsOAuthRequest(RequestType rType) {
            return rType == RequestType.PostOAuthToken || rType == RequestType.PostOAuthRefresh;
        }


        void IRequestExecutor.CheckApiServerVersion(string minVersionForCheck) {
            if (_isServerVersionCompatible && minVersionForCheck == ApiConfig.MinimumApiVersion) {
                return;
            }

            if (_apiVersion == null) {
                ApiServerVersion serverVersion =
                    ((IRequestExecutor)this).DoSyncApiCall<ApiServerVersion>(_client.Builder.GetServerVersion(), RequestType.GetServerVersion);
                string version = serverVersion.RestApiVersion;
                if (version.Contains("-")) {
                    version = version.Remove(version.IndexOf("-"));
                }

                _apiVersion = Regex.Split(version, "\\.", RegexOptions.None, new TimeSpan(0, 5, 0));
            }

            string[] minVersion = Regex.Split(minVersionForCheck, "\\.", RegexOptions.None, new TimeSpan(0, 5, 0));
            for (int iterate = 0; iterate < 3; iterate++) {
                int remoteVersionPart = int.Parse(_apiVersion[iterate]);
                int minVersionPart = int.Parse(minVersion[iterate]);
                if (remoteVersionPart > minVersionPart) {
                    break;
                }

                if (remoteVersionPart < minVersionPart) {
                    if (minVersionForCheck == ApiConfig.MinimumApiVersion) {
                        throw new DracoonApiException(DracoonApiCode.API_VERSION_NOT_SUPPORTED);
                    }

                    throw new DracoonApiException(new DracoonApiCode(DracoonApiCode.API_VERSION_NOT_SUPPORTED.Code, "Server API versions < " + minVersionForCheck + " are not supported."));
                }
            }

            if (minVersionForCheck == ApiConfig.MinimumApiVersion) {
                _isServerVersionCompatible = true;
            }
        }

        T IRequestExecutor.DoSyncApiCall<T>(RestRequest request, RequestType requestType, int sendTry) {
            RestClientOptions clientOptions = new RestClientOptions(_client.ServerUri) {
                UserAgent = DracoonClient.HttpConfig.UserAgent,
                Timeout = TimeSpan.FromMilliseconds(DracoonClient.HttpConfig.Timeout)
            };

            if (DracoonClient.HttpConfig.WebProxy != null) {
                clientOptions.Proxy = DracoonClient.HttpConfig.WebProxy;
            }

            RestClient client = new RestClient(clientOptions);


            RestResponse response = client.Execute(request);
            try {
                if (response.ErrorException is WebException we) {
                    // It's an HTTP exception
                    DracoonErrorParser.ParseError(we, requestType);
                }

                if (!response.IsSuccessful) {
                    // It's an API exception
                    if (RequestIsOAuthRequest(requestType)) {
                        OAuthErrorParser.ParseError(response, requestType);
                    }

                    try {
                        DracoonErrorParser.ParseError(response, requestType);
                    } catch (DracoonApiException apiError) {
                        if (apiError.ErrorCode.Code == DracoonApiCode.AUTH_UNAUTHORIZED.Code && sendTry < 3) {
                            DracoonClient.Log.Debug(Logtag, "Retry the refresh of the access token in " + sendTry * 1000 + " millis again.");
                            Thread.Sleep(1000 * sendTry);
                            _auth.RefreshAccessToken();
                            // Update the auth header with new tokens if it was an auth required test
                            if (request.Parameters.TryFind(ApiConfig.AuthorizationHeader) != null) {
                                request.AddOrUpdateHeader(ApiConfig.AuthorizationHeader, _auth.BuildAuthString());
                            }

                            return ((IRequestExecutor)this).DoSyncApiCall<T>(request, requestType, sendTry + 1);
                        }

                        throw;
                    }
                }
            } catch (DracoonApiException dae) {
                if (sendTry < 3 && CheckTooManyRequestsResult(dae, response)) {
                    return ((IRequestExecutor)this).DoSyncApiCall<T>(request, requestType, sendTry + 1);
                }

                throw;
            }

            if (typeof(T) == typeof(VoidResponse)) {
                return new VoidResponse() as T;
            }

            return JsonConvert.DeserializeObject<T>(response.Content);
        }

        byte[] IRequestExecutor.ExecuteWebClientDownload(WebClient requestClient, Uri target, RequestType type, Thread asyncThread,
            int sendTry) {
            byte[] response = null;
            try {
                Task<byte[]> responseTask = requestClient.DownloadDataTaskAsync(target);
                response = responseTask.Result;
            } catch (AggregateException ae) {
                if (ae.InnerException is WebException we) {
                    if (we.Status == WebExceptionStatus.SecureChannelFailure) {
                        const string message = "Server SSL handshake failed!";
                        DracoonClient.Log.Error(Logtag, message, we);
                        throw new DracoonNetInsecureException(message, we);
                    }

                    if (we.Status == WebExceptionStatus.RequestCanceled) {
                        throw new ThreadInterruptedException();
                    }

                    try {
                        if (we.Status == WebExceptionStatus.ProtocolError) {
                            DracoonErrorParser.ParseError(we, type);
                        } else {
                            string message = "Server communication failed!";
                            DracoonClient.Log.Debug(Logtag, message);
                            if (DracoonClient.HttpConfig.RetryEnabled && sendTry < 3) {
                                DracoonClient.Log.Debug(Logtag, "Retry the request in " + sendTry * 1000 + " millis again.");
                                Thread.Sleep(1000 * sendTry);
                                return ((IRequestExecutor)this).ExecuteWebClientDownload(requestClient, target, type, asyncThread, sendTry + 1);
                            } else {
                                if (asyncThread != null && asyncThread.ThreadState == ThreadState.Aborted) {
                                    throw new ThreadInterruptedException();
                                }

                                DracoonErrorParser.ParseError(we, type);
                            }
                        }
                    } catch (DracoonApiException dae) {
                        if (sendTry < 3 && CheckTooManyRequestsResult(dae, we.Response)) {
                            return ((IRequestExecutor)this).ExecuteWebClientDownload(requestClient, target, type, asyncThread, sendTry + 1);
                        }

                        throw;
                    }

                }
            }

            return response;
        }

        public byte[] ExecuteWebClientChunkUpload(WebClient requestClient, Uri target, byte[] data, RequestType type, Thread asyncThread = null,
            int sendTry = 0) {
            byte[] response = null;
            try {
                string method = "POST";
                if (type == RequestType.PutUploadS3Chunk) {
                    method = "PUT";
                }

                Task<byte[]> responseTask = requestClient.UploadDataTaskAsync(target, method, data);
                if (type == RequestType.PutUploadS3Chunk) {
                    responseTask.Wait();
                    for (int i = 0; i < requestClient.ResponseHeaders.Count; i++) {
                        if (requestClient.ResponseHeaders.GetKey(i).ToLower().Equals("etag")) {
                            string eTag = requestClient.ResponseHeaders.Get(i);
                            eTag = eTag.Replace("\"", "").Replace("\\", "").Replace("/", "");
                            response = ApiConfig.ENCODING.GetBytes(eTag);
                        }
                    }
                } else {
                    response = responseTask.Result;
                }
            } catch (AggregateException ae) {
                if (ae.InnerException is WebException we) {
                    if (we.Status == WebExceptionStatus.SecureChannelFailure) {
                        string message = "Server SSL handshake failed!";
                        DracoonClient.Log.Error(Logtag, message, we);
                        throw new DracoonNetInsecureException(message, we);
                    }

                    if (we.Status == WebExceptionStatus.RequestCanceled) {
                        throw new ThreadInterruptedException();
                    }

                    try {
                        if (we.Status == WebExceptionStatus.ProtocolError) {
                            DracoonErrorParser.ParseError(we, type);
                        } else {
                            string message = "Server communication failed!";
                            DracoonClient.Log.Debug(Logtag, message);
                            if (DracoonClient.HttpConfig.RetryEnabled && sendTry < 3) {
                                DracoonClient.Log.Debug(Logtag, "Retry the request in " + sendTry * 1000 + " millis again.");
                                Thread.Sleep(1000 * sendTry);
                                return ((IRequestExecutor)this).ExecuteWebClientChunkUpload(requestClient, target, data, type, asyncThread, sendTry + 1);
                            } else {
                                if (asyncThread != null && asyncThread.ThreadState == ThreadState.Aborted) {
                                    throw new ThreadInterruptedException();
                                }

                                DracoonErrorParser.ParseError(we, type);
                            }
                        }
                    } catch (DracoonApiException dae) {
                        if (sendTry < 3 && CheckTooManyRequestsResult(dae, we.Response)) {
                            return ((IRequestExecutor)this).ExecuteWebClientChunkUpload(requestClient, target, data, type, asyncThread, sendTry + 1);
                        }

                        throw;
                    }
                }
            }

            return response;
        }

        private bool CheckTooManyRequestsResult(DracoonApiException error, object response) {
            if (error.ErrorCode.Code == DracoonApiCode.SERVER_TOO_MANY_REQUESTS.Code) {
                int retryAfter;
                if (!int.TryParse(DracoonErrorParser.GetResponseHeaderValue(response, "Retry-After"), out retryAfter)) {
                    retryAfter = 1;// retryAfter is given in seconds and if no header is given use fallback time of 1 second
                }

                int waitingTime = retryAfter * 1000 + new Random().Next(0, 500);
                DracoonClient.Log.Debug(Logtag, $"Http status code 429 was given. Retry the request in {waitingTime} millis again.");
                Thread.Sleep(waitingTime);

                return true;
            }

            return false;
        }
    }
}