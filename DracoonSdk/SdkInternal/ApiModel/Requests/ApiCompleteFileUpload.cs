using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCompleteFileUpload {
        [JsonProperty("fileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy { get; set; }

        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey { get; set; }
    }
}