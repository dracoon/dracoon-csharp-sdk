using Newtonsoft.Json;
using System.ComponentModel;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiInfrastructureSettings {
        [JsonProperty("smsConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool SmsConfigEnabled {
            get; set;
        }
        [JsonProperty("mediaServerConfigEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool MediaServerConfigEnabled {
            get; set;
        }
        [JsonProperty("s3DefaultRegion", NullValueHandling = NullValueHandling.Ignore)]
        public string S3DefaultRegion {
            get; set;
        }
        [DefaultValue(false)]
        [JsonProperty("s3EnforceDirectUpload", DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool S3EnforceDirectUpload {
            get; set;
        }
    }
}
