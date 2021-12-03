namespace Dracoon.Sdk.Model {
    /// <summary>
    /// This model stores informations about a users group membership.
    /// </summary>
    public class UserGroup {
        /// <summary>
        ///     The id of the group.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     Indicates if the user is member of this group or not.
        /// </summary>
        public bool IsMember { get; internal set; }

        /// <summary>
        ///     The name of the group.
        /// </summary>
        public string Name { get; internal set; }
    }
}
