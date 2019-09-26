using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiUpdateFileRequest {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("classification", NullValueHandling = NullValueHandling.Ignore)]
        public int? Classification { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration { get; set; }
    }
}