using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiInfrastructureSettings {
        [JsonProperty("smsConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool SmsConfigEnabled { get; set; }

        [JsonProperty("mediaServerConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool MediaServerConfigEnabled { get; set; }

        [JsonProperty("s3DefaultRegion", NullValueHandling = NullValueHandling.Ignore)]
        public string S3DefaultRegion { get; set; }

        [JsonProperty("s3EnforceDirectUpload", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool S3EnforceDirectUpload { get; set; }

        [JsonProperty("isDracoonCloud", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsDracoonCloud { get; set; }

        [JsonProperty("tenantUuid", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TenantUUID { get; set; }
    }
}
