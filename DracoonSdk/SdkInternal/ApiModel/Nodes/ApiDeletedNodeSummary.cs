using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDeletedNodeSummary {
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long ParentId { get; set; }

        [JsonProperty("parentPath", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPath { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("cntVersions", NullValueHandling = NullValueHandling.Ignore)]
        public int CntVersions { get; set; }

        [JsonProperty("firstDeletedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime FirstDeletedAt { get; set; }

        [JsonProperty("lastDeletedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastDeletedAt { get; set; }

        [JsonProperty("lastDeletedNodeId", NullValueHandling = NullValueHandling.Ignore)]
        public long LastDeletedNodeId { get; set; }

        [JsonProperty("timestampCreation", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampCreation { get; set; }

        [JsonProperty("timestampModification", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? TimestampModification { get; set; }
    }
}