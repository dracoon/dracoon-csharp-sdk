using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiGenerateVirusProtectionInfoRequest {
        [JsonProperty("nodeIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> FileIds { get; set; }
    }
}