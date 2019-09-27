using System;
using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiEncryptionPasswordSettings {
        [JsonProperty("characterRules", NullValueHandling = NullValueHandling.Ignore)]
        public ApiCharacterRules CharacterRules { get; set; }

        [JsonProperty("minLength", NullValueHandling = NullValueHandling.Ignore)]
        public int MinimumPasswordLength { get; set; }

        [JsonProperty("rejectUserInfo", NullValueHandling = NullValueHandling.Ignore)]
        public bool RejectUserInfo { get; set; }

        [JsonProperty("rejectKeyboardPatterns", NullValueHandling = NullValueHandling.Ignore)]
        public bool RejectKeyboardPatterns { get; set; }

        [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo UpdatedBy { get; set; }
    }
}