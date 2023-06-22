using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.Sort;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using Telerik.JustMock;

namespace Dracoon.Sdk.UnitTest.Factory {
    public static class FactoryClients {
        internal static IOAuth OAuthMock {
            get {
                IOAuth oauth = Mock.Create<IOAuth>(Behavior.Strict);
                Mock.Arrange(() => oauth.BuildAuthString()).Returns("DummyAuthString");
                Mock.Arrange(() => oauth.RefreshAccessToken()).DoNothing();
                Mock.ArrangeSet(() => oauth.Auth = new DracoonAuth("Token1"));
                return oauth;
            }
        }

        internal static IInternalDracoonClient InternalDracoonClientMock(bool withVersionCheckOccurance = false) {
            IInternalDracoonClient c = Mock.Create<IInternalDracoonClient>(Behavior.Strict);
            Mock.Arrange(() => c.ServerUri).Returns(new Uri("https://dracoon.team"));
            Mock.Arrange(() => c.EncryptionPassword).Returns("Pw1298!");
            Mock.Arrange(() => c.Builder).Returns(new DracoonRequestBuilder(null));
            Mock.Arrange(() => c.OAuth).Returns(OAuthMock);
            Mock.Arrange(() => c.ServerImpl).Returns(new DracoonServerImpl(null));
            Mock.Arrange(() => c.NodesImpl).Returns(new DracoonNodesImpl(null));
            Mock.Arrange(() => c.AccountImpl).Returns(new DracoonAccountImpl(null));
            IRequestExecutor e = new DracoonRequestExecutor(null, null);
            if (withVersionCheckOccurance) {
                Mock.Arrange(() => e.CheckApiServerVersion(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            }

            Mock.Arrange(() => c.Executor).Returns(e);
            return c;
        }

        internal static IRequestBuilder RequestBuilderMock {
            get {
                IRequestBuilder r = new DracoonRequestBuilder(null);

                #region Server request mocks

                Mock.Arrange(() => r.GetServerVersion()).Returns(FactoryRestSharp.RestRequestWithoutAuth(ApiConfig.ApiGetServerVersion, Method.GET));
                Mock.Arrange(() => r.GetServerTime()).Returns(FactoryRestSharp.RestRequestWithoutAuth(ApiConfig.ApiGetServerTime, Method.GET));

                #endregion

                #region User request mocks

                Mock.Arrange(() => r.GetUserAccount()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUserAccount, Method.GET));
                Mock.Arrange(() => r.GetCustomerAccount()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetCustomerAccount, Method.GET));
                Mock.Arrange(() => r.GetUserKeyPair(Arg.AnyString)).Returns((string x) => {
                    return FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUserKeyPair, Method.GET).AddQueryParameter("version", x);
                });
                Mock.Arrange(() => r.GetUserKeyPairs()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUserKeyPairs, Method.GET));
                Mock.Arrange(() => r.GetAuthenticatedPing())
                    .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAuthenticatedPing, Method.GET).AddHeader("Accept", "*/*"));
                Mock.Arrange(() => r.GetAvatar()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAvatar, Method.GET));
                Mock.Arrange(() => r.SetUserKeyPair(Arg.IsAny<ApiUserKeyPair>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostUserKeyPair, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryUser.ApiUserKeyPair_2048), ParameterType.RequestBody));
                Mock.Arrange(() => r.DeleteUserKeyPair(Arg.AnyString))
                    .Returns((string x) => {
                        return FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteUserKeyPair, Method.DELETE).AddQueryParameter("version", x);
                    });
                Mock.Arrange(() => r.DeleteAvatar()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteAvatar, Method.DELETE));
                Mock.Arrange(() => r.GetUserProfileAttributes())
                    .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.GET));
                Mock.Arrange(() => r.GetUserProfileAttribute(Arg.AnyString)).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiGetUserProfileAttributes, Method.GET).AddQueryParameter("filter", "key:eq:" + FactoryAttribute.AttributeList.Items[0].Key));
                Mock.Arrange(() => r.PutUserProfileAttributes(Arg.IsAny<ApiAddOrUpdateAttributeRequest>())).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutUserProfileAttributes, Method.PUT).AddParameter("application/json",
                    JsonConvert.SerializeObject(FactoryAttribute.ApiAddOrUpdateAttributeRequest), ParameterType.RequestBody));
                Mock.Arrange(() => r.DeleteUserProfileAttributes(Arg.AnyString))
                    .Returns(FactoryRestSharp.DeleteUserProfileAttribute(FactoryAttribute.AttributeList.Items[0].Key));
                Mock.Arrange(() => r.ProvideAvatarDownloadWebClient()).Returns(() => {
                    DracoonWebClientExtension wc = new DracoonWebClientExtension();
                    wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                    wc.SetHttpConfigParams(new DracoonHttpConfig());
                    return wc;
                });
                Mock.Arrange(() => r.ProvideAvatarUploadWebClient(Arg.AnyString)).Returns((string x) => {
                    DracoonWebClientExtension wc = new DracoonWebClientExtension();
                    wc.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + x);
                    wc.Headers.Add(ApiConfig.AuthorizationHeader, OAuthMock.BuildAuthString());
                    wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                    wc.SetHttpConfigParams(new DracoonHttpConfig());
                    return wc;
                });

                #endregion

                #region Node request mocks

                Mock.Arrange(() => r.GetNodes(Arg.AnyLong, Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetNodesFilter>(), Arg.IsAny<GetNodesSort>())).Returns(
                    (long parent, long? offset, long? limit, GetNodesFilter f, GetNodesSort s) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetChildNodes, Method.GET);
                        ApplyFilter(f, rr);
                        ApplySort(s, rr);

                        rr.AddQueryParameter("parent_id", parent.ToString());
                        if (offset.HasValue) {
                            rr.AddQueryParameter("offset", offset.ToString());
                        }

                        if (limit.HasValue) {
                            rr.AddQueryParameter("limit", limit.ToString());
                        }

                        return rr;
                    });

                Mock.Arrange(() => r.GetNode(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetNode, Method.GET).AddUrlSegment("nodeId", id));

                Mock.Arrange(() => r.GetFileKey(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetFileKey, Method.GET).AddUrlSegment("fileId", id));

                Mock.Arrange(() => r.GetSearchNodes(Arg.AnyLong, Arg.AnyString, Arg.AnyLong, Arg.AnyLong, Arg.AnyInt, Arg.IsAny<SearchNodesFilter>(),
                    Arg.IsAny<SearchNodesSort>())).Returns(
                    (long id, string search, long offset, long limit, int depth, SearchNodesFilter f, SearchNodesSort s) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetSearchNodes, Method.GET);
                        ApplyFilter(f, rr);
                        ApplySort(s, rr);
                        rr.AddQueryParameter("search_string", search);
                        rr.AddQueryParameter("parent_id", id.ToString());
                        rr.AddQueryParameter("depth_level", depth.ToString());
                        rr.AddQueryParameter("offset", offset.ToString());
                        rr.AddQueryParameter("limit", limit.ToString());
                        return rr;
                    });

                Mock.Arrange(() => r.GetMissingFileKeys(Arg.IsAny<long?>(), Arg.AnyInt, Arg.AnyInt)).Returns(
                    (long? fileId, int limit, int offset) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetMissingFileKeys, Method.GET);
                        if (fileId.HasValue) {
                            rr.AddQueryParameter("fileId", fileId.ToString());
                        }

                        rr.AddQueryParameter("offset", offset.ToString());
                        rr.AddQueryParameter("limit", limit.ToString());

                        return rr;
                    });

                Mock.Arrange(() => r.GetRecycleBin(Arg.AnyLong, Arg.IsAny<long?>(), Arg.IsAny<long?>())).Returns(
                    (long roomId, long? offset, long? limit) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetMissingFileKeys, Method.GET);
                        rr.AddUrlSegment("roomId", roomId);
                        if (offset.HasValue) {
                            rr.AddQueryParameter("offset", offset.ToString());
                        }

                        if (limit.HasValue) {
                            rr.AddQueryParameter("limit", limit.ToString());
                        }

                        return rr;
                    });

                Mock.Arrange(() => r.GetPreviousVersions(Arg.AnyLong, Arg.AnyString, Arg.AnyString, Arg.IsAny<long?>(), Arg.IsAny<long?>())).Returns(
                    (long id, string t, string name, long? offset, long? limit) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPreviousVersions, Method.GET);
                        rr.AddUrlSegment("nodeId", id);
                        rr.AddQueryParameter("type", t);
                        rr.AddQueryParameter("name", name);
                        if (offset.HasValue) {
                            rr.AddQueryParameter("offset", offset.ToString());
                        }

                        if (limit.HasValue) {
                            rr.AddQueryParameter("limit", limit.ToString());
                        }

                        return rr;
                    });

                Mock.Arrange(() => r.GetPreviousVersion(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPreviousVersion, Method.GET).AddUrlSegment("previousNodeId", id));

                Mock.Arrange(() => r.PostRoom(Arg.IsAny<ApiCreateRoomRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostRoom, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryRoom.ApiCreateRoomRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.PostFolder(Arg.IsAny<ApiCreateFolderRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostFolder, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFolder.ApiCreateFolderRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.PostFileDownload(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCreateFileDownload, Method.POST).AddUrlSegment("fileId", id));

                Mock.Arrange(() => r.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostCreateFileUpload, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCreateFileUpload), ParameterType.RequestBody));

                Mock.Arrange(() => r.PostCopyNodes(Arg.AnyLong, Arg.IsAny<ApiCopyNodesRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCopyNodes, Method.POST);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiCopyNodesRequest), ParameterType.RequestBody);
                    rr.AddUrlSegment("nodeId", id);
                    return rr;
                });

                Mock.Arrange(() => r.PostMoveNodes(Arg.AnyLong, Arg.IsAny<ApiMoveNodesRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostMoveNodes, Method.POST);
                    rr.AddUrlSegment("nodeId", id);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiMoveNodesRequest), ParameterType.RequestBody);
                    return rr;
                });

                Mock.Arrange(() => r.PostMissingFileKeys(Arg.IsAny<ApiSetUserFileKeysRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostMissingFileKeys, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiSetUserFileKeysRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.PostFavorite(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostFavorite, Method.POST).AddUrlSegment("nodeId", id));

                Mock.Arrange(() => r.PostRestoreNodeVersion(Arg.IsAny<ApiRestorePreviousVersionsRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostRestoreNodeVersion, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryNode.ApiRestorePreviousVersionsRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.PutRoom(Arg.AnyLong, Arg.IsAny<ApiUpdateRoomRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutRoom, Method.PUT);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiUpdateRoomRequest), ParameterType.RequestBody);
                    rr.AddUrlSegment("roomId", id);
                    return rr;
                });

                Mock.Arrange(() => r.PutEnableRoomEncryption(Arg.AnyLong, Arg.IsAny<ApiEnableRoomEncryptionRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutEnableRoomEncryption, Method.PUT);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiEnableRoomEncryptionRequest),
                        ParameterType.RequestBody);
                    rr.AddUrlSegment("roomId", id);
                    return rr;
                });

                Mock.Arrange(() => r.PutFolder(Arg.AnyLong, Arg.IsAny<ApiUpdateFolderRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutFolder, Method.PUT);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFolder.ApiUpdateFolderRequest), ParameterType.RequestBody);
                    rr.AddUrlSegment("folderId", id);
                    return rr;
                });

                Mock.Arrange(() => r.PutFile(Arg.AnyLong, Arg.IsAny<ApiUpdateFileRequest>())).Returns((long id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutFileUpdate, Method.PUT);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiUpdateFileRequest), ParameterType.RequestBody);
                    rr.AddUrlSegment("fileId", id);
                    return rr;
                });

                Mock.Arrange(() => r.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>())).Returns((string path) =>
                    FactoryRestSharp.RestRequestWithAuth(path, Method.PUT).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryFile.ApiCompleteFileUpload), ParameterType.RequestBody));

                Mock.Arrange(() => r.DeleteNodes(Arg.IsAny<ApiDeleteNodesRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiDeleteNodes, Method.DELETE).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryNode.ApiDeleteNodesRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.DeleteFavorite(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteFavorite, Method.DELETE).AddUrlSegment("nodeId", id));

                Mock.Arrange(() => r.DeleteRecycleBin(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteRecycleBin, Method.DELETE).AddUrlSegment("roomId", id));

                Mock.Arrange(() => r.DeletePreviousVersion(Arg.IsAny<ApiDeletePreviousVersionsRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiDeletePreviousVersions, Method.DELETE).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryNode.ApiDeletePreviousVersionsRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.ProvideChunkDownloadWebClient(Arg.AnyLong, Arg.AnyLong)).Returns((long offset, long count) => {
                    DracoonWebClientExtension wc = new DracoonWebClientExtension(offset, (offset + count) - 1);
                    wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                    wc.SetHttpConfigParams(new DracoonHttpConfig());
                    return wc;
                });
                Mock.Arrange(() => r.ProvideChunkUploadWebClient(Arg.AnyInt, Arg.AnyLong, Arg.AnyString, Arg.AnyString)).Returns(
                    (int length, long offset, string data, string fileSize) => {
                        DracoonWebClientExtension wc = new DracoonWebClientExtension();
                        wc.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + offset + "-" + (offset + length) + "/" + fileSize);
                        wc.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + data);
                        wc.Headers.Add(ApiConfig.AuthorizationHeader, OAuthMock.BuildAuthString());
                        wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                        wc.SetHttpConfigParams(new DracoonHttpConfig());
                        return wc;
                    });
                Mock.Arrange(() => r.PostGetS3Urls(Arg.AnyString, Arg.IsAny<ApiGetS3Urls>())).Returns((string id) => {
                    RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostGetS3Urls, Method.POST);
                    rr.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiGetS3UrlsRequest), ParameterType.RequestBody);
                    rr.AddUrlSegment("uploadId", id);
                    return rr;
                });

                Mock.Arrange(() => r.GetS3Status(Arg.AnyString)).Returns((string id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetS3Status, Method.GET).AddUrlSegment("uploadId", id));


                Mock.Arrange(() => r.GenerateVirusProtectionInfo(Arg.IsAny<ApiGenerateVirusProtectionInfoRequest>())).Returns(
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGenerateVirusProtectionInfo, Method.POST));

                #endregion

                #region Share request mocks

                Mock.Arrange(() =>
                        r.GetDownloadShares(Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetDownloadSharesFilter>(), Arg.IsAny<SharesSort>()))
                    .Returns((long? offset, long? limit, GetDownloadSharesFilter f, SharesSort s) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDownloadShares, Method.GET);
                        ApplyFilter(f, rr);
                        ApplySort(s, rr);

                        if (offset.HasValue) {
                            rr.AddQueryParameter("offset", offset.ToString());
                        }

                        if (limit.HasValue) {
                            rr.AddQueryParameter("limit", limit.ToString());
                        }

                        return rr;
                    });

                Mock.Arrange(() =>
                    r.GetUploadShares(Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetUploadSharesFilter>(), Arg.IsAny<SharesSort>())).Returns(
                    (long? offset, long? limit, GetUploadSharesFilter f, SharesSort s) => {
                        RestRequest rr = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUploadShares, Method.GET);
                        ApplyFilter(f, rr);
                        ApplySort(s, rr);

                        if (offset.HasValue) {
                            rr.AddQueryParameter("offset", offset.ToString());
                        }

                        if (limit.HasValue) {
                            rr.AddQueryParameter("limit", limit.ToString());
                        }

                        return rr;
                    });

                Mock.Arrange(() => r.PostCreateDownloadShare(Arg.IsAny<ApiCreateDownloadShareRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostCreateDownloadShare, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateDownloadShareRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.PostCreateUploadShare(Arg.IsAny<ApiCreateUploadShareRequest>())).Returns(FactoryRestSharp
                    .RestRequestWithAuth(ApiConfig.ApiPostCreateUploadShare, Method.POST).AddParameter("application/json",
                        JsonConvert.SerializeObject(FactoryShare.ApiCreateUploadShareRequest), ParameterType.RequestBody));

                Mock.Arrange(() => r.DeleteDownloadShare(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteDownloadShare, Method.DELETE).AddUrlSegment("shareId", id));

                Mock.Arrange(() => r.DeleteUploadShare(Arg.AnyLong)).Returns((long id) =>
                    FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteUploadShare, Method.DELETE).AddUrlSegment("shareId", id));

                #endregion

                #region OAuth request mocks

                Mock.Arrange(() => r.PostOAuthToken(Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyString)).Returns(
                    (string cid, string cs, string grant, string code) => FactoryRestSharp
                        .RestRequestWithoutAuth(OAuthConfig.OAuthPostAuthToken, Method.POST)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("code", code, ParameterType.GetOrPost));
                Mock.Arrange(() => r.PostOAuthRefresh(Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyString)).Returns(
                    (string cid, string cs, string grant, string token) => FactoryRestSharp
                        .RestRequestWithoutAuth(OAuthConfig.OAuthPostRefreshToken, Method.POST)
                        .AddHeader("Authorization", "Basic " + Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(cid + ":" + cs)))
                        .AddParameter("grant_type", grant, ParameterType.GetOrPost).AddParameter("refresh_token", token, ParameterType.GetOrPost));

                #endregion

                #region Config request mocks

                Mock.Arrange(() => r.GetGeneralSettings()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET));
                Mock.Arrange(() => r.GetInfrastructureSettings())
                    .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetInfrastructureConfig, Method.GET));
                Mock.Arrange(() => r.GetDefaultsSettings()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDefaultsConfig, Method.GET));
                Mock.Arrange(() => r.GetAlgorithms()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAlgorithms, Method.GET));

                #endregion

                #region Resources request mocks

                Mock.Arrange(() => r.GetUserAvatar(Arg.AnyLong, Arg.AnyString)).Returns(FactoryRestSharp
                    .RestRequestWithoutAuth(ApiConfig.ApiResourcesGetAvatar, Method.GET).AddUrlSegment("userId", 123)
                    .AddUrlSegment("uuid", "1HUD743H"));

                #endregion

                return r;
            }
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
    }
}