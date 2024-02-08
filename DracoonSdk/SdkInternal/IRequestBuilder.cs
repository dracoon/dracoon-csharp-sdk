using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.Sort;
using RestSharp;
using System.Net;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IRequestBuilder {
        #region Public

        RestRequest GetServerVersion();

        RestRequest GetServerTime();

        #endregion

        #region User

        RestRequest GetUserAccount();

        RestRequest GetCustomerAccount();

        RestRequest GetUserKeyPair(string algorithm);

        RestRequest GetUserKeyPairs();

        RestRequest GetAuthenticatedPing();

        RestRequest GetAvatar();

        RestRequest SetUserKeyPair(ApiUserKeyPair apiUserKeyPair);

        RestRequest DeleteUserKeyPair(string algorithm);

        RestRequest GetUserProfileAttributes();

        RestRequest GetUserProfileAttribute(string attributeKey);

        RestRequest PutUserProfileAttributes(ApiAddOrUpdateAttributeRequest addOrUpdateParam);

        RestRequest DeleteUserProfileAttributes(string attributeKey);

        RestRequest DeleteAvatar();

        WebClient ProvideAvatarDownloadWebClient();

        WebClient ProvideAvatarUploadWebClient(string formDataBoundary);

        #endregion

        #region Nodes

        RestRequest GetNodes(long parentNodeId, long? offset = null, long? limit = null, GetNodesFilter filter = null);

        RestRequest GetNode(long nodeId);

        RestRequest GetFileKey(long nodeId);

        RestRequest GetSearchNodes(long parentNodeId, string searchString, long offset, long limit, int depthLevel = -1,
            SearchNodesFilter filter = null, SearchNodesSort sort = null);

        RestRequest GetMissingFileKeys(long? fileId, int limit = 10, int offset = 0);

        RestRequest GetRecycleBin(long parentRoomId, long? offset = null, long? limit = null);

        RestRequest GetPreviousVersions(long nodeId, string type, string nodeName, long? offset = null, long? limit = null);

        RestRequest GetPreviousVersion(long previousNodeId);

        RestRequest GetS3Status(string uploadId);

        RestRequest PostRoom(ApiCreateRoomRequest roomParams);

        RestRequest PostFolder(ApiCreateFolderRequest folderParams);

        RestRequest PostFileDownload(long fileId);

        RestRequest PostCreateFileUpload(ApiCreateFileUpload uploadParams);

        RestRequest PostGetS3Urls(string uploadId, ApiGetS3Urls s3UrlParams);

        RestRequest PostCopyNodes(long targetNodeId, ApiCopyNodesRequest copyParams);

        RestRequest PostMoveNodes(long targetNodeId, ApiMoveNodesRequest moveParams);

        RestRequest PostMissingFileKeys(ApiSetUserFileKeysRequest fileKeyParams);

        RestRequest PostFavorite(long nodeId);

        RestRequest PostRestoreNodeVersion(ApiRestorePreviousVersionsRequest restoreParams);

        RestRequest PutRoom(long roomId, ApiUpdateRoomRequest roomParams);

        RestRequest PutEnableRoomEncryption(long roomId, ApiEnableRoomEncryptionRequest encryptionParams);

        RestRequest PutFolder(long folderId, ApiUpdateFolderRequest folderParams);

        RestRequest PutFile(long fileId, ApiUpdateFileRequest fileParams);

        RestRequest PutCompleteFileUpload(string uploadPath, ApiCompleteFileUpload completeParams);

        RestRequest PutCompleteS3FileUpload(string uploadId, ApiCompleteFileUpload completeParams);

        RestRequest DeleteNodes(ApiDeleteNodesRequest deleteParams);

        RestRequest DeleteFavorite(long nodeId);

        RestRequest DeleteRecycleBin(long parentRoomId);

        RestRequest DeletePreviousVersion(ApiDeletePreviousVersionsRequest deleteParams);

        WebClient ProvideChunkDownloadWebClient(long offset, long count);

        WebClient ProvideChunkUploadWebClient(int chunkLength, long offset, string formDataBoundary, string totalFileSize);

        WebClient ProvideS3ChunkUploadWebClient();

        #endregion

        #region Share

        RestRequest GetDownloadShares(long? offset, long? limit, GetDownloadSharesFilter filter = null, SharesSort sort = null);

        RestRequest GetUploadShares(long? offset, long? limit, GetUploadSharesFilter filter = null, SharesSort sort = null);

        RestRequest PostCreateDownloadShare(ApiCreateDownloadShareRequest downloadShareParams);

        RestRequest PostCreateUploadShare(ApiCreateUploadShareRequest uploadShareParams);

        RestRequest DeleteDownloadShare(long shareId);

        RestRequest DeleteUploadShare(long shareId);

        #endregion

        #region OAuth

        RestRequest PostOAuthToken(string clientId, string clientSecret, string grantType, string code);

        RestRequest PostOAuthRefresh(string clientId, string clientSecret, string grantType, string refreshToken);

        #endregion

        #region Config

        RestRequest GetGeneralSettings();

        RestRequest GetInfrastructureSettings();

        RestRequest GetDefaultsSettings();

        RestRequest GetPasswordPolicies();

        RestRequest GetAlgorithms();

        RestRequest GetClassificationPolicies();

        #endregion

        #region Resources

        RestRequest GetUserAvatar(long userId, string avatarUuid);

        #endregion
    }
}