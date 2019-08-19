using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDeletedNodeVersionsList {
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRange Range { get; set; }

        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiDeletedNodeVersion> Items { get; set; }
    }
}