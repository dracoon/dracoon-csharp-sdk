using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserRoleList {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserRole> Items {
            get; set;
        }
    }
}
