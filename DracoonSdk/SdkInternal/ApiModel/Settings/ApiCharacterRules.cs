using System.Collections.Generic;
using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiCharacterRules {
        [JsonProperty("mustContainCharacters", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> MustContainCharacters { get; set; }

        [JsonProperty("numberOfCharacteristicsToEnforce", NullValueHandling = NullValueHandling.Ignore)]
        public int NumberOfCharacteristicsToEnforce { get; set; }
    }
}