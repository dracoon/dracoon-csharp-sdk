using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiNode {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id {
            get; set;
        }
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string Type {
            get; set;
        }
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId {
            get; set;
        }
        [JsonProperty("parentPath", NullValueHandling = NullValueHandling.Ignore)]
        public string ParentPath {
            get; set;
        }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name {
            get; set;
        }
        [JsonProperty("fileType", NullValueHandling = NullValueHandling.Ignore)]
        public string FileType {
            get; set;
        }
        [JsonProperty("mediaType", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaType {
            get; set;
        }
        [JsonProperty("size", NullValueHandling = NullValueHandling.Ignore)]
        public long? Size {
            get; set;
        }
        [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quota {
            get; set;
        }
        [JsonProperty("classification", NullValueHandling = NullValueHandling.Ignore)]
        public int? Classification {
            get; set;
        }
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes {
            get; set;
        }
        [JsonProperty("hash", NullValueHandling = NullValueHandling.Ignore)]
        public string Hash {
            get; set;
        }
        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt {
            get; set;
        }
        [JsonProperty("createdAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt {
            get; set;
        }
        [JsonProperty("createdBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo CreatedBy {
            get; set;
        }
        [JsonProperty("updatedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt {
            get; set;
        }
        [JsonProperty("updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserInfo UpdatedBy {
            get; set;
        }
        [JsonProperty("permissions", NullValueHandling = NullValueHandling.Ignore)]
        public ApiNodePermissions Permissions {
            get; set;
        }
        [JsonProperty("inheritPermissions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InheritPermissions {
            get; set;
        }
        [JsonProperty("isFavorite", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsFavorite {
            get; set;
        }
        [JsonProperty("isEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEncrypted {
            get; set;
        }
        [JsonProperty("encryptionInfo", NullValueHandling = NullValueHandling.Ignore)]
        public ApiEncryptionInfo EncryptionInfo {
            get; set;
        }
        [JsonProperty("cntChildren", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountChildren {
            get; set;
        }
        [JsonProperty("cntRooms", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountRooms {
            get; set;
        }
        [JsonProperty("cntFolders", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountFolders {
            get; set;
        }
        [JsonProperty("cntFiles", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountFiles {
            get; set;
        }
        [JsonProperty("cntDeletedVersions", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountDeletedVersions {
            get; set;
        }
        [JsonProperty("recycleBinRetentionPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int? RecycleBinRetentionPeriod {
            get; set;
        }
        [JsonProperty("cntDownloadShares", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountDownloadShares {
            get; set;
        }
        [JsonProperty("cntUploadShares", NullValueHandling = NullValueHandling.Ignore)]
        public int? CountUploadShares {
            get; set;
        }
        [JsonProperty("branchVersion", NullValueHandling = NullValueHandling.Ignore)]
        public long? BranchVersion {
            get; set;
        }
        [JsonProperty("mediaToken", NullValueHandling = NullValueHandling.Ignore)]
        public string MediaToken {
            get; set;
        }
    }
}
