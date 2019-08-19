using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiNodePermissions {
        [JsonProperty("manage", NullValueHandling = NullValueHandling.Ignore)]
        public bool Manage { get; set; }

        [JsonProperty("read", NullValueHandling = NullValueHandling.Ignore)]
        public bool Read { get; set; }

        [JsonProperty("create", NullValueHandling = NullValueHandling.Ignore)]
        public bool Create { get; set; }

        [JsonProperty("change", NullValueHandling = NullValueHandling.Ignore)]
        public bool Change { get; set; }

        [JsonProperty("delete", NullValueHandling = NullValueHandling.Ignore)]
        public bool Delete { get; set; }

        [JsonProperty("manageDownloadShare", NullValueHandling = NullValueHandling.Ignore)]
        public bool ManageDownloadShare { get; set; }

        [JsonProperty("manageUploadShare", NullValueHandling = NullValueHandling.Ignore)]
        public bool ManageUploadShare { get; set; }

        [JsonProperty("readRecycleBin", NullValueHandling = NullValueHandling.Ignore)]
        public bool ReadRecycleBin { get; set; }

        [JsonProperty("restoreRecycleBin", NullValueHandling = NullValueHandling.Ignore)]
        public bool RestoreRecycleBin { get; set; }

        [JsonProperty("deleteRecycleBin", NullValueHandling = NullValueHandling.Ignore)]
        public bool DeleteRecycleBin { get; set; }
    }
}