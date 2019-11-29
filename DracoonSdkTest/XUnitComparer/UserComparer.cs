using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class UserInfoComparer : IEqualityComparer<UserInfo> {
        public bool Equals(UserInfo x, UserInfo y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Id == y.Id &&
                string.Equals(x.DisplayName, y.DisplayName) &&
                string.Equals(x.AvatarUUID, y.AvatarUUID);
        }

        public int GetHashCode(UserInfo obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUserInfoComparer : IEqualityComparer<ApiUserInfo> {
        public bool Equals(ApiUserInfo x, ApiUserInfo y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Id == y.Id &&
                string.Equals(x.DisplayName, y.DisplayName) &&
                string.Equals(x.AvatarUuid, y.AvatarUuid);
        }

        public int GetHashCode(ApiUserInfo obj) {
            throw new NotImplementedException();
        }
    }

    internal class UserAccountComparer : IEqualityComparer<UserAccount> {
        public bool Equals(UserAccount x, UserAccount y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Id == y.Id &&
                string.Equals(x.LoginName, y.LoginName) &&
                string.Equals(x.UserName, y.UserName) &&
                string.Equals(x.Title, y.Title) &&
                string.Equals(x.FirstName, y.FirstName) &&
                string.Equals(x.LastName, y.LastName) &&
                string.Equals(x.Email, y.Email) &&
                x.HasEncryptionEnabled == y.HasEncryptionEnabled &&
                x.HasManageableRooms == y.HasManageableRooms &&
                x.ExpireAt == y.ExpireAt &&
                x.LastLoginSuccessAt == y.LastLoginSuccessAt &&
                x.LastLoginFailAt == y.LastLoginFailAt &&
                x.HomeRoomId == y.HomeRoomId &&
                CompareHelper.ListIsEqual(x.UserRoles, y.UserRoles);
        }

        public int GetHashCode(UserAccount obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUserKeyPairComparer : IEqualityComparer<ApiUserKeyPair> {
        public bool Equals(ApiUserKeyPair x, ApiUserKeyPair y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.PublicKeyContainer, y.PublicKeyContainer, new ApiUserPublicKeyComparer());
            Assert.Equal(x.PrivateKeyContainer, y.PrivateKeyContainer, new ApiUserPrivateKeyComparer());
            return true;
        }

        public int GetHashCode(ApiUserKeyPair obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUserPublicKeyComparer : IEqualityComparer<ApiUserPublicKey> {
        public bool Equals(ApiUserPublicKey x, ApiUserPublicKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.PublicKey, y.PublicKey) &&
                string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(ApiUserPublicKey obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUserPrivateKeyComparer : IEqualityComparer<ApiUserPrivateKey> {
        public bool Equals(ApiUserPrivateKey x, ApiUserPrivateKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.PrivateKey, y.PrivateKey) &&
                string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(ApiUserPrivateKey obj) {
            throw new NotImplementedException();
        }
    }

    internal class UserKeyPairComparer : IEqualityComparer<UserKeyPair> {
        public bool Equals(UserKeyPair x, UserKeyPair y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.UserPublicKey, y.UserPublicKey, new UserPublicKeyComparer());
            Assert.Equal(x.UserPrivateKey, y.UserPrivateKey, new UserPrivateKeyComparer());
            return true;
        }

        public int GetHashCode(UserKeyPair obj) {
            throw new NotImplementedException();
        }
    }

    internal class UserPublicKeyComparer : IEqualityComparer<UserPublicKey> {
        public bool Equals(UserPublicKey x, UserPublicKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.PublicKey, y.PublicKey) &&
                string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(UserPublicKey obj) {
            throw new NotImplementedException();
        }
    }

    internal class UserPrivateKeyComparer : IEqualityComparer<UserPrivateKey> {
        public bool Equals(UserPrivateKey x, UserPrivateKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.PrivateKey, y.PrivateKey) &&
                string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(UserPrivateKey obj) {
            throw new NotImplementedException();
        }
    }

    internal class AvatarInfoComparer : IEqualityComparer<AvatarInfo> {
        public bool Equals(AvatarInfo x, AvatarInfo y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.IsCustomAvatar == y.IsCustomAvatar &&
                string.Equals(x.AvatarUUID, y.AvatarUUID);
        }

        public int GetHashCode(AvatarInfo obj) {
            throw new NotImplementedException();
        }
    }
}
