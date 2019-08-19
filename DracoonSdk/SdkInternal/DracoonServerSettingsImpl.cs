using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using RestSharp;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonServerSettingsImpl : IServerSettings {
        internal const string Logtag = nameof(DracoonServerSettingsImpl);
        private readonly IInternalDracoonClient _client;

        internal DracoonServerSettingsImpl(IInternalDracoonClient client) {
            _client = client;
        }

        public ServerDefaultSettings GetDefault() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetDefaultsSettings();
            ApiDefaultsSettings apiDefaultsSettings =
                _client.Executor.DoSyncApiCall<ApiDefaultsSettings>(request, DracoonRequestExecutor.RequestType.GetDefaultsSettings);
            return SettingsMapper.FromApiDefaultsSettings(apiDefaultsSettings);
        }

        public ServerGeneralSettings GetGeneral() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetGeneralSettings();
            ApiGeneralSettings apiGeneralSettings =
                _client.Executor.DoSyncApiCall<ApiGeneralSettings>(request, DracoonRequestExecutor.RequestType.GetGeneralSettings);
            return SettingsMapper.FromApiGeneralSettings(apiGeneralSettings);
        }

        public ServerInfrastructureSettings GetInfrastructure() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetInfrastructureSettings();
            ApiInfrastructureSettings apiInfrastructureSettings =
                _client.Executor.DoSyncApiCall<ApiInfrastructureSettings>(request, DracoonRequestExecutor.RequestType.GetInfrastructureSettings);
            return SettingsMapper.FromApiInfrastructureSettings(apiInfrastructureSettings);
        }
    }
}