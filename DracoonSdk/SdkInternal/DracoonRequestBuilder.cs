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

        private void SetGeneralRestValues(RestRequest request, bool requiresAuth, object optionalJsonBody = null) {
            if (requiresAuth) {
                request.AddHeader(ApiConfig.AuthorizationHeader, _auth.BuildAuthString());
            }

            if (optionalJsonBody != null) {
                request.AddParameter("application/json", JsonConvert.SerializeObject(optionalJsonBody), ParameterType.RequestBody);
            }
        }

        private void SetGeneralWebClientValues(DracoonWebClientExtension requestClient) {
            requestClient.Headers.Add(HttpRequestHeader.UserAgent, DracoonClient.HttpConfig.UserAgent);
            requestClient.SetHttpConfigParams(DracoonClient.HttpConfig);
        }

        private DracoonWebClientExtension CreateDefaultWebClient() {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        private void AddFilters<T>(T filter, RestRequest requestForFilterAdding) where T : DracoonFilter {
            if (filter == null) {
                return;
            }

            string filterString = filter.ToString();
            if (string.IsNullOrWhiteSpace(filterString)) {
                return;
            }

            requestForFilterAdding.AddQueryParameter("filter", filterString);
        }

        private void AddSort<T>(T sort, RestRequest requestForSortAdding) where T : DracoonSort {
            if (sort == null) {
                return;
            }

            string sortString = sort.ToString();
            if (string.IsNullOrWhiteSpace(sortString)) {
                return;
            }

            requestForSortAdding.AddQueryParameter("sort", sortString);
        }

        #region Public-Endpoint

        #region GET

        RestRequest IRequestBuilder.GetServerVersion() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerVersion, Method.Get);
            SetGeneralRestValues(request, false);
            return request;
        }

        RestRequest IRequestBuilder.GetServerTime() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerTime, Method.Get);
            SetGeneralRestValues(request, false);
            return request;
        }

        #endregion

        #endregion

        #region User-Endpoint

        #region GET

        RestRequest IRequestBuilder.GetUserAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserAccount, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetCustomerAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetCustomerAccount, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetUserKeyPair(string algorithm) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserKeyPair, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("version", algorithm);
            return request;
        }

        public RestRequest GetUserKeyPairs() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserKeyPairs, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetAuthenticatedPing() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAuthenticatedPing, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddHeader("Accept", "*/*");
            return request;
        }

        RestRequest IRequestBuilder.GetAvatar() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAvatar, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetUserProfileAttributes() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserProfileAttributes, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetUserProfileAttribute(string attributeKey) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserProfileAttributes, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("filter", "key:eq:" + attributeKey);
            return request;
        }

        #endregion

        #region POST

        RestRequest IRequestBuilder.SetUserKeyPair(ApiUserKeyPair apiUserKeyPair) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostUserKeyPair, Method.Post);
            SetGeneralRestValues(request, true, apiUserKeyPair);
            return request;
        }

        #endregion

        #region PUT

        RestRequest IRequestBuilder.PutUserProfileAttributes(ApiAddOrUpdateAttributeRequest addOrUpdateParam) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutUserProfileAttributes, Method.Put);
            SetGeneralRestValues(request, true, addOrUpdateParam);
            return request;
        }

        #endregion

        #region DELETE

        RestRequest IRequestBuilder.DeleteUserKeyPair(string algorithm) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUserKeyPair, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddQueryParameter("version", algorithm);
            return request;
        }

        RestRequest IRequestBuilder.DeleteUserProfileAttributes(string attributeKey) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUserProfileAttributes, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("key", attributeKey);
            return request;
        }

        RestRequest IRequestBuilder.DeleteAvatar() {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteAvatar, Method.Delete);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #region HTTP-Request

        WebClient IRequestBuilder.ProvideAvatarDownloadWebClient() {
            return CreateDefaultWebClient();
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

        RestRequest IRequestBuilder.GetNodes(long parentNodeId, long? offset, long? limit, GetNodesFilter filter) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetChildNodes, Method.Get);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            request.AddQueryParameter("parent_id", parentNodeId.ToString());
            if (offset.HasValue) {
                request.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                request.AddQueryParameter("limit", limit.ToString());
            }

            return request;
        }

        RestRequest IRequestBuilder.GetNode(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetNode, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        RestRequest IRequestBuilder.GetFileKey(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetFileKey, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", nodeId);
            return request;
        }

        RestRequest IRequestBuilder.GetSearchNodes(long parentNodeId, string searchString, long offset, long limit, int depthLevel,
            SearchNodesFilter filter, SearchNodesSort sort) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetSearchNodes, Method.Get);
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

        RestRequest IRequestBuilder.GetMissingFileKeys(long? fileId, int limit, int offset) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetMissingFileKeys, Method.Get);
            SetGeneralRestValues(request, true);
            if (fileId.HasValue) {
                request.AddQueryParameter("fileId", fileId.ToString());
            }

            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        RestRequest IRequestBuilder.GetRecycleBin(long parentRoomId, long? offset, long? limit) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetRecycleBin, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            if (offset.HasValue) {
                request.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                request.AddQueryParameter("limit", limit.ToString());
            }

            return request;
        }

        RestRequest IRequestBuilder.GetPreviousVersions(long nodeId, string type, string nodeName, long? offset, long? limit) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPreviousVersions, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            request.AddQueryParameter("type", type);
            request.AddQueryParameter("name", nodeName);
            if (offset.HasValue) {
                request.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                request.AddQueryParameter("limit", limit.ToString());
            }

            return request;
        }

        RestRequest IRequestBuilder.GetPreviousVersion(long previousNodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPreviousVersion, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("previousNodeId", previousNodeId);
            return request;
        }

        RestRequest IRequestBuilder.GetS3Status(string uploadId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetS3Status, Method.Get);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        #endregion

        #region POST

        RestRequest IRequestBuilder.PostRoom(ApiCreateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRoom, Method.Post);
            SetGeneralRestValues(request, true, roomParams);
            return request;
        }

        RestRequest IRequestBuilder.PostFolder(ApiCreateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFolder, Method.Post);
            SetGeneralRestValues(request, true, folderParams);
            return request;
        }

        RestRequest IRequestBuilder.PostFileDownload(long fileId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileDownload, Method.Post);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        RestRequest IRequestBuilder.PostCreateFileUpload(ApiCreateFileUpload uploadParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileUpload, Method.Post);
            SetGeneralRestValues(request, true, uploadParams);
            return request;
        }

        RestRequest IRequestBuilder.PostGetS3Urls(string uploadId, ApiGetS3Urls s3UrlParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostGetS3Urls, Method.Post);
            SetGeneralRestValues(request, true, s3UrlParams);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        RestRequest IRequestBuilder.PostCopyNodes(long targetNodeId, ApiCopyNodesRequest copyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCopyNodes, Method.Post);
            SetGeneralRestValues(request, true, copyParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        RestRequest IRequestBuilder.PostMoveNodes(long targetNodeId, ApiMoveNodesRequest moveParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMoveNodes, Method.Post);
            SetGeneralRestValues(request, true, moveParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        RestRequest IRequestBuilder.PostMissingFileKeys(ApiSetUserFileKeysRequest fileKeyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMissingFileKeys, Method.Post);
            SetGeneralRestValues(request, true, fileKeyParams);
            return request;
        }

        RestRequest IRequestBuilder.PostFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFavorite, Method.Post);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        RestRequest IRequestBuilder.PostRestoreNodeVersion(ApiRestorePreviousVersionsRequest restoreParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRestoreNodeVersion, Method.Post);
            SetGeneralRestValues(request, true, restoreParams);
            return request;
        }

        #endregion

        #region PUT

        RestRequest IRequestBuilder.PutRoom(long roomId, ApiUpdateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutRoom, Method.Put);
            SetGeneralRestValues(request, true, roomParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        RestRequest IRequestBuilder.PutEnableRoomEncryption(long roomId, ApiEnableRoomEncryptionRequest encryptionParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutEnableRoomEncryption, Method.Put);
            SetGeneralRestValues(request, true, encryptionParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        RestRequest IRequestBuilder.PutFolder(long folderId, ApiUpdateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFolder, Method.Put);
            SetGeneralRestValues(request, true, folderParams);
            request.AddUrlSegment("folderId", folderId);
            return request;
        }

        RestRequest IRequestBuilder.PutFile(long fileId, ApiUpdateFileRequest fileParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFileUpdate, Method.Put);
            SetGeneralRestValues(request, true, fileParams);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        RestRequest IRequestBuilder.PutCompleteFileUpload(string uploadPath, ApiCompleteFileUpload completeParams) {
            RestRequest request = new RestRequest(uploadPath, Method.Put);
            SetGeneralRestValues(request, true, completeParams);
            return request;
        }

        RestRequest IRequestBuilder.PutCompleteS3FileUpload(string uploadId, ApiCompleteFileUpload completeParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutCompleteS3Upload, Method.Put);
            SetGeneralRestValues(request, true, completeParams);
            request.AddUrlSegment("uploadId", uploadId);
            return request;
        }

        #endregion

        #region DELETE

        RestRequest IRequestBuilder.DeleteNodes(ApiDeleteNodesRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteNodes, Method.Delete);
            SetGeneralRestValues(request, true, deleteParams);
            return request;
        }

        RestRequest IRequestBuilder.DeleteFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteFavorite, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        RestRequest IRequestBuilder.DeleteRecycleBin(long parentRoomId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteRecycleBin, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            return request;
        }

        RestRequest IRequestBuilder.DeletePreviousVersion(ApiDeletePreviousVersionsRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeletePreviousVersions, Method.Delete);
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
            return CreateDefaultWebClient();
        }

        #endregion

        #endregion

        #region Share-Endpoint

        #region GET

        RestRequest IRequestBuilder.GetDownloadShares(long? offset, long? limit, GetDownloadSharesFilter filter, SharesSort sort) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetDownloadShares, Method.Get);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            AddSort(sort, request);
            if (offset.HasValue) {
                request.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                request.AddQueryParameter("limit", limit.ToString());
            }

            return request;
        }

        RestRequest IRequestBuilder.GetUploadShares(long? offset, long? limit, GetUploadSharesFilter filter, SharesSort sort) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUploadShares, Method.Get);
            SetGeneralRestValues(request, true);
            AddFilters(filter, request);
            AddSort(sort, request);
            if (offset.HasValue) {
                request.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                request.AddQueryParameter("limit", limit.ToString());
            }

            return request;
        }

        #endregion

        #region POST

        RestRequest IRequestBuilder.PostCreateDownloadShare(ApiCreateDownloadShareRequest downloadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateDownloadShare, Method.Post);
            SetGeneralRestValues(request, true, downloadShareParams);
            return request;
        }

        RestRequest IRequestBuilder.PostCreateUploadShare(ApiCreateUploadShareRequest uploadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateUploadShare, Method.Post);
            SetGeneralRestValues(request, true, uploadShareParams);
            return request;
        }

        #endregion

        #region DELETE

        RestRequest IRequestBuilder.DeleteDownloadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteDownloadShare, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId);
            return request;
        }

        RestRequest IRequestBuilder.DeleteUploadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUploadShare, Method.Delete);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId);
            return request;
        }

        #endregion

        #endregion

        #region OAuth-Endpoint

        #region POST

        RestRequest IRequestBuilder.PostOAuthToken(string clientId, string clientSecret, string grantType, string code) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostAuthToken, Method.Post);
            SetGeneralRestValues(request, false);
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(clientId + ":" + clientSecret)));
            request.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            request.AddParameter("code", code, ParameterType.GetOrPost);
            return request;
        }

        RestRequest IRequestBuilder.PostOAuthRefresh(string clientId, string clientSecret, string grantType, string refreshToken) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostRefreshToken, Method.Post);
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

        RestRequest IRequestBuilder.GetGeneralSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetGeneralConfig, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetInfrastructureSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetInfrastructureConfig, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        RestRequest IRequestBuilder.GetDefaultsSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetDefaultsConfig, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        public RestRequest GetPasswordPolicies() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPasswordPolicies, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        public RestRequest GetAlgorithms() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAlgorithms, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        public RestRequest GetClassificationPolicies() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetClassificationPolicies, Method.Get);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #endregion

        #region Resources-Endpoint

        #region GET

        RestRequest IRequestBuilder.GetUserAvatar(long userId, string avatarUuid) {
            RestRequest request = new RestRequest(ApiConfig.ApiResourcesGetAvatar, Method.Get);
            SetGeneralRestValues(request, false);
            request.AddUrlSegment("userId", userId);
            request.AddUrlSegment("uuid", avatarUuid);
            return request;
        }

        #endregion

        #endregion
    }
}