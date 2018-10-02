using Dracoon.Sdk.Error;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthErrorParser {

        protected readonly string LOGTAG = typeof(OAuthErrorParser).Name;

        private const string ERR_INVALID_REQUEST = "invalid_request";
        private const string ERR_UNSUPPORTED_RESPONSE_TYPE = "unsupported_response_type";
        private const string ERR_UNSUPPORTED_GRANT_TYPE = "unsupported_grant_type";
        private const string ERR_INVALID_CLIENT = "invalid_client";
        private const string ERR_INVALID_GRANT = "invalid_grant";
        private const string ERR_INVALID_SCOPE = "invalid_scope";
        private const string ERR_ACCESS_DENIED = "access_denied";

        private DracoonClient dracoonClient;

        internal OAuthErrorParser(DracoonClient client) {
            dracoonClient = client;
        }

        internal static void ParseError(string error) {
            switch (error) {
                case ERR_UNSUPPORTED_RESPONSE_TYPE:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_REQUEST_INVALID);
                case ERR_INVALID_CLIENT:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_CLIENT_UNKNOWN);
                case ERR_INVALID_GRANT:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED);
                case ERR_INVALID_SCOPE:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_SCOPE_INVALID);
                case ERR_ACCESS_DENIED:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_ACCESS_DENIED);
                default:
                    throw new DracoonApiException(DracoonApiCode.AUTH_UNKNOWN_ERROR);
            }
        }

        private OAuthError GetOAuthError(string errorResponseBody) {
            try {
                OAuthError apiError = JsonConvert.DeserializeObject<OAuthError>(errorResponseBody);
                if (apiError != null) {
                    dracoonClient.Log.Debug(LOGTAG, apiError.ToString());
                }
                return apiError;
            } catch (Exception) {
                return null;
            }
        }

        internal void ParseError(IRestResponse response, RequestType requestType) {
            OAuthError oauthError = GetOAuthError(response.Content);
            DracoonApiCode dracoonResultCode = Parse(response.StatusCode, oauthError, requestType);
            dracoonClient.Log.Debug(LOGTAG, String.Format("Query for '{0}' failed with {1}", requestType.ToString(), dracoonResultCode.Text));

            throw new DracoonApiException(dracoonResultCode);
        }

        private DracoonApiCode Parse(HttpStatusCode statusCode, OAuthError oAuthError, RequestType requestType) {
            switch ((int) statusCode) {
                case (int) HttpStatusCode.BadRequest:
                    return ParseBadRequest(oAuthError, requestType);
                case (int) HttpStatusCode.Unauthorized:
                    return ParseUnauthorized(oAuthError, requestType);
                default:
                    return DracoonApiCode.AUTH_UNKNOWN_ERROR;
            }
        }

        private DracoonApiCode ParseBadRequest(OAuthError oAuthError, RequestType requestType) {
            switch (oAuthError.Error) {
                case ERR_INVALID_REQUEST:
                case ERR_UNSUPPORTED_GRANT_TYPE:
                    if (requestType == RequestType.PostOAuthToken) {
                        return DracoonApiCode.AUTH_OAUTH_TOKEN_REQUEST_INVALID;
                    } else if (requestType == RequestType.PostOAuthRefresh) {
                        return DracoonApiCode.AUTH_OAUTH_REFRESH_REQUEST_INVALID;
                    } else {
                        return DracoonApiCode.AUTH_UNKNOWN_ERROR;
                    }
                case ERR_INVALID_CLIENT:
                    return DracoonApiCode.AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED;
                case ERR_INVALID_GRANT:
                    if (requestType == RequestType.PostOAuthToken) {
                        return DracoonApiCode.AUTH_OAUTH_TOKEN_CODE_INVALID;
                    } else if (requestType == RequestType.PostOAuthRefresh) {
                        return DracoonApiCode.AUTH_OAUTH_REFRESH_TOKEN_INVALID;
                    } else {
                        return DracoonApiCode.AUTH_UNKNOWN_ERROR;
                    }
                default:
                    return DracoonApiCode.AUTH_UNKNOWN_ERROR;
            }
        }

        private DracoonApiCode ParseUnauthorized(OAuthError oAuthError, RequestType requestType) {
            return DracoonApiCode.AUTH_OAUTH_CLIENT_UNAUTHORIZED;
        }
    }
}
