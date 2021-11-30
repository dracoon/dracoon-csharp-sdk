using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDownloadShare {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long ShareId { get; set; }

        [JsonProperty("nodeId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("nodePath", NullValueHandling = NullValueHandling.Ignore)]
        public string NodePath { get; set; }

        [JsonProperty("accessKey", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessKey { get; set; }

        [JsonProperty("notifyCreator", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotifyCreator { get; set; }

        [JsonProperty("cntDownloads", NullValueHandling = NullValueHandling.Ignore)]
        public int CurrentDownloadsCount { get; set; }

        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo CreatedBy { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("internalNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalNotes { get; set; }

        [JsonProperty("showCreatorName", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowCreatorName { get; set; }

        [JsonProperty("ShowCreatorUsername", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowCreatorUserName { get; set; }

        [JsonProperty("isProtected", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsProtected { get; set; }

        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt { get; set; }

        [JsonProperty("maxDownloads", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedDownloads { get; set; }

        [JsonProperty("isEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEncrypted { get; set; }

        [JsonProperty("nodeType", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}