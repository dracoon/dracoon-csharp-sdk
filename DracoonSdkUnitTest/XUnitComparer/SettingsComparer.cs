using Dracoon.Sdk.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class ServerGeneralSettingsComparer : IEqualityComparer<ServerGeneralSettings> {
        public bool Equals(ServerGeneralSettings x, ServerGeneralSettings y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.CryptoEnabled == y.CryptoEnabled &&
                x.EmailNotificationButtonEnabled == y.EmailNotificationButtonEnabled &&
                x.EulaEnabled == y.EulaEnabled &&
                x.SharePasswordSmsEnabled == y.SharePasswordSmsEnabled &&
                x.UseS3Storage == y.UseS3Storage &&
                x.S3TagsEnabled == y.S3TagsEnabled &&
                x.HomeRoomsActive == y.HomeRoomsActive &&
                x.HomeRoomParentId == y.HomeRoomParentId &&
                x.SubscriptionPlan == y.SubscriptionPlan;
        }

        public int GetHashCode(ServerGeneralSettings obj) {
            throw new NotImplementedException();
        }
    }

    internal class ServerInfrastructureSettingsComparer : IEqualityComparer<ServerInfrastructureSettings> {
        public bool Equals(ServerInfrastructureSettings x, ServerInfrastructureSettings y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.MediaServerConfigEnabled == y.MediaServerConfigEnabled &&
                string.Equals(x.S3DefaultRegion, y.S3DefaultRegion) &&
                x.SmsConfigEnabled == y.SmsConfigEnabled &&
                x.S3EnforceDirectUpload == y.S3EnforceDirectUpload &&
                x.IsDracoonCloud == y.IsDracoonCloud &&
                string.Equals(x.TenantUUID, y.TenantUUID);
        }

        public int GetHashCode(ServerInfrastructureSettings obj) {
            throw new NotImplementedException();
        }
    }

    internal class ServerDefaultSettingsComparer : IEqualityComparer<ServerDefaultSettings> {
        public bool Equals(ServerDefaultSettings x, ServerDefaultSettings y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.DownloadShareDefaultExpirationPeriodInDays == y.DownloadShareDefaultExpirationPeriodInDays &&
                x.FileUploadDefaultExpirationPeriodInDays == y.FileUploadDefaultExpirationPeriodInDays &&
                string.Equals(x.LanguageDefault, y.LanguageDefault) &&
                x.UploadShareDefaultExpirationPeriodInDays == y.UploadShareDefaultExpirationPeriodInDays &&
                x.HideLoginInputFields == y.HideLoginInputFields &&
                x.NonMemberViewerDefault == y.NonMemberViewerDefault;
        }

        public int GetHashCode(ServerDefaultSettings obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordEncryptionPolicyComparer : IEqualityComparer<PasswordEncryptionPolicies> {
        public bool Equals(PasswordEncryptionPolicies x, PasswordEncryptionPolicies y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CharacterPolicies, y.CharacterPolicies, new PasswordCharacterPolicyComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());

            return x.MinimumPasswordLength == y.MinimumPasswordLength &&
                x.RejectKeyboardPatterns == y.RejectKeyboardPatterns &&
                x.RejectOwnUserInfo == y.RejectOwnUserInfo &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(PasswordEncryptionPolicies obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordSharePolicyComparer : IEqualityComparer<PasswordSharePolicies> {
        public bool Equals(PasswordSharePolicies x, PasswordSharePolicies y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CharacterPolicies, y.CharacterPolicies, new PasswordCharacterPolicyComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());

            return x.MinimumPasswordLength == y.MinimumPasswordLength &&
                x.RejectKeyboardPatterns == y.RejectKeyboardPatterns &&
                x.RejectOwnUserInfo == y.RejectOwnUserInfo &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(PasswordSharePolicies obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordLoginPolicyComparer : IEqualityComparer<PasswordLoginPolicies> {
        public bool Equals(PasswordLoginPolicies x, PasswordLoginPolicies y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.PasswordExpiration, y.PasswordExpiration, new PasswordExpirationComparer());
            Assert.Equal(x.CharacterPolicies, y.CharacterPolicies, new PasswordCharacterPolicyComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());

            return x.MinimumPasswordLength == y.MinimumPasswordLength &&
                x.RejectKeyboardPatterns == y.RejectKeyboardPatterns &&
                x.RejectOwnUserInfo == y.RejectOwnUserInfo &&
                x.UpdatedAt == y.UpdatedAt &&
                x.NumberOfArchivedPasswords == y.NumberOfArchivedPasswords;
        }

        public int GetHashCode(PasswordLoginPolicies obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordExpirationComparer : IEqualityComparer<PasswordExpiration> {
        public bool Equals(PasswordExpiration x, PasswordExpiration y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.IsEnabled == y.IsEnabled &&
                x.ExpiresAfterDays == y.ExpiresAfterDays;
        }

        public int GetHashCode(PasswordExpiration obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordCharacterPolicyComparer : IEqualityComparer<PasswordCharacterPolicies> {
        public bool Equals(PasswordCharacterPolicies x, PasswordCharacterPolicies y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.NumberOfMustContainCharacteristics == y.NumberOfMustContainCharacteristics
                && CompareHelper.ListIsEqual(x.PredefinedCharacterSets, y.PredefinedCharacterSets);
        }

        public int GetHashCode(PasswordCharacterPolicies obj) {
            throw new NotImplementedException();
        }
    }

    internal class PasswordCharacterSetComparer : IEqualityComparer<PasswordCharacterSet> {
        public bool Equals(PasswordCharacterSet x, PasswordCharacterSet y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return Array.Equals(x.Set, y.Set)
                && x.Type == y.Type;
        }

        public int GetHashCode(PasswordCharacterSet obj) {
            throw new NotImplementedException();
        }
    }

    internal class ClassificationPoliciesComparer : IEqualityComparer<ClassificationPolicies> {
        public bool Equals(ClassificationPolicies x, ClassificationPolicies y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.ShareClassificationPolicy, y.ShareClassificationPolicy, new ShareClassificationPolicyComparer());
            return true;
        }

        public int GetHashCode(ClassificationPolicies obj) {
            throw new NotImplementedException();
        }
    }

    internal class ShareClassificationPolicyComparer : IEqualityComparer<ShareClassificationPolicy> {
        public bool Equals(ShareClassificationPolicy x, ShareClassificationPolicy y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.ClassificationMinimumForSharePasswort == y.ClassificationMinimumForSharePasswort;
        }

        public int GetHashCode(ShareClassificationPolicy obj) {
            throw new NotImplementedException();
        }
    }
}
