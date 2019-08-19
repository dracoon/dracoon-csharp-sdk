using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiMissingFileKeys {
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRange Range { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserIdFileId> Items { get; set; }

        [JsonProperty("users", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserIdPublicKey> UserPublicKey { get; set; }

        [JsonProperty("files", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiFileIdFileKey> FileKeys { get; set; }
    }
}