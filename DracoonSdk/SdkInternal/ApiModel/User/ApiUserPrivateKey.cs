using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserPrivateKey {
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version { get; set; }

        [JsonProperty("privateKey", NullValueHandling = NullValueHandling.Ignore)]
        public string PrivateKey { get; set; }
    }
}