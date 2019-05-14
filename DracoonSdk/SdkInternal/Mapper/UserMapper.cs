
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class UserMapper {
        internal static UserInfo FromApiUserInfo(ApiUserInfo apiUserInfo) {
            if (apiUserInfo == null) {
                return null;
            }

            UserInfo userInfo = new UserInfo() {
                Id = apiUserInfo.Id,
                DisplayName = apiUserInfo.DisplayName,
                AvatarUUID = apiUserInfo.AvatarUUID
            };
            return userInfo;
        }
        internal static UserAccount FromApiUserAccount(ApiUserAccount apiUserAccount) {
            if (apiUserAccount == null) {
                return null;
            }

            UserAccount userAccount = new UserAccount() {
                Id = apiUserAccount.Id,
                LoginName = apiUserAccount.LoginName,
                Title = apiUserAccount.Title,
                FirstName = apiUserAccount.FirstName,
                LastName = apiUserAccount.LastName,
                Email = apiUserAccount.Email,
                HasEncryptionEnabled = apiUserAccount.IsEncryptionEnabled,
                HasManageableRooms = apiUserAccount.HasManageableRooms,
                ExpireAt = apiUserAccount.ExpireAt,
                LastLoginSuccessAt = apiUserAccount.LastLoginSuccessAt,
                LastLoginFailAt = apiUserAccount.LastLoginFailAt,
                UserRoles = ConvertApiUserRoles(apiUserAccount.UserRoles),
                HomeRoomId = apiUserAccount.HomeRoomId
            };
            return userAccount;
        }

        private static List<UserRole> ConvertApiUserRoles(ApiUserRoleList apiUserRoles) {
            List<UserRole> returnValue = new List<UserRole>();
            if (apiUserRoles != null) {
                foreach (ApiUserRole currentRole in apiUserRoles.Items) {
                    returnValue.Add((UserRole) Enum.ToObject(typeof(UserRole), currentRole.Id));
                }
            }
            return returnValue;
        }

        internal static ApiUserKeyPair ToApiUserKeyPair(UserKeyPair userKeyPair) {
            ApiUserKeyPair apiUserKeyPair = new ApiUserKeyPair() {
                PublicKeyContainer = ToApiUserPublicKey(userKeyPair.UserPublicKey),
                PrivateKeyContainer = ToApiUserPrivateKey(userKeyPair.UserPrivateKey)
            };
            return apiUserKeyPair;
        }

        private static ApiUserPublicKey ToApiUserPublicKey(UserPublicKey userPublicKey) {
            ApiUserPublicKey apiUserPublicKey = new ApiUserPublicKey() {
                Version = userPublicKey.Version,
                PublicKey = userPublicKey.PublicKey
            };
            return apiUserPublicKey;
        }

        private static ApiUserPrivateKey ToApiUserPrivateKey(UserPrivateKey userPrivateKey) {
            ApiUserPrivateKey apiUserPrivateKey = new ApiUserPrivateKey() {
                Version = userPrivateKey.Version,
                PrivateKey = userPrivateKey.PrivateKey
            };
            return apiUserPrivateKey;
        }

        internal static UserKeyPair FromApiUserKeyPair(ApiUserKeyPair apiUserKeyPair) {
            UserKeyPair userKeyPair = new UserKeyPair() {
                UserPublicKey = FromApiUserPublicKey(apiUserKeyPair.PublicKeyContainer),
                UserPrivateKey = FromApiUserPrivateKey(apiUserKeyPair.PrivateKeyContainer)
            };
            return userKeyPair;
        }

        private static UserPublicKey FromApiUserPublicKey(ApiUserPublicKey apiUserPublicKey) {
            UserPublicKey userPublicKey = new UserPublicKey() {
                Version = apiUserPublicKey.Version,
                PublicKey = apiUserPublicKey.PublicKey
            };
            return userPublicKey;
        }

        private static UserPrivateKey FromApiUserPrivateKey(ApiUserPrivateKey apiUserPrivateKey) {
            UserPrivateKey userPrivateKey = new UserPrivateKey() {
                Version = apiUserPrivateKey.Version,
                PrivateKey = apiUserPrivateKey.PrivateKey
            };
            return userPrivateKey;
        }

        internal static Dictionary<long, UserPublicKey> ConvertApiUserIdPublicKeys(List<ApiUserIdPublicKey> userIdPublicKeys) {
            Dictionary<long, UserPublicKey> userPublicKeys = new Dictionary<long, UserPublicKey>(userIdPublicKeys.Count);
            foreach (ApiUserIdPublicKey currentPublicKey in userIdPublicKeys) {
                userPublicKeys.Add(currentPublicKey.UserId, FromApiUserPublicKey(currentPublicKey.PublicKeyContainer));
            }
            return userPublicKeys;
        }

        internal static AvatarInfo FromApiAvatarInfo(ApiAvatarInfo apiInfo) {
            AvatarInfo info = new AvatarInfo() {
                AvatarUUID = apiInfo.AvatarUUID,
                IsCustomAvatar = apiInfo.IsCustomAvatar
            };
            return info;
        }
    }
}
