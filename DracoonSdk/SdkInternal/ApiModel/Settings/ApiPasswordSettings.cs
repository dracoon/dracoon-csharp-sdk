﻿using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiPasswordSettings {
        [JsonProperty("sharesPasswordPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiSharePasswordSettings SharePasswordSettings { get; set; }
        [JsonProperty("encryptionPasswordPolicies", NullValueHandling = NullValueHandling.Ignore)]
        public ApiEncryptionPasswordSettings EncryptionPasswordSettings { get; set; }
    }
}