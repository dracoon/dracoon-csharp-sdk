using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using System;
using System.Net;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonUsersImplTest {
        #region GetUserAvatar

        [Fact]
        public void GetUserAvatar() {
            // ARRANGE
            byte[] randomBitmap = new byte[16];
            new Random().NextBytes(randomBitmap);

            long id = 5;
            string uuid = "H7D68J";
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonUsersImpl u = new DracoonUsersImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetUserAvatar(Arg.AnyLong, Arg.AnyString)).Returns(FactoryRestSharp.GetUserAvatarMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAvatarInfo>(Arg.IsAny<RestRequest>(), RequestType.GetResourcesAvatar, 0))
                    .Returns(FactoryUser.ApiAvatarInfo).Occurs(1);
            Mock.Arrange(() => c.Builder.ProvideAvatarDownloadWebClient()).Returns(() => {
                DracoonWebClientExtension wc = new DracoonWebClientExtension();
                wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                wc.SetHttpConfigParams(new DracoonHttpConfig());
                return wc;
            }).Occurs(1);
            Mock.Arrange(() => c.Executor.ExecuteWebClientDownload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), RequestType.GetResourcesAvatar, null, 0))
                .Returns(randomBitmap).Occurs(1);


            // ACT
            byte[] actual = u.GetUserAvatar(id, uuid);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion
    }
}