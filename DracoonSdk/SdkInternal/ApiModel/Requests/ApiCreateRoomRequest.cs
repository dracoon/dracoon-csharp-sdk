using Newtonsoft.Json;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel.Requests {
    internal class ApiCreateRoomRequest {
        [JsonProperty("parentId", NullValueHandling = NullValueHandling.Ignore)]
        public long? ParentId {
            get; set;
        }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name {
            get; set;
        }
        [JsonProperty("quota", NullValueHandling = NullValueHandling.Ignore)]
        public long? Quota {
            get; set;
        }
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes {
            get; set;
        }
        [JsonProperty("recycleBinRetentionPeriod", NullValueHandling = NullValueHandling.Ignore)]
        public int? RecycleBinRetentionPeriod {
            get; set;
        }
        [JsonProperty("inheritPermissions", NullValueHandling = NullValueHandling.Ignore)]
        public bool? InheritPermissions {
            get; set;
        }
        [JsonProperty("adminIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> AdminIds {
            get; set;
        }
        [JsonProperty("adminGroupIds", NullValueHandling = NullValueHandling.Ignore)]
        public List<long> AdminGroupIds {
            get; set;
        }
        [JsonProperty("newGroupMemberAcceptance", NullValueHandling = NullValueHandling.Ignore)]
        public string NewGroupMemberAcceptance {
            get; set;
        }
    }
}
