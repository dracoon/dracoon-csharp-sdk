using Newtonsoft.Json;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiCustomerAccount {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("accountsUsed", NullValueHandling = NullValueHandling.Ignore)]
        public int AccountsUsed { get; set; }

        [JsonProperty("accountsLimit", NullValueHandling = NullValueHandling.Ignore)]
        public int AccountsLimit { get; set; }

        [JsonProperty("spaceUsed", NullValueHandling = NullValueHandling.Ignore)]
        public long SpaceUsed { get; set; }

        [JsonProperty("spaceLimit", NullValueHandling = NullValueHandling.Ignore)]
        public long SpaceLimit { get; set; }

        [JsonProperty("customerEncryptionEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool CustomerEncryptionEnabled { get; set; }

        [JsonProperty("isProviderCustomer", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsProviderCustomer { get; set; }
    }
}