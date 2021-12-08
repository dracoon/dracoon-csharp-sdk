using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiPasswordExpiration {

        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enabled { get; set; }

        [JsonProperty("maxPasswordAge", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxDaysPasswordAge { get; set; }
    }
}
