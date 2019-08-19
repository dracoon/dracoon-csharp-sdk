using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserIdPublicKey {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long UserId { get; set; }

        [JsonProperty("publicKeyContainer", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserPublicKey PublicKeyContainer { get; set; }
    }
}