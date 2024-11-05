using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Telerik.JustMock;
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
                SharePasswordSmsEnabled = expected.SharePasswordSmsEnabled,
                UseS3Storage = expected.UseS3Storage,
                S3TagsEnabled = expected.S3TagsEnabled,
                HomeRoomsActive = expected.HomeRoomsActive,
                HomeRoomParentId = expected.HomeRoomParentId,
                SubscriptionPlan = 1
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
                SmsConfigEnabled = expected.SmsConfigEnabled,
                S3EnforceDirectUpload = expected.S3EnforceDirectUpload,
                IsDracoonCloud = expected.IsDracoonCloud,
                TenantUUID = expected.TenantUUID
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
                UploadShareDefaultExpirationPeriodInDays = expected.UploadShareDefaultExpirationPeriodInDays,
                HideLoginInputFields = expected.HideLoginInputFields,
                NonMemberViewerDefault = expected.NonMemberViewerDefault
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

        #region FromApiPasswordSharePolicies

        [Fact]
        public void FromApiPasswordSharePolicies() {
            // ARRANGE
            PasswordSharePolicies expected = FactoryPolicies.PasswordSharePolicies;

            ApiSharePasswordPolicy param = new ApiSharePasswordPolicy {
                CharacterRules = FactoryPolicies.ApiPasswordCharacterRules,
                MinimumPasswordLength = expected.MinimumPasswordLength,
                RejectDictionaryWords = expected.RejectDictionaryWords,
                RejectKeyboardPatterns = expected.RejectKeyboardPatterns,
                RejectUserInfo = expected.RejectKeyboardPatterns,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = FactoryUser.ApiUserInfo
            };

            Mock.Arrange(() => SettingsMapper.FromApiPasswordCharacterPolicies(param.CharacterRules)).Returns(expected.CharacterPolicies);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.UpdatedBy)).Returns(expected.UpdatedBy);

            // ACT
            PasswordSharePolicies actual = SettingsMapper.FromApiPasswordSharePolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordSharePolicyComparer());
        }

        [Fact]
        public void FromApiPasswordSharePolicies_Null() {
            // ARRANGE
            PasswordSharePolicies expected = null;
            ApiSharePasswordPolicy param = null;

            // ACT
            PasswordSharePolicies actual = SettingsMapper.FromApiPasswordSharePolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordSharePolicyComparer());
        }

        #endregion

        #region FromApiPasswordEncryptionPolicies

        [Fact]
        public void FromApiPasswordEncryptionPolicies() {
            // ARRANGE
            PasswordEncryptionPolicies expected = FactoryPolicies.PasswordEncryptionPolicies;

            ApiEncryptionPasswordPolicy param = new ApiEncryptionPasswordPolicy {
                CharacterRules = FactoryPolicies.ApiPasswordCharacterRules,
                MinimumPasswordLength = expected.MinimumPasswordLength,
                RejectKeyboardPatterns = expected.RejectKeyboardPatterns,
                RejectUserInfo = expected.RejectKeyboardPatterns,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = FactoryUser.ApiUserInfo
            };

            Mock.Arrange(() => SettingsMapper.FromApiPasswordCharacterPolicies(param.CharacterRules)).Returns(expected.CharacterPolicies);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.UpdatedBy)).Returns(expected.UpdatedBy);

            // ACT
            PasswordEncryptionPolicies actual = SettingsMapper.FromApiPasswordEncryptionPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordEncryptionPolicyComparer());
        }

        [Fact]
        public void FromApiPasswordEncryptionPolicies_Null() {
            // ARRANGE
            PasswordEncryptionPolicies expected = null;
            ApiEncryptionPasswordPolicy param = null;

            // ACT
            PasswordEncryptionPolicies actual = SettingsMapper.FromApiPasswordEncryptionPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordEncryptionPolicyComparer());
        }

        #endregion

        #region FromApiPasswordLoginPolicies

        [Fact]
        public void FromApiPasswordLoginPolicies() {
            // ARRANGE
            PasswordLoginPolicies expected = FactoryPolicies.PasswordLoginPolicies;

            ApiLoginPasswordPolicy param = new ApiLoginPasswordPolicy {
                CharacterRules = FactoryPolicies.ApiPasswordCharacterRules,
                MinimumPasswordLength = expected.MinimumPasswordLength,
                RejectDictionaryWords = expected.RejectDictionaryWords,
                RejectKeyboardPatterns = expected.RejectKeyboardPatterns,
                RejectUserInfo = expected.RejectKeyboardPatterns,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = FactoryUser.ApiUserInfo,
                NumberOfArchivedPasswords = expected.NumberOfArchivedPasswords,
                PasswordExpiration = new ApiPasswordExpiration {
                    Enabled = expected.PasswordExpiration.IsEnabled,
                    MaxDaysPasswordAge = expected.PasswordExpiration.ExpiresAfterDays
                }
            };

            Mock.Arrange(() => SettingsMapper.FromApiPasswordCharacterPolicies(param.CharacterRules)).Returns(expected.CharacterPolicies);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.UpdatedBy)).Returns(expected.UpdatedBy);
            Mock.Arrange(() => SettingsMapper.FromApiPasswordExpiration(param.PasswordExpiration)).Returns(expected.PasswordExpiration);

            // ACT
            PasswordLoginPolicies actual = SettingsMapper.FromApiPasswordLoginPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordLoginPolicyComparer());
        }

        [Fact]
        public void FromApiPasswordLoginPolicies_Null() {
            // ARRANGE
            PasswordLoginPolicies expected = null;
            ApiLoginPasswordPolicy param = null;

            // ACT
            PasswordLoginPolicies actual = SettingsMapper.FromApiPasswordLoginPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordLoginPolicyComparer());
        }

        #endregion

        #region FromApiPasswordExpiration

        [Fact]
        public void FromApiPasswordExpiration() {
            // ARRANGE
            PasswordExpiration expected = FactoryPolicies.PasswordLoginPolicies.PasswordExpiration;

            ApiPasswordExpiration param = new ApiPasswordExpiration {
                Enabled = expected.IsEnabled,
                MaxDaysPasswordAge = expected.ExpiresAfterDays
            };

            // ACT
            PasswordExpiration actual = SettingsMapper.FromApiPasswordExpiration(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordExpirationComparer());
        }

        [Fact]
        public void FromApiPasswordExpiration_Null() {
            // ARRANGE
            PasswordExpiration expected = null;
            ApiPasswordExpiration param = null;

            // ACT
            PasswordExpiration actual = SettingsMapper.FromApiPasswordExpiration(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordExpirationComparer());
        }

        #endregion

        #region FromApiPasswordCharacterPolicies

        [Fact]
        public void FromApiPasswordCharacterPolicies() {
            // ARRANGE
            PasswordCharacterPolicies expected = FactoryPolicies.PasswordCharacterPolicies;

            ApiCharacterRules param = new ApiCharacterRules {
                MustContainCharacters = FactoryPolicies.ApiPasswordCharacterRules.MustContainCharacters,
                NumberOfCharacteristicsToEnforce = expected.NumberOfMustContainCharacteristics
            };

            Mock.Arrange(() => EnumConverter.ConvertValueToCharacterSetTypeEnum("upper")).Returns(PasswordCharacterSetType.Uppercase);
            Mock.Arrange(() => EnumConverter.ConvertValueToCharacterSetTypeEnum("numeric")).Returns(PasswordCharacterSetType.Numeric);
            Mock.Arrange(() => EnumConverter.ConvertValueToCharacterSetTypeEnum("lower")).Returns(PasswordCharacterSetType.Lowercase);

            // ACT
            PasswordCharacterPolicies actual = SettingsMapper.FromApiPasswordCharacterPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordCharacterPolicyComparer());
        }

        [Fact]
        public void FromApiPasswordCharacterPolicies_Null() {
            // ARRANGE
            PasswordCharacterPolicies expected = null;
            ApiCharacterRules param = null;

            // ACT
            PasswordCharacterPolicies actual = SettingsMapper.FromApiPasswordCharacterPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new PasswordCharacterPolicyComparer());
        }

        #endregion

        #region FromApiClassificationPolicies

        [Fact]
        public void FromApiClassificationPolicies() {
            // ARRANGE
            ClassificationPolicies expected = FactoryPolicies.ClassificationPolices;

            // ACT
            ClassificationPolicies actual = SettingsMapper.FromApiClassificationPolicies(FactoryPolicies.ApiClassificationPolices);

            // ASSERT
            Assert.Equal(expected, actual, new ClassificationPoliciesComparer());
        }

        [Fact]
        public void FromApiClassificationPolicies_Null() {
            // ARRANGE
            ClassificationPolicies expected = null;
            ApiClassificationPolicies param = null;

            // ACT
            ClassificationPolicies actual = SettingsMapper.FromApiClassificationPolicies(param);

            // ASSERT
            Assert.Equal(expected, actual, new ClassificationPoliciesComparer());
        }

        #endregion
    }
}