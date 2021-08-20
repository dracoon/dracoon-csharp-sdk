using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiShareClassificationPolicy {
        [JsonProperty("classificationRequiresSharePassword", NullValueHandling = NullValueHandling.Ignore)]
        public int PasswordRequirementMinimumClassification { get; set; }
    }
}
