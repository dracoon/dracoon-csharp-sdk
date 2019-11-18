using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/PasswordSharePolicies/*'/>
    public class PasswordSharePolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/CharacterPolicies/*'/>
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/MinimumPasswordLength/*'/>
        public int MinimumPasswordLength { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/RejectDictionaryWords/*'/>
        public bool RejectDictionaryWords { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/RejectOwnUserInfo/*'/>
        public bool RejectOwnUserInfo { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/RejectKeyboardPatterns/*'/>
        public bool RejectKeyboardPatterns { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/UpdatedAt/*'/>
        public DateTime UpdatedAt { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordSharePolicies"]/UpdatedBy/*'/>
        public UserInfo UpdatedBy { get; internal set; }
    }
}