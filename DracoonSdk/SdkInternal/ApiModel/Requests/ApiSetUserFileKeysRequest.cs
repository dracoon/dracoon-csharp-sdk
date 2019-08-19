using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiSetUserFileKeysRequest {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiSetUserFileKey> Items { get; set; }
    }
}