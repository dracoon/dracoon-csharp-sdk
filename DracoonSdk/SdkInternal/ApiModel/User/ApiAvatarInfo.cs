using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiAvatarInfo {
        [JsonProperty("avatarUri", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUri { get; set; }

        [JsonProperty("avatarUuid", NullValueHandling = NullValueHandling.Ignore)]
        public string AvatarUuid { get; set; }

        [JsonProperty("isCustomAvatar", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsCustomAvatar { get; set; }
    }
}