using Dracoon.Sdk.Model;
using System;
using System.Collections.Generic;

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
                x.MediaServerEnabled == y.MediaServerEnabled &&
                x.SharePasswordSmsEnabled == y.SharePasswordSmsEnabled &&
                x.UseS3Storage == y.UseS3Storage &&
                x.WeakPasswordEnabled == y.WeakPasswordEnabled;
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
                x.SmsConfigEnabled == y.SmsConfigEnabled;
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
                x.UploadShareDefaultExpirationPeriodInDays == y.UploadShareDefaultExpirationPeriodInDays;
        }

        public int GetHashCode(ServerDefaultSettings obj) {
            throw new NotImplementedException();
        }
    }
}
