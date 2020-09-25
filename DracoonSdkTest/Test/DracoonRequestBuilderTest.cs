using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.Sort;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text;
using Telerik.JustMock.AutoMock.Ninject.Activation;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test {
    public class DracoonRequestBuilderTest {
        #region Public-Endpoint

        [Fact]
        public void Public_GetServerVersion() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetServerVersion();

            // ACT
            IRestRequest actual = builder.GetServerVersion();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Public_GetServerTime() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetServerTime();

            // ACT
            IRestRequest actual = builder.GetServerTime();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region User-Endpoint

        [Fact]
        public void User_GetUserAccount() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserAccount();

            // ACT
            IRestRequest actual = builder.GetUserAccount();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetCustomerAccount() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetCustomerAccount();

            // ACT
            IRestRequest actual = builder.GetCustomerAccount();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetUserKeyPair_RSA2048() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA2048));

            // ACT
            IRestRequest actual = builder.GetUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA2048));

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetUserKeyPair_RSA4096() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA4096));

            // ACT
            IRestRequest actual = builder.GetUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA4096));

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetUserKeyPairs() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserKeyPairs();

            // ACT
            IRestRequest actual = builder.GetUserKeyPairs();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetAuthenticatedPing() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetAuthenticatedPing();

            // ACT
            IRestRequest actual = builder.GetAuthenticatedPing();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetAvatar() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetAvatar();

            // ACT
            IRestRequest actual = builder.GetAvatar();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_SetUserKeyPair() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.SetUserKeyPair(FactoryUser.ApiUserKeyPair_2048);

            // ACT
            IRestRequest actual = builder.SetUserKeyPair(FactoryUser.ApiUserKeyPair_2048);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_DeleteUserKeyPair_RSA2048() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.DeleteUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA2048));

            // ACT
            IRestRequest actual = builder.DeleteUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA2048));

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }


        [Fact]
        public void User_DeleteUserKeyPair_RSA4096() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.DeleteUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA4096));

            // ACT
            IRestRequest actual = builder.DeleteUserKeyPair(UserMapper.ToApiUserKeyPairVersion(UserKeyPairAlgorithm.RSA4096));

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_DeleteAvatar() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.DeleteAvatar();

            // ACT
            IRestRequest actual = builder.DeleteAvatar();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_ProvideAvatarDownloadWebClient() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            WebClient expected = FactoryClients.RequestBuilderMock.ProvideAvatarDownloadWebClient();

            // ACT
            WebClient actual = builder.ProvideAvatarDownloadWebClient();

            // ASSERT
            Assert.Equal(expected, actual, new WebClientComparer());
        }

        [Fact]
        public void User_ProvideAvatarUploadWebClient() {
            // ARRANGE
            string data = "some test data!";
            IOAuth auth = FactoryClients.OAuthMock;
            IRequestBuilder builder = new DracoonRequestBuilder(auth);
            WebClient expected = FactoryClients.RequestBuilderMock.ProvideAvatarUploadWebClient(data);

            // ACT
            WebClient actual = builder.ProvideAvatarUploadWebClient(data);

            // ASSERT
            Assert.Equal(expected, actual, new WebClientComparer());
        }

        [Fact]
        public void User_GetUserProfileAttributes() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserProfileAttributes();

            // ACT
            IRestRequest actual = builder.GetUserProfileAttributes();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_GetUserProfileAttribute() {
            // ARRANGE
            string attributeKey = FactoryAttribute.AttributeList.Items[0].Key;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetUserProfileAttribute(attributeKey);

            // ACT
            IRestRequest actual = builder.GetUserProfileAttribute(attributeKey);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_AddOrUpdateUserProfileAttributes() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.PutUserProfileAttributes(FactoryAttribute.ApiAddOrUpdateAttributeRequest);

            // ACT
            IRestRequest actual = builder.PutUserProfileAttributes(FactoryAttribute.ApiAddOrUpdateAttributeRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void User_DeleteUserProfileAttribute() {
            // ARRANGE
            string attributeKey = FactoryAttribute.AttributeList.Items[0].Key;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.DeleteUserProfileAttributes(attributeKey);

            // ACT
            IRestRequest actual = builder.DeleteUserProfileAttributes(attributeKey);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region Nodes-Endpoint

        [Fact]
        public void Nodes_GetNodes() {
            // ARRANGE
            long id = 5, offset = 3, limit = 2;
            GetNodesFilter f = new GetNodesFilter();
            f.AddNodeIsEncryptedFilter(GetNodesFilter.IsEncrypted.EqualTo(true).Build());
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetNodes(id, offset, limit, f);

            // ACT
            IRestRequest actual = builder.GetNodes(id, offset, limit, f);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetNodes_NoFilter() {
            // ARRANGE
            long id = 5, offset = 3, limit = 2;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetNodes(id, offset, limit);

            // ACT
            IRestRequest actual = builder.GetNodes(id, offset, limit);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetNode() {
            // ARRANGE
            long id = 4;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetNode(id);

            // ACT
            IRestRequest actual = builder.GetNode(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetFileKey() {
            // ARRANGE
            long id = 2;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetFileKey(id);

            // ACT
            IRestRequest actual = builder.GetFileKey(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetSearchNodes() {
            // ARRANGE
            string searchString = "test";
            long parent = 234, offset = 2, limit = 3;
            int depth = 1;
            SearchNodesFilter f = new SearchNodesFilter();
            f.AddIsFavoriteFilter(SearchNodesFilter.IsFavorite.EqualTo(true).Build());
            SearchNodesSort s = SearchNodesSort.Name.Ascending();
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetSearchNodes(parent, searchString, offset, limit, depth, f, s);

            // ACT
            IRestRequest actual = builder.GetSearchNodes(parent, searchString, offset, limit, depth, f, s);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetSearchNodes_NoFilter_NoSort() {
            // ARRANGE
            string searchString = "test";
            long parent = 234, offset = 2, limit = 3;
            int depth = 1;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetSearchNodes, Method.GET);
            expected.AddQueryParameter("search_string", searchString);
            expected.AddQueryParameter("parent_id", parent.ToString());
            expected.AddQueryParameter("depth_level", depth.ToString());
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetSearchNodes(parent, searchString, offset, limit, depth);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetMissingFileKeys() {
            // ARRANGE
            long id = 346;
            int offset = 2, limit = 3;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetMissingFileKeys, Method.GET);
            expected.AddQueryParameter("fileId", id.ToString());
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetMissingFileKeys(id, limit, offset);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetRecycleBin() {
            // ARRANGE
            long id = 567, offset = 4, limit = 5;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetRecycleBin, Method.GET);
            expected.AddUrlSegment("roomId", id);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetRecycleBin(id, offset, limit);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetPreviousVersions() {
            // ARRANGE
            string type = "room", name = "testRoom";
            long id = 34567;
            int offset = 7, limit = 3;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPreviousVersions, Method.GET);
            expected.AddUrlSegment("nodeId", id);
            expected.AddQueryParameter("type", type);
            expected.AddQueryParameter("name", name);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetPreviousVersions(id, type, name, offset, limit);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetPreviousVersion() {
            // ARRANGE
            long id = 78654;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPreviousVersion, Method.GET);
            expected.AddUrlSegment("previousNodeId", id);

            // ACT
            IRestRequest actual = builder.GetPreviousVersion(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostRoom() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostRoom, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiCreateRoomRequest), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostRoom(FactoryRoom.ApiCreateRoomRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostFolder() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostFolder, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFolder.ApiCreateFolderRequest), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostFolder(FactoryFolder.ApiCreateFolderRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostFileDownload() {
            // ARRANGE
            long id = 394678;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCreateFileDownload, Method.POST);
            expected.AddUrlSegment("fileId", id);

            // ACT
            IRestRequest actual = builder.PostFileDownload(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostCreateFileUpload() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCreateFileUpload, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiCreateFileUpload), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostCreateFileUpload(FactoryFile.ApiCreateFileUpload);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostCopyNodes() {
            // ARRANGE
            long id = 235678;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCopyNodes, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiCopyNodesRequest), ParameterType.RequestBody);
            expected.AddUrlSegment("nodeId", id);

            // ACT
            IRestRequest actual = builder.PostCopyNodes(id, FactoryNode.ApiCopyNodesRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostMoveNodes() {
            // ARRANGE
            long id = 8790;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostMoveNodes, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiMoveNodesRequest), ParameterType.RequestBody);
            expected.AddUrlSegment("nodeId", id);

            // ACT
            IRestRequest actual = builder.PostMoveNodes(id, FactoryNode.ApiMoveNodesRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostMissingFileKeys() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostMissingFileKeys, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiSetUserFileKeysRequest), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostMissingFileKeys(FactoryFile.ApiSetUserFileKeysRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostFavorite() {
            // ARRANGE
            long id = 168;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostFavorite, Method.POST);
            expected.AddUrlSegment("nodeId", id);

            // ACT
            IRestRequest actual = builder.PostFavorite(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PostRestoreNodeVersion() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostRestoreNodeVersion, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiRestorePreviousVersionsRequest),
                ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostRestoreNodeVersion(FactoryNode.ApiRestorePreviousVersionsRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutRoom() {
            // ARRANGE
            long id = 895647;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutRoom, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiUpdateRoomRequest), ParameterType.RequestBody);
            expected.AddUrlSegment("roomId", id);

            // ACT
            IRestRequest actual = builder.PutRoom(id, FactoryRoom.ApiUpdateRoomRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutEnableRoomEncryption() {
            // ARRANGE
            long id = 39478;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutEnableRoomEncryption, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryRoom.ApiEnableRoomEncryptionRequest),
                ParameterType.RequestBody);
            expected.AddUrlSegment("roomId", id);

            // ACT
            IRestRequest actual = builder.PutEnableRoomEncryption(id, FactoryRoom.ApiEnableRoomEncryptionRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutFolder() {
            // ARRANGE
            long id = 897;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutFolder, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFolder.ApiUpdateFolderRequest), ParameterType.RequestBody);
            expected.AddUrlSegment("folderId", id);

            // ACT
            IRestRequest actual = builder.PutFolder(id, FactoryFolder.ApiUpdateFolderRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutFile() {
            // ARRANGE
            long id = 567;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutFileUpdate, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiUpdateFileRequest), ParameterType.RequestBody);
            expected.AddUrlSegment("fileId", id);

            // ACT
            IRestRequest actual = builder.PutFile(id, FactoryFile.ApiUpdateFileRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutCompleteFileUpload() {
            // ARRANGE
            string path = "some/dummy/path";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(path, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiCompleteFileUpload), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PutCompleteFileUpload(path, FactoryFile.ApiCompleteFileUpload);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_DeleteNodes() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteNodes, Method.DELETE);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiDeleteNodesRequest), ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.DeleteNodes(FactoryNode.ApiDeleteNodesRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_DeleteFavorite() {
            // ARRANGE
            long id = 39468345;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteFavorite, Method.DELETE);
            expected.AddUrlSegment("nodeId", id);

            // ACT
            IRestRequest actual = builder.DeleteFavorite(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_DeleteRecycleBin() {
            // ARRANGE
            long id = 39468345;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteRecycleBin, Method.DELETE);
            expected.AddUrlSegment("roomId", id);

            // ACT
            IRestRequest actual = builder.DeleteRecycleBin(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_DeletePreviousVersion() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeletePreviousVersions, Method.DELETE);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryNode.ApiDeletePreviousVersionsRequest),
                ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.DeletePreviousVersion(FactoryNode.ApiDeletePreviousVersionsRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_ProvideChunkDownloadWebClient() {
            // ARRANGE
            long offset = 5, count = 1234;
            DracoonHttpConfig conf = new DracoonHttpConfig();
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            DracoonWebClientExtension expected = new DracoonWebClientExtension(offset, count);
            expected.Headers.Add(HttpRequestHeader.UserAgent, conf.UserAgent);
            expected.SetHttpConfigParams(conf);

            // ACT
            WebClient actual = builder.ProvideChunkDownloadWebClient(offset, count);

            // ASSERT
            Assert.Equal(expected, actual, new WebClientComparer());
        }

        [Fact]
        public void Nodes_ProvideChunkUploadWebClient() {
            // ARRANGE
            int length = 54637;
            long offset = 5;
            string data = "someDataThings!", fileSize = "123";
            DracoonHttpConfig conf = new DracoonHttpConfig();
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            DracoonWebClientExtension expected = new DracoonWebClientExtension();
            expected.Headers.Add(HttpRequestHeader.ContentRange, "bytes " + offset + "-" + (offset + length) + "/" + fileSize);
            expected.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + data);
            expected.Headers.Add(HttpRequestHeader.UserAgent, conf.UserAgent);
            expected.SetHttpConfigParams(conf);

            // ACT
            WebClient actual = builder.ProvideChunkUploadWebClient(length, offset, data, fileSize);

            // ASSERT
            Assert.Equal(expected, actual, new WebClientComparer());
        }

        [Fact]
        public void Nodes_ProvideS3ChunkUploadWebClient() {
            // ARRANGE
            DracoonHttpConfig conf = new DracoonHttpConfig();
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            DracoonWebClientExtension expected = new DracoonWebClientExtension();
            expected.Headers.Add(HttpRequestHeader.UserAgent, conf.UserAgent);
            expected.SetHttpConfigParams(conf);

            // ACT
            WebClient actual = builder.ProvideS3ChunkUploadWebClient();

            // ASSERT
            Assert.Equal(expected, actual, new WebClientComparer());
        }

        [Fact]
        public void Nodes_PostGetS3Urls() {
            // ARRANGE
            string uploadId = "GH6D5";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.PostGetS3Urls(uploadId, FactoryFile.ApiGetS3UrlsRequest);

            // ACT
            IRestRequest actual = builder.PostGetS3Urls(uploadId, FactoryFile.ApiGetS3UrlsRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_GetS3Status() {
            // ARRANGE
            string uploadId = "GH6D5";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            IRestRequest expected = FactoryClients.RequestBuilderMock.GetS3Status(uploadId);

            // ACT
            IRestRequest actual = builder.GetS3Status(uploadId);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Nodes_PutCompleteS3FileUpload() {
            // ARRANGE
            string uploadId = "GH6D5";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPutCompleteS3Upload, Method.PUT);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryFile.ApiCompleteS3FileUpload), ParameterType.RequestBody);
            expected.AddUrlSegment("uploadId", uploadId);

            // ACT
            IRestRequest actual = builder.PutCompleteS3FileUpload("GH6D5", FactoryFile.ApiCompleteS3FileUpload);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region Share-Endpoint

        [Fact]
        public void Shares_GetDownloadShares() {
            // ARRANGE
            string fString = "name:cn:name_part", sString = "name:asc";
            long offset = 2, limit = 3;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDownloadShares, Method.GET);
            expected.AddQueryParameter("filter", fString);
            expected.AddQueryParameter("sort", sString);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            GetDownloadSharesFilter f = new GetDownloadSharesFilter();
            f.AddNameFilter(GetDownloadSharesFilter.Name.Contains("name_part").Build());
            IRestRequest actual = builder.GetDownloadShares(offset, limit, f, SharesSort.Name.Ascending());

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_GetDownloadShares_NoFilter_NoSort() {
            // ARRANGE
            long offset = 2, limit = 3;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDownloadShares, Method.GET);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetDownloadShares(offset, limit);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_GetUploadShares() {
            // ARRANGE
            string fString = "name:cn:name_part_up", sString = "name:desc";
            long offset = 4, limit = 5;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUploadShares, Method.GET);
            expected.AddQueryParameter("filter", fString);
            expected.AddQueryParameter("sort", sString);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            GetUploadSharesFilter f = new GetUploadSharesFilter();
            f.AddNameFilter(GetUploadSharesFilter.Name.Contains("name_part_up").Build());
            IRestRequest actual = builder.GetUploadShares(offset, limit, f, SharesSort.Name.Descending());

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_GetUploadShares_NoFilter_NoSort() {
            // ARRANGE
            long offset = 4, limit = 5;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetUploadShares, Method.GET);
            expected.AddQueryParameter("offset", offset.ToString());
            expected.AddQueryParameter("limit", limit.ToString());

            // ACT
            IRestRequest actual = builder.GetUploadShares(offset, limit);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_PostCreateDownloadShare() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCreateDownloadShare, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryShare.ApiCreateDownloadShareRequest),
                ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostCreateDownloadShare(FactoryShare.ApiCreateDownloadShareRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_PostCreateUploadShare() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiPostCreateUploadShare, Method.POST);
            expected.AddParameter("application/json", JsonConvert.SerializeObject(FactoryShare.ApiCreateUploadShareRequest),
                ParameterType.RequestBody);

            // ACT
            IRestRequest actual = builder.PostCreateUploadShare(FactoryShare.ApiCreateUploadShareRequest);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_DeleteDownloadShare() {
            // ARRANGE
            long id = 893756;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteDownloadShare, Method.DELETE);
            expected.AddUrlSegment("shareId", id);

            // ACT
            IRestRequest actual = builder.DeleteDownloadShare(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Shares_DeleteUploadShare() {
            // ARRANGE
            long id = 893756;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiDeleteUploadShare, Method.DELETE);
            expected.AddUrlSegment("shareId", id);

            // ACT
            IRestRequest actual = builder.DeleteUploadShare(id);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region OAuthMock-Endpoint

        [Fact]
        public void OAuth_PostOAuthToken() {
            // ARRANGE
            string clientId = "GS36SG653FD", clientSecret = "H7BD5D6G", grand = "pw", code = "ty";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithoutAuth(OAuthConfig.OAuthPostAuthToken, Method.POST);
            expected.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));
            expected.AddParameter("grant_type", grand, ParameterType.GetOrPost);
            expected.AddParameter("code", code, ParameterType.GetOrPost);

            // ACT
            IRestRequest actual = builder.PostOAuthToken(clientId, clientSecret, grand, code);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void OAuth_PostOAuthRefresh() {
            // ARRANGE
            string clientId = "GS36SG653FDAY", clientSecret = "H7BD5D6G56", grand = "pw", refreshToken = "things";
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithoutAuth(OAuthConfig.OAuthPostRefreshToken, Method.POST);
            expected.AddHeader("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));
            expected.AddParameter("grant_type", grand, ParameterType.GetOrPost);
            expected.AddParameter("refresh_token", refreshToken, ParameterType.GetOrPost);

            // ACT
            IRestRequest actual = builder.PostOAuthRefresh(clientId, clientSecret, grand, refreshToken);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region Config-Endpoint

        [Fact]
        public void Config_GetGeneralSettings() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET);

            // ACT
            IRestRequest actual = builder.GetGeneralSettings();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Config_GetInfrastructureSettings() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetInfrastructureConfig, Method.GET);

            // ACT
            IRestRequest actual = builder.GetInfrastructureSettings();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Config_GetDefaultsSettings() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDefaultsConfig, Method.GET);

            // ACT
            IRestRequest actual = builder.GetDefaultsSettings();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        [Fact]
        public void Config_GetAlgorithms() {
            // ARRANGE
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAlgorithms, Method.GET);

            // ACT
            IRestRequest actual = builder.GetAlgorithms();

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion

        #region Resources-Endpoint

        [Fact]
        public void Resources_GetUserAvatar() {
            // ARRANGE
            string avatarUuid = "JG7DM2DZ6";
            long userId = 167890;
            IRequestBuilder builder = new DracoonRequestBuilder(FactoryClients.OAuthMock);
            RestRequest expected = FactoryRestSharp.RestRequestWithoutAuth(ApiConfig.ApiResourcesGetAvatar, Method.GET);
            expected.AddUrlSegment("userId", userId);
            expected.AddUrlSegment("uuid", avatarUuid);

            // ACT
            IRestRequest actual = builder.GetUserAvatar(userId, avatarUuid);

            // ASSERT
            Assert.Equal(expected, actual, new RestRequestComparer());
        }

        #endregion
    }
}