using System;

namespace Dracoon.Sdk.Model {
    public class PasswordEncryptionPolicies {
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }
        public int MinimumPasswordLength { get; internal set; }
        public bool RejectOwnUserInfo { get; internal set; }
        public bool RejectKeyboardPatterns { get; internal set; }
        public DateTime UpdatedAt { get; internal set; }
        public UserInfo UpdatedBy { get; internal set; }
    }
}