using System;
using System.Text;

namespace Dracoon.Sdk.SdkInternal {
    internal class ApiConfig {
        internal const string MinimumApiVersion = "4.23.0";
        internal const string ApiPrefix = "api/v4";
        internal const string AuthorizationHeader = "Authorization";
        // mediaserver/image/{mediaToken}/{width}x{height}
        internal const string MediaTokenTemplate = "mediaserver/image/{0}/{1}x{2}";


        // Character set based on https://wiki.dracoon.com/display/DevOrga/Password+Policies
        internal static readonly char[] UPPERCASE_SET = {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
        };

        internal static readonly char[] LOWERCASE_SET = {
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
        };

        internal static readonly char[] NUMERIC_SET = {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };

        internal static readonly char[] SPECIAL_SET = {
            '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '=', '?', '@', '[', '\\', ']', '^', '_', '{', '|',
            '}', '~'
        };

        internal static readonly Encoding ENCODING = Encoding.UTF8;

        #region Crypto-Algorithm

        internal const string ApiVersionMin_Algorithm_UserKeyPair_RSA4096 = "4.24.0";

        #endregion

        #region Public-Endpoint

        #region GET

        internal const string ApiGetServerVersion = ApiPrefix + "/public/software/version";
        internal const string ApiGetServerTime = ApiPrefix + "/public/time";

        #endregion

        #endregion

        #region User-Endpoint

        #region GET

        internal const string ApiGetUserAccount = ApiPrefix + "/user/account";
        internal const string ApiGetCustomerAccount = ApiPrefix + "/user/account/customer";
        internal const string ApiGetUserKeyPair = ApiPrefix + "/user/account/keypair";
        internal const string ApiGetUserKeyPairs = ApiPrefix + "/user/account/keypairs";
        internal const string ApiGetAuthenticatedPing = ApiPrefix + "/user/ping";
        internal const string ApiGetAvatar = ApiPrefix + "/user/account/avatar";
        internal const string ApiGetUserProfileAttributes = ApiPrefix + "/user/profileAttributes";

        #endregion

        #region POST

        internal const string ApiPostUserKeyPair = ApiPrefix + "/user/account/keypair";
        internal const string ApiPostAvatar = ApiPrefix + "/user/account/avatar";

        #endregion

        #region PUT

        internal const string ApiPutUserProfileAttributes = ApiPrefix + "/user/profileAttributes";

        #endregion

        #region DELETE

        internal const string ApiDeleteUserKeyPair = ApiPrefix + "/user/account/keypair";
        internal const string ApiDeleteUserProfileAttributes = ApiPrefix + "/user/profileAttributes/{key}";
        internal const string ApiDeleteAvatar = ApiPrefix + "/user/account/avatar";

        #endregion

        #region Minimum version requirements

        internal const string ApiGetUserKeyPairsMinimumVersion = "4.24.0";

        #endregion

        #endregion

        #region Nodes-Endpoint

        #region GET

        internal const string ApiGetChildNodes = ApiPrefix + "/nodes";
        internal const string ApiGetNode = ApiPrefix + "/nodes/{nodeId}";
        internal const string ApiGetFileKey = ApiPrefix + "/nodes/files/{fileId}/user_file_key";
        internal const string ApiGetSearchNodes = ApiPrefix + "/nodes/search";
        internal const string ApiGetMissingFileKeys = ApiPrefix + "/nodes/missingFileKeys";
        internal const string ApiGetRecycleBin = ApiPrefix + "/nodes/{roomId}/deleted_nodes";
        internal const string ApiGetPreviousVersions = ApiPrefix + "/nodes/{nodeId}/deleted_nodes/versions";
        internal const string ApiGetPreviousVersion = ApiPrefix + "/nodes/deleted_nodes/{previoudNodeId}";
        internal const string ApiGetS3Status = ApiPrefix + "/nodes/files/uploads/{uploadId}";

        #endregion

        #region POST

        internal const string ApiPostRoom = ApiPrefix + "/nodes/rooms";
        internal const string ApiPostFolder = ApiPrefix + "/nodes/folders";
        internal const string ApiPostCreateFileDownload = ApiPrefix + "/nodes/files/{fileId}/downloads";
        internal const string ApiPostCreateFileUpload = ApiPrefix + "/nodes/files/uploads";
        internal const string ApiPostGetS3Urls = ApiPrefix + "/nodes/files/uploads/{uploadId}/s3_urls";
        internal const string ApiPostCopyNodes = ApiPrefix + "/nodes/{nodeId}/copy_to";
        internal const string ApiPostMoveNodes = ApiPrefix + "/nodes/{nodeId}/move_to";
        internal const string ApiPostMissingFileKeys = ApiPrefix + "/nodes/files/keys";
        internal const string ApiPostFavorite = ApiPrefix + "/nodes/{nodeId}/favorite";
        internal const string ApiPostRestoreNodeVersion = ApiPrefix + "/nodes/deleted_nodes/actions/restore";

        #region Minimum version requirements

        internal const string ApiS3DirectUploadPossible = "4.15.0";

        #endregion

        #endregion

        #region PUT

        internal const string ApiPutRoom = ApiPrefix + "/nodes/rooms/{roomId}";
        internal const string ApiPutFolder = ApiPrefix + "/nodes/folders/{folderId}";
        internal const string ApiPutFileUpdate = ApiPrefix + "/nodes/files/{fileId}";
        internal const string ApiPutEnableRoomEncryption = ApiPrefix + "/nodes/rooms/{roomId}/encrypt";
        internal const string ApiPutCompleteS3Upload = ApiPrefix + "/nodes/files/uploads/{uploadId}/s3";

        #endregion

        #region DELETE

        internal const string ApiDeleteNodes = ApiPrefix + "/nodes";
        internal const string ApiDeleteFavorite = ApiPrefix + "/nodes/{nodeId}/favorite";
        internal const string ApiDeleteRecycleBin = ApiPrefix + "/nodes/{roomId}/deleted_nodes";
        internal const string ApiDeletePreviousVersions = ApiPrefix + "/nodes/deleted_nodes";

        #endregion

        #endregion

        #region Shares-Endpoint

        #region GET

        internal const string ApiGetDownloadShares = ApiPrefix + "/shares/downloads";
        internal const string ApiGetUploadShares = ApiPrefix + "/shares/uploads";

        #endregion

        #region POST

        internal const string ApiPostCreateDownloadShare = ApiPrefix + "/shares/downloads";
        internal const string ApiPostCreateUploadShare = ApiPrefix + "/shares/uploads";

        #endregion

        #region DELETE

        internal const string ApiDeleteDownloadShare = ApiPrefix + "/shares/downloads/{shareId}";
        internal const string ApiDeleteUploadShare = ApiPrefix + "/shares/uploads/{shareId}";

        #endregion

        #endregion

        #region Config-Endpoint

        #region GET

        internal const string ApiGetGeneralConfig = ApiPrefix + "/config/info/general";
        internal const string ApiGetInfrastructureConfig = ApiPrefix + "/config/info/infrastructure";
        internal const string ApiGetDefaultsConfig = ApiPrefix + "/config/info/defaults";
        internal const string ApiGetPasswordPolicies = ApiPrefix + "/config/info/policies/passwords";
        internal const string ApiGetAlgorithms = ApiPrefix + "/config/info/policies/algorithms";

        #endregion

        #region Minimum version requirements

        internal const string ApiGetAlgorithmsMinimumVersion = "4.24.0";

        #endregion

        #endregion

        #region Uploads-Endpoint

        #region POST

        internal const string ApiPostFileUpload = ApiPrefix + "/uploads";

        #endregion

        #region PUT

        #endregion

        #endregion

        #region Resources-Endpoint

        #region GET

        internal const string ApiResourcesGetAvatar = ApiPrefix + "/resources/users/{userId}/avatar/{uuid}";

        #endregion

        #endregion

        internal static Uri BuildApiUrl(Uri baseUrl, params string[] pathSegments) {
            UriBuilder uriBuilder = new UriBuilder(baseUrl);
            for (int i = 0; i < pathSegments.Length; i++) {
                uriBuilder.Path += i != 0 ? "/" + pathSegments[i] : pathSegments[i];
            }

            return uriBuilder.Uri;
        }
    }
}