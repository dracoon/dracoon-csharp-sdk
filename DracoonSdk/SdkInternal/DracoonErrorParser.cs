using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonErrorParser {

        private readonly string LOGTAG = typeof(DracoonErrorParser).Name;
        private DracoonClient dracoonClient;

        internal DracoonErrorParser(DracoonClient client) {
            dracoonClient = client;
        }

        private bool CheckResponseHasHeader(dynamic response, string headerName, string headerValue) {
            if (response is IRestResponse restResponse) {
                foreach (Parameter current in restResponse.Headers) {
                    if (current.Name.Equals(headerName) && current.Value.Equals(headerValue)) {
                        return true;
                    }
                }
            }
            if (response is HttpWebResponse webResponse) {
                string searchedValue = webResponse.Headers.Get(headerName);
                if (searchedValue.Equals(headerValue)) {
                    return true;
                }
            }
            return false;
        }

        private ApiErrorResponse GetApiErrorResponse(string errorResponseBody) {
            try {
                ApiErrorResponse apiError = JsonConvert.DeserializeObject<ApiErrorResponse>(errorResponseBody);
                if (apiError != null) {
                    dracoonClient.Log.Debug(LOGTAG, apiError.ToString());
                }
                return apiError;
            } catch (Exception) {
                return null;
            }
        }

        private string ReadErrorResponseFromWebException(WebException exception) {
            try {
                using (StreamReader sr = new StreamReader(exception.Response.GetResponseStream())) {
                    return sr.ReadToEnd();
                }
            } catch (Exception) {
                return null;
            }
        }

        internal void ParseError(IRestResponse response, RequestType requestType) {
            ApiErrorResponse apiError = GetApiErrorResponse(response.Content);
            DracoonApiCode dracoonResultCode = Parse((int) response.StatusCode, response, apiError, requestType);
            dracoonClient.Log.Debug(LOGTAG, String.Format("Query for '{0}' failed with '{1}'", requestType.ToString(), dracoonResultCode.Text));

            throw new DracoonApiException(dracoonResultCode);
        }

        internal void ParseError(WebException exception, RequestType requestType) {
            if (exception.Status == WebExceptionStatus.ProtocolError) {
                ApiErrorResponse apiError = GetApiErrorResponse(ReadErrorResponseFromWebException(exception));
                if (exception.Response is HttpWebResponse response) {
                    DracoonApiCode dracoonResultCode = Parse((int) response.StatusCode, response, apiError, requestType);
                    dracoonClient.Log.Debug(LOGTAG, String.Format("Query for '{0}' failed with '{1}'", requestType.ToString(), dracoonResultCode.Text));
                    throw new DracoonApiException(dracoonResultCode);
                } else {
                    throw new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR);
                }
            } else {
                throw new DracoonNetIOException("The request for '" + requestType.ToString() + "' failed with '" + exception.Message + "'", exception);
            }
        }

        private DracoonApiCode Parse(int httpStatusCode, dynamic response, ApiErrorResponse apiError, RequestType requestType) {
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
                    return ParseInsufficentStorage(apiErrorCode, response, requestType);
                case 901:
                    return ParseCustomError(apiErrorCode, response, requestType);
                default:
                    return DracoonApiCode.SERVER_UNKNOWN_ERROR;
            }
        }

        private DracoonApiCode ParseBadRequest(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -10002) {
                return DracoonApiCode.VALIDATION_PASSWORT_NOT_SECURE;
            } else if (apiErrorCode == -40001) {
                if (requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes) {
                    return DracoonApiCode.VALIDATION_SOURCE_ROOM_ENCRYPTED;
                }
                return DracoonApiCode.VALIDATION_ROOM_NOT_ENCRYPTED;
            } else if (apiErrorCode == -40002) {
                if (requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes) {
                    return DracoonApiCode.VALIDATION_TARGET_ROOM_ENCRYPTED;
                }
                return DracoonApiCode.VALIDATION_ROOM_ENCRYPTED;
            } else if (apiErrorCode == -40003) {
                return DracoonApiCode.VALIDATION_ROOM_CANNOT_UNENCRYPTED_WITH_FILES;
            } else if (apiErrorCode == -40004) {
                return DracoonApiCode.VALIDATION_ROOM_STILL_HAS_RESCUE_KEY;
            } else if (apiErrorCode == -40008) {
                return DracoonApiCode.VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_FILES;
            } else if (apiErrorCode == -40012) {
                return DracoonApiCode.VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_RECYCLEBIN;
            } else if (apiErrorCode == -40013) {
                return DracoonApiCode.VALIDATION_ENCRYPTED_FILE_CAN_ONLY_RESTOREED_IN_ORIGINAL_ROOM;
            } else if (apiErrorCode == -40014) {
                return DracoonApiCode.VALIDATION_USER_HAS_NO_FILE_KEY;
            } else if (apiErrorCode == -40018) {
                return DracoonApiCode.VALIDATION_ROOM_CANNOT_DECRYPTED_WITH_RECYCLEBIN;
            } else if (apiErrorCode == -40755) {
                return DracoonApiCode.VALIDATION_BAD_FILE_NAME;
            } else if (apiErrorCode == -40761) {
                return DracoonApiCode.VALIDATION_USER_HAS_NO_FILE_KEY;
            } else if (apiErrorCode == -41052) {
                if (requestType == RequestType.PostCopyNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_COPY_ROOM;
                } else if (requestType == RequestType.PostMoveNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_MOVE_ROOM;
                }
            } else if (apiErrorCode == -41053) {
                return DracoonApiCode.VALIDATION_FILE_CANNOT_BE_TARGET;
            } else if (apiErrorCode == -41054) {
                return DracoonApiCode.VALIDATION_NODES_NOT_IN_SAME_PARENT;
            } else if (apiErrorCode == -41200) {
                return DracoonApiCode.VALIDATION_PATH_TOO_LONG;
            } else if (apiErrorCode == -41301) {
                return DracoonApiCode.VALIDATION_NODE_IS_NO_FAVORITE;
            } else if (apiErrorCode == -41302 || apiErrorCode == -41303) {
                if (requestType == RequestType.PostCopyNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_COPY_NODE_TO_OWN_PLACE_WITHOUT_RENAME;
                } else if (requestType == RequestType.PostMoveNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_MOVE_NODE_TO_OWN_PLACE;
                }
            } else if (apiErrorCode == -70020) {
                return DracoonApiCode.VALIDATION_USER_HAS_NO_KEY_PAIR;
            } else if (apiErrorCode == -70022 || apiErrorCode == -70023) {
                return DracoonApiCode.VALIDATION_USER_KEY_PAIR_INVALID;
            } else if (apiErrorCode == -80000) {
                return DracoonApiCode.VALIDATION_FIELD_CANNOT_BE_EMPTY;
            } else if (apiErrorCode == -80001) {
                return DracoonApiCode.VALIDATION_FIELD_NOT_POSITIVE;
            } else if (apiErrorCode == -80003) {
                return DracoonApiCode.VALIDATION_FIELD_NOT_ZERO_POSITIVE;
            } else if (apiErrorCode == -80006) {
                return DracoonApiCode.VALIDATION_EXPIRATION_DATE_IN_PAST;
            } else if (apiErrorCode == -80007) {
                return DracoonApiCode.VALIDATION_FIELD_MAX_LENGTH_EXCEEDED;
            } else if (apiErrorCode == -80008 || apiErrorCode == -80012) {
                return DracoonApiCode.VALIDATION_EXPIRATION_DATE_TOO_LATE;
            } else if (apiErrorCode == -80009) {
                return DracoonApiCode.VALIDATION_INVALID_EMAIL_ADDRESS;
            } else if (apiErrorCode == -80018) {
                return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_0_9999;
            } else if (apiErrorCode == -80019) {
                return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_1_9999;
            } else if (apiErrorCode == -80024) {
                return DracoonApiCode.VALIDATION_INVALID_OFFSET_OR_LIMIT;
            } else if (apiErrorCode == -80030) {
                return DracoonApiCode.SERVER_SMS_IS_DISABLED;
            } else if (apiErrorCode == -80034) {
                return DracoonApiCode.VALIDATION_KEEPSHARELINKS_ONLY_WITH_OVERWRITE;
            } else if (apiErrorCode == -80035) {
                return DracoonApiCode.VALIDATION_FIELD_NOT_BETWEEN_0_10;
            }
            return DracoonApiCode.VALIDATION_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParseUnauthorized(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -10006) {
                return DracoonApiCode.AUTH_OAUTH_CLIENT_NO_PERMISSION;
            }
            return DracoonApiCode.AUTH_UNAUTHORIZED;
        }

        private DracoonApiCode ParseForbidden(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (CheckResponseHasHeader(response, "X-Forbidden", "403")) {
                return DracoonApiCode.SERVER_MALICIOUS_FILE_DETECTED;
            } else if (apiErrorCode == -10003 || apiErrorCode == -10007) {
                return DracoonApiCode.AUTH_USER_LOCKED;
            } else if (apiErrorCode == -10004) {
                return DracoonApiCode.AUTH_USER_EXPIRED;
            } else if (apiErrorCode == -10005) {
                return DracoonApiCode.AUTH_USER_TEMPORARY_LOCKED;
            } else if (apiErrorCode == -70020) {
                return DracoonApiCode.SERVER_USER_KEY_PAIR_NOT_FOUND;
            } else if (apiErrorCode == -40761) {
                return DracoonApiCode.SERVER_FILE_KEY_NOT_FOUND;
            } else if (requestType == RequestType.DeleteNodes || requestType == RequestType.PostMoveNodes) {
                return DracoonApiCode.PERMISSION_DELETE_ERROR;
            } else if (requestType == RequestType.PutRoom || requestType == RequestType.PutFolder || requestType == RequestType.PutFile) {
                return DracoonApiCode.PERMISSION_UPDATE_ERROR;
            } else if (requestType == RequestType.GetNode || requestType == RequestType.GetNodes || requestType == RequestType.GetSearchNodes
                || requestType == RequestType.PostDownloadToken || requestType == RequestType.PostFavorite || requestType == RequestType.DeleteFavorite
                || requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes) {
                return DracoonApiCode.PERMISSION_READ_ERROR;
            } else if (requestType == RequestType.PostRoom || requestType == RequestType.PostFolder || requestType == RequestType.PostCopyNodes || requestType == RequestType.PostMoveNodes) {
                return DracoonApiCode.PERMISSION_CREATE_ERROR;
            } else if (requestType == RequestType.PostCreateDownloadShare || requestType == RequestType.DeleteDownloadShare) {
                return DracoonApiCode.PERMISSION_MANAGE_DL_SHARES_ERROR;
            } else if (requestType == RequestType.PostCreateUploadShare || requestType == RequestType.DeleteUploadShare) {
                return DracoonApiCode.PERMISSION_MANAGE_UL_SHARES_ERROR;
            }
            return DracoonApiCode.PERMISSION_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParseNotFound(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -40751) {
                return DracoonApiCode.SERVER_FILE_NOT_FOUND;
            } else if (apiErrorCode == -41000 || apiErrorCode == -40000) {
                if (requestType == RequestType.PostRoom) {
                    return DracoonApiCode.SERVER_TARGET_ROOM_NOT_FOUND;
                } else if (requestType == RequestType.PostFolder) {
                    return DracoonApiCode.SERVER_TARGET_NODE_NOT_FOUND;
                } else if (requestType == RequestType.PutFolder) {
                    return DracoonApiCode.SERVER_FOLDER_NOT_FOUND;
                } else if (requestType == RequestType.PutRoom) {
                    return DracoonApiCode.SERVER_ROOM_NOT_FOUND;
                } else if (requestType == RequestType.GetMissingFileKeys) {
                    return DracoonApiCode.SERVER_ROOM_NOT_FOUND;
                }
                return DracoonApiCode.SERVER_NODE_NOT_FOUND;
            } else if (apiErrorCode == -41050) {
                return DracoonApiCode.SERVER_SOURCE_NODE_NOT_FOUND;
            } else if (apiErrorCode == -41051) {
                return DracoonApiCode.SERVER_TARGET_NODE_NOT_FOUND;
            } else if (apiErrorCode == -41100) {
                return DracoonApiCode.SERVER_RESTOREVERSION_NOT_FOUND;
            } else if (apiErrorCode == -60000) {
                return DracoonApiCode.SERVER_DL_SHARE_NOT_FOUND;
            } else if (apiErrorCode == -60500 || apiErrorCode == -20501) {
                return DracoonApiCode.SERVER_UL_SHARE_NOT_FOUND;
            } else if (apiErrorCode == -70020) {
                return DracoonApiCode.SERVER_USER_KEY_PAIR_NOT_FOUND;
            } else if (apiErrorCode == -70028) {
                return DracoonApiCode.SERVER_USER_AVATAR_NOT_FOUND;
            } else if (apiErrorCode == -70501) {
                return DracoonApiCode.SERVER_USER_NOT_FOUND;
            }
            return DracoonApiCode.SERVER_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParseConflict(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -41001) {
                return DracoonApiCode.VALIDATION_NODE_ALREADY_EXISTS;
            } else if (apiErrorCode == -41304) {
                if (requestType == RequestType.PostMoveNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_MOVE_TO_CHILD;
                } else if (requestType == RequestType.PostCopyNodes) {
                    return DracoonApiCode.VALIDATION_CANNOT_COPY_TO_CHILD;
                }
            } else if (apiErrorCode == -70021) {
                return DracoonApiCode.SERVER_USER_KEY_PAIR_ALREADY_SET;
            } else if (requestType == RequestType.PostRoom) {
                return DracoonApiCode.VALIDATION_ROOM_ALREADY_EXISTS;
            } else if (requestType == RequestType.PostFolder) {
                return DracoonApiCode.VALIDATION_FOLDER_ALREADY_EXISTS;
            } else if (requestType == RequestType.PutFolder) {
                return DracoonApiCode.VALIDATION_FOLDER_ALREADY_EXISTS;
            } else if (requestType == RequestType.PutRoom) {
                return DracoonApiCode.VALIDATION_ROOM_ALREADY_EXISTS;
            } else if (requestType == RequestType.PutFile) {
                return DracoonApiCode.VALIDATION_FILE_ALREADY_EXISTS;
            } else if (apiErrorCode == -40010) {
                return DracoonApiCode.VALIDATION_ROOM_FOLDER_CAN_NOT_BE_OVERWRITTEN;
            } else if (requestType == RequestType.PostCreateUploadShare) {
                return DracoonApiCode.VALIDATION_UL_SHARE_NAME_ALREADY_EXISTS;
            }
            return DracoonApiCode.SERVER_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParsePreconditionFailed(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -10103) {
                return DracoonApiCode.PRECONDITION_MUST_ACCEPT_EULA;
            } else if (apiErrorCode == -10104) {
                return DracoonApiCode.PRECONDITION_MUST_CHANGE_PASSWORD;
            } else if (apiErrorCode == -10106) {
                return DracoonApiCode.PRECONDITION_MUST_CHANGE_USER_NAME;
            }
            return DracoonApiCode.PRECONDITION_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParseBadGateway(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -90090) {
                return DracoonApiCode.SERVER_SMS_COULD_NOT_BE_SENT;
            }
            return DracoonApiCode.SERVER_UNKNOWN_ERROR;
        }

        private DracoonApiCode ParseInsufficentStorage(int? apiErrorCode, dynamic response, RequestType requestType) {
            if (apiErrorCode == -90200) {
                return DracoonApiCode.SERVER_INSUFFICIENT_CUSTOMER_QUOTA;
            } else if (apiErrorCode == -40200) {
                return DracoonApiCode.SERVER_INSUFFICIENT_ROOM_QUOTA;
            } else if (apiErrorCode == -50504) {
                return DracoonApiCode.SERVER_INSUFFICIENT_UL_SHARE_QUOTA;
            }
            return DracoonApiCode.SERVER_INSUFFICIENT_STORAGE;
        }

        private DracoonApiCode ParseCustomError(int? apiErrorCode, dynamic response, RequestType requestType) {
            return DracoonApiCode.SERVER_MALICIOUS_FILE_DETECTED;
        }

    }
}
