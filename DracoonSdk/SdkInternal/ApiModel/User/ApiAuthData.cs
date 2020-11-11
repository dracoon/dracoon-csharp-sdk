using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiAuthData {
        [JsonProperty("method", NullValueHandling = NullValueHandling.Ignore)]
        public string Method { get; set; }
        [JsonProperty("login", NullValueHandling = NullValueHandling.Ignore)]
        public string Login { get; set; }
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
        [JsonProperty("mustChangePassword", NullValueHandling = NullValueHandling.Ignore)]
        public bool? MustChangePassword { get; set; }
        [JsonProperty("adConfigId", NullValueHandling = NullValueHandling.Ignore)]
        public int? ADConfigId { get; set; }
        [JsonProperty("oidConfigId", NullValueHandling = NullValueHandling.Ignore)]
        public int? OIDConfigId { get; set; }
    }
}
