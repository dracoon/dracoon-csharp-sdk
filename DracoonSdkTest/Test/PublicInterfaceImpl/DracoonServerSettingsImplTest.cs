using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Settings;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using RestSharp;
using System.Collections.Generic;
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

        #region GetAlgorithms

        [Fact]
        public void GetUserKeyPairAlgorithms() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Builder.GetAlgorithms()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAlgorithms, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAlgorithms>(Arg.IsAny<IRestRequest>(), RequestType.GetAlgorithms, 0))
                .Returns(FactoryServerSettings.ApiAlgorithms).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiUserKeyPairAlgorithms(Arg.IsAny<List<ApiAlgorithm>>()))
                .Returns(FactoryServerSettings.UserKeyPairAlgorithms).Occurs(1);

            // ACT
            List<UserKeyPairAlgorithmData> actual = ss.GetAvailableUserKeyPairAlgorithms();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => SettingsMapper.FromApiUserKeyPairAlgorithms(Arg.IsAny<List<ApiAlgorithm>>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void GetUserKeyPairAlgorithms_NotSupportedAlgorithms() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Executor.CheckApiServerVersion(Arg.AnyString)).Throws(new DracoonApiException()).Occurs(1);

            // ACT
            List<UserKeyPairAlgorithmData> actual = ss.GetAvailableUserKeyPairAlgorithms();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(actual.Count == 1);
            Mock.Assert(actual[0].Algorithm == Crypto.Sdk.UserKeyPairAlgorithm.RSA2048);
            Mock.Assert(() => c.Executor.CheckApiServerVersion(Arg.AnyString));
        }

        [Fact]
        public void GetFileKeyAlgorithms() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Builder.GetAlgorithms()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetAlgorithms, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAlgorithms>(Arg.IsAny<IRestRequest>(), RequestType.GetAlgorithms, 0))
                .Returns(FactoryServerSettings.ApiAlgorithms).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiFileKeyAlgorithms(Arg.IsAny<List<ApiAlgorithm>>()))
                .Returns(FactoryServerSettings.FileKeyAlgorithms).Occurs(1);

            // ACT
            List<FileKeyAlgorithmData> actual = ss.GetAvailableFileKeyAlgorithms();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => SettingsMapper.FromApiFileKeyAlgorithms(Arg.IsAny<List<ApiAlgorithm>>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void GetFileKeyAlgorithms_NotSupportedAlgorithms() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerSettingsImpl ss = new DracoonServerSettingsImpl(c);
            Mock.Arrange(() => c.Executor.CheckApiServerVersion(Arg.AnyString)).Throws(new DracoonApiException()).Occurs(1);

            // ACT
            List<FileKeyAlgorithmData> actual = ss.GetAvailableFileKeyAlgorithms();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(actual.Count == 1);
            Mock.Assert(actual[0].Algorithm == Crypto.Sdk.EncryptedFileKeyAlgorithm.RSA2048_AES256GCM);
            Mock.Assert(() => c.Executor.CheckApiServerVersion(Arg.AnyString));
        }

        #endregion
    }
}