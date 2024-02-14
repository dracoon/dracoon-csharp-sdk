﻿using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.OAuth;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryUser {
        internal static UserInfo UserInfo => new UserInfo {
            Id = 12,
            UserName = "User",
            AvatarUUID = "HSGF324DSFGJ",
            Email = "test@test.com",
            FirstName = "Max",
            LastName = "Mustermann",
            Title = "B.Sc.",
            UserType = UserType.Internal
        };

        internal static ApiUserInfo ApiUserInfo => new ApiUserInfo {
            Id = 12,
            UserName = "User",
            AvatarUuid = "HSGF324DSFGJ"
        };

        internal static UserAccount UserAccount => new UserAccount {
            Id = 456,
            AuthData = UserAuthData,
            UserName = "JohnSmith1234",
            Title = "M.Sc.",
            FirstName = "John",
            LastName = "Smith",
            Email = "JohnSmith@js.com",
            HasEncryptionEnabled = false,
            HasManageableRooms = true,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            LastLoginSuccessAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            LastLoginFailAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            UserRoles = new List<UserRole> {
                        UserRole.ConfigManager,
                        UserRole.GroupManager
                    },
            HomeRoomId = 2
        };

        internal static UserAuthData UserAuthData => new UserAuthData {
            Method = UserAuthMethod.ActiveDirectory,
            Login = "JohnS",
            Password = "",
            MustChangePassword = false,
            ADConfigId = 1234,
            OIDConfigId = 0
        };

        internal static ApiUserAccount ApiUserAccount => new ApiUserAccount {
            Id = 456,
            AuthData = ApiUserAuthData,
            Title = "M.Sc.",
            FirstName = "John",
            LastName = "Smith",
            Email = "JohnSmith@js.com",
            IsEncryptionEnabled = false,
            HasManageableRooms = true,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            LastLoginSuccessAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            LastLoginFailAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            UserRoles = new ApiUserRoleList {
                Items = new List<ApiUserRole> {
                            new ApiUserRole {
                                Id = 1
                            },
                            new ApiUserRole {
                                Id = 3
                            }
                        }
            },
            HomeRoomId = 2
        };

        internal static ApiAuthData ApiUserAuthData => new ApiAuthData {
            Method = "active_directory",
            Login = "JohnS",
            Password = "",
            MustChangePassword = false,
            ADConfigId = 1234,
            OIDConfigId = 0
        };

        internal static List<UserKeyPair> UserKeyPairs => new List<UserKeyPair> {
                    UserKeyPair_2048,
                    UserKeyPair_4096
                };

        internal static UserPrivateKey UserPrivateKey_2048 => new UserPrivateKey {
            PrivateKey = "KJGSBD34JGFBSDKJSBD34JGFBSKJGSBD34JGFBSD",
            Version = Crypto.Sdk.UserKeyPairAlgorithm.RSA2048
        };

        internal static UserPublicKey UserPublicKey_2048 => new UserPublicKey {
            PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSD",
            Version = Crypto.Sdk.UserKeyPairAlgorithm.RSA2048
        };

        internal static UserPrivateKey UserPrivateKey_4096 => new UserPrivateKey {
            PrivateKey = "KJGSBD34JGFBSDKJSBD34JGFBSKJGSBD34JGFBSDASDFSDFSDF",
            Version = Crypto.Sdk.UserKeyPairAlgorithm.RSA4096
        };

        internal static UserPublicKey UserPublicKey_4096 => new UserPublicKey {
            PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSDSDFSDFDS",
            Version = Crypto.Sdk.UserKeyPairAlgorithm.RSA4096
        };

        internal static UserKeyPair UserKeyPair_2048 => new UserKeyPair {
            UserPrivateKey = UserPrivateKey_2048,
            UserPublicKey = UserPublicKey_2048
        };

        internal static UserKeyPair UserKeyPair_4096 => new UserKeyPair {
            UserPrivateKey = UserPrivateKey_4096,
            UserPublicKey = UserPublicKey_4096
        };

        internal static ApiUserPrivateKey ApiUserPrivateKey_2048 => new ApiUserPrivateKey {
            PrivateKey = "KJGSBD34JGFBSKJGSBD34JGFBSDKJSBD34JGFBSD",
            Version = "A"
        };

        internal static ApiUserPublicKey ApiUserPublicKey_2048 => new ApiUserPublicKey {
            PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSD",
            Version = "A"
        };

        internal static ApiUserPrivateKey ApiUserPrivateKey_4096 => new ApiUserPrivateKey {
            PrivateKey = "KJGSBD34JGFBSDKJSBD34JGFBSKJGSBD34JGFBSDASDFSDFSDF",
            Version = "RSA-4096"
        };

        internal static ApiUserPublicKey ApiUserPublicKey_4096 => new ApiUserPublicKey {
            PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSDSDFSDFDS",
            Version = "RSA-4096"
        };

        internal static ApiUserKeyPair ApiUserKeyPair_2048 => new ApiUserKeyPair {
            PrivateKeyContainer = ApiUserPrivateKey_2048,
            PublicKeyContainer = ApiUserPublicKey_2048
        };

        internal static ApiUserKeyPair ApiUserKeyPair_4096 => new ApiUserKeyPair {
            PrivateKeyContainer = ApiUserPrivateKey_4096,
            PublicKeyContainer = ApiUserPublicKey_4096
        };

        internal static AvatarInfo AvatarInfo => new AvatarInfo {
            AvatarUUID = "JHKSD76fDASJ",
            IsCustomAvatar = true
        };

        internal static ApiAvatarInfo ApiAvatarInfo => new ApiAvatarInfo {
            AvatarUuid = "JHKSD76fDASJ",
            IsCustomAvatar = true,
            AvatarUri = "https://dracoon.team"
        };

        internal static ApiOAuthToken ApiOAuthToken => new ApiOAuthToken {
            AccessToken = "token1",
            RefreshToken = "refreshToken1",
            ExpiresIn = 123
        };
    }
}