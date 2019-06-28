using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiS3FileUploadPart {
        [JsonProperty("partNumber", NullValueHandling = NullValueHandling.Ignore)]
        public int PartNumber { get; set; }

        [JsonProperty("partEtag", NullValueHandling = NullValueHandling.Ignore)]
        public string PartEtag { get; set; }
    }
}