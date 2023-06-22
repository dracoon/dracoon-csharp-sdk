using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiFileVirusProtectionInfo {
        [JsonProperty("verdict", NullValueHandling = NullValueHandling.Ignore)]
        public string Verdict { get; set; }

        [JsonProperty("lastCheckedAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastCheckedAt { get; set; }

        [JsonProperty("sha256", NullValueHandling = NullValueHandling.Ignore)]
        public string Sha256 { get; set; }

        [JsonProperty("nodeId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }
    }
}
