﻿using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonErrorParser {
        private const string LogTag = nameof(DracoonErrorParser);

        private static bool CheckResponseHasHeader(dynamic response, string headerName, string headerValue) {
            if (response is IRestResponse restResponse && restResponse.Headers != null) {
                foreach (Parameter current in restResponse.Headers) {
                    if (headerName.Equals(current.Name) && headerValue.Equals(current.Value)) {
                        return true;
                    }
                }
            }

            if (response is HttpWebResponse webResponse && webResponse.Headers != null) {
                string searchedValue = webResponse.Headers.Get(headerName);
                if (headerValue.Equals(searchedValue)) {
                    return true;
                }
            }

            return false;
        }

        private static ApiErrorResponse GetApiErrorResponse(string errorResponseBody) {
            try {
                ApiErrorResponse apiError = JsonConvert.DeserializeObject<ApiErrorResponse>(errorResponseBody);
                if (apiError != null) {
                    DracoonClient.Log.Debug(LogTag, apiError.ToString());
                }

                return apiError;
            } catch (Exception) {
                return null;
            }
        }

        private static string ReadErrorResponseFromWebException(WebException exception) {
            try {
                using (StreamReader sr = new StreamReader(exception.Response.GetResponseStream() ?? throw new ArgumentNullException())) {
                    return sr.ReadToEnd();
                }
            } catch (Exception) {
                return null;
            }
        }

        internal static void ParseError(IRestResponse response, RequestType requestType) {
            ApiErrorResponse apiError = GetApiErrorResponse(response.Content);
            DracoonApiCode resultCode = Parse((int) response.StatusCode, response, apiError, requestType);
            DracoonClient.Log.Debug(LogTag, $"Query for '{requestType.ToString()}' failed with '{resultCode.Text}'");

            throw new DracoonApiException(resultCode);
        }

        internal static void ParseError(WebException exception, RequestType requestType) {
            if (exception.Status == WebExceptionStatus.ProtocolError) {
                ApiErrorResponse apiError = GetApiErrorResponse(ReadErrorResponseFromWebException(exception));
                if (exception.Response is HttpWebResponse response) {
                    DracoonApiCode resultCode = Parse((int) response.StatusCode, response, apiError, requestType);
                    DracoonClient.Log.Debug(LogTag, $"Query for '{requestType.ToString()}' failed with '{resultCode.Text}'");
                    throw new DracoonApiException(resultCode);
                }

                throw new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR);
            }

            throw new DracoonNetIOException("The request for '" + requestType.ToString() + "' failed with '" + exception.Message + "'", exception);
        }

        internal static void ParseError(ApiErrorResponse apiError, RequestType requestType) {
            int code = 0;
            if (apiError.Code.HasValue) {
                code = apiError.Code.Value;
            }

            DracoonApiCode resultCode = Parse(code, null, apiError, requestType);
            throw new DracoonApiException(resultCode);
        }

        private static DracoonApiCode Parse(int httpStatusCode, dynamic response, ApiErrorResponse apiError, RequestType requestType) {
            int? apiErrorCode = null;
            if (apiError != null) {
                apiErrorCode = apiError.ErrorCode;
            }

            switch (httpStatusCode) {
                case (int) HttpStatusCode.BadRequest:
                    return ParseBadRequest(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.Unauthorized:
                    return ParseUnauthorized(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.Forbidden:
                    return ParseForbidden(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.NotFound:
                    return ParseNotFound(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.Conflict:
                    return ParseConflict(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.PreconditionFailed:
                    return ParsePreconditionFailed(apiErrorCode, response, requestType);
                case (int) HttpStatusCode.BadGateway:
                    return ParseBadGateway(apiErrorCode, response, requestType);
                case 507:
                    return ParseInsufficientStorage(apiErrorCode, response, requestType);
                case 901:
                    return ParseCustomError(apiErrorCode, response, requestType);
                default:
                    return DracoonApiCode.SERVER_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseBadRequest(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -10002:
                    return DracoonApiCode.VALIDATION_PASSWORT_NOT_SECURE;
                case -40001 when requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes:
                    return DracoonApiCode.VALIDATION_SOURCE_ROOM_ENCRYPTED;
                case -40001:
                    return DracoonApiCode.VALIDATION_ROOM_NOT_ENCRYPTED;
                case -40002 when requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes:
                    return DracoonApiCode.VALIDATION_TARGET_ROOM_ENCRYPTED;
                case -40002:
                    return DracoonApiCode.VALIDATION_ROOM_ENCRYPTED;
                case -40003:
                    return DracoonApiCode.VALIDATION_ROOM_CANNOT_UNENCRYPTED_WITH_FILES;
                case -40004:
                    return DracoonApiCode.VALIDATION_ROOM_STILL_HAS_RESCUE_KEY;
                case -40008:
                    return DracoonApiCode.VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_FILES;
                case -40012:
                    return DracoonApiCode.VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_RECYCLEBIN;
                case -40013:
                    return DracoonApiCode.VALIDATION_ENCRYPTED_FILE_CAN_ONLY_RESTOREED_IN_ORIGINAL_ROOM;
                case -40014:
                    return DracoonApiCode.VALIDATION_USER_HAS_NO_FILE_KEY;
                case -40018:
                    return DracoonApiCode.VALIDATION_ROOM_CANNOT_DECRYPTED_WITH_RECYCLEBIN;
                case -40755:
                    return DracoonApiCode.VALIDATION_BAD_FILE_NAME;
                case -40761:
                    return DracoonApiCode.VALIDATION_USER_HAS_NO_FILE_KEY;
                case -41052 when requestType == RequestType.PostCopyNodes:
                    return DracoonApiCode.VALIDATION_CANNOT_COPY_ROOM;
                case -41052 when requestType == RequestType.PostMoveNodes:
                    return DracoonApiCode.VALIDATION_CANNOT_MOVE_ROOM;
                case -41053:
                    return DracoonApiCode.VALIDATION_FILE_CANNOT_BE_TARGET;
                case -41054:
                    return DracoonApiCode.VALIDATION_NODES_NOT_IN_SAME_PARENT;
                case -41200:
                    return DracoonApiCode.VALIDATION_PATH_TOO_LONG;
                case -41301:
                    return DracoonApiCode.VALIDATION_NODE_IS_NO_FAVORITE;
                case -41302:
                case -41303: {
                    switch (requestType) {
                        case RequestType.PostCopyNodes:
                            return DracoonApiCode.VALIDATION_CANNOT_COPY_NODE_TO_OWN_PLACE_WITHOUT_RENAME;
                        case RequestType.PostMoveNodes:
                            return DracoonApiCode.VALIDATION_CANNOT_MOVE_NODE_TO_OWN_PLACE;
                        default:
                            return DracoonApiCode.VALIDATION_UNKNOWN_ERROR;
                    }
                }

                case -70020:
                    return DracoonApiCode.VALIDATION_USER_HAS_NO_KEY_PAIR;
                case -70022:
                case -70023:
                    return DracoonApiCode.VALIDATION_USER_KEY_PAIR_INVALID;
                case -80000:
                    return DracoonApiCode.VALIDATION_FIELD_CANNOT_BE_EMPTY;
                case -80001:
                    return DracoonApiCode.VALIDATION_FIELD_NOT_POSITIVE;
                case -80003:
                    return DracoonApiCode.VALIDATION_FIELD_NOT_ZERO_POSITIVE;
                case -80006:
                    return DracoonApiCode.VALIDATION_EXPIRATION_DATE_IN_PAST;
                case -80007:
                    return DracoonApiCode.VALIDATION_FIELD_MAX_LENGTH_EXCEEDED;
                case -80008:
                case -80012:
                    return DracoonApiCode.VALIDATION_EXPIRATION_DATE_TOO_LATE;
                case -80009:
                    return DracoonApiCode.VALIDATION_INVALID_EMAIL_ADDRESS;
                case -80018:
                    return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_0_9999;
                case -80019:
                    return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_1_9999;
                case -80024:
                    return DracoonApiCode.VALIDATION_INVALID_OFFSET_OR_LIMIT;
                case -80030:
                    return DracoonApiCode.SERVER_SMS_IS_DISABLED;
                case -80034:
                    return DracoonApiCode.VALIDATION_KEEPSHARELINKS_ONLY_WITH_OVERWRITE;
                case -80035:
                    return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_0_10;
                case -80045:
                    return DracoonApiCode.VALIDATION_INVALID_ETAG;
                case -90033:
                    return DracoonApiCode.SERVER_S3_IS_ENFORCED;
                default:
                    return DracoonApiCode.VALIDATION_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseUnauthorized(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -10006:
                    return DracoonApiCode.AUTH_OAUTH_CLIENT_NO_PERMISSION;
                default:
                    return DracoonApiCode.AUTH_UNAUTHORIZED;
            }
        }

        private static DracoonApiCode ParseForbidden(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (CheckResponseHasHeader(response, "X-Forbidden", "403")) {
                return DracoonApiCode.SERVER_MALICIOUS_FILE_DETECTED;
            }

            switch (apiErrorCode) {
                case -10003:
                case -10007:
                    return DracoonApiCode.AUTH_USER_LOCKED;
                case -10004:
                    return DracoonApiCode.AUTH_USER_EXPIRED;
                case -10005:
                    return DracoonApiCode.AUTH_USER_TEMPORARY_LOCKED;
                case -70020:
                    return DracoonApiCode.SERVER_USER_KEY_PAIR_NOT_FOUND;
                case -40761:
                    return DracoonApiCode.SERVER_FILE_KEY_NOT_FOUND;
                default: {
                    switch (requestType) {
                        case RequestType.DeleteNodes:
                            return DracoonApiCode.PERMISSION_DELETE_ERROR;
                        case RequestType.PutRoom:
                        case RequestType.PutFolder:
                        case RequestType.PutFile:
                        case RequestType.PostMoveNodes:
                            return DracoonApiCode.PERMISSION_UPDATE_ERROR;
                        case RequestType.GetNode:
                        case RequestType.GetNodes:
                        case RequestType.GetSearchNodes:
                        case RequestType.PostDownloadToken:
                        case RequestType.PostFavorite:
                        case RequestType.DeleteFavorite:
                            return DracoonApiCode.PERMISSION_READ_ERROR;
                        case RequestType.PostRoom:
                        case RequestType.PostFolder:
                        case RequestType.PostCopyNodes:
                            return DracoonApiCode.PERMISSION_CREATE_ERROR;
                        case RequestType.PostCreateDownloadShare:
                        case RequestType.DeleteDownloadShare:
                            return DracoonApiCode.PERMISSION_MANAGE_DL_SHARES_ERROR;
                        case RequestType.PostCreateUploadShare:
                        case RequestType.DeleteUploadShare:
                            return DracoonApiCode.PERMISSION_MANAGE_UL_SHARES_ERROR;
                    }

                    break;
                }
            }

            return DracoonApiCode.PERMISSION_UNKNOWN_ERROR;
        }

        private static DracoonApiCode ParseNotFound(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -40751:
                    return DracoonApiCode.SERVER_FILE_NOT_FOUND;
                case -41000:
                case -40000: {
                    switch (requestType) {
                        case RequestType.PostRoom:
                            return DracoonApiCode.SERVER_TARGET_ROOM_NOT_FOUND;
                        case RequestType.PostFolder:
                            return DracoonApiCode.SERVER_TARGET_NODE_NOT_FOUND;
                        case RequestType.PutFolder:
                            return DracoonApiCode.SERVER_FOLDER_NOT_FOUND;
                        case RequestType.PutRoom:
                        case RequestType.GetMissingFileKeys:
                            return DracoonApiCode.SERVER_ROOM_NOT_FOUND;
                        default:
                            return DracoonApiCode.SERVER_NODE_NOT_FOUND;
                    }
                }

                case -41050:
                    return DracoonApiCode.SERVER_SOURCE_NODE_NOT_FOUND;
                case -41051:
                    return DracoonApiCode.SERVER_TARGET_NODE_NOT_FOUND;
                case -41100:
                    return DracoonApiCode.SERVER_RESTOREVERSION_NOT_FOUND;
                case -60000:
                    return DracoonApiCode.SERVER_DL_SHARE_NOT_FOUND;
                case -60500:
                case -20501:
                    return DracoonApiCode.SERVER_UL_SHARE_NOT_FOUND;
                case -70020:
                    return DracoonApiCode.SERVER_USER_KEY_PAIR_NOT_FOUND;
                case -70028:
                    return DracoonApiCode.SERVER_USER_AVATAR_NOT_FOUND;
                case -70501:
                    return DracoonApiCode.SERVER_USER_NOT_FOUND;
                case -90034:
                    return DracoonApiCode.SERVER_S3_UPLOAD_ID_NOT_FOUND;
                default:
                    return DracoonApiCode.SERVER_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseConflict(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -41001:
                    return DracoonApiCode.VALIDATION_NODE_ALREADY_EXISTS;
                case -41304 when requestType == RequestType.PostMoveNodes:
                    return DracoonApiCode.VALIDATION_CANNOT_MOVE_TO_CHILD;
                case -41304 when requestType == RequestType.PostCopyNodes:
                    return DracoonApiCode.VALIDATION_CANNOT_COPY_TO_CHILD;
                case -70021:
                    return DracoonApiCode.SERVER_USER_KEY_PAIR_ALREADY_SET;
                default: {
                    switch (requestType) {
                        case RequestType.PostRoom:
                            return DracoonApiCode.VALIDATION_ROOM_ALREADY_EXISTS;
                        case RequestType.PostFolder:
                        case RequestType.PutFolder:
                            return DracoonApiCode.VALIDATION_FOLDER_ALREADY_EXISTS;
                        case RequestType.PutRoom:
                            return DracoonApiCode.VALIDATION_ROOM_ALREADY_EXISTS;
                        case RequestType.PutFile:
                            return DracoonApiCode.VALIDATION_FILE_ALREADY_EXISTS;
                        default: {
                            if (apiErrorCode == -40010) {
                                return DracoonApiCode.VALIDATION_ROOM_FOLDER_CAN_NOT_BE_OVERWRITTEN;
                            }

                            if (requestType == RequestType.PostCreateUploadShare) {
                                return DracoonApiCode.VALIDATION_UL_SHARE_NAME_ALREADY_EXISTS;
                            }

                            return DracoonApiCode.SERVER_UNKNOWN_ERROR;
                        }
                    }
                }
            }
        }

        private static DracoonApiCode ParsePreconditionFailed(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -10103:
                    return DracoonApiCode.PRECONDITION_MUST_ACCEPT_EULA;
                case -10104:
                    return DracoonApiCode.PRECONDITION_MUST_CHANGE_PASSWORD;
                case -10106:
                    return DracoonApiCode.PRECONDITION_MUST_CHANGE_USER_NAME;
                case -90030:
                    return DracoonApiCode.PRECONDITION_S3_DISABLED;
                default:
                    return DracoonApiCode.PRECONDITION_UNKNOWN_ERROR;
            }
        }

        private static DracoonApiCode ParseBadGateway(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -90090:
                    return DracoonApiCode.SERVER_SMS_COULD_NOT_BE_SENT;
                default:
                    switch (requestType) {
                        case RequestType.PutCompleteS3Upload:
                            return DracoonApiCode.SERVER_S3_UPLOAD_COMPLETION_FAILED;
                        default:
                            return DracoonApiCode.SERVER_UNKNOWN_ERROR;
                    }
            }
        }

        private static DracoonApiCode ParseInsufficientStorage(int? apiErrorCode, dynamic response, RequestType requestType) {
            switch (apiErrorCode) {
                case -90200:
                    return DracoonApiCode.SERVER_INSUFFICIENT_CUSTOMER_QUOTA;
                case -40200:
                    return DracoonApiCode.SERVER_INSUFFICIENT_ROOM_QUOTA;
                case -50504:
                    return DracoonApiCode.SERVER_INSUFFICIENT_UL_SHARE_QUOTA;
                default:
                    return DracoonApiCode.SERVER_INSUFFICIENT_STORAGE;
            }
        }

        private static DracoonApiCode ParseCustomError(int? apiErrorCode, dynamic response, RequestType requestType) {
            return DracoonApiCode.SERVER_MALICIOUS_FILE_DETECTED;
        }
    }
}