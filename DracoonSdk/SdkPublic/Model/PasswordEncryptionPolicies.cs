using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/PasswordEncryptionPolicies/*'/>
    public class PasswordEncryptionPolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/CharacterPolicies/*'/>
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/MinimumPasswordLength/*'/>
        public int MinimumPasswordLength { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/RejectOwnUserInfo/*'/>
        public bool RejectOwnUserInfo { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/RejectKeyboardPatterns/*'/>
        public bool RejectKeyboardPatterns { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/UpdatedAt/*'/>
        public DateTime UpdatedAt { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordEncryptionPolicies"]/UpdatedBy/*'/>
        public UserInfo UpdatedBy { get; internal set; }
    }
}