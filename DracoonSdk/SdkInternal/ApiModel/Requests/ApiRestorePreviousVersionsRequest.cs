using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiRestorePreviousVersionsRequest {
        [JsonProperty("deletedNodeIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> DeletedNodeIds { get; set; }

        [JsonProperty("resolutionStrategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ResolutionStrategy { get; set; }

        [JsonProperty("keepShareLinks", NullValueHandling = NullValueHandling.Ignore)]
        public bool KeepShareLinks { get; set; }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId { get; set; }
    }
}