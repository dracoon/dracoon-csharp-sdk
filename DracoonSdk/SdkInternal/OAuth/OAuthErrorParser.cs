using Dracoon.Sdk.Error;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal static class OAuthErrorParser {
        private const string Logtag = nameof(OAuthErrorParser);

        private const string ErrUnsupportedResponseType = "unsupported_response_type";
        private const string ErrUnsupportedGrantType = "unsupported_grant_type";
        private const string ErrInvalidClient = "invalid_client";
        private const string ErrInvalidGrant = "invalid_grant";
        private const string ErrInvalidScope = "invalid_scope";
        private const string ErrAccessDenied = "access_denied";

        private static OAuthError GetOAuthError(string errorResponseBody) {
            try {
                OAuthError apiError = JsonConvert.DeserializeObject<OAuthError>(errorResponseBody);
                if (apiError != null) {
                    DracoonClient.Log.Debug(Logtag, apiError.ToString());
                }

                return apiError;
            } catch (Exception) {
                return null;
            }
        }

        internal static void ParseError(string error) {
            switch (error) {
                case ErrUnsupportedResponseType:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_REQUEST_INVALID);
                case ErrInvalidClient:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_CLIENT_UNKNOWN);
                case ErrInvalidGrant:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED);
                case ErrInvalidScope:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_SCOPE_INVALID);
                case ErrAccessDenied:
                    throw new DracoonApiException(DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_ACCESS_DENIED);
                default:
                    throw new DracoonApiException(DracoonApiCode.AUTH_UNKNOWN_ERROR);
            }
        }

        internal static void ParseError(IRestResponse response, RequestType requestType) {
            OAuthError oauthError = GetOAuthError(response.Content);
            DracoonApiCode resultCode = Parse(response.StatusCode, oauthError, requestType);
            DracoonClient.Log.Debug(Logtag, $"Query for '{requestType.ToString()}' failed with {resultCode.Text}");

            throw new DracoonApiException(resultCode);
        }

        private static DracoonApiCode Parse(HttpStatusCode statusCode, OAuthError oAuthError, RequestType requestType) {
            switch ((int) statusCode) {
                case (int) HttpStatusCode.BadRequest:
                    return ParseBadRequest(oAuthError, requestType);
                case (int) HttpStatusCode.Unauthorized:
                    return ParseUnauthorized();
                default:
                    return DracoonApiCode.AUTH_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseBadRequest(OAuthError oAuthError, RequestType requestType) {
            switch (oAuthError.Error) {
                case ErrUnsupportedGrantType:
                    switch (requestType) {
                        case RequestType.PostOAuthToken:
                            return DracoonApiCode.AUTH_OAUTH_TOKEN_REQUEST_INVALID;
                        case RequestType.PostOAuthRefresh:
                            return DracoonApiCode.AUTH_OAUTH_REFRESH_REQUEST_INVALID;
                        default:
                            return DracoonApiCode.AUTH_UNKNOWN_ERROR;
                    }

                case ErrInvalidClient:
                    return DracoonApiCode.AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED;
                case ErrInvalidGrant:
                    switch (requestType) {
                        case RequestType.PostOAuthToken:
                            return DracoonApiCode.AUTH_OAUTH_TOKEN_CODE_INVALID;
                        case RequestType.PostOAuthRefresh:
                            return DracoonApiCode.AUTH_OAUTH_REFRESH_TOKEN_INVALID;
                        default:
                            return DracoonApiCode.AUTH_UNKNOWN_ERROR;
                    }

                default:
                    return DracoonApiCode.AUTH_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseUnauthorized() {
            return DracoonApiCode.AUTH_OAUTH_CLIENT_UNAUTHORIZED;
        }
    }
}