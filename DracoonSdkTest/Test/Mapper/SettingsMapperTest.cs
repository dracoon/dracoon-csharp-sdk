using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class SettingsMapperTest {
        #region FromApiGeneralSettings

        [Fact]
        public void FromApiGeneralSettings() {
            // ARRANGE
            ServerGeneralSettings expected = FactoryServerSettings.ServerGeneralSettings;

            ApiGeneralSettings param = new ApiGeneralSettings {
                CryptoEnabled = expected.CryptoEnabled,
                EmailNotificationButtonEnabled = expected.EmailNotificationButtonEnabled,
                EulaEnabled = expected.EulaEnabled,
                MediaServerEnabled = expected.MediaServerEnabled,
                SharePasswordSmsEnabled = expected.SharePasswordSmsEnabled,
                UseS3Storage = expected.UseS3Storage,
                WeakPasswordEnabled = expected.WeakPasswordEnabled
            };

            // ACT
            ServerGeneralSettings actual = SettingsMapper.FromApiGeneralSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerGeneralSettingsComparer());
        }

        [Fact]
        public void FromApiGeneralSettings_Null() {
            // ARRANGE
            ServerGeneralSettings expected = null;
            ApiGeneralSettings param = null;

            // ACT
            ServerGeneralSettings actual = SettingsMapper.FromApiGeneralSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerGeneralSettingsComparer());
        }

        #endregion

        #region FromApiInfrastructureSettings

        [Fact]
        public void FromApiInfrastructureSettings() {
            // ARRANGE
            ServerInfrastructureSettings expected = FactoryServerSettings.ServerInfrastructureSettings;

            ApiInfrastructureSettings param = new ApiInfrastructureSettings {
                MediaServerConfigEnabled = expected.MediaServerConfigEnabled,
                S3DefaultRegion = expected.S3DefaultRegion,
                SmsConfigEnabled = expected.SmsConfigEnabled
            };

            // ACT
            ServerInfrastructureSettings actual = SettingsMapper.FromApiInfrastructureSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerInfrastructureSettingsComparer());
        }

        [Fact]
        public void FromApiInfrastructureSettings_Null() {
            // ARRANGE
            ServerInfrastructureSettings expected = null;
            ApiInfrastructureSettings param = null;

            // ACT
            ServerInfrastructureSettings actual = SettingsMapper.FromApiInfrastructureSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerInfrastructureSettingsComparer());
        }

        #endregion

        #region FromApiDefaultsSettings

        [Fact]
        public void FromApiDefaultsSettings() {
            // ARRANGE
            ServerDefaultSettings expected = FactoryServerSettings.ServerDefaultSettings;

            ApiDefaultsSettings param = new ApiDefaultsSettings {
                DownloadShareDefaultExpirationPeriodInDays = expected.DownloadShareDefaultExpirationPeriodInDays,
                FileUploadDefaultExpirationPeriodInDays = expected.FileUploadDefaultExpirationPeriodInDays,
                LanguageDefault = expected.LanguageDefault,
                UploadShareDefaultExpirationPeriodInDays = expected.UploadShareDefaultExpirationPeriodInDays
            };

            // ACT
            ServerDefaultSettings actual = SettingsMapper.FromApiDefaultsSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerDefaultSettingsComparer());
        }

        [Fact]
        public void FromApiDefaultsSettings_Null() {
            // ARRANGE
            ServerDefaultSettings expected = null;
            ApiDefaultsSettings param = null;

            // ACT
            ServerDefaultSettings actual = SettingsMapper.FromApiDefaultsSettings(param);

            // ASSERT
            Assert.Equal(expected, actual, new ServerDefaultSettingsComparer());
        }

        #endregion
    }
}