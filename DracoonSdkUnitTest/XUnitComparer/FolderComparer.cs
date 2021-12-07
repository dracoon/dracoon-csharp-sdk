using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class ApiCreateFolderRequestComparer : IEqualityComparer<ApiCreateFolderRequest> {
        public bool Equals(ApiCreateFolderRequest x, ApiCreateFolderRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.ParentId == y.ParentId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                x.Classification == y.Classification &&
                x.TimestampCreation == y.TimestampCreation &&
                x.TimestampModification == y.TimestampModification;
        }

        public int GetHashCode(ApiCreateFolderRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiUpdateFolderRequestComparer : IEqualityComparer<ApiUpdateFolderRequest> {
        public bool Equals(ApiUpdateFolderRequest x, ApiUpdateFolderRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes);
        }

        public int GetHashCode(ApiUpdateFolderRequest obj) {
            throw new NotImplementedException();
        }
    }
}
