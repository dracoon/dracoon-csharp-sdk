using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.User;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using System.Collections.Generic;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class UserMapperTest {
        #region FromApiUserInfo

        [Fact]
        public void FromApiUserInfo() {
            // ARRANGE
            UserInfo expected = FactoryUser.UserInfo;

            ApiUserInfo param = new ApiUserInfo {
                Id = expected.Id,
                UserName = expected.UserName,
                AvatarUuid = expected.AvatarUUID,
                Email = expected.Email,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                UserType = "internal"
            };

            // ACT
            UserInfo actual = UserMapper.FromApiUserInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserInfoComparer());
        }

        [Fact]
        public void FromApiUserInfo_Null() {
            // ARRANGE
            UserInfo expected = null;

            ApiUserInfo param = null;

            // ACT
            UserInfo actual = UserMapper.FromApiUserInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserInfoComparer());
        }

        #endregion

        #region FromApiUserAccount

        [Fact]
        public void FromApiUserAccount() {
            // ARRANGE
            UserAccount expected = FactoryUser.UserAccount;
            expected.UserRoles = new List<UserRole> {
                UserRole.ConfigManager,
                UserRole.RoomManager
            };

            ApiUserAccount param = new ApiUserAccount {
                Id = expected.Id,
                AuthData = FactoryUser.ApiUserAuthData,
                UserName = expected.UserName,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Email = expected.Email,
                IsEncryptionEnabled = expected.HasEncryptionEnabled,
                HasManageableRooms = expected.HasManageableRooms,
                ExpireAt = expected.ExpireAt,
                LastLoginSuccessAt = expected.LastLoginSuccessAt,
                LastLoginFailAt = expected.LastLoginFailAt,
                UserRoles = new ApiUserRoleList {
                    Items = new List<ApiUserRole>(expected.UserRoles.Count)
                },
                HomeRoomId = expected.HomeRoomId,
                IsLocked = expected.IsLocked,
                Language = expected.Language,
                MustSetEmail = expected.MustSetEmail,
                NeedsToAcceptEULA = expected.NeedsToAcceptEULA,
                Phone = expected.Phone,
                UserGroups = new List<ApiUserGroup>(expected.UserGroups.Count)
            };

            foreach(UserGroup current in expected.UserGroups) {
                ApiUserGroup currentGroup = new ApiUserGroup {
                    Id = current.Id,
                    IsMember = current.IsMember,
                    Name = current.Name
                };
                param.UserGroups.Add(currentGroup);
            }

            foreach (UserRole current in expected.UserRoles) {
                ApiUserRole currentApi = new ApiUserRole {
                    Id = (int)current
                };
                param.UserRoles.Items.Add(currentApi);
            }

            // ACT
            UserAccount actual = UserMapper.FromApiUserAccount(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserAccountComparer());
        }

        [Fact]
        public void FromApiUserAccount_NullRoles() {
            // ARRANGE
            UserAccount expected = FactoryUser.UserAccount;
            expected.UserRoles = new List<UserRole>();

            ApiUserAccount param = new ApiUserAccount {
                Id = expected.Id,
                AuthData = FactoryUser.ApiUserAuthData,
                UserName = expected.UserName,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Email = expected.Email,
                IsEncryptionEnabled = expected.HasEncryptionEnabled,
                HasManageableRooms = expected.HasManageableRooms,
                ExpireAt = expected.ExpireAt,
                LastLoginSuccessAt = expected.LastLoginSuccessAt,
                LastLoginFailAt = expected.LastLoginFailAt,
                UserRoles = null,
                HomeRoomId = expected.HomeRoomId,
                IsLocked = expected.IsLocked,
                Language = expected.Language,
                MustSetEmail = expected.MustSetEmail,
                NeedsToAcceptEULA = expected.NeedsToAcceptEULA,
                Phone = expected.Phone,
                UserGroups = new List<ApiUserGroup>(expected.UserGroups.Count)
            };

            foreach (UserGroup current in expected.UserGroups) {
                ApiUserGroup currentGroup = new ApiUserGroup {
                    Id = current.Id,
                    IsMember = current.IsMember,
                    Name = current.Name
                };
                param.UserGroups.Add(currentGroup);
            }

            // ACT
            UserAccount actual = UserMapper.FromApiUserAccount(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserAccountComparer());
        }

        [Fact]
        public void FromApiUserAccount_NullGroups() {
            // ARRANGE
            UserAccount expected = FactoryUser.UserAccount;
            expected.UserGroups = new List<UserGroup>();

            ApiUserAccount param = new ApiUserAccount {
                Id = expected.Id,
                AuthData = FactoryUser.ApiUserAuthData,
                UserName = expected.UserName,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Email = expected.Email,
                IsEncryptionEnabled = expected.HasEncryptionEnabled,
                HasManageableRooms = expected.HasManageableRooms,
                ExpireAt = expected.ExpireAt,
                LastLoginSuccessAt = expected.LastLoginSuccessAt,
                LastLoginFailAt = expected.LastLoginFailAt,
                UserRoles = new ApiUserRoleList {
                    Items = new List<ApiUserRole>(expected.UserRoles.Count)
                },
                HomeRoomId = expected.HomeRoomId,
                IsLocked = expected.IsLocked,
                Language = expected.Language,
                MustSetEmail = expected.MustSetEmail,
                NeedsToAcceptEULA = expected.NeedsToAcceptEULA,
                Phone = expected.Phone,
                UserGroups = null
            };

            foreach (UserRole current in expected.UserRoles) {
                ApiUserRole currentApi = new ApiUserRole {
                    Id = (int)current
                };
                param.UserRoles.Items.Add(currentApi);
            }

            // ACT
            UserAccount actual = UserMapper.FromApiUserAccount(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserAccountComparer());
        }

        [Fact]
        public void FromApiUserAccount_Null() {
            // ARRANGE
            UserAccount expected = null;

            ApiUserAccount param = null;

            // ACT
            UserAccount actual = UserMapper.FromApiUserAccount(param);

            // ASSERT
            Assert.True(expected == actual);
        }

        #endregion

        #region ToApiUserKeyPair

        [Fact]
        public void ToApiUserKeyPair() {
            // ARRANGE
            ApiUserKeyPair expected = FactoryUser.ApiUserKeyPair_2048;
            Mock.Arrange(() => UserMapper.ToApiUserKeyPairVersion(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(expected.PrivateKeyContainer.Version).Occurs(2);

            UserKeyPair param = new UserKeyPair {
                UserPrivateKey = new UserPrivateKey {
                    PrivateKey = expected.PrivateKeyContainer.PrivateKey,
                    Version = UserKeyPairAlgorithm.RSA2048
                },
                UserPublicKey = new UserPublicKey {
                    PublicKey = expected.PublicKeyContainer.PublicKey,
                    Version = UserKeyPairAlgorithm.RSA2048
                }
            };

            // ACT
            ApiUserKeyPair actual = UserMapper.ToApiUserKeyPair(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiUserKeyPairComparer());
            Mock.Assert(() => UserMapper.ToApiUserKeyPairVersion(Arg.IsAny<UserKeyPairAlgorithm>()));
        }

        #endregion

        #region FromApiUserKeyPair

        [Fact]
        public void FromApiUserKeyPair() {
            // ARRANGE
            UserKeyPair expected = FactoryUser.UserKeyPair_2048;
            Mock.Arrange(() => UserMapper.FromApiUserKeyPairVersion(Arg.AnyString)).Returns(expected.UserPrivateKey.Version).Occurs(2);

            ApiUserKeyPair param = new ApiUserKeyPair {
                PrivateKeyContainer = new ApiUserPrivateKey {
                    PrivateKey = expected.UserPrivateKey.PrivateKey,
                    Version = "A"
                },
                PublicKeyContainer = new ApiUserPublicKey {
                    PublicKey = expected.UserPublicKey.PublicKey,
                    Version = "A"
                }
            };

            // ACT
            UserKeyPair actual = UserMapper.FromApiUserKeyPair(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserKeyPairComparer());
            Mock.Assert(() => UserMapper.FromApiUserKeyPairVersion(Arg.AnyString));
        }

        #endregion

        #region ConvertApiUserIdPublicKeys

        [Fact]
        public void ConvertApiUserIdPublicKeys() {
            // ARRANGE
            Dictionary<long, UserPublicKey> expected = new Dictionary<long, UserPublicKey>(1) {
                {
                    1, FactoryUser.UserPublicKey_2048
                }
            };

            List<ApiUserIdPublicKey> param = new List<ApiUserIdPublicKey>(1) {
                new ApiUserIdPublicKey {
                    UserId = 1,
                    PublicKeyContainer = new ApiUserPublicKey {
                        PublicKey = expected[1].PublicKey,
                        Version = "A"
                    }
                }
            };

            // ACT
            Dictionary<long, UserPublicKey> actual = UserMapper.ConvertApiUserIdPublicKeys(param);

            // ASSERT
            Assert.True(expected.Count == actual.Count);
            foreach (long currentKey in expected.Keys) {
                Assert.True(actual.ContainsKey(currentKey));
            }

            foreach (long currentKey in expected.Keys) {
                Assert.True(expected[currentKey].PublicKey == actual[currentKey].PublicKey);
                Assert.True(expected[currentKey].Version == actual[currentKey].Version);
            }
        }

        #endregion

        #region FromApiAvatarInfo

        [Fact]
        public void FromApiAvatarInfo() {
            // ARRANGE
            AvatarInfo expected = FactoryUser.AvatarInfo;

            ApiAvatarInfo param = new ApiAvatarInfo {
                AvatarUuid = expected.AvatarUUID,
                IsCustomAvatar = expected.IsCustomAvatar,
                AvatarUri = "https://www.dracoon.team"
            };

            // ACT
            AvatarInfo actual = UserMapper.FromApiAvatarInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new AvatarInfoComparer());
        }

        #endregion

        #region FromApiUserKeyPairVersion

        [Fact]
        public void FromApiUserKeyPairVersion_2096() {
            // ARRANGE
            UserKeyPairAlgorithm expected = UserKeyPairAlgorithm.RSA2048;

            string param = "A";

            // ACT
            UserKeyPairAlgorithm actual = UserMapper.FromApiUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromApiUserKeyPairVersion_4096() {
            // ARRANGE
            UserKeyPairAlgorithm expected = UserKeyPairAlgorithm.RSA4096;

            string param = "RSA-4096";

            // ACT
            UserKeyPairAlgorithm actual = UserMapper.FromApiUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromApiUserKeyPairVersion_Fail() {
            // ARRANGE
            int expected = DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code;

            string param = "UnknownAlgorithm";

            try {
                // ACT
                UserMapper.FromApiUserKeyPairVersion(param);
            } catch (DracoonCryptoException e) {
                // ASSERT
                Assert.Equal(expected, e.ErrorCode.Code);
            }
        }

        #endregion

        #region ToApiUserKeyPairVersion

        [Fact]
        public void ToApiUserKeyPairVersion_2048() {
            // ARRANGE
            string expected = "A";

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA2048;

            // ACT
            string actual = UserMapper.ToApiUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToApiUserKeyPairVersion_4096() {
            // ARRANGE
            string expected = "RSA-4096";

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA4096;

            // ACT
            string actual = UserMapper.ToApiUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region FromApiShareSubscription

        [Fact]
        public void FromApiShareSubscription() {
            // ARRANGE
            ShareSubscription expected = FactoryUser.ShareSubscription;

            ApiShareSubscription param = FactoryUser.ApiShareSubscription;

            // ACT
            ShareSubscription actual = UserMapper.FromApiShareSubscription(param);

            // ASSERT
            Assert.Equal(expected, actual, new ShareSubscriptionComparer());
        }

        #endregion

        #region FromApiShareSubscriptionList

        [Fact]
        public void FromApiShareSubscriptionList() {
            // ARRANGE
            ShareSubscriptionList expected = FactoryUser.ShareSubscriptionList;

            ApiShareSubscriptionList param = FactoryUser.ApiShareSubscriptionList;

            // ACT
            ShareSubscriptionList actual = UserMapper.FromApiShareSubscriptionList(param);

            // ASSERT
            Assert.Equal(expected, actual, new ShareSubscriptionListComparer());
        }

        #endregion
    }
}