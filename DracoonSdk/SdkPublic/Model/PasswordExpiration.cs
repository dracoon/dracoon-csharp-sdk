namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordExpiration"]/PasswordExpiration/*'/>
    public class PasswordExpiration {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordExpiration"]/IsEnabled/*'/>
        public bool IsEnabled { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordExpiration"]/ExpiresAfterDays/*'/>
        public int ExpiresAfterDays { get; internal set; }
    }
}