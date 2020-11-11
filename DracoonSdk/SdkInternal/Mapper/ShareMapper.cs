using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Util;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal class ShareMapper {
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
                Expiration = apiExpiration,
                ShowCreatorName = request.ShowCreatorName,
                ShowCreatorUserName = request.ShowCreatorUserName,
                NotifyCreator = request.NotifyCreator,
                MaxAllowedDownloads = request.MaxAllowedDownloads,
                Password = request.AccessPassword
            };

            if (request.EmailRecipients != null) {
                // Check if the list is not empty is still in the previous validator done
                apiCreateDownloadShareRequest.SendMail = true;
                apiCreateDownloadShareRequest.MailRecipients = GenerateRecipientString(request.EmailRecipients);
                apiCreateDownloadShareRequest.MailBody = request.EmailBody;
                apiCreateDownloadShareRequest.MailSubject = request.EmailSubject;
            } else {
                apiCreateDownloadShareRequest.SendMail = false;
            }

            if (request.SmsRecipients != null) {
                // Check if the list is not empty is still in the previous validator done
                apiCreateDownloadShareRequest.SendSms = true;
                apiCreateDownloadShareRequest.SmsRecipients = GenerateRecipientString(request.SmsRecipients);
            } else {
                apiCreateDownloadShareRequest.SendSms = false;
            }

            return apiCreateDownloadShareRequest;
        }

        internal static DownloadShare FromApiDownloadShare(ApiDownloadShare apiDownloadShare) {
            DownloadShare downloadShare = new DownloadShare {
                ShareId = apiDownloadShare.ShareId,
                NodeId = apiDownloadShare.NodeId,
                NodePath = apiDownloadShare.NodePath,
                Name = apiDownloadShare.Name,
                Notes = apiDownloadShare.Notes,
                Classification = EnumConverter.ConvertValueToClassificationEnum(apiDownloadShare.Classification),
                ExpireAt = apiDownloadShare.ExpireAt,
                AccessKey = apiDownloadShare.AccessKey,
                ShowCreatorName = apiDownloadShare.ShowCreatorName,
                ShowCreatorUserName = apiDownloadShare.ShowCreatorUserName,
                NotifyCreator = apiDownloadShare.NotifyCreator,
                MaxAllowedDownloads = apiDownloadShare.MaxAllowedDownloads,
                CurrentDownloadsCount = apiDownloadShare.CurrentDownloadsCount,
                CreatedAt = apiDownloadShare.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiDownloadShare.CreatedBy),
                IsProtected = apiDownloadShare.IsProtected,
                IsEncrypted = apiDownloadShare.IsEncrypted,
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiDownloadShare.Type)
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
                AccessPassword = request.AccessPassword,
                Expiration = apiExpiration,
                UploadedFilesExpirationPeriod = request.UploadedFilesExpirationPeriod,
                Notes = request.Notes,
                NotifyCreator = request.NotifyCreator,
                ShowUploadedFiles = request.ShowUploadedFiles,
                MaxAllowedUploads = request.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = request.MaxAllowedTotalSizeOverAllUploadedFiles
            };

            if (request.EmailRecipients != null) {
                // Check if the list is not empty is still in the previous validator done
                apiCreateUploadShareRequest.SendMail = true;
                apiCreateUploadShareRequest.MailRecipients = GenerateRecipientString(request.EmailRecipients);
                apiCreateUploadShareRequest.MailBody = request.EmailBody;
                apiCreateUploadShareRequest.MailSubject = request.EmailSubject;
            } else {
                apiCreateUploadShareRequest.SendMail = false;
            }

            if (request.SmsRecipients != null) {
                // Check if the list is not empty is still in the previous validator done
                apiCreateUploadShareRequest.SendSms = true;
                apiCreateUploadShareRequest.SmsRecipients = GenerateRecipientString(request.SmsRecipients);
            } else {
                apiCreateUploadShareRequest.SendSms = false;
            }

            return apiCreateUploadShareRequest;
        }

        internal static UploadShare FromApiUploadShare(ApiUploadShare apiUploadShare) {
            UploadShare uploadShare = new UploadShare {
                ShareId = apiUploadShare.ShareId,
                NodeId = apiUploadShare.NodeId,
                Name = apiUploadShare.Name,
                IsProtected = apiUploadShare.IsProtected,
                AccessKey = apiUploadShare.AccessKey,
                NotifyCreator = apiUploadShare.NotifyCreator,
                CreatedAt = apiUploadShare.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiUploadShare.CreatedBy),
                ExpireAt = apiUploadShare.ExpireAt,
                NodePath = apiUploadShare.NodePath,
                IsEncrypted = apiUploadShare.IsEncrypted,
                Notes = apiUploadShare.Notes,
                UploadedFilesExpirationPeriod = apiUploadShare.UploadedFilesExpirationPeriod,
                CurrentDoneUploadsCount = apiUploadShare.CurrentDoneUploadsCount,
                CurrentUploadedFilesCount = apiUploadShare.CurrentUploadedFilesCount,
                ShowUploadedFiles = apiUploadShare.ShowUploadedFiles,
                MaxAllowedUploads = apiUploadShare.MaxAllowedUploads,
                MaxAllowedTotalSizeOverAllUploadedFiles = apiUploadShare.MaxAllowedTotalSizeOverAllUploadedFiles,
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiUploadShare.Type)
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

        private static string GenerateRecipientString(IReadOnlyList<string> recipientList) {
            string recipientsString = "";
            for (int i = 0; i < recipientList.Count; i++) {
                if (i == recipientList.Count - 1) {
                    recipientsString += recipientList[i];
                } else {
                    recipientsString += recipientList[i] + ",";
                }
            }

            return recipientsString;
        }
    }
}