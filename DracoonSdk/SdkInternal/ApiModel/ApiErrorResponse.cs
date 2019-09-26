using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiErrorResponse {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public int? Code { get; private set; }

        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; private set; }

        [JsonProperty("debugInfo", NullValueHandling = NullValueHandling.Ignore)]
        public string DebugInfo { get; private set; }

        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public int? ErrorCode { get; private set; }

        public override string ToString() {
            return "ErrorResponse{code=" + Code + ", message=" + Message + ", debugInfo=" + DebugInfo + ", errorCode=" + ErrorCode + "}";
        }
    }
}