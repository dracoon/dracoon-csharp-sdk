using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using RestSharp;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonServerSettingsImplTest {
        #region GetDefault

        [Fact]
        public void GetDefault() {
            // ARRANGE
            ServerDefaultSettings expected = FactoryServerSettings.ServerDefaultSettings;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Builder.GetDefaultsSettings()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetDefaultsConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDefaultsSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetDefaultsSettings, 0)).Returns(FactoryServerSettings.ApiDefaultsSettings).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiDefaultsSettings(Arg.IsAny<ApiDefaultsSettings>())).Returns(FactoryServerSettings.ServerDefaultSettings).Occurs(1);
            
            // ACT
            ServerDefaultSettings actual = ss.GetDefault();

            // ASSERT
            Assert.Equal(expected, actual, new ServerDefaultSettingsComparer());
            Mock.Assert(() => SettingsMapper.FromApiDefaultsSettings(Arg.IsAny<ApiDefaultsSettings>()));
            Mock.Assert(c.Executor);
            Mock.Assert(c.Builder);
        }

        #endregion

        #region GetGeneral

        [Fact]
        public void GetGeneral() {
            // ARRANGE
            ServerGeneralSettings expected = FactoryServerSettings.ServerGeneralSettings;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Builder.GetGeneralSettings()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0)).Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiGeneralSettings(Arg.IsAny<ApiGeneralSettings>())).Returns(FactoryServerSettings.ServerGeneralSettings).Occurs(1);

            // ACT
            ServerGeneralSettings actual = ss.GetGeneral();

            // ASSERT
            Assert.Equal(expected, actual, new ServerGeneralSettingsComparer());
            Mock.Assert(() => SettingsMapper.FromApiGeneralSettings(Arg.IsAny<ApiGeneralSettings>()));
            Mock.Assert(c.Executor);
            Mock.Assert(c.Builder);
        }

        #endregion

        #region GetInfrastructure

        [Fact]
        public void GetInfrastructure() {
            // ARRANGE
            ServerInfrastructureSettings expected = FactoryServerSettings.ServerInfrastructureSettings;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Builder.GetInfrastructureSettings()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetInfrastructureConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiInfrastructureSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetInfrastructureSettings, 0))
                    .Returns(FactoryServerSettings.ApiInfrastructureSettings).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiInfrastructureSettings(Arg.IsAny<ApiInfrastructureSettings>()))
                .Returns(FactoryServerSettings.ServerInfrastructureSettings).Occurs(1);

            // ACT
            ServerInfrastructureSettings actual = ss.GetInfrastructure();

            // ASSERT
            Assert.Equal(expected, actual, new ServerInfrastructureSettingsComparer());
            Mock.Assert(() => SettingsMapper.FromApiInfrastructureSettings(Arg.IsAny<ApiInfrastructureSettings>()));
            Mock.Assert(c.Executor);
            Mock.Assert(c.Builder);
        }

        #endregion
    }
}