using System;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.Validator;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test {
    public class DracoonClientTest {
        [Fact]
        public void DracoonClient_Ctor() {
            // ARRANGE
            Uri expectedUri = new Uri("https://dracoon.team");
            DracoonAuth expectedAuth = new DracoonAuth("token");
            string expectedEncryptionPassword = "encPw1";
            EmptyLog expectedLog = new EmptyLog();
            DracoonHttpConfig expectedConfig = new DracoonHttpConfig(true);
            Mock.Arrange(() => Arg.IsAny<Uri>().MustBeValid(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT
            DracoonClient dc = new DracoonClient(expectedUri, null, expectedEncryptionPassword, expectedLog, expectedConfig);
            IInternalDracoonClient dcInternal = dc;

            // ASSERT
            Assert.Equal(expectedUri, dc.ServerUri);
            Assert.Null(dc.Auth);
            dc.Auth = expectedAuth;
            Assert.Equal(expectedAuth, dc.Auth);
            Assert.Equal(expectedEncryptionPassword, dc.EncryptionPassword);
            Assert.Equal(expectedConfig, DracoonClient.HttpConfig);
            Assert.Equal(expectedLog, DracoonClient.Log);
            Assert.NotNull(dc.Nodes);
            Assert.NotNull(dc.Account);
            Assert.NotNull(dc.Server);
            Assert.NotNull(dc.Shares);
            Assert.NotNull(dc.Users);
            Assert.NotNull(dcInternal.Executor);
            Assert.NotNull(dcInternal.Builder);
            Assert.NotNull(dcInternal.OAuth);
            Assert.NotNull(dcInternal.NodesImpl);
            Assert.NotNull(dcInternal.AccountImpl);
            Assert.NotNull(dcInternal.ServerImpl);
            Assert.NotNull(dcInternal.SharesImpl);
            Assert.NotNull(dcInternal.UsersImpl);
            Mock.Assert(() => Arg.IsAny<Uri>().MustBeValid(Arg.AnyString));
        }
    }
}