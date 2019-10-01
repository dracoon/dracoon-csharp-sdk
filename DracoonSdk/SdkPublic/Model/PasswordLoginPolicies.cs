using System;
namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/PasswordLoginPolicies/*'/>
    public class PasswordLoginPolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/CharacterPolicies/*'/>
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/MinimumPasswordLength/*'/>
        public int MinimumPasswordLength { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/RejectDictionaryWords/*'/>
        public bool RejectDictionaryWords { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/RejectOwnUserInfo/*'/>
        public bool RejectOwnUserInfo { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/RejectKeyboardPatterns/*'/>
        public bool RejectKeyboardPatterns { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/NumberOfArchivedPasswords/*'/>
        public int NumberOfArchivedPasswords { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/PasswordExpirationPolicies/*'/>
        public PasswordExpiration PasswordExpirationPolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/LoginFailurePolicies/*'/>
        public PasswordLoginFailurePolicies LoginFailurePolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/UpdatedAt/*'/>
        public DateTime UpdatedAt { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordLoginPolicies"]/UpdatedBy/*'/>
        public UserInfo UpdatedBy { get; internal set; }
    }
}