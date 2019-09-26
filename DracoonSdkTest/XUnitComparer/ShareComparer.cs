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
            return string.Equals(x.MailBody, y.MailBody) &&
                string.Equals(x.MailRecipients, y.MailRecipients) &&
                string.Equals(x.MailSubject, y.MailSubject) &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.Password, y.Password) &&
                string.Equals(x.SmsRecipients, y.SmsRecipients) &&
                x.MaxAllowedDownloads == y.MaxAllowedDownloads &&
                x.NodeId == y.NodeId &&
                x.NotifyCreator == y.NotifyCreator &&
                x.SendMail == y.SendMail &&
                x.SendSms == y.SendSms &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName &&
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
            return string.Equals(x.AccessKey, y.AccessKey) &&
                x.Classification == y.Classification &&
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
                x.NotifyCreator == y.NotifyCreator &&
                x.ShareId == y.ShareId &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName;
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
            return string.Equals(x.AccessKey, y.AccessKey) &&
                x.Classification == y.Classification &&
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
                x.NotifyCreator == y.NotifyCreator &&
                x.ShareId == y.ShareId &&
                x.ShowCreatorName == y.ShowCreatorName &&
                x.ShowCreatorUserName == y.ShowCreatorUserName;
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
            return string.Equals(x.MailBody, y.MailBody) &&
                string.Equals(x.MailRecipients, y.MailRecipients) &&
                string.Equals(x.MailSubject, y.MailSubject) &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.SmsRecipients, y.SmsRecipients) &&
                x.NodeId == y.NodeId &&
                x.NotifyCreator == y.NotifyCreator &&
                x.SendMail == y.SendMail &&
                x.SendSms == y.SendSms &&
                string.Equals(x.AccessPassword, y.AccessPassword) &&
                x.MaxAllowedTotalSizeOverAllUploadedFiles == y.MaxAllowedTotalSizeOverAllUploadedFiles &&
                x.MaxAllowedUploads == y.MaxAllowedUploads &&
                x.ShowUploadedFiles == y.ShowUploadedFiles &&
                x.UploadedFilesExpirationPeriod == y.UploadedFilesExpirationPeriod &&
                expirationEqual;
        }

        public int GetHashCode(ApiCreateUploadShareRequest obj) {
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
            return string.Equals(x.AccessKey, y.AccessKey) &&
                x.CreatedAt == y.CreatedAt &&
                x.ExpireAt == y.ExpireAt &&
                x.IsEncrypted == y.IsEncrypted &&
                x.IsProtected == y.IsProtected &&
                x.NodeId == y.NodeId &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.NodePath, y.NodePath) &&
                string.Equals(x.Notes, y.Notes) &&
                x.NotifyCreator == y.NotifyCreator &&
                x.ShareId == y.ShareId &&
                x.CurrentDoneUploadsCount == y.CurrentDoneUploadsCount &&
                x.CurrentUploadedFilesCount == y.CurrentUploadedFilesCount &&
                x.MaxAllowedTotalSizeOverAllUploadedFiles == y.MaxAllowedTotalSizeOverAllUploadedFiles &&
                x.MaxAllowedUploads == y.MaxAllowedUploads &&
                x.UploadedFilesExpirationPeriod == y.UploadedFilesExpirationPeriod;
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
