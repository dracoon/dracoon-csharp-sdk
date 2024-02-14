using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.OAuth {
    public class OAuthClientTest {
        [Fact]
        public void BuildAuthString() {
            // ARRANGE
            string expected = "Bearer " + FactoryUser.ApiOAuthToken.AccessToken;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IOAuth oa = new OAuthClient(c, new DracoonAuth("id1", "secret1", "code1"));
            Mock.Arrange(() => c.Builder.PostOAuthToken(Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyString)).Returns(FactoryRestSharp.PostOAuthTokenMock("id1", "secret1", "grant", "code1")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiOAuthToken>(Arg.IsAny<RestRequest>(), RequestType.PostOAuthToken, 0)).Returns(FactoryUser.ApiOAuthToken).Occurs(1);

            // ACT
            string actual = oa.BuildAuthString();

            // ASSERT
            Assert.Equal(expected, actual);
            Assert.Equal(DracoonAuth.Mode.ACCESS_REFRESH_TOKEN, oa.Auth.UsedMode);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void RefreshAccessToken() {
            // ARRANGE
            DracoonAuth expected = new DracoonAuth("id1", "secret1", FactoryUser.ApiOAuthToken.AccessToken, FactoryUser.ApiOAuthToken.RefreshToken);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IOAuth oa = new OAuthClient(c, new DracoonAuth("id1", "secret1", "tokenInit", "refreshInit"));
            Mock.Arrange(() => c.Builder.PostOAuthRefresh(Arg.AnyString, Arg.AnyString, Arg.AnyString, Arg.AnyString)).Returns(FactoryRestSharp.PostOAuthRefreshMock("id1", "secret1", "grant", "token1")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiOAuthToken>(Arg.IsAny<RestRequest>(), RequestType.PostOAuthRefresh, 0)).Returns(FactoryUser.ApiOAuthToken).Occurs(1);

            // ACT
            oa.RefreshAccessToken();

            // ASSERT
            Assert.Equal(expected.AccessToken, oa.Auth.AccessToken);
            Assert.Equal(expected.RefreshToken, oa.Auth.RefreshToken);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }
    }
}