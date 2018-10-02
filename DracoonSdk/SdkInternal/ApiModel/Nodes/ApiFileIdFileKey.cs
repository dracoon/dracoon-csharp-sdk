using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiFileIdFileKey {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long FileId {
            get; set;
        }
        [JsonProperty("fileKeyContainer", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKeyContainer {
            get; set;
        }
    }
}
