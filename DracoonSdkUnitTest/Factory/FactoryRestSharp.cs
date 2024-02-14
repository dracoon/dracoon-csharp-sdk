using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.Sort;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
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
                Method = method,
                Resource = apiPath
            };
            return r;
        }

        internal static RestRequest RestRequestWithAuth(string apiPath, Method method) {
            DracoonHttpConfig config = new DracoonHttpConfig();
            RestRequest r = new RestRequest(apiPath, method) {
                Method = method,
                Resource = apiPath
            };
            r.AddHeader(ApiConfig.AuthorizationHeader, FactoryClients.OAuthMock.BuildAuthString());
            return r;
        }

        private static void ApplyFilter<T>(T filter, RestRequest requestForFilterAdding) where T : DracoonFilter {
            if (filter == null) {
                return;
            }

            string filterString = filter.ToString();
            if (string.IsNullOrWhiteSpace(filterString)) {
                return;
            }

            requestForFilterAdding.AddQueryParameter("filter", filterString);
        }

        private static void ApplySort<T>(T sort, RestRequest requestForSortAdding) where T : DracoonSort {
            if (sort == null) {
                return;
            }

            string sortString = sort.ToString();
            if (string.IsNullOrWhiteSpace(sortString)) {
                return;
            }

            requestForSortAdding.AddQueryParameter("sort", sortString);
        }

        internal static RestRequest GetNodesMock(long parent, long? offset = null, long? limit = null, GetNodesFilter filter = null, GetNodesSort sort = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetChildNodes, Method.Get);
            ApplyFilter(filter, rr);
            ApplySort(sort, rr);
            rr.AddQueryParameter("parent_id", parent.ToString());
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }

            return rr;
        }

        internal static RestRequest GetNodeMock(long nodeId) {
            return RestRequestWithAuth(ApiConfig.ApiGetNode, Method.Get).AddUrlSegment("nodeId", nodeId);
        }

        internal static RestRequest DeleteNodesMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteNodes, Method.Delete).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiDeleteNodesRequest), ParameterType.RequestBody);
        }

        internal static RestRequest CopyNodesMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPostCopyNodes, Method.Post);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiCopyNodesRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("nodeId", id);
            return rr;
        }

        internal static RestRequest MoveNodesMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPostMoveNodes, Method.Post);
            rr.AddUrlSegment("nodeId", id);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiMoveNodesRequest), ParameterType.RequestBody);
            return rr;
        }

        internal static RestRequest GetSearchNodesMock(long id, string search, long offset, long limit, int depth = -1, SearchNodesFilter f = null, SearchNodesSort s = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetSearchNodes, Method.Get);
            ApplyFilter(f, rr);
            ApplySort(s, rr);
            rr.AddQueryParameter("search_string", search);
            rr.AddQueryParameter("parent_id", id.ToString());
            rr.AddQueryParameter("depth_level", depth.ToString());
            rr.AddQueryParameter("offset", offset.ToString());
            rr.AddQueryParameter("limit", limit.ToString());
            return rr;
        }

        internal static RestRequest PostFavoriteMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostFavorite, Method.Post).AddUrlSegment("nodeId", id);
        }

        internal static RestRequest DeleteFavoriteMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteFavorite, Method.Delete).AddUrlSegment("nodeId", id);
        }

        internal static RestRequest GetRecycleBinItemsMock(long id, long? offset = null, long? limit = null) {
            return RestRequestWithAuth(ApiConfig.ApiGetRecycleBin, Method.Get).AddUrlSegment("roomId", id).AddQueryParameter("offset", offset.ToString()).AddQueryParameter("limit", limit.ToString());
        }

        internal static RestRequest DeleteRecycleBinMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteRecycleBin, Method.Delete).AddUrlSegment("roomId", id);
        }

        internal static RestRequest GetPreviousVersionsMock(long id, string type, string name, long? offset = null, long? limit = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetPreviousVersions, Method.Get);
            rr.AddUrlSegment("nodeId", id);
            rr.AddQueryParameter("type", type);
            rr.AddQueryParameter("name", name);
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }

            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }

            return rr;
        }

        internal static RestRequest GetPreviousVersionMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiGetPreviousVersion, Method.Get).AddUrlSegment("previousNodeId", id);
        }

        internal static RestRequest PostRestoreNodeVersionMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostRestoreNodeVersion, Method.Post).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiRestorePreviousVersionsRequest), ParameterType.RequestBody);
        }

        internal static RestRequest DeletePreviousVersionMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeletePreviousVersions, Method.Delete).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryNode.ApiDeletePreviousVersionsRequest), ParameterType.RequestBody);
        }

        internal static RestRequest PostRoomMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostRoom, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryRoom.ApiCreateRoomRequest), ParameterType.RequestBody);
        }

        internal static RestRequest PutRoomMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutRoom, Method.Put);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiUpdateRoomRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("roomId", id);
            return rr;
        }

        internal static RestRequest PutEnableRoomEncryptionMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutEnableRoomEncryption, Method.Put);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiEnableRoomEncryptionRequest),
                ParameterType.RequestBody);
            rr.AddUrlSegment("roomId", id);
            return rr;
        }

        internal static RestRequest PostFolderMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostFolder, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFolder.ApiCreateFolderRequest), ParameterType.RequestBody);
        }

        internal static RestRequest PutFolderMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutFolder, Method.Put);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFolder.ApiUpdateFolderRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("folderId", id);
            return rr;
        }

        internal static RestRequest PutFileMock(long id) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiPutFileUpdate, Method.Put);
            rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiUpdateFileRequest), ParameterType.RequestBody);
            rr.AddUrlSegment("fileId", id);
            return rr;
        }

        internal static RestRequest GetMissingFileKeysMock(int limit, int offset, long? fileId = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetMissingFileKeys, Method.Get);
            if (fileId.HasValue) {
                rr.AddQueryParameter("fileId", fileId.ToString());
            }
            rr.AddQueryParameter("offset", offset.ToString());
            rr.AddQueryParameter("limit", limit.ToString());
            return rr;
        }

        internal static RestRequest PostMissingFileKeysMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostMissingFileKeys, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiSetUserFileKeysRequest), ParameterType.RequestBody);
        }

        internal static RestRequest GetFileKeyMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiGetFileKey, Method.Get).AddUrlSegment("fileId", id);
        }

        internal static RestRequest GetUserAvatarMock() {
            return RestRequestWithoutAuth(ApiConfig.ApiResourcesGetAvatar, Method.Get).AddUrlSegment("userId", 123)
                    .AddUrlSegment("uuid", "1HUD743H");
        }

        internal static RestRequest GetUserProfileAttributes() {
            return RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.Get);
        }

        internal static RestRequest GetUserProfileAttribute(string key) {
            return RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.Get).AddQueryParameter("filter", "key:eq:" + key);
        }

        internal static RestRequest PutUserProfileAttributes() {
            return RestRequestWithAuth(ApiConfig.ApiPutUserProfileAttributes, Method.Put).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryAttribute.ApiAddOrUpdateAttributeRequest), ParameterType.RequestBody);
        }

        internal static RestRequest DeleteUserProfileAttribute(string key) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUserProfileAttributes, Method.Delete).AddUrlSegment("key", key);
        }

        internal static RestRequest GetUserAccountMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetUserAccount, Method.Get);
        }

        internal static RestRequest GetCustomerAccountMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetCustomerAccount, Method.Get);
        }

        internal static RestRequest SetUserKeyPairMock(ApiUserKeyPair pair) {
            return RestRequestWithAuth(ApiConfig.ApiPostUserKeyPair, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(pair), ParameterType.RequestBody);
        }

        internal static RestRequest DeleteUserKeyPairMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUserKeyPair, Method.Delete);
        }

        internal static RestRequest GetUserKeyPairMock(string version) {
            return RestRequestWithAuth(ApiConfig.ApiGetUserKeyPair, Method.Get).AddQueryParameter("version", version);
        }

        internal static RestRequest GetAuthenticatedPingMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetAuthenticatedPing, Method.Get).AddHeader("Accept", "*/*");
        }

        internal static RestRequest GetAvatarMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetAvatar, Method.Get);
        }

        internal static RestRequest DeleteAvatarMock() {
            return RestRequestWithAuth(ApiConfig.ApiDeleteAvatar, Method.Delete);
        }

        internal static RestRequest PostCreateDownloadShareMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateDownloadShare, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateDownloadShareRequest), ParameterType.RequestBody);
        }

        internal static RestRequest DeleteDownloadShareMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteDownloadShare, Method.Delete).AddUrlSegment("shareId", id);
        }

        internal static RestRequest GetDownloadSharesMock(long? offset = null, long? limit = null, GetDownloadSharesFilter f = null, SharesSort s = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetDownloadShares, Method.Get);
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

        internal static RestRequest PostCreateUploadShareMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateUploadShare, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateUploadShareRequest), ParameterType.RequestBody);
        }

        internal static RestRequest DeleteUploadShareMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUploadShare, Method.Delete).AddUrlSegment("shareId", id);
        }

        internal static RestRequest PostMailDownloadShare(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostMailDownloadShare, Method.Post).AddUrlSegment("shareId", id).AddParameter("application/json", 
                JsonConvert.SerializeObject(FactoryShare.ApiMailShareInfoRequest), ParameterType.RequestBody);
        }

        internal static RestRequest PostMailUploadShare(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostMailUploadShare, Method.Post).AddUrlSegment("shareId", id).AddParameter("application/json",
                JsonConvert.SerializeObject(FactoryShare.ApiMailShareInfoRequest), ParameterType.RequestBody);
        }

        internal static RestRequest GetUploadSharesMock(long? offset = null, long? limit = null, GetUploadSharesFilter f = null, SharesSort s = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetUploadShares, Method.Get);
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

        internal static RestRequest PostFileDownloadMock(long id) {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateFileDownload, Method.Post).AddUrlSegment("fileId", id);
        }

        internal static RestRequest PostOAuthTokenMock(string cid, string cs, string grant, string code) {
            return RestRequestWithoutAuth(OAuthConfig.OAuthPostAuthToken, Method.Post)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("code", code, ParameterType.GetOrPost);
        }

        internal static RestRequest PostOAuthRefreshMock(string cid, string cs, string grant, string token) {
            return RestRequestWithoutAuth(OAuthConfig.OAuthPostRefreshToken, Method.Post)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("refresh_token", token, ParameterType.GetOrPost);
        }

        internal static RestRequest GetServerVersionMock() {
            return RestRequestWithoutAuth(ApiConfig.ApiGetServerVersion, Method.Get);
        }

        internal static RestRequest PostCreateFileUploadMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostCreateFileUpload, Method.Post).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCreateFileUpload), ParameterType.RequestBody);
        }

        internal static RestRequest PutCompleteFileUploadMock(string path) {
            return RestRequestWithAuth(path, Method.Put).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCompleteFileUpload), ParameterType.RequestBody);
        }

        internal static RestRequest PostGetS3UrlsMock() {
            return RestRequestWithAuth(ApiConfig.ApiPostGetS3Urls, Method.Post).AddUrlSegment("uploadId", 1);
        }

        internal static RestRequest GetS3StatusMock() {
            return RestRequestWithAuth(ApiConfig.ApiGetS3Status, Method.Get).AddUrlSegment("uploadId", 1);
        }

        internal static RestRequest GenerateVirusProtectionInfoMock() {
            return RestRequestWithAuth(ApiConfig.ApiGenerateVirusProtectionInfo, Method.Post);
        }

        internal static RestRequest DeleteMaliciousFileMock(long fileId) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteMaliciousFile, Method.Delete).AddUrlSegment("fileId", fileId);
        }

        internal static RestRequest GetDownloadShareSubscriptionsMock(long? offset = null, long? limit = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetDownloadShareSubscriptions, Method.Get);
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }
            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }
            return rr;
        }

        internal static RestRequest GetUploadShareSubscriptionsMock(long? offset = null, long? limit = null) {
            RestRequest rr = RestRequestWithAuth(ApiConfig.ApiGetUploadShareSubscriptions, Method.Get);
            if (offset.HasValue) {
                rr.AddQueryParameter("offset", offset.ToString());
            }
            if (limit.HasValue) {
                rr.AddQueryParameter("limit", limit.ToString());
            }
            return rr;
        }

        internal static RestRequest PostDownloadShareSubscriptionMock(long shareId) {
            return RestRequestWithAuth(ApiConfig.ApiPostDownloadShareSubscription, Method.Post).AddUrlSegment("shareId", shareId);
        }

        internal static RestRequest PostUploadShareSubscriptionMock(long shareId) {
            return RestRequestWithAuth(ApiConfig.ApiPostUploadShareSubscription, Method.Post).AddUrlSegment("shareId", shareId);
        }

        internal static RestRequest DeleteDownloadShareSubscriptionMock(long shareId) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteDownloadShareSubscription, Method.Delete).AddUrlSegment("shareId", shareId);
        }

        internal static RestRequest DeleteUploadShareSubscriptionMock(long shareId) {
            return RestRequestWithAuth(ApiConfig.ApiDeleteUploadShareSubscription, Method.Delete).AddUrlSegment("shareId", shareId);
        }
    }
}