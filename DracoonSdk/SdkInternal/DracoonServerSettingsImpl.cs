using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using RestSharp;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonServerSettingsImpl : IServerSettings {

        internal static readonly string LOGTAG = typeof(DracoonServerSettingsImpl).Name;
        private DracoonClient client;
        internal DracoonServerSettingsImpl(DracoonClient client) {
            this.client = client;
        }

        public ServerDefaultSettings GetDefault() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetDefaultsSettings();
            ApiDefaultsSettings apiDefaultsSettings = client.RequestExecutor.DoSyncApiCall<ApiDefaultsSettings>(request, DracoonRequestExecuter.RequestType.GetDefaultsSettings);
            return SettingsMapper.FromApiDefaultsSettings(apiDefaultsSettings);
        }

        public ServerGeneralSettings GetGeneral() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetGeneralSettings();
            ApiGeneralSettings apiGeneralSettings = client.RequestExecutor.DoSyncApiCall<ApiGeneralSettings>(request, DracoonRequestExecuter.RequestType.GetGeneralSettings);
            return SettingsMapper.FromApiGeneralSettings(apiGeneralSettings);
        }

        public ServerInfrastructureSettings GetInfrastructure() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetInfrastructureSettings();
            ApiInfrastructureSettings apiInfrastructureSettings = client.RequestExecutor.DoSyncApiCall<ApiInfrastructureSettings>(request, DracoonRequestExecuter.RequestType.GetInfrastructureSettings);
            return SettingsMapper.FromApiInfrastructureSettings(apiInfrastructureSettings);
        }


    }
}
