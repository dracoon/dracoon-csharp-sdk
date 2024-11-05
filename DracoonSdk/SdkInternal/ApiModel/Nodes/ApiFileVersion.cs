using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Nodes {
    internal class ApiFileVersion {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("referenceId", NullValueHandling = NullValueHandling.Ignore)]
        public long ReferenceId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId { get; set; }

        [JsonProperty("deleted", NullValueHandling = NullValueHandling.Ignore)]
        public bool Deleted { get; set; }

    }
}
