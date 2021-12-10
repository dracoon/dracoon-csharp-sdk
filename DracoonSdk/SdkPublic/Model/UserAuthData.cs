namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the login data of the user.
    /// </summary>
    public class UserAuthData {
        /// <summary>
        ///     The authentication method of the user.
        /// </summary>
        public UserAuthMethod Method { get; internal set; }

        /// <summary>
        ///     The login name of the user.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Login { get; internal set; }

        /// <summary>
        ///     The password of the user.
        ///     <para>
        ///         Nullable if <see cref="Method"/> is NOT <see cref="UserAuthMethod.Basic"/>
        ///     </para>
        /// </summary>
        public string Password { get; internal set; }

        /// <summary>
        ///     Indicates if the user have to change his password.
        /// </summary>
        public bool MustChangePassword { get; internal set; }

        /// <summary>
        ///     The id of the ad configuration.
        ///     <para>
        ///         Nullable if <see cref="Method"/> is NOT <see cref="UserAuthMethod.ActiveDirectory"/>
        ///     </para>
        /// </summary>
        public int? ADConfigId { get; internal set; }

        /// <summary>
        ///     The id of the open id configuration.
        ///     <para>
        ///         Nullable if <see cref="Method"/> is NOT <see cref="UserAuthMethod.OpenID"/>
        ///     </para>
        /// </summary>
        public int? OIDConfigId { get; internal set; }
    }
}
