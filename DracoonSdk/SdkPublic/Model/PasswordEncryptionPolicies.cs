using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores the policies for the encryption password.
    /// </summary>
    public class PasswordEncryptionPolicies {

        /// <summary>
        ///     The password containment definition.
        /// </summary>
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }

        /// <summary>
        ///     The minimum password length.
        /// </summary>
        public int MinimumPasswordLength { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if own user information like "Firstname" or "Lastname" is rejected. Otherwise <c>false</c>.
        /// </summary>
        public bool RejectOwnUserInfo { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if keyboard patterns like "qwer" or "asdf" are rejected. Otherwise <c>false</c>.
        /// </summary>
        public bool RejectKeyboardPatterns { get; internal set; }

        /// <summary>
        ///     Defines when the encryption password policies are updated last.
        /// </summary>
        public DateTime UpdatedAt { get; internal set; }

        /// <summary>
        ///     Defines who updated the encryption password policies last.
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }
    }
}