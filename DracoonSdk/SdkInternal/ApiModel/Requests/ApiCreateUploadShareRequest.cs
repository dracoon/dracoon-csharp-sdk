using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateUploadShareRequest {
        [JsonProperty("targetId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration { get; set; }

        [JsonProperty("filesExpiryPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int? UploadedFilesExpirationPeriod { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("internalNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalNotes { get; set; }

        [JsonProperty("showUploadedFiles", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowUploadedFiles { get; set; }

        [JsonProperty("maxSlots", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedUploads { get; set; }

        [JsonProperty("maxSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; set; }

        [JsonProperty("receiverLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string ReceiverLanguage { get; set; }

        [JsonProperty("textMessageRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TextMessageRecipients { get; set; }

        [JsonProperty("showCreatorName", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorName { get; set; }

        [JsonProperty("showCreatorUsername", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorUsername { get; set; }
    }
}