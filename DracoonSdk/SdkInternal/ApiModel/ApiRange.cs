using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiRange {
        [JsonProperty("offset", NullValueHandling = NullValueHandling.Ignore)]
        public long Offset { get; set; }

        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public long Limit { get; set; }

        [JsonProperty("total", NullValueHandling = NullValueHandling.Ignore)]
        public long Total { get; set; }
    }
}