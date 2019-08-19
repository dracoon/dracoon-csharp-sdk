using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiInfrastructureSettings {
        [JsonProperty("smsConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool SmsConfigEnabled { get; set; }

        [JsonProperty("mediaServerConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool MediaServerConfigEnabled { get; set; }

        [JsonProperty("s3DefaultRegion", NullValueHandling = NullValueHandling.Ignore)]
        public string S3DefaultRegion { get; set; }
    }
}