using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiClassificationPolicies {
        [JsonProperty("shareClassificationPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiShareClassificationPolicy SharePolicy { get; set; }
    }
}
