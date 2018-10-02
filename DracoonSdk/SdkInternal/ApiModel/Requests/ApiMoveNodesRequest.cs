using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiMoveNodesRequest {
        [JsonProperty("items", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiMoveNode> Nodes {
            get; set;
        }
        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy {
            get; set;
        }
        [JsonProperty("keepShareLinks", NullValueHandling = NullValueHandling.Ignore)]
        public bool KeepShareLinks {
            get; set;
        }
    }
}
