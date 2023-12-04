
using System;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Collection of DRACOON API error codes.
    /// </summary>
    public sealed class DracoonApiCode : IEquatable<DracoonApiCode> {

        /// <summary>
        /// The server api is not compatible with this version of the SDK
        /// </summary>
        public static readonly DracoonApiCode API_VERSION_NOT_SUPPORTED = new DracoonApiCode(0, "Server API versions < " + SdkInternal.ApiConfig.MinimumApiVersion + " are not supported.");

        #region Error codes '1000' --> AUTH
        /// <summary>
        /// An authentication error occurred.
        /// </summary>
        public static readonly DracoonApiCode AUTH_UNKNOWN_ERROR = new DracoonApiCode(1000, "An authentication error occurred.");

        /// <summary>
        /// OAuth client is unknown.
        /// 
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_UNKNOWN = new DracoonApiCode(1100, "OAuth client is unknown.");

        /// <summary>
        /// OAuth client is unauthorized.
        ///
        /// Api-Error-Codes or contexts: PostOAuthToken, PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_UNAUTHORIZED = new DracoonApiCode(1101, "OAuth client is unauthorized.");

        /// <summary>
        /// OAuth grant type is not allowed.
        ///
        /// Api-Error-Codes or contexts: PostOAuthToken, PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_GRANT_TYPE_NOT_ALLOWED = new DracoonApiCode(1102, "OAuth grant type is not allowed.");

        /// <summary>
        /// OAuth authorization request is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_REQUEST_INVALID = new DracoonApiCode(1103, "OAuth authorization request is invalid.");

        /// <summary>
        /// OAuth scope is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_SCOPE_INVALID = new DracoonApiCode(1104, "OAuth scope is invalid.");

        /// <summary>
        /// OAuth access was denied.
        ///
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_AUTHORIZATION_ACCESS_DENIED = new DracoonApiCode(1105, "OAuth access was denied.");

        /// <summary>
        /// OAuth token request is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthToken
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_TOKEN_REQUEST_INVALID = new DracoonApiCode(1106, "OAuth token request is invalid.");

        /// <summary>
        /// OAuth authorization code is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthToken
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_TOKEN_CODE_INVALID = new DracoonApiCode(1107, "OAuth authorization code is invalid.");

        /// <summary>
        /// OAuth token refresh request is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_REFRESH_REQUEST_INVALID = new DracoonApiCode(1108, "OAuth token refresh request is invalid.");

        /// <summary>
        /// OAuth refresh token is invalid.
        ///
        /// Api-Error-Codes or contexts: PostOAuthRefresh
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_REFRESH_TOKEN_INVALID = new DracoonApiCode(1109, "OAuth refresh token is invalid.");

        /// <summary>
        /// OAuth client has no permission to execute the action.
        ///
        /// Api-Error-Codes or contexts: -10006
        /// </summary>
        public static readonly DracoonApiCode AUTH_OAUTH_CLIENT_NO_PERMISSION = new DracoonApiCode(1150, "OAuth client has no permission to execute the action.");

        /// <summary>
        /// Unauthorized.
        ///
        /// Api-Error-Codes or contexts: -10012, -20502
        /// </summary>
        public static readonly DracoonApiCode AUTH_UNAUTHORIZED = new DracoonApiCode(1200, "Unauthorized.");

        /// <summary>
        /// User is temporary locked.
        ///
        /// Api-Error-Codes or contexts: -10005
        /// </summary>
        public static readonly DracoonApiCode AUTH_USER_TEMPORARY_LOCKED = new DracoonApiCode(1300, "User is temporary locked.");

        /// <summary>
        /// User is locked.
        ///
        /// Api-Error-Codes or contexts: -10003, -10007
        /// </summary>
        public static readonly DracoonApiCode AUTH_USER_LOCKED = new DracoonApiCode(1301, "User is locked.");

        /// <summary>
        /// User is expired.
        ///
        /// Api-Error-Codes or contexts: -10004
        /// </summary>
        public static readonly DracoonApiCode AUTH_USER_EXPIRED = new DracoonApiCode(1302, "User is expired.");

        #endregion

        #region Error codes '2000' --> PRECONDITION

        /// <summary>
        /// A precondition is not fulfilled.
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_UNKNOWN_ERROR = new DracoonApiCode(2000, "A precondition is not fulfilled.");

        /// <summary>
        /// User must accept EULA.
        ///
        /// Api-Error-Codes or contexts: -10103
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_MUST_ACCEPT_EULA = new DracoonApiCode(2101, "User must accept EULA.");

        /// <summary>
        /// User must change his user name.
        ///
        /// Api-Error-Codes or contexts: -10106
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_MUST_CHANGE_USER_NAME = new DracoonApiCode(2102, "User must change his user name.");

        /// <summary>
        /// User must change his password.
        ///
        /// Api-Error-Codes or contexts: -10104
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_MUST_CHANGE_PASSWORD = new DracoonApiCode(2103, "User must change his password.");

        /// <summary>
        /// S3 storage is disabled.
        ///
        /// Api-Error-Codes or contexts: -90030
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_S3_DISABLED = new DracoonApiCode(2104, "S3 storage is disabled.");

        /// <summary>
        /// You are not allowed to call this method because further payment is required.
        ///
        /// Api-Error-Codes or contexts:
        /// </summary>
        public static readonly DracoonApiCode PRECONDITION_PAYMENT_REQUIRED = new DracoonApiCode(2105, "You are not allowed to call this method because further payment is required.");

        #endregion

        #region Error codes '3000' --> VALIDATION

        #region GENERAL

        /// <summary>
        /// The server request was invalid.
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_UNKNOWN_ERROR = new DracoonApiCode(3000, "The server request was invalid.");

        /// <summary>
        /// Mandatory fields cannot be empty.
        ///
        /// Api-Error-Codes or contexts: -80000
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_CANNOT_BE_EMPTY = new DracoonApiCode(3001, "Mandatory fields cannot be empty.");

        /// <summary>
        /// Field value must be zero or positive.
        ///
        /// Api-Error-Codes or contexts: -80003
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_ZERO_POSITIVE = new DracoonApiCode(3002, "Field value must be zero or positive.");

        /// <summary>
        /// Field value must be positive.
        ///
        /// Api-Error-Codes or contexts: -80001
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_POSITIVE = new DracoonApiCode(3003, "Field value must be positive.");

        /// <summary>
        /// Maximum allowed field length is exceeded.
        ///
        /// Api-Error-Codes or contexts: -80007
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_MAX_LENGTH_EXCEEDED = new DracoonApiCode(3004, "Maximum allowed field length is exceeded.");

        /// <summary>
        /// Field value must be between 0 and 10.
        ///
        /// Api-Error-Codes or contexts: -80035
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_0_10 = new DracoonApiCode(3005, "Field value must be between 0 and 10.");

        /// <summary>
        /// Field value must be between 0 and 9999.
        ///
        /// Api-Error-Codes or contexts: -80018
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_0_9999 = new DracoonApiCode(3006, "Field value must be between 0 and 9999.");

        /// <summary>
        /// Field value must be between 1 and 9999.
        ///
        /// Api-Error-Codes or contexts: -80019
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FIELD_NOT_BETWEEN_1_9999 = new DracoonApiCode(3007, "Field value must be between 1 and 9999.");

        /// <summary>
        /// Invalid offset or limit.
        ///
        /// Api-Error-Codes or contexts: -80019
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_INVALID_OFFSET_OR_LIMIT = new DracoonApiCode(3008, "Invalid offset or limit.");

        /// <summary>
        /// Invalid characters contained.
        ///
        /// Api-Error-Codes or contexts: -80023
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_INVALID_CHARACTERS_CONTAINED = new DracoonApiCode(3009, "Invalid characters contained.");

        #endregion
        #region NODES

        /// <summary>
        /// File cannot be target of a copy or move operation.
        ///
        /// Api-Error-Codes or contexts: -41053
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FILE_CANNOT_BE_TARGET = new DracoonApiCode(3100, "File cannot be target of a copy or move operation.");

        /// <summary>
        /// Bad file name.
        ///
        /// Api-Error-Codes or contexts: -40755
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_BAD_FILE_NAME = new DracoonApiCode(3101, "Bad file name.");

        /// <summary>
        /// Expiration date is in past.
        ///
        /// Api-Error-Codes or contexts: -80006
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_EXPIRATION_DATE_IN_PAST = new DracoonApiCode(3102, "Expiration date is in past.");

        /// <summary>
        /// The year is too far in the future. Max year is limited to 9999.
        ///
        /// Api-Error-Codes or contexts: -80008, -80012
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_EXPIRATION_DATE_TOO_LATE = new DracoonApiCode(3103, "The year is too far in the future. Max year is limited to 9999.");

        /// <summary>
        /// Node exists already.
        ///
        /// Api-Error-Codes or contexts: -41001
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_NODE_ALREADY_EXISTS = new DracoonApiCode(3104, "Node exists already.");

        /// <summary>
        /// A room with the same name already exists.
        ///
        /// Api-Error-Codes or contexts: PutRoom, PostRoom
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_ALREADY_EXISTS = new DracoonApiCode(3105, "A room with the same name already exists.");

        /// <summary>
        /// A folder with the same name already exists.
        ///
        /// Api-Error-Codes or contexts: PutFolder, PostFolder
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FOLDER_ALREADY_EXISTS = new DracoonApiCode(3106, "A folder with the same name already exists.");

        /// <summary>
        /// A file with the same name already exists.
        ///
        /// Api-Error-Codes or contexts: PutFile
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_FILE_ALREADY_EXISTS = new DracoonApiCode(3107, "A file with the same name already exists.");

        /// <summary>
        /// Folders/files must be in same parent.
        ///
        /// Api-Error-Codes or contexts: -41054
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_NODES_NOT_IN_SAME_PARENT = new DracoonApiCode(3108, "Folders/files must be in same parent.");

        /// <summary>
        /// Node cannot be copied to its own place without renaming.
        ///
        /// Api-Error-Codes or contexts: -41302, -41303
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_NODE_TO_OWN_PLACE_WITHOUT_RENAME = new DracoonApiCode(3109, "Node cannot be copied to its own place without renaming.");

        /// <summary>
        /// Node cannot be moved to its own place.
        ///
        /// Api-Error-Codes or contexts: -41302
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_NODE_TO_OWN_PLACE = new DracoonApiCode(3110, "Node cannot be moved to its own place.");

        /// <summary>
        /// A room or folder cannot be overwritten.
        ///
        /// Api-Error-Codes or contexts: -40010
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_FOLDER_CAN_NOT_BE_OVERWRITTEN = new DracoonApiCode(3111, "A room or folder cannot be overwritten.");

        /// <summary>
        /// Node cannot be copied into its child node.
        ///
        /// Api-Error-Codes or contexts: -41304
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_TO_CHILD = new DracoonApiCode(3112, "Node cannot be copied into its child node.");

        /// <summary>
        /// Node cannot be moved into its child node.
        ///
        /// Api-Error-Codes or contexts: -41304
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_TO_CHILD = new DracoonApiCode(3113, "Node cannot be moved into its child node.");

        /// <summary>
        /// Rooms cannot be copied.
        ///
        /// Api-Error-Codes or contexts: -41052
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_COPY_ROOM = new DracoonApiCode(3114, "Rooms cannot be copied.");

        /// <summary>
        /// Rooms cannot be moved.
        ///
        /// Api-Error-Codes or contexts: -41052
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_CANNOT_MOVE_ROOM = new DracoonApiCode(3115, "Rooms cannot be moved.");

        /// <summary>
        /// Path is too long.
        ///
        /// Api-Error-Codes or contexts: -41200
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_PATH_TOO_LONG = new DracoonApiCode(3116, "Path is too long.");

        /// <summary>
        /// Node is not marked as favorite.
        ///
        /// Api-Error-Codes or contexts: -41301
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_NODE_IS_NO_FAVORITE = new DracoonApiCode(3117, "Node is not marked as favorite.");

        /// <summary>
        /// Room not encrypted.
        ///
        /// Api-Error-Codes or contexts: -40001
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_NOT_ENCRYPTED = new DracoonApiCode(3118, "Room not encrypted.");

        /// <summary>
        /// Encrypted files cannot be copied or moved to an unencrypted room.
        ///
        /// Api-Error-Codes or contexts: -40001
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_SOURCE_ROOM_ENCRYPTED = new DracoonApiCode(3119, "Encrypted files cannot be copied or moved to an unencrypted room.");

        /// <summary>
        /// Not encrypted files cannot be copied or moved to an encrypted room.
        ///
        /// Api-Error-Codes or contexts: -40001
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_TARGET_ROOM_ENCRYPTED = new DracoonApiCode(3120, "Not encrypted files cannot be copied or moved to an encrypted room.");

        /// <summary>
        /// Room is encrypted.
        ///
        /// Api-Error-Codes or contexts: -40002
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_ENCRYPTED = new DracoonApiCode(3121, "Room is encrypted.");

        /// <summary>
        /// Room with files cannot be unencrypted.
        ///
        /// Api-Error-Codes or contexts: -40003
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_UNENCRYPTED_WITH_FILES = new DracoonApiCode(3122, "Room with files cannot be unencrypted.");

        /// <summary>
        /// Room with files cannot be encrypted.
        ///
        /// Api-Error-Codes or contexts: -40008
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_FILES = new DracoonApiCode(3123, "Room with files cannot be encrypted.");

        /// <summary>
        /// Only one room emergency password (rescue key) is allowed.
        ///
        /// Api-Error-Codes or contexts: -40004
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_STILL_HAS_RESCUE_KEY = new DracoonApiCode(3124, "Only one room emergency password (rescue key) is allowed.");

        /// <summary>
        /// Room with not empty recycle bin cannot be encrypted.
        ///
        /// Api-Error-Codes or contexts: -40012
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_ENCRYPTED_WITH_RECYCLEBIN = new DracoonApiCode(3125, "Room with not empty recycle bin cannot be encrypted.");

        /// <summary>
        /// Room with not empty recycle bin cannot be decrypted.
        ///
        /// Api-Error-Codes or contexts: -40018
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ROOM_CANNOT_DECRYPTED_WITH_RECYCLEBIN = new DracoonApiCode(3126, "Room with not empty recycle bin cannot be decrypted.");

        /// <summary>
        /// Encrypted files cannot be restored inside another than its original room.
        ///
        /// Api-Error-Codes or contexts: -40013
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_ENCRYPTED_FILE_CAN_ONLY_RESTOREED_IN_ORIGINAL_ROOM = new DracoonApiCode(3127, "Encrypted files cannot be restored inside another than its original room.");

        /// <summary>
        /// Keep share links is only allowed with resolution strategy 'overwrite'.
        ///
        /// Api-Error-Codes or contexts: -80034
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_KEEPSHARELINKS_ONLY_WITH_OVERWRITE = new DracoonApiCode(3128, "Keep share links is only allowed with resolution strategy 'overwrite'.");

        /// <summary>
        /// Invalid Etag(s).
        ///
        /// Api-Error-Codes or contexts: -80045
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_INVALID_ETAG = new DracoonApiCode(3129, "Invalid Etag(s).");


        /// <summary>
        /// Node is not a file.
        ///
        /// Api-Error-Codes or contexts: -41002
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_NODE_NOT_A_FILE = new DracoonApiCode(3130, "Node is not a file.");

        #endregion
        #region SHARES

        /// <summary>
        /// A download share cannot be created on a encrypted room or folder.
        ///
        /// Api-Error-Codes or contexts: -50004
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_DL_SHARE_CANNOT_CREATE_ON_ENCRYPTED_ROOM_FOLDER = new DracoonApiCode(3200, "A download share cannot be created on a encrypted room or folder.");

        /// <summary>
        /// Upload share name already exists.
        ///
        /// Api-Error-Codes or contexts: PostCreateUploadShare
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_UL_SHARE_NAME_ALREADY_EXISTS = new DracoonApiCode(3201, "Upload share name already exists.");

        #endregion
        #region CUSTOMER

        /// <summary>
        /// Customer error.
        /// </summary>
        public static readonly DracoonApiCode PLACEHOLDER_CUSTOMER = new DracoonApiCode(3400, "");

        #endregion
        #region USERS

        /// <summary>
        /// Users error.
        /// </summary>
        public static readonly DracoonApiCode PLACEHOLDER_USERS = new DracoonApiCode(3500, "");

        /// <summary>
        /// User has no encryption key pair.
        ///
        /// Api-Error-Codes or contexts: -70020
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_USER_HAS_NO_KEY_PAIR = new DracoonApiCode(3550, "User has no encryption key pair.");

        /// <summary>
        /// Invalid encryption key pair.
        ///
        /// Api-Error-Codes or contexts: -70022, -70023
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_USER_KEY_PAIR_INVALID = new DracoonApiCode(3551, "Invalid encryption key pair.");

        /// <summary>
        /// User has no encryption file key.
        ///
        /// Api-Error-Codes or contexts: -70761
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_USER_HAS_NO_FILE_KEY = new DracoonApiCode(3552, "User has no encryption file key.");

        #endregion
        #region GROUPS

        /// <summary>
        /// Groups error.
        /// </summary>
        public static readonly DracoonApiCode PLACEHOLDER_GROUPS = new DracoonApiCode(3600, "");

        #endregion
        #region Policies

        /// <summary>
        /// Policies error.
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_POLICY_VIOLATION = new DracoonApiCode(3750, "A policy is violated.");

        #endregion
        #region OTHERS

        /// <summary>
        /// Password is not secure.
        ///
        /// Api-Error-Codes or contexts: -10002
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_PASSWORT_NOT_SECURE = new DracoonApiCode(3800, "Password is not secure.");

        /// <summary>
        /// Invalid email address.
        ///
        /// Api-Error-Codes or contexts: -80009
        /// </summary>
        public static readonly DracoonApiCode VALIDATION_INVALID_EMAIL_ADDRESS = new DracoonApiCode(3801, "Invalid email address.");

        #endregion

        #endregion

        #region Error codes '4000' --> PERMISSION

        /// <summary>
        /// User has no permissions to execute the action in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_UNKNOWN_ERROR = new DracoonApiCode(4000, "User has no permissions to execute the action in this room.");

        /// <summary>
        /// User has no permission to manage this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_MANAGE_ERROR = new DracoonApiCode(4100, "User has no permission to manage this room.");

        /// <summary>
        /// User has no permission to read nodes.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_READ_ERROR = new DracoonApiCode(4101, "User has no permission to read nodes.");

        /// <summary>
        /// User has no permission to create nodes.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_CREATE_ERROR = new DracoonApiCode(4102, "User has no permission to create nodes.");

        /// <summary>
        /// User has no permission to change nodes.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_UPDATE_ERROR = new DracoonApiCode(4103, "User has no permission to change nodes.");

        /// <summary>
        /// User has no permission to delete nodes.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_DELETE_ERROR = new DracoonApiCode(4104, "User has no permission to delete nodes.");

        /// <summary>
        /// User has no permission to manage download shares in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_MANAGE_DL_SHARES_ERROR = new DracoonApiCode(4105, "User has no permission to manage download shares in this room.");

        /// <summary>
        /// User has no permission to manage upload shares in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_MANAGE_UL_SHARES_ERROR = new DracoonApiCode(4106, "User has no permission to manage upload shares in this room.");

        /// <summary>
        /// User has no permission to read recycle bin in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_READ_RECYCLE_BIN_ERROR = new DracoonApiCode(4107, "User has no permission to read recycle bin in this room.");

        /// <summary>
        /// User has no permission to restore recycle bin items in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_RESTORE_RECYCLE_BIN_ERROR = new DracoonApiCode(4108, "User has no permission to restore recycle bin items in this room.");

        /// <summary>
        /// User has no permission to delete recycle bin items in this room.
        /// </summary>
        public static readonly DracoonApiCode PERMISSION_DELETE_RECYCLE_BIN_ERROR = new DracoonApiCode(4109, "User has no permission to delete recycle bin items in this room.");

        #endregion

        #region Error codes '5000' --> SERVER

        #region GENERAL

        /// <summary>
        /// A server error occurred.
        /// </summary>
        public static readonly DracoonApiCode SERVER_UNKNOWN_ERROR = new DracoonApiCode(5000, "A server error occurred.");

        /// <summary>
        /// Crypto version not supported.
        /// </summary>
        public static readonly DracoonApiCode SERVER_CRYPTO_VERSION_NOT_SUPPORTED = new DracoonApiCode(5010, "Crypto version not supported.");

        /// <summary>
        /// You sent to many requests in a short time, wait {0} seconds until your next call.
        /// </summary>
        public static readonly DracoonApiCode SERVER_TOO_MANY_REQUESTS = new DracoonApiCode(5011, "You sent to many requests in a short time, wait {0} seconds until your next call.");

        /// <summary>
        /// Detected that the file could be malicious.
        /// </summary>
        public static readonly DracoonApiCode SERVER_MALICIOUS_FILE_DETECTED = new DracoonApiCode(5090, "Detected that the file could be malicious.");

        /// <summary>
        /// Virus scan in progress.
        /// </summary>
        public static readonly DracoonApiCode SERVER_VIRUS_SCAN_IN_PROGRESS = new DracoonApiCode(5091, "Virus scan in progress.");

        #endregion

        #region NODES

        /// <summary>
        /// Requested room/folder/file was not found.
        ///
        /// Api-Error-Codes or contexts: -41000
        /// </summary>
        public static readonly DracoonApiCode SERVER_NODE_NOT_FOUND = new DracoonApiCode(5100, "Requested room/folder/file was not found.");

        /// <summary>
        /// Requested room was not found.
        ///
        /// Api-Error-Codes or contexts: -41000
        /// </summary>
        public static readonly DracoonApiCode SERVER_ROOM_NOT_FOUND = new DracoonApiCode(5101, "Requested room was not found.");

        /// <summary>
        /// Requested folder was not found.
        ///
        /// Api-Error-Codes or contexts: -41000
        /// </summary>
        public static readonly DracoonApiCode SERVER_FOLDER_NOT_FOUND = new DracoonApiCode(5102, "Requested folder was not found.");

        /// <summary>
        /// Requested file was not found.
        ///
        /// Api-Error-Codes or contexts: -40751
        /// </summary>
        public static readonly DracoonApiCode SERVER_FILE_NOT_FOUND = new DracoonApiCode(5103, "Requested file was not found.");

        /// <summary>
        /// Source node not found.
        ///
        /// Api-Error-Codes or contexts: -41050
        /// </summary>
        public static readonly DracoonApiCode SERVER_SOURCE_NODE_NOT_FOUND = new DracoonApiCode(5104, "Source node not found.");

        /// <summary>
        /// Target room or folder was not found.
        ///
        /// Api-Error-Codes or contexts: -41000, -41051
        /// </summary>
        public static readonly DracoonApiCode SERVER_TARGET_NODE_NOT_FOUND = new DracoonApiCode(5105, "Target room or folder was not found.");

        /// <summary>
        /// Target room was not found.
        ///
        /// Api-Error-Codes or contexts: -41000
        /// </summary>
        public static readonly DracoonApiCode SERVER_TARGET_ROOM_NOT_FOUND = new DracoonApiCode(5106, "Target room was not found.");

        /// <summary>
        /// Not enough free storage on the server.
        ///
        /// Api-Error-Codes or contexts: -90201
        /// </summary>
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_STORAGE = new DracoonApiCode(5107, "Not enough free storage on the server.");

        /// <summary>
        /// Not enough quota for the customer.
        ///
        /// Api-Error-Codes or contexts: -90200
        /// </summary>
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_CUSTOMER_QUOTA = new DracoonApiCode(5108, "Not enough quota for the customer.");

        /// <summary>
        /// Not enough quota for the room.
        ///
        /// Api-Error-Codes or contexts: -90200
        /// </summary>
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_ROOM_QUOTA = new DracoonApiCode(5109, "Not enough quota for the room.");

        /// <summary>
        /// Not enough quota for the upload share.
        ///
        /// Api-Error-Codes or contexts: -50504
        /// </summary>
        public static readonly DracoonApiCode SERVER_INSUFFICIENT_UL_SHARE_QUOTA = new DracoonApiCode(5110, "Not enough quota for the upload share.");

        /// <summary>
        /// The restore version id was not found.
        ///
        /// Api-Error-Codes or contexts: -41100
        /// </summary>
        public static readonly DracoonApiCode SERVER_RESTOREVERSION_NOT_FOUND = new DracoonApiCode(5111, "The restore version id was not found.");

        /// <summary>
        /// The upload with the given id was not found.
        ///
        /// Api-Error-Codes or contexts: -20501
        /// </summary>
        public static readonly DracoonApiCode SERVER_UPLOAD_NOT_FOUND = new DracoonApiCode(5112, "The upload with the given id was not found.");

        /// <summary>
        /// Corresponding S3 upload ID not found.
        ///
        /// Api-Error-Codes or contexts: -90034
        /// </summary>
        public static readonly DracoonApiCode SERVER_S3_UPLOAD_ID_NOT_FOUND = new DracoonApiCode(5113, "Corresponding S3 upload ID not found.");

        /// <summary>
        /// Server failed to complete S3 upload.
        /// </summary>
        public static readonly DracoonApiCode SERVER_S3_UPLOAD_COMPLETION_FAILED = new DracoonApiCode(5114, "Server failed to complete S3 upload.");

        /// <summary>
        /// S3 connection failed.
        /// 
        /// Api-Error-Codes or contexts: -90027
        /// </summary>
        public static readonly DracoonApiCode SERVER_S3_CONNECTION_FAILED = new DracoonApiCode(5115, "S3 connection failed.");

        /// <summary>
        /// Malicious file was not found.
        /// 
        /// Api-Error-Codes or contexts: -41150
        /// </summary>
        public static readonly DracoonApiCode SERVER_MALICIOUS_FILE_NOT_FOUND = new DracoonApiCode(5116, "Malicious file not found.");
        #endregion

        #region SHARES

        /// <summary>
        /// Download share could not be found.
        /// 
        /// Api-Error-Codes or contexts: -60000
        /// </summary>
        public static readonly DracoonApiCode SERVER_DL_SHARE_NOT_FOUND = new DracoonApiCode(5200, "Download share could not be found.");

        /// <summary>
        /// Upload share could not be found.
        /// 
        /// Api-Error-Codes or contexts: -20501, -65000
        /// </summary>
        public static readonly DracoonApiCode SERVER_UL_SHARE_NOT_FOUND = new DracoonApiCode(5201, "Upload share could not be found.");

        #endregion

        #region CUSTOMER

        /// <summary>
        /// Server customer error.
        /// </summary>
        public static readonly DracoonApiCode SERVER_PLACEHOLDER_CUSTOMER = new DracoonApiCode(5400, "");

        #endregion

        #region USERS

        /// <summary>
        /// User could not be found.
        /// 
        /// Api-Error-Codes or contexts: -70501
        /// </summary>
        public static readonly DracoonApiCode SERVER_USER_NOT_FOUND = new DracoonApiCode(5500, "User could not be found.");

        /// <summary>
        /// Encryption key pair was not found.
        /// 
        /// Api-Error-Codes or contexts: -70020
        /// </summary>
        public static readonly DracoonApiCode SERVER_USER_KEY_PAIR_NOT_FOUND = new DracoonApiCode(5550, "Encryption key pair was not found.");

        /// <summary>
        /// Encryption key pair was already set.
        /// 
        /// Api-Error-Codes or contexts: -70021
        /// </summary>
        public static readonly DracoonApiCode SERVER_USER_KEY_PAIR_ALREADY_SET = new DracoonApiCode(5551, "Encryption key pair was already set.");

        /// <summary>
        /// Encryption file key could not be found.
        /// 
        /// Api-Error-Codes or contexts: -40761
        /// </summary>
        public static readonly DracoonApiCode SERVER_FILE_KEY_NOT_FOUND = new DracoonApiCode(5552, "Encryption file key could not be found.");

        /// <summary>
        /// Avatar for this user could not be found.
        /// 
        /// Api-Error-Codes or contexts: -70028
        /// </summary>
        public static readonly DracoonApiCode SERVER_USER_AVATAR_NOT_FOUND = new DracoonApiCode(5553, "Avatar for this user could not be found.");

        /// <summary>
        /// Attribute not found.
        /// 
        /// Api-Error-Codes or contexts: -70550
        /// </summary>
        public static readonly DracoonApiCode SERVER_ATTRIBUTE_NOT_FOUND = new DracoonApiCode(5554, "Attribute not found.");

        #endregion

        #region GROUPS

        /// <summary>
        /// Server groups error.
        /// </summary>
        public static readonly DracoonApiCode SERVER_PLACEHOLDER_GROUPS = new DracoonApiCode(5600, "");

        #endregion

        #region CONFIG

        /// <summary>
        /// SMS sending is disabled.
        /// 
        /// Api-Error-Codes or contexts: -80030
        /// </summary>
        public static readonly DracoonApiCode SERVER_SMS_IS_DISABLED = new DracoonApiCode(5800, "SMS sending is disabled.");

        /// <summary>
        /// SMS could not be sent.
        /// 
        /// Api-Error-Codes or contexts: -90090
        /// </summary>
        public static readonly DracoonApiCode SERVER_SMS_COULD_NOT_BE_SENT = new DracoonApiCode(5801, "SMS could not be sent.");

        /// <summary>
        /// S3 direct upload is enforced.
        /// 
        /// Api-Error-Codes or contexts: -90033
        /// </summary>
        public static readonly DracoonApiCode SERVER_S3_IS_ENFORCED = new DracoonApiCode(5802, "S3 direct upload is enforced.");

        #endregion

        #endregion

        /// <summary>
        ///     The error message.
        /// </summary>
        public string Text {
            get;
        }

        /// <summary>
        ///     The error code.
        /// </summary>
        public int Code {
            get;
        }

        internal DracoonApiCode(int code, string text) {
            Code = code;
            Text = text;
        }

        /// <summary>
        ///     Creates a string which contains the error number and the error message.
        /// </summary>
        /// <returns>
        ///     A string with: Code + " " + Text
        /// </returns>
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

        /// <inheritdoc />
        public bool Equals(DracoonApiCode other) {
            return string.Equals(Text, other.Text) && Code == other.Code;
        }

        /// <inheritdoc />
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

            return Equals((DracoonApiCode)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode() {
            return Text.GetHashCode() ^ Code.GetHashCode();
        }
    }
}
