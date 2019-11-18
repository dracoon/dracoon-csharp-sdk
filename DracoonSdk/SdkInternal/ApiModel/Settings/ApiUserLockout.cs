using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserLockout {
        [JsonProperty("enabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool Enabled { get; set; }

        [JsonProperty("maxNumberOfLoginFailures", NullValueHandling = NullValueHandling.Ignore)]
        public int MaximumLoginFailureAttempts { get; set; }

        [JsonProperty("lockoutPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int MinutesToNextLoginAttempt { get; set; }
    }
}