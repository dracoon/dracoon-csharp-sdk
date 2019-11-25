using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    public class ApiAddOrUpdateUserProfileAttributeRequest {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserProfileAttribute> Items { get; set; }
    }
}