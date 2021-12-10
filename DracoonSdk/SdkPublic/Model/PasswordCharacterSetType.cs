namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Defines the type of a character set.
    /// </summary>
    public enum PasswordCharacterSetType {
        /// <summary>
        ///     Every character set.
        /// </summary>
        None,
        /// <summary>
        ///     Upper case characters set.
        /// </summary>
        Uppercase,
        /// <summary>
        ///     Lower case characters set.
        /// </summary>
        Lowercase,
        /// <summary>
        ///     Numeric characters set.
        /// </summary>
        Numeric,
        /// <summary>
        ///     Special characters set.
        /// </summary>
        Special
    }
}