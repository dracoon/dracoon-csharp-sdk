using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryShare {
        internal static DownloadShare DownloadShare => new DownloadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "/Root/SubRoot",
            Name = "ShareName",
            Notes = "Some notes!",
            InternalNotes = "Some internal notes.",
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            AccessKey = "LFD98725GVD!",
            ShowCreatorName = true,
            ShowCreatorUserName = false,
            NotifyCreator = true,
            MaxAllowedDownloads = 5,
            CurrentDownloadsCount = 3,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.UserInfo,
            IsProtected = true,
            IsEncrypted = true,
            Type = NodeType.Folder
        };

        internal static DownloadShareList DownloadShareList => new DownloadShareList {
            Offset = 0,
            Limit = 1,
            Total = 3,
            Items = new List<DownloadShare> {
                        DownloadShare
                    }
        };

        internal static ApiDownloadShareList ApiDownloadShareList => new ApiDownloadShareList {
            Range = new ApiRange {
                Limit = 1,
                Offset = 0,
                Total = 1
            },
            Items = new List<ApiDownloadShare> {
                        ApiDownloadShare
                    }
        };

        internal static ApiDownloadShare ApiDownloadShare => new ApiDownloadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "/Root/SubRoot",
            Name = "ShareName",
            Notes = "Some notes!",
            InternalNotes = "Some internal notes.",
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            AccessKey = "LFD98725GVD!",
            ShowCreatorName = true,
            ShowCreatorUserName = false,
            NotifyCreator = true,
            MaxAllowedDownloads = 5,
            CurrentDownloadsCount = 3,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.ApiUserInfo,
            IsProtected = true,
            IsEncrypted = true,
            Type = "folder"
        };

        internal static UploadShare UploadShare => new UploadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "Root/SubRoot",
            Name = "ShareName",
            IsProtected = true,
            AccessKey = "SKHGF34GDHJK",
            NotifyCreator = false,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            CreatedBy = FactoryUser.UserInfo,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            IsEncrypted = true,
            Notes = "Some notes.",
            InternalNotes = "Some internal notes.",
            UploadedFilesExpirationPeriod = 10,
            CurrentDoneUploadsCount = 3,
            CurrentUploadedFilesCount = 5,
            ShowUploadedFiles = false,
            MaxAllowedUploads = 7,
            MaxAllowedTotalSizeOverAllUploadedFiles = 51348,
            Type = NodeType.Folder
        };

        internal static ApiUploadShare ApiUploadShare => new ApiUploadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "Root/SubRoot",
            Name = "ShareName",
            IsProtected = true,
            AccessKey = "SKHGF34GDHJK",
            NotifyCreator = false,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            CreatedBy = FactoryUser.ApiUserInfo,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            IsEncrypted = true,
            Notes = "Some notes.",
            InternalNotes = "Some internal notes.",
            UploadedFilesExpirationPeriod = 10,
            CurrentDoneUploadsCount = 3,
            CurrentUploadedFilesCount = 5,
            ShowUploadedFiles = false,
            MaxAllowedUploads = 7,
            MaxAllowedTotalSizeOverAllUploadedFiles = 51348,
            Type = "folder"
        };

        internal static UploadShareList UploadShareList => new UploadShareList {
            Offset = 0,
            Limit = 1,
            Total = 3,
            Items = new List<UploadShare> {
                        UploadShare
                    }
        };

        internal static ApiUploadShareList ApiUploadShareList => new ApiUploadShareList {
            Range = new ApiRange {
                Limit = 1,
                Offset = 0,
                Total = 1
            },
            Items = new List<ApiUploadShare> {
                        ApiUploadShare
                    }
        };

        internal static ApiCreateDownloadShareRequest ApiCreateDownloadShareRequest => new ApiCreateDownloadShareRequest {
            NodeId = 567986,
            Name = "Share",
            Notes = "Some notes.",
            InternalNotes = "Only for internal view.",
            Expiration = new ApiExpiration {
                ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                EnableExpiration = true
            },
            ShowCreatorName = true,
            ShowCreatorUserName = false,
            NotifyCreator = true,
            MaxAllowedDownloads = 5,
            Password = "Pass1234!",
            MailRecipients = null,
            MailBody = null,
            MailSubject = null,
            SendMail = false,
            SmsRecipients = null,
            SendSms = false
        };

        internal static CreateDownloadShareRequest CreateDownloadShareRequest => new CreateDownloadShareRequest(567986) {
            Name = "Share",
            Notes = "Some notes.",
            InternalNotes = "Only for internal view.",
            Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
            ShowCreatorName = true,
            ShowCreatorUserName = false,
            NotifyCreator = true,
            MaxAllowedDownloads = 5,
            AccessPassword = "Pass1234!",
            EncryptionPassword = "Pass1234!",
            EmailRecipients = null,
            EmailBody = null,
            EmailSubject = null,
            SmsRecipients = null
        };

        internal static ApiCreateUploadShareRequest ApiCreateUploadShareRequest => new ApiCreateUploadShareRequest {
            NodeId = 45638,
            Name = "Share",
            Notes = "Some notes.",
            InternalNotes = "Some internal notes.",
            Expiration = new ApiExpiration {
                ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                EnableExpiration = true
            },
            ShowUploadedFiles = true,
            NotifyCreator = false,
            MaxAllowedTotalSizeOverAllUploadedFiles = 12345,
            MaxAllowedUploads = 4,
            AccessPassword = "Pass1234!",
            UploadedFilesExpirationPeriod = 1000,
            MailRecipients = null,
            MailBody = null,
            MailSubject = null,
            SendMail = false,
            SmsRecipients = null,
            SendSms = false
        };

        internal static CreateUploadShareRequest CreateUploadShareRequest => new CreateUploadShareRequest(45638, "Share") {
            Notes = "Some notes.",
            InternalNotes = "Some internal notes.",
            Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
            ShowUploadedFiles = true,
            NotifyCreator = false,
            MaxAllowedTotalSizeOverAllUploadedFiles = 12345,
            MaxAllowedUploads = 4,
            AccessPassword = "Pass1234!",
            UploadedFilesExpirationPeriod = 1000,
            EmailRecipients = null,
            EmailBody = null,
            EmailSubject = null,
            SmsRecipients = null,
        };
    }
}