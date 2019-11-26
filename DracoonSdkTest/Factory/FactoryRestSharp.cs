using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.Sort;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Telerik.JustMock;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryRestSharp {
        internal static RestResponse RestResponse {
            get {
                RestResponse r = Mock.Create<RestResponse>();
                r.Content = "{\"code\": 403,\"message\": \"Some message!\",\"debugInfo\": \"Some debug info!\",\"errorCode\": -10003}";
                r.StatusCode = HttpStatusCode.Forbidden;
                Mock.NonPublic.Arrange<bool>(r, "IsSuccessful").Returns(false);
                return r;
            }
        }

        internal static RestRequest RestRequestWithoutAuth(string apiPath, Method method) {
            DracoonHttpConfig config = new DracoonHttpConfig();
            RestRequest r = new RestRequest(apiPath, method) {
                ReadWriteTimeout = config.ReadWriteTimeout,
                Timeout = config.ConnectionTimeout,
                Method = method,
                Resource = apiPath
            };
            return r;
        }

        internal static RestRequest RestRequestWithAuth(string apiPath, Method method) {
            DracoonHttpConfig config = new DracoonHttpConfig();
            RestRequest r = new RestRequest(apiPath, method) {
                ReadWriteTimeout = config.ReadWriteTimeout,
                Timeout = config.ConnectionTimeout,
                Method = method,
                Resource = apiPath
            };
            r.AddHeader(ApiConfig.AuthorizationHeader, FactoryClients.OAuthMock.BuildAuthString());
            return r;
        }

        private static void ApplyFilter<T>(T filter, IRestRequest requestForFilterAdding) where T : DracoonFilter {
            if (filter == null) {
                return;
            }

            string filterString = filter.ToString();
            if (string.IsNullOrWhiteSpace(filterString)) {
                return;
            }

            requestForFilterAdding.AddQueryParameter("filter", filterString);
        }

        private static void ApplySort<T>(T sort, IRestRequest requestForSortAdding) where T : DracoonSort {
            if (sort == null) {
                return;
            }

            string sortString = sort.ToString();
            if (string.IsNullOrWhiteSpace(sortString)) {
                return;
            }

            requestForSortAdding.AddQueryParameter("sort", sortString);
        }

        internal static IRestRequest GetNodesMock(long parent, long? offset = null, long? limit = null, GetNodesFilter filter = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetChildNodes, Method.GET);
            ApplyFilter(filter, rr);
            rr.AddQueryParameter("parent_id", parent.ToString());
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }

            return rr;
        }

        internal static IRestRequest GetNodeMock(long nodeId) {
            return RestRequestWithAuth(ApiConfig.ApiGetNode, Method.GET).AddUrlSegment("nodeId", nodeId);
        }

        internal static IRestRequest DeleteNodesMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteNodes, Method.DELETE).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiDeleteNodesRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest CopyNodesMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPostCopyNodes, Method.POST);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiCopyNodesRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("nodeId", id);
            return rr;
        }

        internal static IRestRequest MoveNodesMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPostMoveNodes, Method.POST);
            rr.AddUrlSegment("nodeId", id);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiMoveNodesRequest), ParameterType.RequestBody);
            return rr;
        }

        internal static IRestRequest GetSearchNodesMock(long id, string search, long offset, long limit, int depth = -1, SearchNodesFilter f=null, SearchNodesSort s=null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetSearchNodes, Method.GET);
            ApplyFilter(f, rr);
            ApplySort(s, rr);
            rr.AddQueryParameter("search_string", search);
            rr.AddQueryParameter("parent_id", id.ToString());
            rr.AddQueryParameter("depth_level", depth.ToString());
            rr.AddQueryParameter("offset", offset.ToString());
            rr.AddQueryParameter("limit", limit.ToString());
            return rr;
        }

        internal static IRestRequest PostFavoriteMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostFavorite, Method.POST).AddUrlSegment("nodeId", id);
        }

        internal static IRestRequest DeleteFavoriteMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteFavorite, Method.DELETE).AddUrlSegment("nodeId", id);
        }

        internal static IRestRequest GetRecycleBinItemsMock(long id, long? offset = null, long? limit = null) {
            return RestRequestWithAuth(ApiConfig.ApiGetRecycleBin, Method.GET).AddUrlSegment("roomId", id).AddQueryParameter("offset", offset.ToString()).AddQueryParameter("limit", limit.ToString());
        }

        internal static IRestRequest DeleteRecycleBinMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteRecycleBin, Method.DELETE).AddUrlSegment("roomId", id);
        }

        internal static IRestRequest GetPreviousVersionsMock(long id, string type, string name, long? offset = null, long? limit = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetPreviousVersions, Method.GET);
            rr.AddUrlSegment("nodeId", id);
            rr.AddQueryParameter("type", type);
            rr.AddQueryParameter("name", name);
            if (offset.HasValue)
                rr.AddQueryParameter("offset", offset.ToString());
            if (limit.HasValue)
                rr.AddQueryParameter("limit", limit.ToString());
            return rr;
        }

        internal static IRestRequest GetPreviousVersionMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiGetPreviousVersion, Method.GET).AddUrlSegment("previousNodeId", id);
        }

        internal static IRestRequest PostRestoreNodeVersionMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostRestoreNodeVersion, Method.POST).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiRestorePreviousVersionsRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest DeletePreviousVersionMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeletePreviousVersions, Method.DELETE).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiDeletePreviousVersionsRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest PostRoomMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostRoom, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryRoom.ApiCreateRoomRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest PutRoomMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutRoom, Method.PUT);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiUpdateRoomRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("roomId", id);
            return rr;
        }

        internal static IRestRequest PutEnableRoomEncryptionMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutEnableRoomEncryption, Method.PUT);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiEnableRoomEncryptionRequest),
                ParameterType.RequestBody);
            rr.AddUrlSegment("roomId", id);
            return rr;
        }

        internal static IRestRequest PostFolderMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostFolder, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFolder.ApiCreateFolderRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest PutFolderMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutFolder, Method.PUT);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFolder.ApiUpdateFolderRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("folderId", id);
            return rr;
        }

        internal static IRestRequest PutFileMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutFileUpdate, Method.PUT);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiUpdateFileRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("fileId", id);
            return rr;
        }

        internal static IRestRequest GetMissingFileKeysMock(int limit, int offset, long? fileId = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetMissingFileKeys, Method.GET);
            if (fileId.HasValue) {
                rr.AddQueryParameter("fileId", fileId.ToString());
            }
            rr.AddQueryParameter("offset", offset.ToString());
            rr.AddQueryParameter("limit", limit.ToString());
            return rr;
        }

        internal static IRestRequest PostMissingFileKeysMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostMissingFileKeys, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiSetUserFileKeysRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest GetFileKeyMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiGetFileKey, Method.GET).AddUrlSegment("fileId", id);
        }

        internal static IRestRequest GetUserAvatarMock() {
            return RestRequestWithoutAuth(ApiConfig.ApiResourcesGetAvatar, Method.GET).AddUrlSegment("userId", 123)
                    .AddUrlSegment("uuid", "1HUD743H");
        }

        internal static IRestRequest GetUserProfileAttributes() {
            return RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.GET);
        }

        internal static IRestRequest GetUserProfileAttribute(string key) {
            return RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.GET).AddQueryParameter("filter", "key:eq:" + key);
        }

        internal static IRestRequest PutUserProfileAttributes() {
            return RestRequestWithAuth(ApiConfig.ApiPutUserProfileAttributes, Method.PUT).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryAttribute.ApiAddOrUpdateAttributeRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest DeleteUserProfileAttribute(string key) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUserProfileAttributes, Method.DELETE).AddUrlSegment("key", key);
        }

        internal static IRestRequest GetUserAccountMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetUserAccount, Method.GET);
        }

        internal static IRestRequest GetCustomerAccountMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetCustomerAccount, Method.GET);
        }

        internal static IRestRequest SetUserKeyPairMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostUserKeyPair, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryUser.ApiUserKeyPair), ParameterType.RequestBody);
        }

        internal static IRestRequest DeleteUserKeyPairMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUserKeyPair, Method.DELETE);
        }

        internal static IRestRequest GetUserKeyPairMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetUserKeyPair, Method.GET);
        }

        internal static IRestRequest GetAuthenticatedPingMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetAuthenticatedPing, Method.GET).AddHeader("Accept", "*/*");
        }

        internal static IRestRequest GetAvatarMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetAvatar, Method.GET);
        }

        internal static IRestRequest DeleteAvatarMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteAvatar, Method.DELETE);
        }

        internal static IRestRequest PostCreateDownloadShareMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateDownloadShare, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateDownloadShareRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest DeleteDownloadShareMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteDownloadShare, Method.DELETE).AddUrlSegment("shareId", id);
        }

        internal static IRestRequest GetDownloadSharesMock(long? offset = null, long? limit = null, GetDownloadSharesFilter f = null, SharesSort s = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetDownloadShares, Method.GET);
            ApplyFilter(f, rr);
            ApplySort(s, rr);
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }
            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }
            return rr;
        }

        internal static IRestRequest PostCreateUploadShareMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateUploadShare, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateUploadShareRequest), ParameterType.RequestBody);
        }

        internal static IRestRequest DeleteUploadShareMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUploadShare, Method.DELETE).AddUrlSegment("shareId", id);
        }

        internal static IRestRequest GetUploadSharesMock(long? offset = null, long? limit = null, GetUploadSharesFilter f = null, SharesSort s = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetUploadShares, Method.GET);
            ApplyFilter(f, rr);
            ApplySort(s, rr);
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }
            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }
            return rr;
        }

        internal static IRestRequest PostFileDownloadMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateFileDownload, Method.POST).AddUrlSegment("fileId", id);
        }

        internal static IRestRequest PostOAuthTokenMock(string cid, string cs, string grant, string code) {
            return RestRequestWithoutAuth(OAuthConfig.OAuthPostAuthToken, Method.POST)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("code", code, ParameterType.GetOrPost);
        }

        internal static IRestRequest PostOAuthRefreshMock(string cid, string cs, string grant, string token) {
            return RestRequestWithoutAuth(OAuthConfig.OAuthPostRefreshToken, Method.POST)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("refresh_token", token, ParameterType.GetOrPost);
        }

        internal static IRestRequest GetServerVersionMock() {
            return RestRequestWithoutAuth(ApiConfig.ApiGetServerVersion, Method.GET);
        }

        internal static IRestRequest PostCreateFileUploadMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateFileUpload, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCreateFileUpload), ParameterType.RequestBody);
        }

        internal static IRestRequest PutCompleteFileUploadMock(string path) {
            return RestRequestWithAuth(path, Method.PUT).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCompleteFileUpload), ParameterType.RequestBody);
        }

        internal static IRestRequest PostGetS3UrlsMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostGetS3Urls, Method.POST).AddUrlSegment("uploadId", 1);
        }

        internal static IRestRequest GetS3StatusMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetS3Status, Method.GET).AddUrlSegment("uploadId", 1);
        }

    }
}