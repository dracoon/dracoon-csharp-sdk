using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Util;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class UserMapper {
        internal static UserInfo FromApiUserInfo(ApiUserInfo apiUserInfo) {
            if (apiUserInfo == null) {
                return null;
            }

            UserInfo userInfo = new UserInfo {
                Id = apiUserInfo.Id,
                UserName = apiUserInfo.UserName,
                AvatarUUID = apiUserInfo.AvatarUuid,
                Email = apiUserInfo.Email,
                FirstName = apiUserInfo.FirstName,
                LastName = apiUserInfo.LastName,
                UserType = EnumConverter.ConvertValueToUserTypeEnum(apiUserInfo.UserType)
            };
            return userInfo;
        }

        internal static UserAccount FromApiUserAccount(ApiUserAccount apiUserAccount) {
            if (apiUserAccount == null) {
                return null;
            }

            UserAccount userAccount = new UserAccount {
                Id = apiUserAccount.Id,
                AuthData = FromApiUserAuthData(apiUserAccount.AuthData),
                UserName = apiUserAccount.UserName,
                FirstName = apiUserAccount.FirstName,
                LastName = apiUserAccount.LastName,
                Email = apiUserAccount.Email,
                HasEncryptionEnabled = apiUserAccount.IsEncryptionEnabled,
                HasManageableRooms = apiUserAccount.HasManageableRooms,
                ExpireAt = apiUserAccount.ExpireAt,
                LastLoginSuccessAt = apiUserAccount.LastLoginSuccessAt,
                LastLoginFailAt = apiUserAccount.LastLoginFailAt,
                UserRoles = ConvertApiUserRoles(apiUserAccount.UserRoles),
                HomeRoomId = apiUserAccount.HomeRoomId,
                IsLocked = apiUserAccount.IsLocked,
                Language = apiUserAccount.Language,
                MustSetEmail = apiUserAccount.MustSetEmail,
                NeedsToAcceptEULA = apiUserAccount.NeedsToAcceptEULA,
                Phone = apiUserAccount.Phone,
                UserGroups = new List<UserGroup>()
            };
            if (apiUserAccount.UserGroups != null) {
                foreach (ApiUserGroup currentGroup in apiUserAccount.UserGroups) {
                    userAccount.UserGroups.Add(FromApiUserGroup(currentGroup));
                }
            }
            return userAccount;
        }

        internal static UserAuthData FromApiUserAuthData(ApiAuthData apiUserAuthData) {
            if (apiUserAuthData == null) {
                return null;
            }

            UserAuthData userAuthData = new UserAuthData {
                Method = EnumConverter.ConvertValueToUserAuthMethodEnum(apiUserAuthData.Method),
                Login = apiUserAuthData.Login,
                Password = apiUserAuthData.Password,
                MustChangePassword = apiUserAuthData.MustChangePassword,
                ADConfigId = apiUserAuthData.ADConfigId,
                OIDConfigId = apiUserAuthData.OIDConfigId
            };
            return userAuthData;
        }

        internal static UserGroup FromApiUserGroup(ApiUserGroup apiUserGroup) {
            if (apiUserGroup == null) {
                return null;
            }

            UserGroup userGroup = new UserGroup {
                Id = apiUserGroup.Id,
                IsMember = apiUserGroup.IsMember,
                Name = apiUserGroup.Name
            };
            return userGroup;
        }

        private static List<UserRole> ConvertApiUserRoles(ApiUserRoleList apiUserRoles) {
            List<UserRole> returnValue = new List<UserRole>();
            if (apiUserRoles == null) {
                return returnValue;
            }

            foreach (ApiUserRole currentRole in apiUserRoles.Items) {
                returnValue.Add((UserRole)Enum.ToObject(typeof(UserRole), currentRole.Id));
            }

            return returnValue;
        }

        internal static ApiUserKeyPair ToApiUserKeyPair(UserKeyPair userKeyPair) {
            ApiUserKeyPair apiUserKeyPair = new ApiUserKeyPair {
                PublicKeyContainer = ToApiUserPublicKey(userKeyPair.UserPublicKey),
                PrivateKeyContainer = ToApiUserPrivateKey(userKeyPair.UserPrivateKey)
            };
            return apiUserKeyPair;
        }

        private static ApiUserPublicKey ToApiUserPublicKey(UserPublicKey userPublicKey) {
            ApiUserPublicKey apiUserPublicKey = new ApiUserPublicKey {
                Version = ToApiUserKeyPairVersion(userPublicKey.Version),
                PublicKey = userPublicKey.PublicKey
            };
            return apiUserPublicKey;
        }

        private static ApiUserPrivateKey ToApiUserPrivateKey(UserPrivateKey userPrivateKey) {
            ApiUserPrivateKey apiUserPrivateKey = new ApiUserPrivateKey {
                Version = ToApiUserKeyPairVersion(userPrivateKey.Version),
                PrivateKey = userPrivateKey.PrivateKey
            };
            return apiUserPrivateKey;
        }

        internal static UserKeyPair FromApiUserKeyPair(ApiUserKeyPair apiUserKeyPair) {
            UserKeyPair userKeyPair = new UserKeyPair {
                UserPublicKey = FromApiUserPublicKey(apiUserKeyPair.PublicKeyContainer),
                UserPrivateKey = FromApiUserPrivateKey(apiUserKeyPair.PrivateKeyContainer)
            };
            return userKeyPair;
        }

        private static UserPublicKey FromApiUserPublicKey(ApiUserPublicKey apiUserPublicKey) {
            UserPublicKey userPublicKey = new UserPublicKey {
                Version = FromApiUserKeyPairVersion(apiUserPublicKey.Version),
                PublicKey = apiUserPublicKey.PublicKey
            };
            return userPublicKey;
        }

        private static UserPrivateKey FromApiUserPrivateKey(ApiUserPrivateKey apiUserPrivateKey) {
            UserPrivateKey userPrivateKey = new UserPrivateKey {
                Version = FromApiUserKeyPairVersion(apiUserPrivateKey.Version),
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
            AvatarInfo info = new AvatarInfo {
                AvatarUUID = apiInfo.AvatarUuid,
                IsCustomAvatar = apiInfo.IsCustomAvatar
            };
            return info;
        }

        internal static UserKeyPairAlgorithm FromApiUserKeyPairVersion(string version) {
            switch (version) {
                case "RSA-4096":
                    return UserKeyPairAlgorithm.RSA4096;
                case "A":
                    return UserKeyPairAlgorithm.RSA2048;
                default:
                    throw new DracoonCryptoException(new DracoonCryptoCode(DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code, "Unknown user key pair algorithm: " + version + "."));
            }
        }

        internal static string ToApiUserKeyPairVersion(UserKeyPairAlgorithm algorithm) {
            switch (algorithm) {
                case UserKeyPairAlgorithm.RSA4096:
                    return "RSA-4096";
                case UserKeyPairAlgorithm.RSA2048:
                    return "A";
                default:
                    throw new DracoonCryptoException(new DracoonCryptoCode(DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code, "Unknown user key pair algorithm: " + algorithm.GetStringValue() + "."));
            }
        }

    }
}