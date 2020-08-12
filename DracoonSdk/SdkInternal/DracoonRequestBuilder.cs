using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.Sort;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonRequestBuilder : IRequestBuilder {
        private readonly IOAuth _auth;

        internal DracoonRequestBuilder(IOAuth auth) {
            _auth = auth;
        }

        private void SetGeneralRestValues(IRestRequest request, bool requiresAuth, object optionalJsonBody = null) {
            if (requiresAuth) {
                request.AddHeader(ApiConfig.AuthorizationHeader, _auth.BuildAuthString());
            }

            if (optionalJsonBody != null) {
                request.AddParameter("application/json", JsonConvert.SerializeObject(optionalJsonBody), ParameterType.RequestBody);
            }

            request.ReadWriteTimeout = DracoonClient.HttpConfig.ReadWriteTimeout;
            request.Timeout = DracoonClient.HttpConfig.ConnectionTimeout;
        }

        private void SetGeneralWebClientValues(DracoonWebClientExtension requestClient) {
            requestClient.Headers.Add(HttpRequestHeader.UserAgent, DracoonClient.HttpConfig.UserAgent);
            requestClient.SetHttpConfigParams(DracoonClient.HttpConfig);
        }

        private void AddFilters<T>(T filter, IRestRequest requestForFilterAdding) where T : DracoonFilter {
            if (filter == null)
                return;
            string filterString = filter.ToString();
            if (string.IsNullOrWhiteSpace(filterString))
                return;
            requestForFilterAdding.AddQueryParameter("filter", filterString);
        }

        private void AddSort<T>(T sort, IRestRequest requestForSortAdding) where T : DracoonSort {
            if (sort == null)
                return;
            string sortString = sort.ToString();
            if (string.IsNullOrWhiteSpace(sortString))
                return;
            requestForSortAdding.AddQueryParameter("sort", sortString);
        }

        #region Public-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetServerVersion() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerVersion, Method.GET);
            SetGeneralRestValues(request, false);
            return request;
        }

        IRestRequest IRequestBuilder.GetServerTime() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerTime, Method.GET);
            SetGeneralRestValues(request, false);
            return request;
        }

        #endregion

        #endregion

        #region User-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetUserAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserAccount, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetCustomerAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetCustomerAccount, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetUserKeyPair(string algorithm) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserKeyPair, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("version", algorithm);
            return request;
        }

        IRestRequest IRequestBuilder.GetAuthenticatedPing() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAuthenticatedPing, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddHeader("Accept", "*/*");
            return request;
        }

        IRestRequest IRequestBuilder.GetAvatar() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAvatar, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetUserProfileAttributes() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserProfileAttributes, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetUserProfileAttribute(string attributeKey) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserProfileAttributes, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("filter", "key:eq:" + attributeKey);
            return request;
        }

        #endregion

        #region POST

        IRestRequest IRequestBuilder.SetUserKeyPair(ApiUserKeyPair apiUserKeyPair) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostUserKeyPair, Method.POST);
            SetGeneralRestValues(request, true, apiUserKeyPair);
            return request;
        }

        #endregion

        #region PUT

        IRestRequest IRequestBuilder.PutUserProfileAttributes(ApiAddOrUpdateAttributeRequest addOrUpdateParam) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutUserProfileAttributes, Method.PUT);
            SetGeneralRestValues(request, true, addOrUpdateParam);
            return request;
        }

        #endregion

        #region DELETE

        IRestRequest IRequestBuilder.DeleteUserKeyPair(string algorithm) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUserKeyPair, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("version", algorithm);
            return request;
        }

        IRestRequest IRequestBuilder.DeleteUserProfileAttributes(string attributeKey) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUserProfileAttributes, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("key", attributeKey);
            return request;
        }

        IRestRequest IRequestBuilder.DeleteAvatar() {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteAvatar, Method.DELETE);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #region HTTP-Request

        WebClient IRequestBuilder.ProvideAvatarDownloadWebClient() {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        WebClient IRequestBuilder.ProvideAvatarUploadWebClient(string formDataBoundary) {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            requestClient.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + formDataBoundary);
            requestClient.Headers.Add(ApiConfig.AuthorizationHeader, _auth.BuildAuthString());
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        #endregion

        #endregion

        #region Nodes-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetNodes(long parentNodeId, long? offset = null, long? limit = null, GetNodesFilter filter = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetChildNodes, Method.GET);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            request.AddQueryParameter("parent_id", parentNodeId.ToString());
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetNode(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetNode, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        IRestRequest IRequestBuilder.GetFileKey(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetFileKey, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", nodeId);
            return request;
        }

        IRestRequest IRequestBuilder.GetSearchNodes(long parentNodeId, string searchString, long offset, long limit, int depthLevel = -1,
            SearchNodesFilter filter = null, SearchNodesSort sort = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetSearchNodes, Method.GET);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            AddSort(sort, request);
            request.AddQueryParameter("search_string", searchString);
            request.AddQueryParameter("parent_id", parentNodeId.ToString());
            request.AddQueryParameter("depth_level", depthLevel.ToString());
            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetMissingFileKeys(long? fileId, int limit = 10, int offset = 0) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetMissingFileKeys, Method.GET);
            SetGeneralRestValues(request, true);
            if (fileId.HasValue) {
                request.AddQueryParameter("fileId", fileId.ToString());
            }

            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetRecycleBin(long parentRoomId, long? offset = null, long? limit = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetRecycleBin, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetPreviousVersions(long nodeId, string type, string nodeName, long? offset = null, long? limit = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPreviousVersions, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            request.AddQueryParameter("type", type);
            request.AddQueryParameter("name", nodeName);
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetPreviousVersion(long previousNodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPreviousVersion, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("previousNodeId", previousNodeId);
            return request;
        }

        IRestRequest IRequestBuilder.GetS3Status(string uploadId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetS3Status, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        #endregion

        #region POST

        IRestRequest IRequestBuilder.PostRoom(ApiCreateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRoom, Method.POST);
            SetGeneralRestValues(request, true, roomParams);
            return request;
        }

        IRestRequest IRequestBuilder.PostFolder(ApiCreateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFolder, Method.POST);
            SetGeneralRestValues(request, true, folderParams);
            return request;
        }

        IRestRequest IRequestBuilder.PostFileDownload(long fileId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileDownload, Method.POST);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        IRestRequest IRequestBuilder.PostCreateFileUpload(ApiCreateFileUpload uploadParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileUpload, Method.POST);
            SetGeneralRestValues(request, true, uploadParams);
            return request;
        }

        IRestRequest IRequestBuilder.PostGetS3Urls(string uploadId, ApiGetS3Urls s3UrlParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostGetS3Urls, Method.POST);
            SetGeneralRestValues(request, true, s3UrlParams);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        IRestRequest IRequestBuilder.PostCopyNodes(long targetNodeId, ApiCopyNodesRequest copyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCopyNodes, Method.POST);
            SetGeneralRestValues(request, true, copyParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        IRestRequest IRequestBuilder.PostMoveNodes(long targetNodeId, ApiMoveNodesRequest moveParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMoveNodes, Method.POST);
            SetGeneralRestValues(request, true, moveParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        IRestRequest IRequestBuilder.PostMissingFileKeys(ApiSetUserFileKeysRequest fileKeyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMissingFileKeys, Method.POST);
            SetGeneralRestValues(request, true, fileKeyParams);
            return request;
        }

        IRestRequest IRequestBuilder.PostFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFavorite, Method.POST);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        IRestRequest IRequestBuilder.PostRestoreNodeVersion(ApiRestorePreviousVersionsRequest restoreParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRestoreNodeVersion, Method.POST);
            SetGeneralRestValues(request, true, restoreParams);
            return request;
        }

        #endregion

        #region PUT

        IRestRequest IRequestBuilder.PutRoom(long roomId, ApiUpdateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutRoom, Method.PUT);
            SetGeneralRestValues(request, true, roomParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        IRestRequest IRequestBuilder.PutEnableRoomEncryption(long roomId, ApiEnableRoomEncryptionRequest encryptionParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutEnableRoomEncryption, Method.PUT);
            SetGeneralRestValues(request, true, encryptionParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        IRestRequest IRequestBuilder.PutFolder(long folderId, ApiUpdateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFolder, Method.PUT);
            SetGeneralRestValues(request, true, folderParams);
            request.AddUrlSegment("folderId", folderId);
            return request;
        }

        IRestRequest IRequestBuilder.PutFile(long fileId, ApiUpdateFileRequest fileParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFileUpdate, Method.PUT);
            SetGeneralRestValues(request, true, fileParams);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        IRestRequest IRequestBuilder.PutCompleteFileUpload(string uploadPath, ApiCompleteFileUpload completeParams) {
            RestRequest request = new RestRequest(uploadPath, Method.PUT);
            SetGeneralRestValues(request, true, completeParams);
            return request;
        }

        IRestRequest IRequestBuilder.PutCompleteS3FileUpload(string uploadId, ApiCompleteFileUpload completeParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutCompleteS3Upload, Method.PUT);
            SetGeneralRestValues(request, true, completeParams);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        #endregion

        #region DELETE

        IRestRequest IRequestBuilder.DeleteNodes(ApiDeleteNodesRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteNodes, Method.DELETE);
            SetGeneralRestValues(request, true, deleteParams);
            return request;
        }

        IRestRequest IRequestBuilder.DeleteFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteFavorite, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        IRestRequest IRequestBuilder.DeleteRecycleBin(long parentRoomId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteRecycleBin, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            return request;
        }

        IRestRequest IRequestBuilder.DeletePreviousVersion(ApiDeletePreviousVersionsRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeletePreviousVersions, Method.DELETE);
            SetGeneralRestValues(request, true, deleteParams);
            return request;
        }

        #endregion

        #region HTTP-Request

        WebClient IRequestBuilder.ProvideChunkDownloadWebClient(long offset, long count) {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension(rangeFrom: offset, rangeTo: (offset + count) - 1);
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        WebClient IRequestBuilder.ProvideChunkUploadWebClient(int chunkLength, long offset, string formDataBoundary, string totalFileSize) {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            requestClient.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + offset + "-" + (offset + chunkLength) + "/" + totalFileSize);
            requestClient.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + formDataBoundary);
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        WebClient IRequestBuilder.ProvideS3ChunkUploadWebClient() {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        #endregion

        #endregion

        #region Share-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetDownloadShares(long? offset, long? limit, GetDownloadSharesFilter filter = null, SharesSort sort = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetDownloadShares, Method.GET);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            AddSort(sort, request);
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        IRestRequest IRequestBuilder.GetUploadShares(long? offset, long? limit, GetUploadSharesFilter filter = null, SharesSort sort = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUploadShares, Method.GET);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            AddSort(sort, request);
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        #endregion

        #region POST

        IRestRequest IRequestBuilder.PostCreateDownloadShare(ApiCreateDownloadShareRequest downloadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateDownloadShare, Method.POST);
            SetGeneralRestValues(request, true, downloadShareParams);
            return request;
        }

        IRestRequest IRequestBuilder.PostCreateUploadShare(ApiCreateUploadShareRequest uploadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateUploadShare, Method.POST);
            SetGeneralRestValues(request, true, uploadShareParams);
            return request;
        }

        #endregion

        #region DELETE

        IRestRequest IRequestBuilder.DeleteDownloadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteDownloadShare, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId);
            return request;
        }

        IRestRequest IRequestBuilder.DeleteUploadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUploadShare, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId);
            return request;
        }

        #endregion

        #endregion

        #region OAuth-Endpoint

        #region POST

        IRestRequest IRequestBuilder.PostOAuthToken(string clientId, string clientSecret, string grantType, string code) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostAuthToken, Method.POST);
            SetGeneralRestValues(request, false);
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(clientId + ":" + clientSecret)));
            request.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            request.AddParameter("code", code, ParameterType.GetOrPost);
            return request;
        }

        IRestRequest IRequestBuilder.PostOAuthRefresh(string clientId, string clientSecret, string grantType, string refreshToken) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostRefreshToken, Method.POST);
            SetGeneralRestValues(request, false);
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(clientId + ":" + clientSecret)));
            request.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            request.AddParameter("refresh_token", refreshToken, ParameterType.GetOrPost);
            return request;
        }

        #endregion

        #endregion

        #region Config-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetGeneralSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetGeneralConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetInfrastructureSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetInfrastructureConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        IRestRequest IRequestBuilder.GetDefaultsSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetDefaultsConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        public IRestRequest GetPasswordPolicies() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPasswordPolicies, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        public IRestRequest GetAlgorithms() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAlgorithms, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #endregion

        #region Resources-Endpoint

        #region GET

        IRestRequest IRequestBuilder.GetUserAvatar(long userId, string avatarUuid) {
            RestRequest request = new RestRequest(ApiConfig.ApiResourcesGetAvatar, Method.GET);
            SetGeneralRestValues(request, false);
            request.AddUrlSegment("userId", userId);
            request.AddUrlSegment("uuid", avatarUuid);
            return request;
        }

        #endregion

        #endregion
    }
}