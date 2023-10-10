using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.User {
    internal class ApiShareSubscription {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long ShareId { get; set; }
        [JsonProperty("authParentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? AuthParentId { get; set; }
    }
}
