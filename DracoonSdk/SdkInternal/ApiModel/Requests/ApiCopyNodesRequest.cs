using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCopyNodesRequest {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiCopyNode> Nodes { get; set; }

        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy { get; set; }

        [JsonProperty("keepShareLinks", NullValueHandling = NullValueHandling.Ignore)]
        public bool KeepShareLinks { get; set; }
    }
}