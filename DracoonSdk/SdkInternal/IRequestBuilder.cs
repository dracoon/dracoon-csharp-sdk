using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.Sort;
using RestSharp;
using System.Net;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IRequestBuilder {
        #region Public

        IRestRequest GetServerVersion();

        IRestRequest GetServerTime();

        #endregion

        #region User

        IRestRequest GetUserAccount();

        IRestRequest GetCustomerAccount();

        IRestRequest GetUserKeyPair(string algorithm);

        IRestRequest GetUserKeyPairs();

        IRestRequest GetAuthenticatedPing();

        IRestRequest GetAvatar();

        IRestRequest SetUserKeyPair(ApiUserKeyPair apiUserKeyPair);

        IRestRequest DeleteUserKeyPair(string algorithm);

        IRestRequest GetUserProfileAttributes();

        IRestRequest GetUserProfileAttribute(string attributeKey);

        IRestRequest PutUserProfileAttributes(ApiAddOrUpdateAttributeRequest addOrUpdateParam);

        IRestRequest DeleteUserProfileAttributes(string attributeKey);

        IRestRequest DeleteAvatar();

        WebClient ProvideAvatarDownloadWebClient();

        WebClient ProvideAvatarUploadWebClient(string formDataBoundary);

        #endregion

        #region Nodes

        IRestRequest GetNodes(long parentNodeId, long? offset = null, long? limit = null, GetNodesFilter filter = null);

        IRestRequest GetNode(long nodeId);

        IRestRequest GetFileKey(long nodeId);

        IRestRequest GetSearchNodes(long parentNodeId, string searchString, long offset, long limit, int depthLevel = -1,
            SearchNodesFilter filter = null, SearchNodesSort sort = null);

        IRestRequest GetMissingFileKeys(long? fileId, int limit = 10, int offset = 0);

        IRestRequest GetRecycleBin(long parentRoomId, long? offset = null, long? limit = null);

        IRestRequest GetPreviousVersions(long nodeId, string type, string nodeName, long? offset = null, long? limit = null);

        IRestRequest GetPreviousVersion(long previousNodeId);

        IRestRequest GetS3Status(string uploadId);

        IRestRequest PostRoom(ApiCreateRoomRequest roomParams);

        IRestRequest PostFolder(ApiCreateFolderRequest folderParams);

        IRestRequest PostFileDownload(long fileId);

        IRestRequest PostCreateFileUpload(ApiCreateFileUpload uploadParams);

        IRestRequest PostGetS3Urls(string uploadId, ApiGetS3Urls s3UrlParams);

        IRestRequest PostCopyNodes(long targetNodeId, ApiCopyNodesRequest copyParams);

        IRestRequest PostMoveNodes(long targetNodeId, ApiMoveNodesRequest moveParams);

        IRestRequest PostMissingFileKeys(ApiSetUserFileKeysRequest fileKeyParams);

        IRestRequest PostFavorite(long nodeId);

        IRestRequest PostRestoreNodeVersion(ApiRestorePreviousVersionsRequest restoreParams);

        IRestRequest PutRoom(long roomId, ApiUpdateRoomRequest roomParams);

        IRestRequest PutEnableRoomEncryption(long roomId, ApiEnableRoomEncryptionRequest encryptionParams);

        IRestRequest PutFolder(long folderId, ApiUpdateFolderRequest folderParams);

        IRestRequest PutFile(long fileId, ApiUpdateFileRequest fileParams);

        IRestRequest PutCompleteFileUpload(string uploadPath, ApiCompleteFileUpload completeParams);

        IRestRequest PutCompleteS3FileUpload(string uploadId, ApiCompleteFileUpload completeParams);

        IRestRequest DeleteNodes(ApiDeleteNodesRequest deleteParams);

        IRestRequest DeleteFavorite(long nodeId);

        IRestRequest DeleteRecycleBin(long parentRoomId);

        IRestRequest DeletePreviousVersion(ApiDeletePreviousVersionsRequest deleteParams);

        WebClient ProvideChunkDownloadWebClient(long offset, long count);

        WebClient ProvideChunkUploadWebClient(int chunkLength, long offset, string formDataBoundary, string totalFileSize);

        WebClient ProvideS3ChunkUploadWebClient();

        #endregion

        #region Share

        IRestRequest GetDownloadShares(long? offset, long? limit, GetDownloadSharesFilter filter = null, SharesSort sort = null);

        IRestRequest GetUploadShares(long? offset, long? limit, GetUploadSharesFilter filter = null, SharesSort sort = null);

        IRestRequest PostCreateDownloadShare(ApiCreateDownloadShareRequest downloadShareParams);

        IRestRequest PostCreateUploadShare(ApiCreateUploadShareRequest uploadShareParams);

        IRestRequest PostMailDownloadShare(long shareId, ApiMailShareInfoRequest mailParams);

        IRestRequest PostMailUploadShare(long shareId, ApiMailShareInfoRequest mailParams);

        IRestRequest DeleteDownloadShare(long shareId);

        IRestRequest DeleteUploadShare(long shareId);

        #endregion

        #region OAuth

        IRestRequest PostOAuthToken(string clientId, string clientSecret, string grantType, string code);

        IRestRequest PostOAuthRefresh(string clientId, string clientSecret, string grantType, string refreshToken);

        #endregion

        #region Config

        IRestRequest GetGeneralSettings();

        IRestRequest GetInfrastructureSettings();

        IRestRequest GetDefaultsSettings();

        IRestRequest GetPasswordPolicies();

        IRestRequest GetAlgorithms();

        IRestRequest GetClassificationPolicies();

        #endregion

        #region Resources

        IRestRequest GetUserAvatar(long userId, string avatarUuid);

        #endregion
    }
}