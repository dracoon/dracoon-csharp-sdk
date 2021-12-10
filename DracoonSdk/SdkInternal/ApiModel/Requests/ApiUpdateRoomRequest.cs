using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiUpdateRoomRequest {
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quota { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("timestampCreation", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampCreation { get; set; }

        [JsonProperty("timestampModification", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampModification { get; set; }
    }
}