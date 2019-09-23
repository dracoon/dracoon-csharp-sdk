using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryServerSettings {
        internal static ServerGeneralSettings ServerGeneralSettings {
            get {
                return new ServerGeneralSettings {
                    CryptoEnabled = true,
                    EmailNotificationButtonEnabled = false,
                    EulaEnabled = true,
                    MediaServerEnabled = false,
                    SharePasswordSmsEnabled = true,
                    UseS3Storage = false,
                    WeakPasswordEnabled = true
                };
            }
        }

        internal static ApiGeneralSettings ApiGeneralSettings {
            get {
                return new ApiGeneralSettings {
                    CryptoEnabled = true,
                    EmailNotificationButtonEnabled = false,
                    EulaEnabled = true,
                    MediaServerEnabled = false,
                    SharePasswordSmsEnabled = true,
                    UseS3Storage = false,
                    WeakPasswordEnabled = true
                };
            }
        }

        internal static ServerInfrastructureSettings ServerInfrastructureSettings {
            get {
                return new ServerInfrastructureSettings {
                    MediaServerConfigEnabled = true,
                    S3DefaultRegion = "DE",
                    SmsConfigEnabled = true,
                    S3EnforceDirectUpload = false
                };
            }
        }

        internal static ApiInfrastructureSettings ApiInfrastructureSettings {
            get {
                return new ApiInfrastructureSettings {
                    MediaServerConfigEnabled = true,
                    S3DefaultRegion = "DE",
                    SmsConfigEnabled = true
                };
            }
        }

        internal static ServerDefaultSettings ServerDefaultSettings {
            get {
                return new ServerDefaultSettings {
                    DownloadShareDefaultExpirationPeriodInDays = 5,
                    FileUploadDefaultExpirationPeriodInDays = 4,
                    LanguageDefault = "DE",
                    UploadShareDefaultExpirationPeriodInDays = 7
                };
            }
        }

        internal static ApiDefaultsSettings ApiDefaultsSettings {
            get {
                return new ApiDefaultsSettings {
                    DownloadShareDefaultExpirationPeriodInDays = 5,
                    FileUploadDefaultExpirationPeriodInDays = 4,
                    LanguageDefault = "DE",
                    UploadShareDefaultExpirationPeriodInDays = 7
                };
            }
        }
    }
}