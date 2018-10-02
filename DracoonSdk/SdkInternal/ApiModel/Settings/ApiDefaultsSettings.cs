using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiDefaultsSettings {
        [JsonProperty("languageDefault", NullValueHandling = NullValueHandling.Ignore)]
        public string LanguageDefault {
            get; set;
        }
        [JsonProperty("downloadShareDefaultExpirationPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int DownloadShareDefaultExpirationPeriodInDays {
            get; set;
        }
        [JsonProperty("uploadShareDefaultExpirationPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int UploadShareDefaultExpirationPeriodInDays {
            get; set;
        }
        [JsonProperty("fileDefaultExpirationPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int FileUploadDefaultExpirationPeriodInDays {
            get; set;
        }
    }
}
