using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateDownloadShareRequest {
        [JsonProperty("nodeId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("internalNotes", NullValueHandling = NullValueHandling.Ignore)]
        public string InternalNotes { get; set; }

        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration { get; set; }

        [JsonProperty("showCreatorName", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorName { get; set; }

        [JsonProperty("showCreatorUsername", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorUserName { get; set; }

        [JsonProperty("maxDownloads", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedDownloads { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }

        [JsonProperty("keyPair", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserKeyPair KeyPair { get; set; }

        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey { get; set; }

        [JsonProperty("receiverLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string ReceiverLanguage { get; set; }

        [JsonProperty("textMessageRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> TextMessageRecipients { get; set; }
    }
}