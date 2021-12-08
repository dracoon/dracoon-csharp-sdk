using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiPasswordPolicies {
        [JsonProperty("loginPasswordPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiLoginPasswordPolicy LoginPasswordSettings { get; set; }

        [JsonProperty("sharesPasswordPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiSharePasswordPolicy SharePasswordSettings { get; set; }

        [JsonProperty("encryptionPasswordPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiEncryptionPasswordPolicy EncryptionPasswordSettings { get; set; }
    }
}