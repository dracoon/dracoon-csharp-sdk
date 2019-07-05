using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiS3Status {
        [JsonProperty("status", NullValueHandling = NullValueHandling.Ignore)]
        public string Status { get; set; }

        [JsonProperty("node", NullValueHandling = NullValueHandling.Ignore)]
        public ApiNode Node { get; set; }
    }
}