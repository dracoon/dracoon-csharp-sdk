
namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/ServerGeneralSettings/*'/>
    public class ServerGeneralSettings {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/SharePasswordSmsEnabled/*'/>
        public bool SharePasswordSmsEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/CryptoEnabled/*'/>
        public bool CryptoEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/EmailNotificationButtonEnabled/*'/>
        public bool EmailNotificationButtonEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/EulaEnabled/*'/>
        public bool EulaEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/MediaServerEnabled/*'/>
        public bool MediaServerEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/WeakPasswordEnabled/*'/>
        public bool WeakPasswordEnabled {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="serverGeneralSettings"]/UseS3Storage/*'/>
        public bool UseS3Storage {
            get; internal set;
        }
    }
}
