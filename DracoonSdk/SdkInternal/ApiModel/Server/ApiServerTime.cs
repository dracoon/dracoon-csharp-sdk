using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiServerTime {
        [JsonProperty("time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? Time { get; set; }
    }
}