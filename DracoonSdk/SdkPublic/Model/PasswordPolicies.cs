namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordPolicies"]/PasswordPolicies/*'/>
    public class PasswordPolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordPolicies"]/LoginPolicies/*'/>
        public PasswordLoginPolicies LoginPolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordPolicies"]/SharePolicies/*'/>
        public PasswordSharePolicies SharePolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordPolicies"]/EncryptionPolicies/*'/>
        public PasswordEncryptionPolicies EncryptionPolicies { get; internal set; }
    }
}