namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of user types.
    /// </summary>
    public enum UserType {
        /// <summary>
        ///     The user is a internal user with his own DRACOON account.
        /// </summary>
        Internal,
        /// <summary>
        ///     The user hasn't an own DRACOON account.
        /// </summary>
        External,
        /// <summary>
        ///     The user is a non human user.
        /// </summary>
        System,
        /// <summary>
        ///     The user was a user with his own DRACOON account but is know deleted.
        /// </summary>
        Deleted
    }
}