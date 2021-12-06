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

        [Fact]
        public void ToUnencryptedApiCreateDownloadShareRequest() {
            // ARRANGE
            ApiCreateDownloadShareRequest expected = FactoryShare.ApiCreateDownloadShareRequest;

            CreateDownloadShareRequest param = new CreateDownloadShareRequest(expected.NodeId) {
                Name = expected.Name,
                Notes = expected.Notes,
                InternalNotes = expected.InternalNotes,
                Expiration = expected.Expiration.ExpireAt,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUserName = expected.ShowCreatorUserName,
                MaxAllowedDownloads = expected.MaxAllowedDownloads,
                Password = expected.Password,
                ReceiverLanguage = expected.ReceiverLanguage,
                TextMessageRecipients = expected.TextMessageRecipients
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
                InternalNotes = expected.InternalNotes,
                ExpireAt = expected.ExpireAt,
                AccessKey = expected.AccessKey,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUserName = expected.ShowCreatorUserName,
                MaxAllowedDownloads = expected.MaxAllowedDownloads,
                CurrentDownloadsCount = expected.CurrentDownloadsCount,
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    UserName = expected.CreatedBy.UserName,
                    Id = expected.CreatedBy.Id,
                    Email = expected.CreatedBy.Email,
                    FirstName = expected.CreatedBy.FirstName,
                    LastName = expected.CreatedBy.LastName,
                    UserType = "internal"
                },
                IsProtected = expected.IsProtected,
                IsEncrypted = expected.IsEncrypted,
                Type = "folder",
                DataUrl = expected.DataUrl,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = new ApiUserInfo {
                    AvatarUuid = expected.UpdatedBy.AvatarUUID,
                    UserName = expected.UpdatedBy.UserName,
                    Id = expected.UpdatedBy.Id,
                    Email = expected.UpdatedBy.Email,
                    FirstName = expected.UpdatedBy.FirstName,
                    LastName = expected.UpdatedBy.LastName,
                    UserType = "internal"
                }
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
                    MaxAllowedDownloads = current.MaxAllowedDownloads,
                    CurrentDownloadsCount = current.CurrentDownloadsCount,
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        UserName = current.CreatedBy.UserName,
                        Id = current.CreatedBy.Id,
                        Email = current.CreatedBy.Email,
                        FirstName = current.CreatedBy.FirstName,
                        LastName = current.CreatedBy.LastName,
                        UserType = "internal"
                    },
                    IsProtected = current.IsProtected,
                    IsEncrypted = current.IsEncrypted,
                    Type = "folder",
                    DataUrl = current.DataUrl,
                    UpdatedAt = current.UpdatedAt,
                    UpdatedBy = new ApiUserInfo {
                        AvatarUuid = current.UpdatedBy.AvatarUUID,
                        UserName = current.UpdatedBy.UserName,
                        Id = current.UpdatedBy.Id,
                        Email = current.UpdatedBy.Email,
                        FirstName = current.UpdatedBy.FirstName,
                        LastName = current.UpdatedBy.LastName,
                        UserType = "internal"
                    }
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

        [Fact]
        public void ToApiCreateUploadShareRequest() {
            // ARRANGE
            ApiCreateUploadShareRequest expected = FactoryShare.ApiCreateUploadShareRequest;

            CreateUploadShareRequest param = new CreateUploadShareRequest(expected.NodeId, expected.Name) {
                Notes = expected.Notes,
                InternalNotes = expected.InternalNotes,
                Expiration = expected.Expiration.ExpireAt,
                ShowUploadedFiles = expected.ShowUploadedFiles,
                MaxAllowedTotalSizeOverAllUploadedFiles = expected.MaxAllowedTotalSizeOverAllUploadedFiles,
                MaxAllowedUploads = expected.MaxAllowedUploads,
                Password = expected.Password,
                UploadedFilesExpirationPeriod = expected.UploadedFilesExpirationPeriod,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUsername = expected.ShowCreatorUsername,
                ReceiverLanguage = expected.ReceiverLanguage,
                TextMessageRecipients = expected.TextMessageRecipients
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
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    UserName = expected.CreatedBy.UserName,
                    Id = expected.CreatedBy.Id,
                    Email = expected.CreatedBy.Email,
                    FirstName = expected.CreatedBy.FirstName,
                    LastName = expected.CreatedBy.LastName,
                    UserType = "internal"
                },
                ExpireAt = expected.ExpireAt,
                IsEncrypted = expected.IsEncrypted,
                Notes = expected.Notes,
                InternalNotes = expected.InternalNotes,
                UploadedFilesExpirationPeriod = expected.UploadedFilesExpirationPeriod,
                CurrentDoneUploadsCount = expected.CurrentDoneUploadsCount,
                CurrentUploadedFilesCount = expected.CurrentUploadedFilesCount,
                ShowUploadedFiles = expected.ShowUploadedFiles,
                MaxAllowedUploads = expected.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = expected.MaxAllowedTotalSizeOverAllUploadedFiles,
                Type = "folder",
                DataUrl = expected.DataUrl,
                ShowCreatorName = expected.ShowCreatorName,
                ShowCreatorUsername = expected.ShowCreatorUsername,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = new ApiUserInfo {
                    AvatarUuid = expected.UpdatedBy.AvatarUUID,
                    UserName = expected.UpdatedBy.UserName,
                    Id = expected.UpdatedBy.Id,
                    Email = expected.UpdatedBy.Email,
                    FirstName = expected.UpdatedBy.FirstName,
                    LastName = expected.UpdatedBy.LastName,
                    UserType = "internal"
                }
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
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        UserName = current.CreatedBy.UserName,
                        Id = current.CreatedBy.Id,
                        Email = current.CreatedBy.Email,
                        FirstName = current.CreatedBy.FirstName,
                        LastName = current.CreatedBy.LastName,
                        UserType = "internal"
                    },
                    IsProtected = current.IsProtected,
                    IsEncrypted = current.IsEncrypted,
                    CurrentDoneUploadsCount = current.CurrentDoneUploadsCount,
                    CurrentUploadedFilesCount = current.CurrentUploadedFilesCount,
                    MaxAllowedTotalSizeOverAllUploadedFiles = current.MaxAllowedTotalSizeOverAllUploadedFiles,
                    MaxAllowedUploads = current.MaxAllowedUploads,
                    ShowUploadedFiles = current.ShowUploadedFiles,
                    UploadedFilesExpirationPeriod = current.UploadedFilesExpirationPeriod,
                    Type = "folder",
                    DataUrl = current.DataUrl,
                    ShowCreatorName = current.ShowCreatorName,
                    ShowCreatorUsername = current.ShowCreatorUsername,
                    UpdatedAt = current.UpdatedAt,
                    UpdatedBy = new ApiUserInfo {
                        AvatarUuid = current.UpdatedBy.AvatarUUID,
                        UserName = current.UpdatedBy.UserName,
                        Id = current.UpdatedBy.Id,
                        Email = current.UpdatedBy.Email,
                        FirstName = current.UpdatedBy.FirstName,
                        LastName = current.UpdatedBy.LastName,
                        UserType = "internal"
                    }
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