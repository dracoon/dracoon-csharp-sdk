using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCompleteFileUpload {
        [JsonProperty("fileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy { get; set; }

        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey { get; set; }

        [JsonProperty("parts", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiS3FileUploadPart> Parts { get; set; }

        [JsonProperty("isPrioritisedVirusScan", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsPrioritisedVirusScan { get; set; }
    }
}