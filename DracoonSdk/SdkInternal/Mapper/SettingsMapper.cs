using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class SettingsMapper {
        internal static ServerGeneralSettings FromApiGeneralSettings(ApiGeneralSettings apiGeneralConfig) {
            if (apiGeneralConfig == null) {
                return null;
            }

            ServerGeneralSettings general = new ServerGeneralSettings {
                CryptoEnabled = apiGeneralConfig.CryptoEnabled,
                EmailNotificationButtonEnabled = apiGeneralConfig.EmailNotificationButtonEnabled,
                EulaEnabled = apiGeneralConfig.EulaEnabled,
                MediaServerEnabled = apiGeneralConfig.MediaServerEnabled,
                SharePasswordSmsEnabled = apiGeneralConfig.SharePasswordSmsEnabled,
                UseS3Storage = apiGeneralConfig.UseS3Storage,
                WeakPasswordEnabled = apiGeneralConfig.WeakPasswordEnabled
            };
            return general;
        }

        internal static ServerInfrastructureSettings FromApiInfrastructureSettings(ApiInfrastructureSettings apiInfrastructureConfig) {
            if (apiInfrastructureConfig == null) {
                return null;
            }

            ServerInfrastructureSettings infrastructure = new ServerInfrastructureSettings {
                MediaServerConfigEnabled = apiInfrastructureConfig.MediaServerConfigEnabled,
                S3DefaultRegion = apiInfrastructureConfig.S3DefaultRegion,
                SmsConfigEnabled = apiInfrastructureConfig.SmsConfigEnabled
            };
            return infrastructure;
        }

        internal static ServerDefaultSettings FromApiDefaultsSettings(ApiDefaultsSettings apiDefaultsConfig) {
            if (apiDefaultsConfig == null) {
                return null;
            }

            ServerDefaultSettings defaults = new ServerDefaultSettings {
                LanguageDefault = apiDefaultsConfig.LanguageDefault,
                DownloadShareDefaultExpirationPeriodInDays = apiDefaultsConfig.DownloadShareDefaultExpirationPeriodInDays,
                FileUploadDefaultExpirationPeriodInDays = apiDefaultsConfig.FileUploadDefaultExpirationPeriodInDays,
                UploadShareDefaultExpirationPeriodInDays = apiDefaultsConfig.UploadShareDefaultExpirationPeriodInDays
            };
            return defaults;
        }
    }
}