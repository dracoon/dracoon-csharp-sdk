using Dracoon.Sdk.Model;
using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores the policies for the login password.
    /// </summary>
    public class PasswordLoginPolicies {

        /// <summary>
        ///     The password containment definition.
        /// </summary>
        public PasswordCharacterPolicies CharacterPolicies { get; internal set; }

        /// <summary>
        ///     The minimum password length.
        /// </summary>
        public int MinimumPasswordLength { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if dictionary words like "Password" are rejected. Otherwise <c>false</c>.
        /// </summary>
        public bool RejectDictionaryWords { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if own user information like "Firstname" or "Lastname" is rejected. Otherwise <c>false</c>.
        /// </summary>
        public bool RejectOwnUserInfo { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if keyboard patterns like "qwer" or "asdf" are rejected. Otherwise <c>false</c>.
        /// </summary>
        public bool RejectKeyboardPatterns { get; internal set; }

        /// <summary>
        ///     The number of passwords to archive. Range: 0 - 10. If 0 = disabled.
        /// </summary>
        public int NumberOfArchivedPasswords { get; internal set; }

        /// <summary>
        ///     Informations about the expiration of a login password.
        /// </summary>
        public PasswordExpiration PasswordExpiration { get; internal set; }

        /// <summary>
        ///     Defines when the share password policies are updated last.
        /// </summary>
        public DateTime UpdatedAt { get; internal set; }

        /// <summary>
        ///     Defines who updated the share password policies last.
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }
    }
}
