using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserPublicKey {
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version {
            get; set;
        }
        [JsonProperty("publicKey", NullValueHandling = NullValueHandling.Ignore)]
        public string PublicKey {
            get; set;
        }
    }
}
