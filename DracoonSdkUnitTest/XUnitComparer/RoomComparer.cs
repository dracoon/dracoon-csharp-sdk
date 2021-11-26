using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class ApiCreateRoomRequestComparer : IEqualityComparer<ApiCreateRoomRequest> {
        public bool Equals(ApiCreateRoomRequest x, ApiCreateRoomRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return CompareHelper.ListIsEqual(x.AdminGroupIds, y.AdminGroupIds) &&
                CompareHelper.ListIsEqual(x.AdminIds, y.AdminIds) &&
                x.InheritPermissions == y.InheritPermissions &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.NewGroupMemberAcceptance, y.NewGroupMemberAcceptance) &&
                string.Equals(x.Notes, y.Notes) &&
                x.ParentId == y.ParentId &&
                x.Quota == y.Quota &&
                x.RecycleBinRetentionPeriod == y.RecycleBinRetentionPeriod;
        }

        public int GetHashCode(ApiCreateRoomRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUpdateRoomRequestComparer : IEqualityComparer<ApiUpdateRoomRequest> {
        public bool Equals(ApiUpdateRoomRequest x, ApiUpdateRoomRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Quota == y.Quota &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes);
        }

        public int GetHashCode(ApiUpdateRoomRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiEnableRoomEncryptionRequestComparer : IEqualityComparer<ApiEnableRoomEncryptionRequest> {
        public bool Equals(ApiEnableRoomEncryptionRequest x, ApiEnableRoomEncryptionRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.DataRoomRescueKey, y.DataRoomRescueKey, new ApiUserKeyPairComparer());
            return x.IsEncryptionEnabled == y.IsEncryptionEnabled &&
                x.UseDataSpaceRescueKey == y.UseDataSpaceRescueKey;
        }

        public int GetHashCode(ApiEnableRoomEncryptionRequest obj) {
            throw new NotImplementedException();
        }
    }
}
