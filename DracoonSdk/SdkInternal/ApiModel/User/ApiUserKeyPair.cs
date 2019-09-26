using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserKeyPair {
        [JsonProperty("privateKeyContainer", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserPrivateKey PrivateKeyContainer { get; set; }

        [JsonProperty("publicKeyContainer", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserPublicKey PublicKeyContainer { get; set; }
    }
}