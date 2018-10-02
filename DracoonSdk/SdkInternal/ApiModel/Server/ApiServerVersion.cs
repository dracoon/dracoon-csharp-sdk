using Newtonsoft.Json;
using System;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiServerVersion {
        [JsonProperty("sdsServerVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string ServerVersion {
            get; set;
        }
        [JsonProperty("restApiVersion", NullValueHandling = NullValueHandling.Ignore)]
        public string RestApiVersion {
            get; set;
        }
        [JsonProperty("buildDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime BuildDate {
            get; set;
        }
    }
}
