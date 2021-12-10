namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores the settings for the password expiration.
    /// </summary>
    public class PasswordExpiration {

        /// <summary>
        ///     Is <c>true</c> if password will expire. Otherwise <c>false</c>.
        /// </summary>
        public bool IsEnabled { get; internal set; }

        /// <summary>
        ///     Defines how old passwords can be (in Days).
        /// </summary>
        public int ExpiresAfterDays { get; internal set; }
    }
}