using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserIdFileId {
        [JsonProperty("userId", NullValueHandling = NullValueHandling.Ignore)]
        public long UserId { get; set; }

        [JsonProperty("fileId", NullValueHandling = NullValueHandling.Ignore)]
        public long FileId { get; set; }
    }
}