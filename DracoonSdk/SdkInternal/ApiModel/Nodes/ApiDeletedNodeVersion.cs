using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDeletedNodeVersion {
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long ParentId { get; set; }

        [JsonProperty("parentPath", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPath { get; set; }

        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long? Id { get; set; }

        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt { get; set; }

        [JsonProperty("accessedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? AccessedAt { get; set; }

        [JsonProperty("isEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEncrypted { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size { get; set; }

        [JsonProperty("classification", NullValueHandling = NullValueHandling.Ignore)]
        public int? Classification { get; set; }

        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }

        [JsonProperty("createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo CreatedBy { get; set; }

        [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }

        [JsonProperty("updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo UpdatedBy { get; set; }

        [JsonProperty("deletedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? DeletedAt { get; set; }

        [JsonProperty("deletedBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo DeletedBy { get; set; }
    }
}