using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiFileKey {
        [JsonProperty("key", NullValueHandling = NullValueHandling.Ignore)]
        public string Key {
            get; set;
        }
        [JsonProperty("iv", NullValueHandling = NullValueHandling.Ignore)]
        public string Iv {
            get; set;
        }
        [JsonProperty("tag", NullValueHandling = NullValueHandling.Ignore)]
        public string Tag {
            get; set;
        }
        [JsonProperty("version", NullValueHandling = NullValueHandling.Ignore)]
        public string Version {
            get; set;
        }
    }
}
