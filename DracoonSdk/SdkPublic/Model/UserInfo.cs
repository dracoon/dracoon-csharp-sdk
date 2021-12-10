namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the user.
    /// </summary>
    public class UserInfo {
        /// <summary>
        ///     The id of the user.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     The user name.
        /// </summary>
        public string UserName { get; internal set; }

        /// <summary>
        ///     The uuid of the avatar image for this user.
        /// </summary>
        public string AvatarUUID { get; internal set; }

        /// <summary>
        ///     The first name of the user. 
        ///     <para>
        ///         Nullable if <see cref="UserType"/> is <see cref="UserType.External"/> or <see cref="UserType.Deleted"/>
        ///     </para>
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        ///     The last name of the user. 
        ///     <para>
        ///         Nullable if <see cref="UserType"/> is <see cref="UserType.External"/> or <see cref="UserType.Deleted"/>
        ///     </para>
        /// </summary>
        public string LastName { get; internal set; }

        /// <summary>
        ///     The email address of the user.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        ///     The type of the user.
        /// </summary>
        public UserType UserType { get; internal set; }
    }
}