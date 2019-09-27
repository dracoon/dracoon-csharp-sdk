using System;

namespace Dracoon.Sdk.Model {
    public class PasswordSharePolicies {
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }
        public int MinimumPasswordLength { get; internal set; }
        public bool RejectDictionaryWords { get; internal set; }
        public bool RejectUserInfo { get; internal set; }
        public bool RejectKeyboardPatterns { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public UserInfo UpdatedBy { get; internal set; }
    }
}