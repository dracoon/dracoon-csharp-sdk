using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiUpdateFolderRequest {
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
