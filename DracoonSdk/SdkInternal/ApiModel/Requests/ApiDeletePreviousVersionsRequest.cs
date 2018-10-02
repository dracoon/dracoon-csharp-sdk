using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiDeletePreviousVersionsRequest {
        [JsonProperty("deletedNodeIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> VersionsToBeDeleted {
            get; set;
        }
    }
}
