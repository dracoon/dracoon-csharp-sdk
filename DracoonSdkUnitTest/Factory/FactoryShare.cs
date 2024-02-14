using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;
using System.Text;

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
            MaxAllowedDownloads = 5,
            CurrentDownloadsCount = 3,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.UserInfo,
            IsProtected = true,
            IsEncrypted = true,
            Type = NodeType.Folder,
            DataUrl = null,
            UpdatedAt = new DateTime(2001, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.UserInfo
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
            MaxAllowedDownloads = 5,
            CurrentDownloadsCount = 3,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.ApiUserInfo,
            IsProtected = true,
            IsEncrypted = true,
            Type = "folder",
            DataUrl = null,
            UpdatedAt = new DateTime(2001, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.ApiUserInfo
        };

        internal static UploadShare UploadShare => new UploadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "Root/SubRoot",
            Name = "ShareName",
            IsProtected = true,
            AccessKey = "SKHGF34GDHJK",
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
            Type = NodeType.Folder,
            DataUrl = null,
            ShowCreatorName = true,
            ShowCreatorUsername = false,
            UpdatedAt = new DateTime(2001, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.UserInfo
        };

        internal static ApiUploadShare ApiUploadShare => new ApiUploadShare {
            ShareId = 1,
            NodeId = 2,
            NodePath = "Root/SubRoot",
            Name = "ShareName",
            IsProtected = true,
            AccessKey = "SKHGF34GDHJK",
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
            Type = "folder",
            DataUrl = null,
            ShowCreatorName = true,
            ShowCreatorUsername = false,
            UpdatedAt = new DateTime(2001, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.ApiUserInfo
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
            MaxAllowedDownloads = 5,
            Password = "Pass1234!",
            ReceiverLanguage = "de",
            TextMessageRecipients = new List<string> { "092834574" }
        };

        internal static CreateDownloadShareRequest CreateDownloadShareRequest => new CreateDownloadShareRequest(567986) {
            Name = "Share",
            Notes = "Some notes.",
            InternalNotes = "Only for internal view.",
            Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
            ShowCreatorName = true,
            ShowCreatorUserName = false,
            MaxAllowedDownloads = 5,
            Password = Encoding.UTF8.GetBytes("Pass1234!"),
            ReceiverLanguage = "de",
            TextMessageRecipients = new List<string> { "092834574" }
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
            MaxAllowedTotalSizeOverAllUploadedFiles = 12345,
            MaxAllowedUploads = 4,
            Password = "Pass1234!",
            UploadedFilesExpirationPeriod = 1000,
            ShowCreatorName = true,
            ShowCreatorUsername = false,
            ReceiverLanguage = "de",
            TextMessageRecipients = new List<string> { "092834574" }
        };

        internal static CreateUploadShareRequest CreateUploadShareRequest => new CreateUploadShareRequest(45638, "Share") {
            Notes = "Some notes.",
            InternalNotes = "Some internal notes.",
            Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
            ShowUploadedFiles = true,
            MaxAllowedTotalSizeOverAllUploadedFiles = 12345,
            MaxAllowedUploads = 4,
            Password = "Pass1234!",
            UploadedFilesExpirationPeriod = 1000,
            ShowCreatorName = true,
            ShowCreatorUsername = false,
            ReceiverLanguage = "de",
            TextMessageRecipients = new List<string> { "092834574" }
        };

        internal static MailShareInfoRequest MailShareInfoRequest => new MailShareInfoRequest(123, "I'm a mail info message for a share!", new List<string> { "0481234", "049123" }) {
            ReceiverLanguage = "de"
        };

        internal static ApiMailShareInfoRequest ApiMailShareInfoRequest => new ApiMailShareInfoRequest() {
            Body = "I'm a mail info message for a share!",
            Recipients = new List<string> { "0481234", "049123" },
            ReceiverLanguage = "de"
        };
    }
}