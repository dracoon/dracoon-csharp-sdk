namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of the user roles.
    /// </summary>
    public enum UserRole {
        /// <summary>
        ///     The role of the user not defined.
        /// </summary>
        Undefined,
        /// <summary>
        ///     The user has the permission to change the configuration of the server.
        /// </summary>
        ConfigManager,
        /// <summary>
        ///     The user has the permission to manage the user accounts.
        /// </summary>
        UserManager,
        /// <summary>
        ///     The user has the permission to manage the groups.
        /// </summary>
        GroupManager,
        /// <summary>
        ///     The user has the permission to change room configurations.
        /// </summary>
        RoomManager,
        /// <summary>
        ///     The user has the permission to change the configuration of the log.
        /// </summary>
        LogAuditor
    }
}