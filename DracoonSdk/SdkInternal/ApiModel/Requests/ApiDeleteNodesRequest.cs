using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiDeleteNodesRequest {
        [JsonProperty("nodeIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> NodeIds { get; set; }
    }
}