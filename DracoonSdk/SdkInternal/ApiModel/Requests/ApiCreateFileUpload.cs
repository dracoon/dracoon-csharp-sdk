using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateFileUpload {
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long ParentId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("classification", NullValueHandling = NullValueHandling.Ignore)]
        public int? Classification { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration {
            get; set;
        }
        [JsonProperty("directS3Upload", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UseS3 {
            get; set;
        }
    }
}