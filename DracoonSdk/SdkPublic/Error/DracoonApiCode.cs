
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiCode"]/DracoonApiCode/*'/>
    public class DracoonApiCode : IEquatable<DracoonApiCode> {
        public static readonly DracoonApiCode API_VERSION_NOT_SUPPORTED = new DracoonApiCode(0, "Server API versions < " + SdkInternal.ApiConfig.MinimumApiVersion + " are not supported.");

        #region Error codes '1000' --> AUTH

        public static readonly DracoonApiCode AUTH_UNKNOWN_ERROR = new DracoonApiCode(1000, "An authentication error occurred.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_UNKNOWN = new DracoonApiCode(1100, "OAuth client is unknown.");
        // CODES: PostOAuthToken, PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_UNAUTHORIZED = new DracoonApiCode(1101, "OAuth client is unauthorized.");
        // CODES: PostOAuthToken, PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED = new DracoonApiCode(1102, "OAuth grant type is not allowed.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_REQUEST_INVALID = new DracoonApiCode(1103, "OAuth authorization request is invalid.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_SCOPE_INVALID = new DracoonApiCode(1104, "OAuth scope is invalid.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_ACCESS_DENIED = new DracoonApiCode(1105, "OAuth access was denied.");
        // CODES: PostOAuthToken
        public static readonly DracoonApiCode AUTH_OAUTH_TOKEN_REQUEST_INVALID = new DracoonApiCode(1106, "OAuth token request is invalid.");
        // CODES: PostOAuthToken
        public static readonly DracoonApiCode AUTH_OAUTH_TOKEN_CODE_INVALID = new DracoonApiCode(1107, "OAuth authorization code is invalid.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_REFRESH_REQUEST_INVALID = new DracoonApiCode(1108, "OAuth token refresh request is invalid.");
        // CODES: PostOAuthRefresh
        public static readonly DracoonApiCode AUTH_OAUTH_REFRESH_TOKEN_INVALID = new DracoonApiCode(1109, "OAuth refresh token is invalid.");
        // CODES: -10006
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_NO_PERMISSION = new DracoonApiCode(1150, "OAuth client has no permission to execute the action.");
        // CODES: -10012, -20502
        public static readonly DracoonApiCode AUTH_UNAUTHORIZED = new DracoonApiCode(1200, "Unauthorized.");
        // CODES: -10005
        public static readonly DracoonApiCode AUTH_USER_TEMPORARY_LOCKED = new DracoonApiCode(1300, "User is temporary locked.");
        // CODES: -10003, -10007
        public static readonly DracoonApiCode AUTH_USER_LOCKED = new DracoonApiCode(1301, "User is locked.");
        // CODES: -10004
        public static readonly DracoonApiCode AUTH_USER_EXPIRED = new DracoonApiCode(1302, "User is expired.");

        #endregion

        #region Error codes '2000' --> PRECONDITION

        public static readonly DracoonApiCode PRECONDITION_UNKNOWN_ERROR = new DracoonApiCode(2000, "A precondition is not fulfilled.");
        // CODES: -10103
        public static readonly DracoonApiCode PRECONDITION_MUST_ACCEPT_EULA = new DracoonApiCode(2101, "User must accept EULA.");
        // CODES: -10106
        public static readonly DracoonApiCode PRECONDITION_MUST_CHANGE_USER_NAME = new DracoonApiCode(2102, "User must change his user name.");
        // CODES: -10104
        public static readonly DracoonApiCode PRECONDITION_MUST_CHANGE_PASSWORD = new DracoonApiCode(2103, "User must change his password.");
        // CODES: -90030
        public static readonly DracoonApiCode PRECONDITION_S3_DISABLED = new DracoonApiCode(2104, "S3 storage is disabled.");

        #endregion

        #region Error codes '3000' --> VALIDATION

        #region GENERAL

        public static readonly DracoonApiCode VALIDATION_UNKNOWN_ERROR = new DracoonApiCode(3000, "The server request was invalid.");
        // CODES: -80000
        public static readonly DracoonApiCode VALIDATION_FIELD_CANNOT_BE_EMPTY = new DracoonApiCode(3001, "Mandatory fields cannnot be empty.");
        // CODES: -80003
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_ZERO_POSITIVE = new DracoonApiCode(3002, "Field value must be zero or positive.");
        // CODES: -80001
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_POSITIVE = new DracoonApiCode(3003, "Field value must be positive.");
        // CODES: -80007
        public static readonly DracoonApiCode VALIDATION_FIELD_MAX_LENGTH_EXCEEDED = new DracoonApiCode(3004, "Maximum allowed field length is exceeded.");
        // CODES: -80035
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_0_10 = new DracoonApiCode(3005, "Field value must be between 0 and 10.");
        // CODES: -80018
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_0_9999 = new DracoonApiCode(3006, "Field value must be between 0 and 9999.");
        // CODES: -80019
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_1_9999 = new DracoonApiCode(3007, "Field value must be between 1 and 9999.");
        // CODES: -80024
        public static readonly DracoonApiCode VALIDATION_INVALID_OFFSET_OR_LIMIT = new DracoonApiCode(3008, "Invalid offset or limit.");

        #endregion
        #region NODES

        // CODES: -41053
        public static readonly DracoonApiCode VALIDATION_FILE_CANNOT_BE_TARGET = new DracoonApiCode(3100, "File cannot be target of a copy or move operation.");
        // CODES: -40755
        public static readonly DracoonApiCode VALIDATION_BAD_FILE_NAME = new DracoonApiCode(3101, "Bad file name.");
        // CODES: -80006
        public static readonly DracoonApiCode VALIDATION_EXPIRATION_DATE_IN_PAST = new DracoonApiCode(3102, "Expiration date is in past.");
        // CODES: -80008, -80012
        public static readonly DracoonApiCode VALIDATION_EXPIRATION_DATE_TOO_LATE = new DracoonApiCode(3103, "The year is too far in the future. Max year is limited to 9999.");
        // CODES: -41001
        public static readonly DracoonApiCode VALIDATION_NODE_ALREADY_EXISTS = new DracoonApiCode(3104, "Node exists already.");
        // CODES: PutRoom, PostRoom
        public static readonly DracoonApiCode VALIDATION_ROOM_ALREADY_EXISTS = new DracoonApiCode(3105, "A room with the same name already exists.");
        // CODES: PutFolder, PostFolder
        public static readonly DracoonApiCode VALIDATION_FOLDER_ALREADY_EXISTS = new DracoonApiCode(3106, "A folder with the same name already exists.");
        // CODES: PutFile
        public static readonly DracoonApiCode VALIDATION_FILE_ALREADY_EXISTS = new DracoonApiCode(3107, "A file with the same name already exists.");
        // CODES: -41054
        public static readonly DracoonApiCode VALIDATION_NODES_NOT_IN_SAME_PARENT = new DracoonApiCode(3108, "Folders/files must be in same parent.");
        // CODES: -41302, -41303
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_NODE_TO_OWN_PLACE_WITHOUT_RENAME = new DracoonApiCode(3109, "Node cannot be copied to its own place without renaming.");
        // CODES: -41302
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_NODE_TO_OWN_PLACE = new DracoonApiCode(3110, "Node cannot be moved to its own place.");
        // CODES: -40010
        public static readonly DracoonApiCode VALIDATION_ROOM_FOLDER_CAN_NOT_BE_OVERWRITTEN = new DracoonApiCode(3111, "A room or folder cannot be overwritten.");
        // CODES: -41304
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_TO_CHILD = new DracoonApiCode(3112, "Node cannot be copied into its child node.");
        // CODES: -41304
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_TO_CHILD = new DracoonApiCode(3113, "Node cannot be moved into its child node.");
        // CODES: -41052
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_ROOM = new DracoonApiCode(3114, "Rooms cannot be copied.");
        // CODES: -41052
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_ROOM = new DracoonApiCode(3115, "Rooms cannot be moved.");
        // CODES: -41200
        public static readonly DracoonApiCode VALIDATION_PATH_TOO_LONG = new DracoonApiCode(3116, "Path is too long.");
        // CODES: -41301
        public static readonly DracoonApiCode VALIDATION_NODE_IS_NO_FAVORITE = new DracoonApiCode(3117, "Node is not marked as favorite.");
        // CODES: -40001
        public static readonly DracoonApiCode VALIDATION_ROOM_NOT_ENCRYPTED = new DracoonApiCode(3118, "Room not encrypted.");
        // CODES: -40001
        public static readonly DracoonApiCode VALIDATION_SOURCE_ROOM_ENCRYPTED = new DracoonApiCode(3119, "Encrypted files cannot be copied or moved to an unencrypted room.");
        // CODES: -40001
        public static readonly DracoonApiCode VALIDATION_TARGET_ROOM_ENCRYPTED = new DracoonApiCode(3120, "Not encrypted files cannot be copied or moved to an encrypted room.");
        // CODES: -40002
        public static readonly DracoonApiCode VALIDATION_ROOM_ENCRYPTED = new DracoonApiCode(3121, "Room is encrypted.");
        // CODES: -40003
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_UNENCRYPTED_WITH_FILES = new DracoonApiCode(3122, "Room with files cannot be unencrypted.");
        // CODES: -40008
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_FILES = new DracoonApiCode(3123, "Room with files cannot be encrypted.");
        // CODES: -40004
        public static readonly DracoonApiCode VALIDATION_ROOM_STILL_HAS_RESCUE_KEY = new DracoonApiCode(3124, "Only one room emergency password (rescue key) is allowed.");
        // CODES: -40012
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_RECYCLEBIN = new DracoonApiCode(3125, "Room with not empty recycle bin cannot be encrypted.");
        // CODES: -40018
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_DECRYPTED_WITH_RECYCLEBIN = new DracoonApiCode(3126, "Room with not empty recycle bin cannot be decrypted.");
        // CODES: -40013
        public static readonly DracoonApiCode VALIDATION_ENCRYPTED_FILE_CAN_ONLY_RESTOREED_IN_ORIGINAL_ROOM = new DracoonApiCode(3127, "Encrypted files cannot be restored inside antoher than its original room.");
        // CODES: -80034
        public static readonly DracoonApiCode VALIDATION_KEEPSHARELINKS_ONLY_WITH_OVERWRITE = new DracoonApiCode(3128, "Keep share links is only allowed with resolution strategy 'overwrite'.");
        // CODES: -80045
        public static readonly DracoonApiCode VALIDATION_INVALID_ETAG = new DracoonApiCode(3129, "Invalid Etag(s).");

        #endregion
        #region SHARES

        // CODES: -50004
        public static readonly DracoonApiCode VALIDATION_DL_SHARE_CANNOT_CREATE_ON_ENCRYPTED_ROOM_FOLDER = new DracoonApiCode(3200, "A download share cannot be created on a encrypted room or folder.");
        // CODES: PostCreateUploadShare
        public static readonly DracoonApiCode VALIDATION_UL_SHARE_NAME_ALREADY_EXISTS = new DracoonApiCode(3201, "Upload share name already exists.");

        #endregion
        #region CUSTOMER

        public static readonly DracoonApiCode PLACEHOLDER_CUSTOMER = new DracoonApiCode(3400, "");

        #endregion
        #region USERS

        public static readonly DracoonApiCode PLACEHOLDER_USERS = new DracoonApiCode(3500, "");
        // CODES: -70020
        public static readonly DracoonApiCode VALIDATION_USER_HAS_NO_KEY_PAIR = new DracoonApiCode(3550, "User has no encryption key pair.");
        // CODES: -70022, -70023
        public static readonly DracoonApiCode VALIDATION_USER_KEY_PAIR_INVALID = new DracoonApiCode(3551, "Invalid encryption key pair.");
        // CODES: -70761
        public static readonly DracoonApiCode VALIDATION_USER_HAS_NO_FILE_KEY = new DracoonApiCode(3552, "User has no encryption file key.");

        #endregion
        #region GROUPS

        public static readonly DracoonApiCode PLACEHOLDER_GROUPS = new DracoonApiCode(3600, "");

        #endregion
        #region OTHERS

        // CODES: -10002
        public static readonly DracoonApiCode VALIDATION_PASSWORT_NOT_SECURE = new DracoonApiCode(3800, "Password is not secure.");
        // CODES: -80009
        public static readonly DracoonApiCode VALIDATION_INVALID_EMAIL_ADDRESS = new DracoonApiCode(3801, "Invalid email address.");

        #endregion

        #endregion

        #region Error codes '4000' --> PERMISSION


        public static readonly DracoonApiCode PERMISSION_UNKNOWN_ERROR = new DracoonApiCode(4000, "User has no permissions to execute the action in this room.");
        public static readonly DracoonApiCode PERMISSION_MANAGE_ERROR = new DracoonApiCode(4100, "User has no permission to manage this room.");
        public static readonly DracoonApiCode PERMISSION_READ_ERROR = new DracoonApiCode(4101, "User has no permission to read nodes.");
        public static readonly DracoonApiCode PERMISSION_CREATE_ERROR = new DracoonApiCode(4102, "User has no permission to create nodes.");
        public static readonly DracoonApiCode PERMISSION_UPDATE_ERROR = new DracoonApiCode(4103, "User has no permission to change nodes.");
        public static readonly DracoonApiCode PERMISSION_DELETE_ERROR = new DracoonApiCode(4104, "User has no permission to delete nodes.");
        public static readonly DracoonApiCode PERMISSION_MANAGE_DL_SHARES_ERROR = new DracoonApiCode(4105, "User has no permission to manage download shares in this room.");
        public static readonly DracoonApiCode PERMISSION_MANAGE_UL_SHARES_ERROR = new DracoonApiCode(4106, "User has no permission to manage upload shares in this room.");
        public static readonly DracoonApiCode PERMISSION_READ_RECYCLE_BIN_ERROR = new DracoonApiCode(4107, "User has no permission to read recycle bin in this room.");
        public static readonly DracoonApiCode PERMISSION_RESTORE_RECYCLE_BIN_ERROR = new DracoonApiCode(4108, "User has no permission to restore recycle bin items in this room.");
        public static readonly DracoonApiCode PERMISSION_DELETE_RECYCLE_BIN_ERROR = new DracoonApiCode(4109, "User has no permission to delete recycle bin items in this room.");




        #endregion

        #region Error codes '5000' --> SERVER

        #region GENERAL

        public static readonly DracoonApiCode SERVER_UNKNOWN_ERROR = new DracoonApiCode(5000, "A server error occurred.");
        public static readonly DracoonApiCode SERVER_MALICIOUS_FILE_DETECTED = new DracoonApiCode(5090, "The AV scanner detected that the file could be malicious.");

        #endregion

        #region NODES

        // CODES: -41000
        public static readonly DracoonApiCode SERVER_NODE_NOT_FOUND = new DracoonApiCode(5100, "Requested room/folder/file was not found.");
        // CODES: -41000
        public static readonly DracoonApiCode SERVER_ROOM_NOT_FOUND = new DracoonApiCode(5101, "Requested room was not found.");
        // CODES: -41000
        public static readonly DracoonApiCode SERVER_FOLDER_NOT_FOUND = new DracoonApiCode(5102, "Requested folder was not found.");
        // CODES: -40751
        public static readonly DracoonApiCode SERVER_FILE_NOT_FOUND = new DracoonApiCode(5103, "Requested file was not found.");
        // CODES: -41050
        public static readonly DracoonApiCode SERVER_SOURCE_NODE_NOT_FOUND = new DracoonApiCode(5104, "Source node not found.");
        // CODES: -41000, -41051
        public static readonly DracoonApiCode SERVER_TARGET_NODE_NOT_FOUND = new DracoonApiCode(5105, "Target room or folder was not found.");
        // CODES: -41000
        public static readonly DracoonApiCode SERVER_TARGET_ROOM_NOT_FOUND = new DracoonApiCode(5106, "Target room was not found.");
        // CODES: -90201
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_STORAGE = new DracoonApiCode(5107, "Not enough free storage on the server.");
        // CODES: -90200
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_CUSTOMER_QUOTA = new DracoonApiCode(5108, "Not enough quota for the customer.");
        // CODES: -40200
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_ROOM_QUOTA = new DracoonApiCode(5109, "Not enough quota for the room.");
        // CODES: -50504
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_UL_SHARE_QUOTA = new DracoonApiCode(5110, "Not enough quota for the upload share.");
        // CODES: -41100
        public static readonly DracoonApiCode SERVER_RESTOREVERSION_NOT_FOUND = new DracoonApiCode(5111, "The restore version id was not found.");
        // CODES: -20501
        public static readonly DracoonApiCode SERVER_UPLOAD_NOT_FOUND = new DracoonApiCode(5112, "The upload with the given id was not found.");
        // CODES: -90034
        public static readonly DracoonApiCode SERVER_S3_UPLOAD_ID_NOT_FOUND = new DracoonApiCode(5113, "Corresponding S3 upload ID not found.");
        public static readonly DracoonApiCode SERVER_S3_UPLOAD_COMPLETION_FAILED = new DracoonApiCode(5114, "Server failed to complete S3 upload.");
        #endregion

        #region SHARES

        // CODES: -60000
        public static readonly DracoonApiCode SERVER_DL_SHARE_NOT_FOUND = new DracoonApiCode(5200, "Download share could not be found.");
        // CODES: -20501, -65000
        public static readonly DracoonApiCode SERVER_UL_SHARE_NOT_FOUND = new DracoonApiCode(5201, "Upload share could not be found.");

        #endregion

        #region CUSTOMER

        public static readonly DracoonApiCode SERVER_PLACEHOLDER_CUSTOMER = new DracoonApiCode(5400, "");

        #endregion

        #region USERS

        // CODES: -70501
        public static readonly DracoonApiCode SERVER_USER_NOT_FOUND = new DracoonApiCode(5500, "User could not be found.");
        // CODES: -70020
        public static readonly DracoonApiCode SERVER_USER_KEY_PAIR_NOT_FOUND = new DracoonApiCode(5550, "Encryption key pair was not found.");
        // CODES: -70021
        public static readonly DracoonApiCode SERVER_USER_KEY_PAIR_ALREADY_SET = new DracoonApiCode(5551, "Encryption key pair was already set.");
        // CODES: -40761
        public static readonly DracoonApiCode SERVER_FILE_KEY_NOT_FOUND = new DracoonApiCode(5552, "Encryption file key could not be found.");
        // CODES: -70028
        public static readonly DracoonApiCode SERVER_USER_AVATAR_NOT_FOUND = new DracoonApiCode(5553, "Avatar for this user could not be found.");
        #endregion

        #region GROUPS

        public static readonly DracoonApiCode SERVER_PLACEHOLDER_GROUPS = new DracoonApiCode(5600, "");

        #endregion

        #region CONFIG

        // CODES: -80030
        public static readonly DracoonApiCode SERVER_SMS_IS_DISABLED = new DracoonApiCode(5800, "SMS sending is disabled.");
        // CODES: -90090
        public static readonly DracoonApiCode SERVER_SMS_COULD_NOT_BE_SENT = new DracoonApiCode(5801, "SMS could not be sent.");
        // CODES: -90033
        public static readonly DracoonApiCode SERVER_S3_IS_ENFORCED = new DracoonApiCode(5802, "S3 direct upload is enforced.");

        #endregion

        #endregion

        /// <summary>
        /// The error message.
        /// </summary>
        public string Text {
            get;
        }

        /// <summary>
        /// The error code.
        /// </summary>
        public int Code {
            get;
        }

        internal DracoonApiCode(int code, string text) {
            Code = code;
            Text = text;
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiCode"]/ToString/*'/>
        public override string ToString() {
            return Code + " " + Text;
        }

        /// <summary>
        /// Checks if the error is a authorization error.
        /// </summary>
        /// <returns><c>true</c> if error is a authorization error; <c>false</c> otherwise</returns>
        public bool IsAuthError() {
            return Code >= 1000 && Code < 2000;
        }

        /// <summary>
        /// Checks if the error is a precondition error.
        /// </summary>
        /// <returns><c>true</c> if error is a precondition error; <c>false</c> otherwise</returns>
        public bool IsPreconditionError() {
            return Code >= 2000 && Code < 3000;
        }

        /// <summary>
        /// Checks if the error is a validation error.
        /// </summary>
        /// <returns><c>true</c> if error is a validation error; <c>false</c> otherwise</returns>
        public bool IsValidationError() {
            return Code >= 3000 && Code < 4000;
        }

        /// <summary>
        /// Checks if the error is a permission error.
        /// </summary>
        /// <returns><c>true</c> if error is a permission error; <c>false</c> otherwise</returns>
        public bool IsPermissionError() {
            return Code >= 4000 && Code < 5000;
        }

        /// <summary>
        /// Checks if the error is a server error.
        /// </summary>
        /// <returns><c>true</c> if error is a server error; <c>false</c> otherwise</returns>
        public bool IsServerError() {
            return Code >= 5000 && Code < 6000;
        }

        public bool Equals(DracoonApiCode other) {
            return string.Equals(Text, other.Text) && Code == other.Code;
        }

        public override bool Equals(object obj) {
            if (obj is null) {
                return false;
            }

            if (ReferenceEquals(this, obj)) {
                return true;
            }

            if (obj.GetType() != GetType()) {
                return false;
            }

            return Equals((DracoonApiCode) obj);
        }
    }
}
