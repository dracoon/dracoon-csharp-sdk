using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using RestSharp;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonServerPoliciesImpl : IServerPolicies {
        internal const string Logtag = nameof(DracoonServerSettingsImpl);
        private readonly IInternalDracoonClient _client;


        internal DracoonServerPoliciesImpl(IInternalDracoonClient client) {
            _client = client;
        }

        public PasswordEncryptionPolicies GetEncryptionPasswordPolicies() {
            _client.Executor.CheckApiServerVersion();
            RestRequest request = _client.Builder.GetPasswordPolicies();
            ApiPasswordPolicies apiPasswordPolicies =
                _client.Executor.DoSyncApiCall<ApiPasswordPolicies>(request, DracoonRequestExecutor.RequestType.GetPasswordPolicies);
            return SettingsMapper.FromApiPasswordEncryptionPolicies(apiPasswordPolicies.EncryptionPasswordSettings);
        }

        public PasswordSharePolicies GetSharesPasswordPolicies() {
            _client.Executor.CheckApiServerVersion();
            RestRequest request = _client.Builder.GetPasswordPolicies();
            ApiPasswordPolicies apiPasswordPolicies =
                _client.Executor.DoSyncApiCall<ApiPasswordPolicies>(request, DracoonRequestExecutor.RequestType.GetPasswordPolicies);
            return SettingsMapper.FromApiPasswordSharePolicies(apiPasswordPolicies.SharePasswordSettings);
        }

        public ClassificationPolicies GetClassificationPolicies() {
            _client.Executor.CheckApiServerVersion();

            RestRequest request = _client.Builder.GetClassificationPolicies();
            ApiClassificationPolicies apiClassificationPolicies = _client.Executor.DoSyncApiCall<ApiClassificationPolicies>(request, DracoonRequestExecutor.RequestType.GetClassificationPolicies);
            return SettingsMapper.FromApiClassificationPolicies(apiClassificationPolicies);
        }
    }
}
