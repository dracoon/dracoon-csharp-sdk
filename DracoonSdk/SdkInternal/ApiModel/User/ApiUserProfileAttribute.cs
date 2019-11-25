using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    public class ApiUserProfileAttribute {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key { get; set; }
        [JsonProperty("value", NullValueHandling = NullValueHandling.Ignore)]
        public string Value { get; set; }
    }
}