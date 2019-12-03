using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using System;
using System.Collections.Generic;
using Dracoon.Sdk.SdkInternal.OAuth;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryUser {
        internal static UserInfo UserInfo {
            get {
                return new UserInfo {
                    Id = 12,
                    DisplayName = "User",
                    AvatarUUID = "HSGF324DSFGJ",
                    Email = "test@test.com",
                    FirstName = "Max",
                    LastName = "Mustermann",
                    Title = "B.Sc.",
                    UserType = UserType.Internal
                };
            }
        }

        internal static ApiUserInfo ApiUserInfo {
            get {
                return new ApiUserInfo {
                    Id = 12,
                    DisplayName = "User",
                    AvatarUuid = "HSGF324DSFGJ"
                };
            }
        }

        internal static UserAccount UserAccount {
            get {
                return new UserAccount {
                    Id = 456,
                    LoginName = "JohnSmith123",
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
            }
        }

        internal static ApiUserAccount ApiUserAccount {
            get {
                return new ApiUserAccount {
                    Id = 456,
                    LoginName = "JohnSmith123",
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
            }
        }

        internal static UserPrivateKey UserPrivateKey {
            get {
                return new UserPrivateKey {
                    PrivateKey = "KJGSBD34JGFBSDKJSBD34JGFBSKJGSBD34JGFBSD",
                    Version = "A"
                };
            }
        }

        internal static UserPublicKey UserPublicKey {
            get {
                return new UserPublicKey {
                    PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSD",
                    Version = "B"
                };
            }
        }

        internal static UserKeyPair UserKeyPair {
            get {
                return new UserKeyPair {
                    UserPrivateKey = UserPrivateKey,
                    UserPublicKey = UserPublicKey
                };
            }
        }

        internal static ApiUserPrivateKey ApiUserPrivateKey {
            get {
                return new ApiUserPrivateKey {
                    PrivateKey = "KJGSBD34JGFBSKJGSBD34JGFBSDKJSBD34JGFBSD",
                    Version = "A"
                };
            }
        }

        internal static ApiUserPublicKey ApiUserPublicKey {
            get {
                return new ApiUserPublicKey {
                    PublicKey = "GKSHDGO5324GHBKJSDGKSHDGO5324GHBKJSD",
                    Version = "B"
                };
            }
        }

        internal static ApiUserKeyPair ApiUserKeyPair {
            get {
                return new ApiUserKeyPair {
                    PrivateKeyContainer = ApiUserPrivateKey,
                    PublicKeyContainer = ApiUserPublicKey
                };
            }
        }

        internal static AvatarInfo AvatarInfo {
            get {
                return new AvatarInfo {
                    AvatarUUID = "JHKSD76fDASJ",
                    IsCustomAvatar = true
                };
            }
        }

        internal static ApiAvatarInfo ApiAvatarInfo {
            get {
                return new ApiAvatarInfo {
                    AvatarUuid = "JHKSD76fDASJ",
                    IsCustomAvatar = true,
                    AvatarUri = "https://dracoon.team"
                };
            }
        }

        internal static ApiOAuthToken ApiOAuthToken {
            get {
                return new ApiOAuthToken {
                    AccessToken = "token1",
                    RefreshToken = "refreshToken1",
                    ExpiresIn = 123
                };
            }
        }
    }
}