using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiFileKeyList {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiFileKey> Items {
            get; set;
        }
    }
}
