namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of group member acceptance types.
    /// </summary>
    public enum GroupMemberAcceptance {
        /// <summary>
        ///     Indicates that new members automatically will be accepted on there group join.
        /// </summary>
        AutoAllow,
        /// <summary>
        ///     Indicates that new members must wait for approval on there group join.
        /// </summary>
        Pending
    }
}