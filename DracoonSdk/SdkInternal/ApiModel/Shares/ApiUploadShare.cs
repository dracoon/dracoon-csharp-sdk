using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUploadShare {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long ShareId { get; set; }

        [JsonProperty("targetId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("isProtected", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsProtected { get; set; }

        [JsonProperty("accessKey", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessKey { get; set; }

        [JsonProperty("notifyCreator", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotifyCreator { get; set; }

        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo CreatedBy { get; set; }

        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt { get; set; }

        [JsonProperty("targetPath", NullValueHandling = NullValueHandling.Ignore)]
        public string NodePath { get; set; }

        [JsonProperty("isEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEncrypted { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("internalNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalNotes { get; set; }

        [JsonProperty("filesExpiryPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int? UploadedFilesExpirationPeriod { get; set; }

        [JsonProperty("cntUploads", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentDoneUploadsCount { get; set; }

        [JsonProperty("cntFiles", NullValueHandling = NullValueHandling.Ignore)]
        public int? CurrentUploadedFilesCount { get; set; }

        [JsonProperty("showUploadedFiles", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ShowUploadedFiles { get; set; }

        [JsonProperty("maxSlots", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedUploads { get; set; }

        [JsonProperty("maxSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; set; }

        [JsonProperty("targetType", NullValueHandling = NullValueHandling.Ignore)]
        public string Type { get; set; }
    }
}