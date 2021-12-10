using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiAddOrUpdateAttributeRequest {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiAttribute> Items { get; set; }
    }
}