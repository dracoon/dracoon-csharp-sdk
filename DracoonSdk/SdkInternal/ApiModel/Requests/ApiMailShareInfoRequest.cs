using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiMailShareInfoRequest {

        [JsonProperty("recipients", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Recipients { get; set; }

        [JsonProperty("body", NullValueHandling = NullValueHandling.Ignore)]
        public string Body { get; set; }

        [JsonProperty("receiverLanguage", NullValueHandling = NullValueHandling.Ignore)]
        public string ReceiverLanguage { get; set; }
    }
}
