using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Util;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class FileMapper {
        internal static ApiUpdateFileRequest ToApiUpdateFileRequest(UpdateFileRequest updateFileRequest) {
            ApiExpiration apiExpiration = null;
            if (updateFileRequest.Expiration.HasValue) {
                apiExpiration = new ApiExpiration() {
                    ExpireAt = updateFileRequest.Expiration,
                    EnableExpiration = updateFileRequest.Expiration.Value.Ticks != 0
                };
            }
            ApiUpdateFileRequest apiUpdateFileRequest = new ApiUpdateFileRequest() {
                Name = updateFileRequest.Name,
                Notes = updateFileRequest.Notes,
                Classification = EnumConverter.ConvertClassificationEnumToValue(updateFileRequest.Classification),
                Expiration = apiExpiration
            };
            return apiUpdateFileRequest;
        }

        internal static ApiFileKey ToApiFileKey(EncryptedFileKey encryptedFileKey) {
            ApiFileKey apiEncryptedFileKey = new ApiFileKey() {
                Key = encryptedFileKey.Key,
                Iv = encryptedFileKey.Iv,
                Tag = encryptedFileKey.Tag,
                Version = encryptedFileKey.Version
            };
            return apiEncryptedFileKey;
        }

        internal static EncryptedFileKey FromApiFileKey(ApiFileKey apiEncryptedFileKey) {
            EncryptedFileKey encryptedFileKey = new EncryptedFileKey() {
                Key = apiEncryptedFileKey.Key,
                Iv = apiEncryptedFileKey.Iv,
                Tag = apiEncryptedFileKey.Tag,
                Version = apiEncryptedFileKey.Version
            };
            return encryptedFileKey;
        }

        internal static ApiCreateFileUpload ToApiCreateFileUpload(FileUploadRequest fileUploadRequest) {
            ApiExpiration apiExpiration = null;
            if (fileUploadRequest.ExpirationDate.HasValue) {
                apiExpiration = new ApiExpiration() {
                    ExpireAt = fileUploadRequest.ExpirationDate,
                    EnableExpiration = fileUploadRequest.ExpirationDate.Value.Ticks != 0
                };
            }
            ApiCreateFileUpload apiCreateFileUpload = new ApiCreateFileUpload() {
                ParentId = fileUploadRequest.ParentId,
                Name = fileUploadRequest.Name,
                Classification = EnumConverter.ConvertClassificationEnumToValue(fileUploadRequest.Classification),
                Notes = fileUploadRequest.Notes,
                Expiration = apiExpiration
            };
            return apiCreateFileUpload;
        }

        internal static ApiCompleteFileUpload ToApiCompleteFileUpload(FileUploadRequest fileUploadRequest) {
            ApiCompleteFileUpload apiCompleteFileUpload = new ApiCompleteFileUpload() {
                FileName = fileUploadRequest.Name,
                ResolutionStrategy = EnumConverter.ConvertResolutionStrategyToValue(fileUploadRequest.ResolutionStrategy)
            };
            return apiCompleteFileUpload;
        }
    }
}
