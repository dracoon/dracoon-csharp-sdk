using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiS3Url {
        [JsonProperty("url", NullValueHandling = NullValueHandling.Ignore)]
        public string Url {
            get; set;
        }
        [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PartNumber {
            get; set;
        }
    }
}
