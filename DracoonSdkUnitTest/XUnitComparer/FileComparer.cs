using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class ApiUpdateFileRequestComparer : IEqualityComparer<ApiUpdateFileRequest> {
        public bool Equals(ApiUpdateFileRequest x, ApiUpdateFileRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            bool expirationEqual = false;
            if (x != null && y != null && x.Expiration != null && y.Expiration != null) {
                if (x.Expiration.EnableExpiration == y.Expiration.EnableExpiration
                    && x.Expiration.ExpireAt == y.Expiration.ExpireAt) {
                    expirationEqual = true;
                }
            }
            if (x.Expiration == null && y.Expiration == null) {
                expirationEqual = true;
            }
            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                x.Classification == y.Classification &&
                expirationEqual;
        }

        public int GetHashCode(ApiUpdateFileRequest obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class ApiFileKeyComparer : IEqualityComparer<ApiFileKey> {
        public bool Equals(ApiFileKey x, ApiFileKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.Iv, y.Iv) &&
                string.Equals(x.Key, y.Key) &&
                string.Equals(x.Tag, y.Tag) &&
                string.Equals(x.Version, y.Version);

        }

        public int GetHashCode(ApiFileKey obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class EncryptedFileKeyComparer : IEqualityComparer<EncryptedFileKey> {
        public bool Equals(EncryptedFileKey x, EncryptedFileKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.Iv, y.Iv) &&
                string.Equals(x.Key, y.Key) &&
                string.Equals(x.Tag, y.Tag) &&
                string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(EncryptedFileKey obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class PlainFileKeyComparer : IEqualityComparer<PlainFileKey> {
        public bool Equals(PlainFileKey x, PlainFileKey y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.Iv, y.Iv) &&
                   Enumerable.SequenceEqual(x.Key, y.Key) &&
                   string.Equals(x.Tag, y.Tag) &&
                   string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(PlainFileKey obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class ApiCreateFileUploadComparer : IEqualityComparer<ApiCreateFileUpload> {
        public bool Equals(ApiCreateFileUpload x, ApiCreateFileUpload y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            bool expirationEqual = false;
            if (x != null && y != null && x.Expiration != null && y.Expiration != null) {
                if (x.Expiration.EnableExpiration == y.Expiration.EnableExpiration
                    && x.Expiration.ExpireAt == y.Expiration.ExpireAt) {
                    expirationEqual = true;
                }
            }
            if (x.Expiration == null && y.Expiration == null) {
                expirationEqual = true;
            }
            return x.ParentId == y.ParentId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                x.Classification == y.Classification &&
                expirationEqual;
        }

        public int GetHashCode(ApiCreateFileUpload obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class ApiCompleteFileUploadComparer : IEqualityComparer<ApiCompleteFileUpload> {
        public bool Equals(ApiCompleteFileUpload x, ApiCompleteFileUpload y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.FileKey, y.FileKey, new ApiFileKeyComparer());
            return string.Equals(x.FileName, y.FileName) &&
                string.Equals(x.ResolutionStrategy, y.ResolutionStrategy) &&
                x.KeepShareLinks == y.KeepShareLinks;
        }

        public int GetHashCode(ApiCompleteFileUpload obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class FileVirusProtectionInfoComparer : IEqualityComparer<FileVirusProtectionInfo> {
        public bool Equals(FileVirusProtectionInfo x, FileVirusProtectionInfo y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Verdict == y.Verdict &&
                x.CheckedAt == y.CheckedAt &&
                x.Sha256 == y.Sha256 &&
                x.NodeId == y.NodeId;
        }

        public int GetHashCode(FileVirusProtectionInfo obj) {
            throw new System.NotImplementedException();
        }
    }
}
