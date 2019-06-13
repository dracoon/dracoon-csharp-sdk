using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiS3Urls {
        [JsonProperty("urls", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiS3Url> Urls {
            get; set;
        }
    }
}
