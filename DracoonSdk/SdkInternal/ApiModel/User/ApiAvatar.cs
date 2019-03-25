using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiAvatar {
        [JsonProperty("avatarUri", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUri {
            get; set;
        }
        [JsonProperty("isCustomAvatar", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCustomAvatar {
            get; set;
        }
    }
}
