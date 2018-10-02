using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiEnableRoomEncryptionRequest {
        [JsonProperty("isEncrypted", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsEncryptionEnabled {
            get; set;
        }
        [JsonProperty("useDataSpaceRescueKey", NullValueHandling = NullValueHandling.Ignore)]
        public bool UseDataSpaceRescueKey {
            get; set;
        }
        [JsonProperty("dataRoomRescueKey", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserKeyPair DataRoomRescueKey {
            get; set;
        }
    }
}
