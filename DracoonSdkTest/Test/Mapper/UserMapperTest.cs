using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class UserMapperTest {
        #region FromApiUserInfo

        [Fact]
        public void FromApiUserInfo() {
            // ARRANGE
            UserInfo expected = FactoryUser.UserInfo;

            ApiUserInfo param = new ApiUserInfo {
                Id = expected.Id.Value,
                DisplayName = expected.DisplayName,
                AvatarUuid = expected.AvatarUUID,
                Email = expected.Email,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Title = expected.Title,
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
                LoginName = expected.LoginName,
                UserName = expected.UserName,
                Title = expected.Title,
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
                HomeRoomId = 2
            };

            foreach (UserRole current in expected.UserRoles) {
                ApiUserRole currentApi = new ApiUserRole {
                    Id = (int) current
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
                LoginName = expected.LoginName,
                UserName = expected.UserName,
                Title = expected.Title,
                FirstName = expected.FirstName,
                LastName = expected.LastName,
                Email = expected.Email,
                IsEncryptionEnabled = expected.HasEncryptionEnabled,
                HasManageableRooms = expected.HasManageableRooms,
                ExpireAt = expected.ExpireAt,
                LastLoginSuccessAt = expected.LastLoginSuccessAt,
                LastLoginFailAt = expected.LastLoginFailAt,
                UserRoles = null,
                HomeRoomId = 2
            };

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
            ApiUserKeyPair expected = FactoryUser.ApiUserKeyPair;

            UserKeyPair param = new UserKeyPair {
                UserPrivateKey = new UserPrivateKey {
                    PrivateKey = expected.PrivateKeyContainer.PrivateKey,
                    Version = expected.PrivateKeyContainer.Version
                },
                UserPublicKey = new UserPublicKey {
                    PublicKey = expected.PublicKeyContainer.PublicKey,
                    Version = expected.PublicKeyContainer.Version
                }
            };

            // ACT
            ApiUserKeyPair actual = UserMapper.ToApiUserKeyPair(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiUserKeyPairComparer());
        }

        #endregion

        #region FromApiUserKeyPair

        [Fact]
        public void FromApiUserKeyPair() {
            // ARRANGE
            UserKeyPair expected = FactoryUser.UserKeyPair;

            ApiUserKeyPair param = new ApiUserKeyPair {
                PrivateKeyContainer = new ApiUserPrivateKey {
                    PrivateKey = expected.UserPrivateKey.PrivateKey,
                    Version = expected.UserPrivateKey.Version
                },
                PublicKeyContainer = new ApiUserPublicKey {
                    PublicKey = expected.UserPublicKey.PublicKey,
                    Version = expected.UserPublicKey.Version
                }
            };

            // ACT
            UserKeyPair actual = UserMapper.FromApiUserKeyPair(param);

            // ASSERT
            Assert.Equal(expected, actual, new UserKeyPairComparer());
        }

        #endregion

        #region ConvertApiUserIdPublicKeys

        [Fact]
        public void ConvertApiUserIdPublicKeys() {
            // ARRANGE
            Dictionary<long, UserPublicKey> expected = new Dictionary<long, UserPublicKey>(1) {
                {
                    1, FactoryUser.UserPublicKey
                }
            };

            List<ApiUserIdPublicKey> param = new List<ApiUserIdPublicKey>(1) {
                new ApiUserIdPublicKey {
                    UserId = 1,
                    PublicKeyContainer = new ApiUserPublicKey {
                        PublicKey = expected[1].PublicKey,
                        Version = expected[1].Version
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
    }
}