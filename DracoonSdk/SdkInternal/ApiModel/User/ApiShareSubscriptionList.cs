using Dracoon.Sdk.SdkInternal.ApiModel;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.User {
    internal class ApiShareSubscriptionList {
        [JsonProperty("range", NullValueHandling = NullValueHandling.Ignore)]
        public ApiRange Range { get; set; }
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiShareSubscription> Items { get; set; }
    }
}
