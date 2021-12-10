using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.ApiModel {
    internal class ApiUserAccount {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty("authData", NullValueHandling = NullValueHandling.Ignore)]
        public ApiAuthData AuthData { get; set; }

        [JsonProperty("userName", NullValueHandling = NullValueHandling.Ignore)]
        public string UserName { get; set; }

        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty("isEncryptionEnabled", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsEncryptionEnabled { get; set; }

        [JsonProperty("hasManageableRooms", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasManageableRooms { get; set; }

        [JsonProperty("expireAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? ExpireAt { get; set; }

        [JsonProperty("lastLoginSuccessAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastLoginSuccessAt { get; set; }

        [JsonProperty("lastLoginFailAt", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? LastLoginFailAt { get; set; }

        [JsonProperty("userRoles", NullValueHandling = NullValueHandling.Ignore)]
        public ApiUserRoleList UserRoles { get; set; }

        [JsonProperty("homeRoomId", NullValueHandling = NullValueHandling.Ignore)]
        public long? HomeRoomId { get; set; }

        [JsonProperty("isLocked", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLocked { get; set; }

        [JsonProperty("language", NullValueHandling = NullValueHandling.Ignore)]
        public string Language { get; set; }

        [JsonProperty("mustSetEmail", NullValueHandling = NullValueHandling.Ignore)]
        public bool MustSetEmail { get; set; }

        [JsonProperty("needsToAcceptEULA", NullValueHandling = NullValueHandling.Ignore)]
        public bool? NeedsToAcceptEULA { get; set; }

        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public string Phone { get; set; }

        [JsonProperty("userGroups", NullValueHandling = NullValueHandling.Ignore)]
        public List<ApiUserGroup> UserGroups { get; set; }
    }
}