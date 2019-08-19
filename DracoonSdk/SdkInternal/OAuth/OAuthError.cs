using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthError {
        [JsonProperty("error", NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        [JsonProperty("error_description", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorDescription { get; set; }

        public override string ToString() {
            return typeof(OAuthError).Name + "{error=" + Error + ", description='" + ErrorDescription + "'}";
        }
    }
}