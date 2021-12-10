using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCopyNode {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string NewName { get; set; }

        [JsonProperty("timestampCreation", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampCreation { get; set; }

        [JsonProperty("timestampModification", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampModification { get; set; }
    }
}