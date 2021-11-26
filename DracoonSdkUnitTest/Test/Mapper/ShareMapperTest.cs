using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using System.Collections.Generic;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class ShareMapperTest {
        #region ToUnencryptedApiCreateDownloadShareRequest

        [Theory]
        [InlineData(null, null)]
        [InlineData("0151789513,017115674615", "test@dracoon.com,test2@dracoon.com")]
        public void ToUnencryptedApiCreateDownloadShareRequest(string smsRecipients, string emailRecipients) {
            // ARRANGE
            ApiCreateDownloadShareRequest expected = FactoryShare.ApiCreateDownloadShareRequest;

            List<string> smsRecList = null;
            if (smsRecipients != null) {
                smsRecList = new List<string>();
                smsRecList.AddRange(smsRecipients.Split(','));
                expected.SmsRecipients = smsRecipients;
                expected.SendSms = true;
            }

            List<string> emailRecList = null;
            if (emailRecipients != null) {
                emailRecList = new List<string>();
                emailRecList.AddRange(emailRecipients.Split(','));
                expected.MailBody = "Some body";
                expected.MailSubject = "You received a DRACOON share!";
                expected.MailRecipients = emailRecipients;
                expected.SendMail = true;
            }

            CreateDownloadShareRequest param = new CreateDownloadShareRequest(expected.NodeId) {
                Name = expected.Name,
                Notes = expected.Notes,
                Expiration = expected.Expiration.ExpireAt,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUserName = expected.ShowCreatorUserName,
                NotifyCreator = expected.NotifyCreator,
                MaxAllowedDownloads = expected.MaxAllowedDownloads,
                AccessPassword = expected.Password,
                EmailRecipients = emailRecList,
                EmailBody = expected.MailBody,
                EmailSubject = expected.MailSubject,
                SmsRecipients = smsRecList
            };

            // ACT
            ApiCreateDownloadShareRequest actual = ShareMapper.ToUnencryptedApiCreateDownloadShareRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCreateDownloadShareRequestComparer());
        }

        #endregion

        #region FromApiDownloadShare

        [Fact]
        public void FromApiDownloadShare() {
            // ARRANGE
            DownloadShare expected = FactoryShare.DownloadShare;

            ApiDownloadShare param = new ApiDownloadShare {
                ShareId = expected.ShareId,
                NodeId = expected.NodeId,
                NodePath = expected.NodePath,
                Name = expected.Name,
                Notes = expected.Notes,
                ExpireAt = expected.ExpireAt,
                AccessKey = expected.AccessKey,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUserName = expected.ShowCreatorUserName,
                NotifyCreator = expected.NotifyCreator,
                MaxAllowedDownloads = expected.MaxAllowedDownloads,
                CurrentDownloadsCount = expected.CurrentDownloadsCount,
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    UserName = expected.CreatedBy.UserName,
                    Id = expected.CreatedBy.Id.Value
                },
                IsProtected = expected.IsProtected,
                IsEncrypted = expected.IsEncrypted,
                Type = "folder"
            };

            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.CreatedBy)).Returns(expected.CreatedBy);

            // ACT
            DownloadShare actual = ShareMapper.FromApiDownloadShare(param);

            // ASSERT
            Assert.Equal(expected, actual, new DownloadShareComparer());
        }

        #endregion

        #region FromApiDownloadShareList

        [Fact]
        public void FromApiDownloadShareList() {
            // ARRANGE
            DownloadShareList expected = FactoryShare.DownloadShareList;

            ApiDownloadShareList param = new ApiDownloadShareList {
                Range = new ApiRange {
                    Offset = expected.Offset,
                    Limit = expected.Limit,
                    Total = expected.Total
                },
                Items = new List<ApiDownloadShare>(expected.Items.Count)
            };

            foreach (DownloadShare current in expected.Items) {
                ApiDownloadShare currentApi = new ApiDownloadShare {
                    ShareId = current.ShareId,
                    NodeId = current.NodeId,
                    NodePath = current.NodePath,
                    Name = current.Name,
                    Notes = current.Notes,
                    ExpireAt = current.ExpireAt,
                    AccessKey = current.AccessKey,
                    ShowCreatorName = current.ShowCreatorName,
                    ShowCreatorUserName = current.ShowCreatorUserName,
                    NotifyCreator = current.NotifyCreator,
                    MaxAllowedDownloads = current.MaxAllowedDownloads,
                    CurrentDownloadsCount = current.CurrentDownloadsCount,
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        UserName = current.CreatedBy.UserName,
                        Id = current.CreatedBy.Id.Value
                    },
                    IsProtected = current.IsProtected,
                    IsEncrypted = current.IsEncrypted,
                    Type = "folder"
                };
                param.Items.Add(currentApi);
                Mock.Arrange(() => ShareMapper.FromApiDownloadShare(currentApi)).Returns(current);
            }

            // ACT
            DownloadShareList actual = ShareMapper.FromApiDownloadShareList(param);

            // ASSERT
            Assert.Equal(expected, actual, new DownloadShareListComparer());
        }

        #endregion

        #region ToApiCreateUploadShareRequest

        [Theory]
        [InlineData(null, null)]
        [InlineData("0151789513,017115674615", "test@dracoon.com,test2@dracoon.com")]
        public void ToApiCreateUploadShareRequest(string smsRecipients, string emailRecipients) {
            // ARRANGE
            ApiCreateUploadShareRequest expected = FactoryShare.ApiCreateUploadShareRequest;

            List<string> smsRecList = null;
            if (smsRecipients != null) {
                smsRecList = new List<string>();
                smsRecList.AddRange(smsRecipients.Split(','));
                expected.SmsRecipients = smsRecipients;
                expected.SendSms = true;
            }

            List<string> emailRecList = null;
            if (emailRecipients != null) {
                emailRecList = new List<string>();
                emailRecList.AddRange(emailRecipients.Split(','));
                expected.MailBody = "Some body";
                expected.MailSubject = "You received a DRACOON share!";
                expected.MailRecipients = emailRecipients;
                expected.SendMail = true;
            }

            CreateUploadShareRequest param = new CreateUploadShareRequest(expected.NodeId, expected.Name) {
                Notes = expected.Notes,
                Expiration = expected.Expiration.ExpireAt,
                ShowUploadedFiles = expected.ShowUploadedFiles,
                NotifyCreator = expected.NotifyCreator,
                MaxAllowedTotalSizeOverAllUploadedFiles = expected.MaxAllowedTotalSizeOverAllUploadedFiles,
                MaxAllowedUploads = expected.MaxAllowedUploads,
                AccessPassword = expected.AccessPassword,
                UploadedFilesExpirationPeriod = expected.UploadedFilesExpirationPeriod,
                EmailRecipients = emailRecList,
                EmailBody = expected.MailBody,
                EmailSubject = expected.MailSubject,
                SmsRecipients = smsRecList
            };

            // ACT
            ApiCreateUploadShareRequest actual = ShareMapper.ToApiCreateUploadShareRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCreateUploadShareRequestComparer());
        }

        #endregion

        #region FromApiUploadShare

        [Fact]
        public void FromApiUploadShare() {
            // ARRANGE
            UploadShare expected = FactoryShare.UploadShare;

            ApiUploadShare param = new ApiUploadShare {
                ShareId = expected.ShareId,
                NodeId = expected.NodeId,
                NodePath = expected.NodePath,
                Name = expected.Name,
                IsProtected = expected.IsProtected,
                AccessKey = expected.AccessKey,
                NotifyCreator = expected.NotifyCreator,
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    UserName = expected.CreatedBy.UserName,
                    Id = expected.CreatedBy.Id.Value
                },
                ExpireAt = expected.ExpireAt,
                IsEncrypted = expected.IsEncrypted,
                Notes = expected.Notes,
                UploadedFilesExpirationPeriod = expected.UploadedFilesExpirationPeriod,
                CurrentDoneUploadsCount = expected.CurrentDoneUploadsCount,
                CurrentUploadedFilesCount = expected.CurrentUploadedFilesCount,
                ShowUploadedFiles = expected.ShowUploadedFiles,
                MaxAllowedUploads = expected.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = expected.MaxAllowedTotalSizeOverAllUploadedFiles,
                Type = "folder"
            };

            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.CreatedBy)).Returns(expected.CreatedBy);

            // ACT
            UploadShare actual = ShareMapper.FromApiUploadShare(param);

            // ASSERT
            Assert.Equal(expected, actual, new UploadShareComparer());
        }

        #endregion

        #region FromApiUploadShareList

        [Fact]
        public void FromApiUploadShareList() {
            // ARRANGE
            UploadShareList expected = FactoryShare.UploadShareList;

            ApiUploadShareList param = new ApiUploadShareList {
                Range = new ApiRange {
                    Offset = expected.Offset,
                    Limit = expected.Limit,
                    Total = expected.Total
                },
                Items = new List<ApiUploadShare>(expected.Items.Count)
            };

            foreach (UploadShare current in expected.Items) {
                ApiUploadShare currentApi = new ApiUploadShare {
                    ShareId = current.ShareId,
                    NodeId = current.NodeId,
                    NodePath = current.NodePath,
                    Name = current.Name,
                    Notes = current.Notes,
                    ExpireAt = current.ExpireAt,
                    AccessKey = current.AccessKey,
                    NotifyCreator = current.NotifyCreator,
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        UserName = current.CreatedBy.UserName,
                        Id = current.CreatedBy.Id.Value
                    },
                    IsProtected = current.IsProtected,
                    IsEncrypted = current.IsEncrypted,
                    CurrentDoneUploadsCount = current.CurrentDoneUploadsCount,
                    CurrentUploadedFilesCount = current.CurrentUploadedFilesCount,
                    MaxAllowedTotalSizeOverAllUploadedFiles = current.MaxAllowedTotalSizeOverAllUploadedFiles,
                    MaxAllowedUploads = current.MaxAllowedUploads,
                    ShowUploadedFiles = current.ShowUploadedFiles,
                    UploadedFilesExpirationPeriod = current.UploadedFilesExpirationPeriod,
                    Type = "folder"
                };
                param.Items.Add(currentApi);
                Mock.Arrange(() => ShareMapper.FromApiUploadShare(currentApi)).Returns(current);
            }

            // ACT
            UploadShareList actual = ShareMapper.FromApiUploadShareList(param);

            // ASSERT
            Assert.Equal(expected, actual, new UploadShareListComparer());
        }

        #endregion
    }
}