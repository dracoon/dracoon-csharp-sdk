using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class ApiCreateDownloadShareRequestComparer : IEqualityComparer<ApiCreateDownloadShareRequest> {
        public bool Equals(ApiCreateDownloadShareRequest x, ApiCreateDownloadShareRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            bool expirationEqual = false;
            if (x != null && y != null && x.Expiration != null && y.Expiration != null) {
                if (x.Expiration.EnableExpiration == y.Expiration.EnableExpiration
                    && y.Expiration.ExpireAt == y.Expiration.ExpireAt) {
                    expirationEqual = true;
                }
            }
            if (x.Expiration == null && y.Expiration == null) {
                expirationEqual = true;
            }
            Assert.Equal(x.FileKey, y.FileKey, new ApiFileKeyComparer());
            Assert.Equal(x.KeyPair, y.KeyPair, new ApiUserKeyPairComparer());
            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.InternalNotes, y.InternalNotes) &&
                string.Equals(x.Password, y.Password) &&
                x.MaxAllowedDownloads == y.MaxAllowedDownloads &&
                x.NodeId == y.NodeId &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName &&
                x.ReceiverLanguage == y.ReceiverLanguage &&
                x.TextMessageRecipients == y.TextMessageRecipients &&
                expirationEqual;
        }

        public int GetHashCode(ApiCreateDownloadShareRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiDownloadShareComparer : IEqualityComparer<ApiDownloadShare> {
        public bool Equals(ApiDownloadShare x, ApiDownloadShare y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CreatedBy, y.CreatedBy, new ApiUserInfoComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new ApiUserInfoComparer());
            return string.Equals(x.AccessKey, y.AccessKey) &&
                   x.CreatedAt == y.CreatedAt &&
                x.CurrentDownloadsCount == y.CurrentDownloadsCount &&
                x.ExpireAt == y.ExpireAt &&
                x.IsEncrypted == y.IsEncrypted &&
                x.IsProtected == y.IsProtected &&
                x.MaxAllowedDownloads == y.MaxAllowedDownloads &&
                x.NodeId == y.NodeId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.NodePath, y.NodePath) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.InternalNotes, y.InternalNotes) &&
                x.ShareId == y.ShareId &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName &&
                string.Equals(x.DataUrl, y.DataUrl) &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(ApiDownloadShare obj) {
            throw new NotImplementedException();
        }
    }

    internal class DownloadShareComparer : IEqualityComparer<DownloadShare> {
        public bool Equals(DownloadShare x, DownloadShare y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CreatedBy, y.CreatedBy, new UserInfoComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());
            return string.Equals(x.AccessKey, y.AccessKey) &&
                   x.CreatedAt == y.CreatedAt &&
                x.CurrentDownloadsCount == y.CurrentDownloadsCount &&
                x.ExpireAt == y.ExpireAt &&
                x.IsEncrypted == y.IsEncrypted &&
                x.IsProtected == y.IsProtected &&
                x.MaxAllowedDownloads == y.MaxAllowedDownloads &&
                x.NodeId == y.NodeId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.NodePath, y.NodePath) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.InternalNotes, y.InternalNotes) &&
                x.ShareId == y.ShareId &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName &&
                string.Equals(x.DataUrl, y.DataUrl) &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(DownloadShare obj) {
            throw new NotImplementedException();
        }
    }

    internal class DownloadShareListComparer : IEqualityComparer<DownloadShareList> {
        public bool Equals(DownloadShareList x, DownloadShareList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                x.Limit == y.Limit &&
                x.Total == y.Total &&
                CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(DownloadShareList obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiCreateUploadShareRequestComparer : IEqualityComparer<ApiCreateUploadShareRequest> {
        public bool Equals(ApiCreateUploadShareRequest x, ApiCreateUploadShareRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            bool expirationEqual = false;
            if (x != null && y != null && x.Expiration != null && y.Expiration != null) {
                if (x.Expiration.EnableExpiration == y.Expiration.EnableExpiration
                    && y.Expiration.ExpireAt == y.Expiration.ExpireAt) {
                    expirationEqual = true;
                }
            }
            if (x.Expiration == null && y.Expiration == null) {
                expirationEqual = true;
            }
            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.InternalNotes, y.InternalNotes) &&
                x.NodeId == y.NodeId &&
                string.Equals(x.Password, y.Password) &&
                x.MaxAllowedTotalSizeOverAllUploadedFiles == y.MaxAllowedTotalSizeOverAllUploadedFiles &&
                x.MaxAllowedUploads == y.MaxAllowedUploads &&
                x.ShowUploadedFiles == y.ShowUploadedFiles &&
                x.UploadedFilesExpirationPeriod == y.UploadedFilesExpirationPeriod &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUsername == y.ShowCreatorUsername &&
                x.ReceiverLanguage == y.ReceiverLanguage &&
                x.TextMessageRecipients == y.TextMessageRecipients &&
                expirationEqual;
        }

        public int GetHashCode(ApiCreateUploadShareRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiMailShareInfoRequestComparer : IEqualityComparer<ApiMailShareInfoRequest> {
        public bool Equals(ApiMailShareInfoRequest x, ApiMailShareInfoRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return string.Equals(x.Body, y.Body) &&
                string.Equals(x.ReceiverLanguage, y.ReceiverLanguage) &&
                CompareHelper.ListIsEqual(x.Recipients, y.Recipients);
        }

        public int GetHashCode(ApiMailShareInfoRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class UploadShareComparer : IEqualityComparer<UploadShare> {
        public bool Equals(UploadShare x, UploadShare y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CreatedBy, y.CreatedBy, new UserInfoComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());
            return string.Equals(x.AccessKey, y.AccessKey) &&
                x.CreatedAt == y.CreatedAt &&
                x.ExpireAt == y.ExpireAt &&
                x.IsEncrypted == y.IsEncrypted &&
                x.IsProtected == y.IsProtected &&
                x.NodeId == y.NodeId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.NodePath, y.NodePath) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.InternalNotes, y.InternalNotes) &&
                x.ShareId == y.ShareId &&
                x.CurrentDoneUploadsCount == y.CurrentDoneUploadsCount &&
                x.CurrentUploadedFilesCount == y.CurrentUploadedFilesCount &&
                x.MaxAllowedTotalSizeOverAllUploadedFiles == y.MaxAllowedTotalSizeOverAllUploadedFiles &&
                x.MaxAllowedUploads == y.MaxAllowedUploads &&
                x.UploadedFilesExpirationPeriod == y.UploadedFilesExpirationPeriod &&
                string.Equals(x.DataUrl, y.DataUrl) &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUsername == y.ShowCreatorUsername &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(UploadShare obj) {
            throw new NotImplementedException();
        }
    }

    internal class UploadShareListComparer : IEqualityComparer<UploadShareList> {
        public bool Equals(UploadShareList x, UploadShareList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                x.Limit == y.Limit &&
                x.Total == y.Total &&
                CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(UploadShareList obj) {
            throw new NotImplementedException();
        }
    }
}
