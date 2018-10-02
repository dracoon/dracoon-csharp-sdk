using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class ApiOAuthToken {
        [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessToken {
            get; set;
        }
        [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
        public string TokenType {
            get; set;
        }
        [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
        public string RefreshToken {
            get; set;
        }
        [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
        public long ExpiresIn {
            get; set;
        }
        [JsonProperty("scope", NullValueHandling = NullValueHandling.Ignore)]
        public string Scope {
            get; set;
        }
    }
}
