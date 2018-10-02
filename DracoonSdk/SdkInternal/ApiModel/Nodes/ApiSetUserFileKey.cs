using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiSetUserFileKey {
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public long UserId {
            get; set;
        }
        [JsonProperty("fileId", NullValueHandling = NullValueHandling.Ignore)]
        public long FileId {
            get; set;
        }
        [JsonProperty("fileKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiFileKey FileKey {
            get; set;
        }
    }
}
