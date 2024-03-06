using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Util;
using System.Collections.Generic;
using System.Text;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class ShareMapper {
        internal static ApiCreateDownloadShareRequest ToUnencryptedApiCreateDownloadShareRequest(CreateDownloadShareRequest request) {
            ApiExpiration apiExpiration = null;
            if (request.Expiration.HasValue) {
                apiExpiration = new ApiExpiration {
                    ExpireAt = request.Expiration,
                    EnableExpiration = request.Expiration.Value.Ticks != 0
                };
            }

            ApiCreateDownloadShareRequest apiCreateDownloadShareRequest = new ApiCreateDownloadShareRequest {
                NodeId = request.NodeId,
                Name = request.Name,
                Notes = request.Notes,
                InternalNotes = request.InternalNotes,
                Expiration = apiExpiration,
                ShowCreatorName = request.ShowCreatorName,
                ShowCreatorUserName = request.ShowCreatorUserName,
                MaxAllowedDownloads = request.MaxAllowedDownloads,
                Password = request.Password != null ? Encoding.UTF8.GetString(request.Password, 0, request.Password.Length) : null,
                ReceiverLanguage = request.ReceiverLanguage,
                TextMessageRecipients = request.TextMessageRecipients
            };

            return apiCreateDownloadShareRequest;
        }

        internal static DownloadShare FromApiDownloadShare(ApiDownloadShare apiDownloadShare) {
            DownloadShare downloadShare = new DownloadShare {
                ShareId = apiDownloadShare.ShareId,
                NodeId = apiDownloadShare.NodeId,
                NodePath = apiDownloadShare.NodePath,
                Name = apiDownloadShare.Name,
                Notes = apiDownloadShare.Notes,
                InternalNotes = apiDownloadShare.InternalNotes,
                ExpireAt = apiDownloadShare.ExpireAt,
                AccessKey = apiDownloadShare.AccessKey,
                ShowCreatorName = apiDownloadShare.ShowCreatorName,
                ShowCreatorUserName = apiDownloadShare.ShowCreatorUserName,
                MaxAllowedDownloads = apiDownloadShare.MaxAllowedDownloads,
                CurrentDownloadsCount = apiDownloadShare.CurrentDownloadsCount,
                CreatedAt = apiDownloadShare.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiDownloadShare.CreatedBy),
                IsProtected = apiDownloadShare.IsProtected,
                IsEncrypted = apiDownloadShare.IsEncrypted,
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiDownloadShare.Type),
                DataUrl = apiDownloadShare.DataUrl,
                UpdatedAt = apiDownloadShare.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiDownloadShare.UpdatedBy)
            };
            return downloadShare;
        }

        internal static DownloadShareList FromApiDownloadShareList(ApiDownloadShareList apiDownloadShareList) {
            DownloadShareList shareList = new DownloadShareList {
                Offset = apiDownloadShareList.Range.Offset,
                Limit = apiDownloadShareList.Range.Limit,
                Total = apiDownloadShareList.Range.Total,
                Items = new List<DownloadShare>()
            };
            foreach (ApiDownloadShare currentShare in apiDownloadShareList.Items) {
                shareList.Items.Add(FromApiDownloadShare(currentShare));
            }

            return shareList;
        }

        internal static ApiCreateUploadShareRequest ToApiCreateUploadShareRequest(CreateUploadShareRequest request) {
            ApiExpiration apiExpiration = null;
            if (request.Expiration.HasValue) {
                apiExpiration = new ApiExpiration {
                    ExpireAt = request.Expiration,
                    EnableExpiration = request.Expiration.Value.Ticks != 0
                };
            }

            ApiCreateUploadShareRequest apiCreateUploadShareRequest = new ApiCreateUploadShareRequest {
                NodeId = request.NodeId,
                Name = request.Name,
                Password = request.Password,
                Expiration = apiExpiration,
                UploadedFilesExpirationPeriod = request.UploadedFilesExpirationPeriod,
                Notes = request.Notes,
                InternalNotes = request.InternalNotes,
                ShowUploadedFiles = request.ShowUploadedFiles,
                MaxAllowedUploads = request.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = request.MaxAllowedTotalSizeOverAllUploadedFiles,
                ShowCreatorName = request.ShowCreatorName,
                ShowCreatorUsername = request.ShowCreatorUsername,
                ReceiverLanguage = request.ReceiverLanguage,
                TextMessageRecipients = request.TextMessageRecipients
            };

            return apiCreateUploadShareRequest;
        }

        internal static UploadShare FromApiUploadShare(ApiUploadShare apiUploadShare) {
            UploadShare uploadShare = new UploadShare {
                ShareId = apiUploadShare.ShareId,
                NodeId = apiUploadShare.NodeId,
                Name = apiUploadShare.Name,
                IsProtected = apiUploadShare.IsProtected,
                AccessKey = apiUploadShare.AccessKey,
                CreatedAt = apiUploadShare.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiUploadShare.CreatedBy),
                ExpireAt = apiUploadShare.ExpireAt,
                NodePath = apiUploadShare.NodePath,
                IsEncrypted = apiUploadShare.IsEncrypted,
                Notes = apiUploadShare.Notes,
                InternalNotes = apiUploadShare.InternalNotes,
                UploadedFilesExpirationPeriod = apiUploadShare.UploadedFilesExpirationPeriod,
                CurrentDoneUploadsCount = apiUploadShare.CurrentDoneUploadsCount,
                CurrentUploadedFilesCount = apiUploadShare.CurrentUploadedFilesCount,
                ShowUploadedFiles = apiUploadShare.ShowUploadedFiles,
                MaxAllowedUploads = apiUploadShare.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = apiUploadShare.MaxAllowedTotalSizeOverAllUploadedFiles,
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiUploadShare.Type),
                DataUrl = apiUploadShare.DataUrl,
                ShowCreatorName = apiUploadShare.ShowCreatorName,
                ShowCreatorUsername = apiUploadShare.ShowCreatorUsername,
                UpdatedAt = apiUploadShare.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiUploadShare.UpdatedBy)
            };
            return uploadShare;
        }

        internal static UploadShareList FromApiUploadShareList(ApiUploadShareList apiUploadShareList) {
            UploadShareList shareList = new UploadShareList {
                Offset = apiUploadShareList.Range.Offset,
                Limit = apiUploadShareList.Range.Limit,
                Total = apiUploadShareList.Range.Total,
                Items = new List<UploadShare>()
            };
            foreach (ApiUploadShare currentShare in apiUploadShareList.Items) {
                shareList.Items.Add(FromApiUploadShare(currentShare));
            }

            return shareList;
        }

        internal static ApiMailShareInfoRequest ToApiMailShareInfoRequest(MailShareInfoRequest request) {
            ApiMailShareInfoRequest apiRequest = new ApiMailShareInfoRequest() {
                Body = request.Body,
                ReceiverLanguage = request.ReceiverLanguage,
                Recipients = request.Recipients
            };
            return apiRequest;
        }
    }
}