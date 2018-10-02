using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiExpiration {
        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt {
            get; set;
        }
        [JsonProperty("enableExpiration", NullValueHandling = NullValueHandling.Ignore)]
        public bool EnableExpiration {
            get; set;
        }
    }
}
