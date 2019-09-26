using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDownloadToken {
        [JsonProperty("downloadUrl", NullValueHandling = NullValueHandling.Ignore)]
        public string DownloadUrl { get; set; }
    }
}