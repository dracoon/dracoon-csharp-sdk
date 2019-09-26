using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateUploadShareRequest {
        [JsonProperty("targetId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string AccessPassword { get; set; }

        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration { get; set; }

        [JsonProperty("filesExpiryPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int? UploadedFilesExpirationPeriod { get; set; }

        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }

        [JsonProperty("notifyCreator", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotifyCreator { get; set; }

        [JsonProperty("showUploadedFiles", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowUploadedFiles { get; set; }

        [JsonProperty("maxSlots", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedUploads { get; set; }

        [JsonProperty("maxSize", NullValueHandling = NullValueHandling.Ignore)]
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; set; }

        [JsonProperty("sendMail", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendMail { get; set; }

        [JsonProperty("mailRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public string MailRecipients { get; set; }

        [JsonProperty("mailSubject", NullValueHandling = NullValueHandling.Ignore)]
        public string MailSubject { get; set; }

        [JsonProperty("mailBody", NullValueHandling = NullValueHandling.Ignore)]
        public string MailBody { get; set; }

        [JsonProperty("sendSms", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendSms { get; set; }

        [JsonProperty("smsRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public string SmsRecipients { get; set; }
    }
}