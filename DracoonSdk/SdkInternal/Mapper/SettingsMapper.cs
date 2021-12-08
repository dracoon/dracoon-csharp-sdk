using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Settings;
using Dracoon.Sdk.SdkInternal.Util;
using System.Collections.Generic;

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
                SharePasswordSmsEnabled = apiGeneralConfig.SharePasswordSmsEnabled,
                UseS3Storage = apiGeneralConfig.UseS3Storage,
                S3TagsEnabled = apiGeneralConfig.S3TagsEnabled,
                HomeRoomsActive = apiGeneralConfig.HomeRoomsActive,
                HomeRoomParentId = apiGeneralConfig.HomeRoomParentId,
                SubscriptionPlan = EnumConverter.ConvertValueToSubscriptionPlanEnum(apiGeneralConfig.SubscriptionPlan)
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
                SmsConfigEnabled = apiInfrastructureConfig.SmsConfigEnabled,
                S3EnforceDirectUpload = apiInfrastructureConfig.S3EnforceDirectUpload,
                IsDracoonCloud = apiInfrastructureConfig.IsDracoonCloud,
                TenantUUID = apiInfrastructureConfig.TenantUUID
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
                UploadShareDefaultExpirationPeriodInDays = apiDefaultsConfig.UploadShareDefaultExpirationPeriodInDays,
                NonMemberViewerDefault = apiDefaultsConfig.NonMemberViewerDefault,
                HideLoginInputFields = apiDefaultsConfig.HideLoginInputFields
            };
            return defaults;
        }

        internal static PasswordSharePolicies FromApiPasswordSharePolicies(ApiSharePasswordPolicy apiPolicies) {
            if (apiPolicies == null) {
                return null;
            }

            PasswordSharePolicies policies = new PasswordSharePolicies {
                CharacterPolicies = FromApiPasswordCharacterPolicies(apiPolicies.CharacterRules),
                MinimumPasswordLength = apiPolicies.MinimumPasswordLength,
                RejectDictionaryWords = apiPolicies.RejectDictionaryWords,
                RejectKeyboardPatterns = apiPolicies.RejectKeyboardPatterns,
                RejectOwnUserInfo = apiPolicies.RejectUserInfo,
                UpdatedAt = apiPolicies.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiPolicies.UpdatedBy)
            };
            return policies;
        }

        internal static PasswordEncryptionPolicies FromApiPasswordEncryptionPolicies(ApiEncryptionPasswordPolicy apiPolicies) {
            if (apiPolicies == null) {
                return null;
            }

            PasswordEncryptionPolicies policies = new PasswordEncryptionPolicies {
                CharacterPolicies = FromApiPasswordCharacterPolicies(apiPolicies.CharacterRules),
                MinimumPasswordLength = apiPolicies.MinimumPasswordLength,
                RejectKeyboardPatterns = apiPolicies.RejectKeyboardPatterns,
                RejectOwnUserInfo = apiPolicies.RejectUserInfo,
                UpdatedAt = apiPolicies.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiPolicies.UpdatedBy)
            };
            return policies;
        }

        internal static PasswordLoginPolicies FromApiPasswordLoginPolicies(ApiLoginPasswordPolicy apiPolicies) {
            if (apiPolicies == null) {
                return null;
            }

            PasswordLoginPolicies policies = new PasswordLoginPolicies {
                CharacterPolicies = FromApiPasswordCharacterPolicies(apiPolicies.CharacterRules),
                MinimumPasswordLength = apiPolicies.MinimumPasswordLength,
                RejectKeyboardPatterns = apiPolicies.RejectKeyboardPatterns,
                RejectOwnUserInfo = apiPolicies.RejectUserInfo,
                RejectDictionaryWords = apiPolicies.RejectDictionaryWords,
                NumberOfArchivedPasswords = apiPolicies.NumberOfArchivedPasswords,
                PasswordExpiration = FromApiPasswordExpiration(apiPolicies.PasswordExpiration),
                UpdatedAt = apiPolicies.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiPolicies.UpdatedBy)
            };
            return policies;
        }

        internal static PasswordExpiration FromApiPasswordExpiration(ApiPasswordExpiration apiExpiration) {
            if (apiExpiration == null) {
                return null;
            }

            PasswordExpiration expiration = new PasswordExpiration {
                IsEnabled = apiExpiration.Enabled,
                ExpiresAfterDays = apiExpiration.MaxDaysPasswordAge
            };
            return expiration;
        }

        internal static PasswordCharacterPolicies FromApiPasswordCharacterPolicies(ApiCharacterRules apiPolicies) {
            if (apiPolicies == null) {
                return null;
            }

            PasswordCharacterPolicies policies = new PasswordCharacterPolicies {
                NumberOfMustContainCharacteristics = apiPolicies.NumberOfCharacteristicsToEnforce
            };

            policies.PredefinedCharacterSets = new List<PasswordCharacterSet>();
            foreach (string current in apiPolicies.MustContainCharacters) {
                if (current != "alpha") {
                    PasswordCharacterSetType type = EnumConverter.ConvertValueToCharacterSetTypeEnum(current);
                    policies.PredefinedCharacterSets.Add(new PasswordCharacterSet {
                        Type = type,
                        Set = GeneratePasswordPoliciesSet(type)
                    });
                }
            }

            // Convert "alpha" value to uppercase & lowercase if one of them are not included yet
            if (apiPolicies.MustContainCharacters.Contains("alpha")) {
                if (!apiPolicies.MustContainCharacters.Contains("lowercase")) {
                    PasswordCharacterSetType type = PasswordCharacterSetType.Lowercase;
                    policies.PredefinedCharacterSets.Add(new PasswordCharacterSet {
                        Type = type,
                        Set = GeneratePasswordPoliciesSet(type)
                    });
                }

                if (!apiPolicies.MustContainCharacters.Contains("uppercase")) {
                    PasswordCharacterSetType type = PasswordCharacterSetType.Uppercase;
                    policies.PredefinedCharacterSets.Add(new PasswordCharacterSet {
                        Type = type,
                        Set = GeneratePasswordPoliciesSet(type)
                    });
                }
            }

            return policies;
        }

        internal static char[] GeneratePasswordPoliciesSet(PasswordCharacterSetType type) {
            switch (type) {
                case PasswordCharacterSetType.Lowercase:
                    return ApiConfig.LOWERCASE_SET;
                case PasswordCharacterSetType.Uppercase:
                    return ApiConfig.UPPERCASE_SET;
                case PasswordCharacterSetType.Numeric:
                    return ApiConfig.NUMERIC_SET;
                case PasswordCharacterSetType.Special:
                    return ApiConfig.SPECIAL_SET;
                default:
                    return new char[0];
            }
        }

        internal static List<FileKeyAlgorithmData> FromApiFileKeyAlgorithms(List<ApiAlgorithm> apiFileKeyAlgorithms) {
            if (apiFileKeyAlgorithms == null) {
                return new List<FileKeyAlgorithmData>(0);
            }

            List<FileKeyAlgorithmData> fileKeyAlgorithms = new List<FileKeyAlgorithmData>(apiFileKeyAlgorithms.Count);
            foreach (ApiAlgorithm current in apiFileKeyAlgorithms) {
                fileKeyAlgorithms.Add(new FileKeyAlgorithmData() {
                    Algorithm = FileMapper.FromApiFileKeyVersion(current.Version),
                    State = EnumConverter.ConvertValueToAlgorithmState(current.Status)
                });
            }

            return fileKeyAlgorithms;
        }

        internal static List<UserKeyPairAlgorithmData> FromApiUserKeyPairAlgorithms(List<ApiAlgorithm> apiUserKeyPairAlgorithms) {
            if (apiUserKeyPairAlgorithms == null) {
                return new List<UserKeyPairAlgorithmData>(0);
            }

            List<UserKeyPairAlgorithmData> userKeyPairAlgorithms = new List<UserKeyPairAlgorithmData>(apiUserKeyPairAlgorithms.Count);
            foreach (ApiAlgorithm current in apiUserKeyPairAlgorithms) {
                userKeyPairAlgorithms.Add(new UserKeyPairAlgorithmData() {
                    Algorithm = UserMapper.FromApiUserKeyPairVersion(current.Version),
                    State = EnumConverter.ConvertValueToAlgorithmState(current.Status)
                });
            }

            return userKeyPairAlgorithms;
        }

        internal static ClassificationPolicies FromApiClassificationPolicies(ApiClassificationPolicies apiClassificationPolicies) {
            if (apiClassificationPolicies == null) {
                return null;
            }

            ClassificationPolicies classificationPolicies = new ClassificationPolicies {
                ShareClassificationPolicy = FromApiShareClassificationPolicy(apiClassificationPolicies.SharePolicy)
            };

            return classificationPolicies;
        }

        internal static ShareClassificationPolicy FromApiShareClassificationPolicy(ApiShareClassificationPolicy apiShareClassificationPolicy) {
            if (apiShareClassificationPolicy == null) {
                return null;
            }

            ShareClassificationPolicy shareClassificationPolicy = new ShareClassificationPolicy {
                ClassificationMinimumForSharePasswort = EnumConverter.ConvertValueToClassificationEnum(apiShareClassificationPolicy.PasswordRequirementMinimumClassification)
            };

            return shareClassificationPolicy;
        }
    }
}