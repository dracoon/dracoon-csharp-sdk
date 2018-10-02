using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateDownloadShareRequest {
        [JsonProperty("nodeId", NullValueHandling = NullValueHandling.Ignore)]
        public long NodeId {
            get; set;
        }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name {
            get; set;
        }
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes {
            get; set;
        }
        [JsonProperty("expiration", NullValueHandling = NullValueHandling.Ignore)]
        public ApiExpiration Expiration {
            get; set;
        }
        [JsonProperty("showCreatorName", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorName {
            get; set;
        }
        [JsonProperty("showCreatorUsername", NullValueHandling = NullValueHandling.Ignore)]
        public bool ShowCreatorUserName {
            get; set;
        }
        [JsonProperty("notifyCreator", NullValueHandling = NullValueHandling.Ignore)]
        public bool NotifyCreator {
            get; set;
        }
        [JsonProperty("maxDownloads", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxAllowedDownloads {
            get; set;
        }
        [JsonProperty("password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password {
            get; set;
        }
        [JsonProperty("keyPair", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserKeyPair KeyPair {
            get; set;
        }
        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey {
            get; set;
        }
        [JsonProperty("sendMail", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendMail {
            get; set;
        }
        [JsonProperty("mailRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public string MailRecipients {
            get; set;
        }
        [JsonProperty("mailSubject", NullValueHandling = NullValueHandling.Ignore)]
        public string MailSubject {
            get; set;
        }
        [JsonProperty("mailBody", NullValueHandling = NullValueHandling.Ignore)]
        public string MailBody {
            get; set;
        }
        [JsonProperty("sendSms", NullValueHandling = NullValueHandling.Ignore)]
        public bool SendSms {
            get; set;
        }
        [JsonProperty("smsRecipients", NullValueHandling = NullValueHandling.Ignore)]
        public string SmsRecipients {
            get; set;
        }
    }
}
