using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUploadToken {
        [JsonProperty("uploadUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string UploadUrl {
            get; set;
        }
    }
}
