using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateFolderRequest {
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long ParentId {
            get; set;
        }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name {
            get; set;
        }
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes {
            get; set;
        }
    }
}
