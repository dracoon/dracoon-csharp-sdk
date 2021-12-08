namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores the character set definition.
    /// </summary>
    public class PasswordCharacterSet {

        /// <summary>
        ///     A array of the allowed characters of this set.
        /// </summary>
        public char[] Set { get; internal set; }

        /// <summary>
        ///     The type of this set.
        /// </summary>
        public PasswordCharacterSetType Type { get; internal set; }
    }
}