using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserProfileAttributeList {
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRange Range { get; set; }
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserProfileAttribute> Items { get; set; }
    }
}