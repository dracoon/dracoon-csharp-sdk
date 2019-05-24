using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCompleteFileUpload {
        [JsonProperty("fileName", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy { get; set; }

        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey {
            get; set;
        }
        [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
        public List<int> PartNumber {
            get; set;
        }
        [JsonProperty("partEtag", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> PartEtags {
            get; set;
        }
    }
}