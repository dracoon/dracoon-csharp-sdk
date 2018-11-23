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
using System.Text;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonRequestBuilder {

        private DracoonClient client;

        internal DracoonRequestBuilder(DracoonClient client) {
            this.client = client;
        }

        private void SetGeneralRestValues(RestRequest request, bool requiresAuth, object optionalJsonBody = null) {
            if (requiresAuth) {
                request.AddHeader(ApiConfig.AuthorizationHeader, client.OAuthClient.BuildAuthString());
            }
            if (optionalJsonBody != null) {
                request.AddParameter("application/json", JsonConvert.SerializeObject(optionalJsonBody), ParameterType.RequestBody);
            }
            request.ReadWriteTimeout = client.HttpConfig.ReadWriteTimeout;
            request.Timeout = client.HttpConfig.ConnectionTimeout;
        }

        private void SetGeneralWebClientValues(DracoonWebClientExtension requestClient) {
            requestClient.Headers.Add(HttpRequestHeader.UserAgent, client.HttpConfig.UserAgent);
            requestClient.SetHttpConfigParams(client.HttpConfig);
        }

        private void AddFilters<T>(T filter, RestRequest requestForFilterAdding) where T : DracoonFilter {
            if (filter == null)
                return;
            string filterString = filter.ToString();
            if (String.IsNullOrWhiteSpace(filterString))
                return;
            requestForFilterAdding.AddQueryParameter("filter", filterString);
        }

        private void AddSort<T>(T sort, RestRequest requestForSortAdding) where T : DracoonSort {
            if (sort == null)
                return;
            string sortString = sort.ToString();
            if (String.IsNullOrWhiteSpace(sortString))
                return;
            requestForSortAdding.AddQueryParameter("sort", sortString);
        }

        #region Public-Endpoint

        #region GET

        internal RestRequest GetServerVersion() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerVersion, Method.GET);
            SetGeneralRestValues(request, false);
            return request;
        }

        internal RestRequest GetServerTime() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetServerTime, Method.GET);
            SetGeneralRestValues(request, false);
            return request;
        }

        #endregion

        #endregion

        #region User-Endpoint

        #region GET

        internal RestRequest GetUserAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserAccount, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        internal RestRequest GetCustomerAccount() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetCustomerAccount, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        internal RestRequest GetUserKeyPair() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetUserKeyPair, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        internal RestRequest GetAuthenticatedPing() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetAuthenticatedPing, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddHeader("Accept", "*/*");
            return request;
        }

        #endregion
        #region POST

        internal RestRequest SetUserKeyPair(ApiUserKeyPair apiUserKeyPair) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostUserKeyPair, Method.POST);
            SetGeneralRestValues(request, true, apiUserKeyPair);
            return request;
        }

        #endregion
        #region DELETE

        internal RestRequest DeleteUserKeyPair() {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUserKeyPair, Method.DELETE);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #endregion

        #region Nodes-Endpoint

        #region GET

        internal RestRequest GetNodes(long parentNodeId, long? offset = null, long? limit = null, GetNodesFilter filter = null) {
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

        internal RestRequest GetNode(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetNode, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId.ToString());
            return request;
        }

        internal RestRequest GetFileKey(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetFileKey, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", nodeId.ToString());
            return request;
        }

        internal RestRequest GetSearchNodes(long parentNodeId, string searchString, long offset, long limit, int depthLevel = -1, SearchNodesFilter filter = null, SearchNodesSort sort = null) {
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

        internal RestRequest GetMissingFileKeys(long? fileId, int limit = 10, int offset = 0) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetMissingFileKeys, Method.GET);
            SetGeneralRestValues(request, true);
            if (fileId.HasValue) {
                request.AddQueryParameter("fileId", fileId.ToString());
            }
            request.AddQueryParameter("offset", offset.ToString());
            request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        internal RestRequest GetRecycleBin(long parentRoomId, long? offset = null, long? limit = null) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetRecycleBin, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            if (offset.HasValue)
                request.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                request.AddQueryParameter("limit", limit.ToString());
            return request;
        }

        internal RestRequest GetPreviousVersions(long nodeId, string type, string nodeName, long? offset = null, long? limit = null) {
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

        internal RestRequest GetPreviousVersion(long previoudNodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiGetPreviousVersion, Method.GET);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("previoudNodeId", previoudNodeId);
            return request;
        }

        #endregion
        #region POST

        internal RestRequest PostRoom(ApiCreateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRoom, Method.POST);
            SetGeneralRestValues(request, true, roomParams);
            return request;
        }

        internal RestRequest PostFolder(ApiCreateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFolder, Method.POST);
            SetGeneralRestValues(request, true, folderParams);
            return request;
        }

        internal RestRequest PostFileDownload(long fileId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileDownload, Method.POST);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        internal RestRequest PostCreateFileUpload(ApiCreateFileUpload uploadParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateFileUpload, Method.POST);
            SetGeneralRestValues(request, true, uploadParams);
            return request;
        }

        internal RestRequest PostCopyNodes(long targetNodeId, ApiCopyNodesRequest copyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCopyNodes, Method.POST);
            SetGeneralRestValues(request, true, copyParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        internal RestRequest PostMoveNodes(long targetNodeId, ApiMoveNodesRequest moveParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMoveNodes, Method.POST);
            SetGeneralRestValues(request, true, moveParams);
            request.AddUrlSegment("nodeId", targetNodeId);
            return request;
        }

        internal RestRequest PostMissingFileKeys(ApiSetUserFileKeysRequest fileKeyParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostMissingFileKeys, Method.POST);
            SetGeneralRestValues(request, true, fileKeyParams);
            return request;
        }

        internal RestRequest PostFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostFavorite, Method.POST);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        internal RestRequest PostRestoreNodeVersion(ApiRestorePreviousVersionsRequest restoreParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostRestoreNodeVersion, Method.POST);
            SetGeneralRestValues(request, true, restoreParams);
            return request;
        }

        #endregion
        #region PUT

        internal RestRequest PutRoom(long roomId, ApiUpdateRoomRequest roomParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutRoom, Method.PUT);
            SetGeneralRestValues(request, true, roomParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        internal RestRequest PutEnableRoomEncryption(long roomId, ApiEnableRoomEncryptionRequest encryptionParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutEnableRoomEncryption, Method.PUT);
            SetGeneralRestValues(request, true, encryptionParams);
            request.AddUrlSegment("roomId", roomId);
            return request;
        }

        internal RestRequest PutFolder(long folderId, ApiUpdateFolderRequest folderParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFolder, Method.PUT);
            SetGeneralRestValues(request, true, folderParams);
            request.AddUrlSegment("folderId", folderId);
            return request;
        }

        internal RestRequest PutFile(long fileId, ApiUpdateFileRequest fileParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPutFileUpdate, Method.PUT);
            SetGeneralRestValues(request, true, fileParams);
            request.AddUrlSegment("fileId", fileId);
            return request;
        }

        internal RestRequest PutCompleteFileUpload(string uploadPath, ApiCompleteFileUpload completeParams) {
            RestRequest request = new RestRequest(uploadPath, Method.PUT);
            SetGeneralRestValues(request, true, completeParams);
            return request;
        }

        #endregion
        #region DELETE

        internal RestRequest DeleteNodes(ApiDeleteNodesRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteNodes, Method.DELETE);
            SetGeneralRestValues(request, true, deleteParams);
            return request;
        }

        internal RestRequest DeleteFavorite(long nodeId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteFavorite, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("nodeId", nodeId);
            return request;
        }

        internal RestRequest DeleteRecycleBin(long parentRoomId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteRecycleBin, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("roomId", parentRoomId);
            return request;
        }

        internal RestRequest DeletePreviousVersion(ApiDeletePreviousVersionsRequest deleteParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeletePreviousVersions, Method.DELETE);
            SetGeneralRestValues(request, true, deleteParams);
            return request;
        }

        #endregion
        #region HTTP-Request

        internal WebClient ProvideChunkDownloadWebClient(long offset, long count) {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension(rangeFrom: offset, rangeTo: (offset + count) - 1);
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        internal WebClient ProvideChunkUploadWebClient(int chunkLength, long offset, string formDataBoundary, string totalFileSize) {
            DracoonWebClientExtension requestClient = new DracoonWebClientExtension();
            requestClient.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + offset + "-" + (offset + chunkLength) + "/" + totalFileSize);
            requestClient.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + formDataBoundary);
            SetGeneralWebClientValues(requestClient);
            return requestClient;
        }

        #endregion

        #endregion

        #region Share-Endpoint

        #region GET

        internal RestRequest GetDownloadShares(long? offset, long? limit, GetDownloadSharesFilter filter = null, SharesSort sort = null) {
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

        internal RestRequest GetUploadShares(long? offset, long? limit, GetUploadSharesFilter filter = null, SharesSort sort = null) {
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

        internal RestRequest PostCreateDownloadShare(ApiCreateDownloadShareRequest downloadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateDownloadShare, Method.POST);
            SetGeneralRestValues(request, true, downloadShareParams);
            return request;
        }

        internal RestRequest PostCreateUploadShare(ApiCreateUploadShareRequest uploadShareParams) {
            RestRequest request = new RestRequest(ApiConfig.ApiPostCreateUploadShare, Method.POST);
            SetGeneralRestValues(request, true, uploadShareParams);
            return request;
        }

        #endregion
        #region DELETE

        internal RestRequest DeleteDownloadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteDownloadShare, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId.ToString());
            return request;
        }

        internal RestRequest DeleteUploadShare(long shareId) {
            RestRequest request = new RestRequest(ApiConfig.ApiDeleteUploadShare, Method.DELETE);
            SetGeneralRestValues(request, true);
            request.AddUrlSegment("shareId", shareId.ToString());
            return request;
        }

        #endregion

        #endregion

        #region OAuth-Endpoint

        #region POST

        internal RestRequest PostOAuthToken(string clientId, string clientSecret, string grantType, string code) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostAuthToken, Method.POST);
            SetGeneralRestValues(request, false);
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));
            request.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            request.AddParameter("code", code, ParameterType.GetOrPost);
            return request;
        }

        internal RestRequest PostOAuthRefresh(string clientId, string clientSecret, string grantType, string refreshToken) {
            RestRequest request = new RestRequest(OAuthConfig.OAuthPostRefreshToken, Method.POST);
            SetGeneralRestValues(request, false);
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));
            request.AddParameter("grant_type", grantType, ParameterType.GetOrPost);
            request.AddParameter("refresh_token", refreshToken, ParameterType.GetOrPost);
            return request;
        }

        #endregion

        #endregion

        #region Config-Endpoint

        #region GET

        internal RestRequest GetGeneralSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetGeneralConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        internal RestRequest GetInfrastructureSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetInfrastructureConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        internal RestRequest GetDefaultsSettings() {
            RestRequest request = new RestRequest(ApiConfig.ApiGetDefaultsConfig, Method.GET);
            SetGeneralRestValues(request, true);
            return request;
        }

        #endregion

        #endregion
    }
}
