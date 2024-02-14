using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using System;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonServerImplTest {
        #region GetVersion

        [Fact]
        public void GetVersion() {
            // ARRANGE
            string expected = "4.13.0";
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerImpl s = new DracoonServerImpl(c);
            Mock.Arrange(() => c.Builder.GetServerVersion()).Returns(FactoryRestSharp.RestRequestWithoutAuth(ApiConfig.ApiGetServerVersion, Method.Get)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiServerVersion>(Arg.IsAny<RestRequest>(), RequestType.GetServerVersion, 0)).Returns(FactoryServer.ApiServerVersionMock).Occurs(1);

            // ACT
            string actual = s.GetVersion();

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetTime

        [Fact]
        public void GetTime() {
            // ARRANGE
            DateTime expected = new DateTime(2000, 1, 1, 0, 0, 0);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerImpl s = new DracoonServerImpl(c);
            Mock.Arrange(() => c.Builder.GetServerTime()).Returns(FactoryRestSharp.RestRequestWithoutAuth(ApiConfig.ApiGetServerTime, Method.Get)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiServerTime>(Arg.IsAny<RestRequest>(), RequestType.GetServerTime, 0)).Returns(FactoryServer.ApiServerTimeMock).Occurs(1);

            // ACT
            DateTime? actual = s.GetTime();

            // ASSERT
            Assert.True(actual.HasValue);
            Assert.Equal(expected, actual.Value);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region ServerSettings

        [Fact]
        public void ServerSettingsProperty() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerImpl s = new DracoonServerImpl(c);

            // ACT

            // ASSERT
            Assert.NotNull(s.ServerSettings);
        }

        #endregion

        #region ServerPolices

        [Fact]
        public void ServerPoliciesProperty() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerImpl s = new DracoonServerImpl(c);

            // ACT

            // ASSERT
            Assert.NotNull(s.ServerPolicies);
        }

        #endregion
    }
}