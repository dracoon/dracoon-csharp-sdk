using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Settings;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryServerSettings {
        internal static ServerGeneralSettings ServerGeneralSettings => new ServerGeneralSettings {
            CryptoEnabled = true,
            EmailNotificationButtonEnabled = false,
            EulaEnabled = true,
            SharePasswordSmsEnabled = true,
            UseS3Storage = false,
            S3TagsEnabled = true,
            HomeRoomsActive = true,
            HomeRoomParentId = 123,
            SubscriptionPlan = SubscriptionPlan.Premium
        };

        internal static ApiGeneralSettings ApiGeneralSettings => new ApiGeneralSettings {
            CryptoEnabled = true,
            EmailNotificationButtonEnabled = false,
            EulaEnabled = true,
            SharePasswordSmsEnabled = true,
            UseS3Storage = false,
            S3TagsEnabled = true,
            HomeRoomsActive = true,
            HomeRoomParentId = 123,
            SubscriptionPlan = 1
        };

        internal static ServerInfrastructureSettings ServerInfrastructureSettings => new ServerInfrastructureSettings {
            MediaServerConfigEnabled = true,
            S3DefaultRegion = "DE",
            SmsConfigEnabled = true,
            S3EnforceDirectUpload = false,
            IsDracoonCloud = true,
            TenantUUID = "RANDOMUUID"
        };

        internal static List<FileKeyAlgorithmData> FileKeyAlgorithms => new List<FileKeyAlgorithmData> {
                    new FileKeyAlgorithmData {
                        Algorithm = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM,
                        State = AlgorithmState.Discouraged
                    },
                    new FileKeyAlgorithmData {
                        Algorithm = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM,
                        State = AlgorithmState.Required
                    }
                };

        internal static List<UserKeyPairAlgorithmData> UserKeyPairAlgorithms => new List<UserKeyPairAlgorithmData> {
                    new UserKeyPairAlgorithmData {
                        Algorithm = UserKeyPairAlgorithm.RSA2048,
                        State = AlgorithmState.Discouraged
                    },
                    new UserKeyPairAlgorithmData {
                        Algorithm = UserKeyPairAlgorithm.RSA4096,
                        State = AlgorithmState.Required
                    }
                };

        internal static ApiAlgorithms ApiAlgorithms => new ApiAlgorithms {
            FileKeyAlgorithms = new List<ApiAlgorithm> {
                        new ApiAlgorithm {
                            Version = "RSA-4096/AES-256-GCM",
                            Status = "REQUIRED",
                            Description = "RSA-4096/AES-256-GCM"
                        },
                        new ApiAlgorithm {
                            Version = "A",
                            Status = "DISCOURAGED",
                            Description = "RSA-2048/AES-256-GCM"
                        }
                    },
            KeyPairAlgorithms = new List<ApiAlgorithm> {
                        new ApiAlgorithm {
                            Version = "RSA-4096",
                            Status = "REQUIRED",
                            Description = "RSA-4096-OAEP-SHA256MGFSHA256"
                        },
                        new ApiAlgorithm {
                            Version = "A",
                            Status = "DISCOURAGED",
                            Description = "RSA-2048-OAEP-SHA256MGF1"
                        }
                    }
        };

        internal static ApiInfrastructureSettings ApiInfrastructureSettings => new ApiInfrastructureSettings {
            MediaServerConfigEnabled = true,
            S3DefaultRegion = "DE",
            SmsConfigEnabled = true,
            IsDracoonCloud = true,
            TenantUUID = "RANDOMUUID",
            S3EnforceDirectUpload = false
        };

        internal static ServerDefaultSettings ServerDefaultSettings => new ServerDefaultSettings {
            DownloadShareDefaultExpirationPeriodInDays = 5,
            FileUploadDefaultExpirationPeriodInDays = 4,
            LanguageDefault = "DE",
            UploadShareDefaultExpirationPeriodInDays = 7,
            HideLoginInputFields = false,
            NonMemberViewerDefault = false
        };

        internal static ApiDefaultsSettings ApiDefaultsSettings => new ApiDefaultsSettings {
            DownloadShareDefaultExpirationPeriodInDays = 5,
            FileUploadDefaultExpirationPeriodInDays = 4,
            LanguageDefault = "DE",
            UploadShareDefaultExpirationPeriodInDays = 7,
            HideLoginInputFields = false,
            NonMemberViewerDefault = false,
        };
    }
}