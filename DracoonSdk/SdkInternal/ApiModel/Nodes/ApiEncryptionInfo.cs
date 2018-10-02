using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiEncryptionInfo {
        [JsonProperty("userKeyState", NullValueHandling = NullValueHandling.Ignore)]
        public string UserKeyState {
            get; set;
        }
        [JsonProperty("roomKeyState", NullValueHandling = NullValueHandling.Ignore)]
        public string RoomKeyState {
            get; set;
        }
        [JsonProperty("dataSpaceKeyState", NullValueHandling = NullValueHandling.Ignore)]
        public string DataspaceKeyState {
            get; set;
        }
    }
}
