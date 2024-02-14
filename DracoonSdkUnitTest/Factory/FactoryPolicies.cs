using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryPolicies {
        internal static PasswordEncryptionPolicies PasswordEncryptionPolicies => new PasswordEncryptionPolicies {
            CharacterPolicies = PasswordCharacterPolicies,
            MinimumPasswordLength = 9,
            RejectKeyboardPatterns = true,
            RejectOwnUserInfo = true,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.UserInfo
        };

        internal static PasswordSharePolicies PasswordSharePolicies => new PasswordSharePolicies {
            CharacterPolicies = PasswordCharacterPolicies,
            MinimumPasswordLength = 9,
            RejectKeyboardPatterns = true,
            RejectOwnUserInfo = true,
            RejectDictionaryWords = false,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            UpdatedBy = FactoryUser.UserInfo
        };

        internal static PasswordCharacterPolicies PasswordCharacterPolicies => new PasswordCharacterPolicies {
            NumberOfMustContainCharacteristics = 2,
            PredefinedCharacterSets = new List<PasswordCharacterSet> {
                        new PasswordCharacterSet {
                            Set = ApiConfig.UPPERCASE_SET,
                            Type = PasswordCharacterSetType.Uppercase
                        },
                        new PasswordCharacterSet {
                            Set = ApiConfig.NUMERIC_SET,
                            Type = PasswordCharacterSetType.Numeric
                        }
                    }
        };

        internal static ApiCharacterRules ApiPasswordCharacterRules => new ApiCharacterRules {
            MustContainCharacters = new List<string> { "upper", "numeric" },
            NumberOfCharacteristicsToEnforce = 2
        };

        internal static ApiPasswordPolicies ApiPasswordSettings => new ApiPasswordPolicies {
            EncryptionPasswordSettings = new ApiEncryptionPasswordSettings {
                CharacterRules = ApiPasswordCharacterRules,
                MinimumPasswordLength = 9,
                RejectKeyboardPatterns = true,
                RejectUserInfo = true,
                UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
                UpdatedBy = FactoryUser.ApiUserInfo
            },
            SharePasswordSettings = new ApiSharePasswordSettings {
                CharacterRules = ApiPasswordCharacterRules,
                MinimumPasswordLength = 9,
                RejectKeyboardPatterns = true,
                RejectUserInfo = true,
                RejectDictionaryWords = false,
                UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0),
                UpdatedBy = FactoryUser.ApiUserInfo
            }
        };

        internal static ApiClassificationPolicies ApiClassificationPolices => new ApiClassificationPolicies {
            SharePolicy = new ApiShareClassificationPolicy {
                PasswordRequirementMinimumClassification = 2
            }
        };

        internal static ClassificationPolicies ClassificationPolices => new ClassificationPolicies {
            ShareClassificationPolicy = new ShareClassificationPolicy {
                ClassificationMinimumForSharePasswort = Classification.Internal
            }
        };
    }
}
