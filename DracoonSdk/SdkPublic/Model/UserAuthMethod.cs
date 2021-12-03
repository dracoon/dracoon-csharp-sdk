namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     The method which are available for authentication.
    /// </summary>
    public enum UserAuthMethod {
        /// <summary>
        ///     The basic authentication.
        /// </summary>
        Basic,
        /// <summary>
        ///     The active directory authentication.
        /// </summary>
        ActiveDirectory,
        /// <summary>
        ///     The radius authentication.
        /// </summary>
        Radius,
        /// <summary>
        ///     The openID authentication.
        /// </summary>
        OpenID,
        /// <summary>
        ///     An authentication method which the sdk currently doesn't knows.
        /// </summary>
        Unknown
    }
}
