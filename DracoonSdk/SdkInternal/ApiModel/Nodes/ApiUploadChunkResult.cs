using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUploadChunkResult {
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size {
            get; set;
        }
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash {
            get; set;
        }
    }
}
