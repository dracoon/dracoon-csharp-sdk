using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiGetS3Urls {
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long Size {
            get; set;
        }
        [JsonProperty("firstPartNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int FirstPartNumber {
            get; set;
        }
        [JsonProperty("lastPartNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int LastPartNumber {
            get; set;
        }
    }
}
