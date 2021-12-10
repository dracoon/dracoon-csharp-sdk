using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the user account.
    /// </summary>
    public class UserAccount {
        /// <summary>
        ///     The id of the user.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     The login data of the user. <seealso cref="Dracoon.Sdk.Model.UserAuthData"/>
        /// </summary>
        public UserAuthData AuthData { get; internal set; }

        /// <summary>
        ///     The user name of the user.
        /// </summary>
        public string UserName { get; internal set; }

        /// <summary>
        ///     The first name of the user.
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        ///     The last name of the user.
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
        ///     If <c>true</c> then the user has set a personal encryption key.
        ///     <para>
        ///         Nullable if the system wide encryption is not active. See also <seealso cref="ServerGeneralSettings.CryptoEnabled"/>
        ///     </para>
        /// </summary>
        public bool? HasEncryptionEnabled { get; internal set; }

        /// <summary>
        ///     If <c>true</c> then the user has rooms in which he is the admin.
        /// </summary>
        public bool HasManageableRooms { get; internal set; }

        /// <summary>
        ///     Indicates at which date the user gets invalid.
        ///     <para>
        ///         Nullable if account never expires
        ///     </para>
        /// </summary>
        public DateTime? ExpireAt { get; internal set; }

        /// <summary>
        ///     Indicates the date of the last successful login of the user.
        ///     <para>
        ///         Nullable if no successful login is happend so far
        ///     </para>
        /// </summary>
        public DateTime? LastLoginSuccessAt { get; internal set; }

        /// <summary>
        ///     Indicates the date of the last failed login of the user.
        ///     <para>
        ///         Nullable if no unsuccessful login is happend so far
        ///     </para>
        /// </summary>
        public DateTime? LastLoginFailAt { get; internal set; }

        /// <summary>
        ///     The user roles which the user has. See also <seealso cref="Dracoon.Sdk.Model.UserRole"/>
        /// </summary>
        public List<UserRole> UserRoles { get; internal set; }

        /// <summary>
        ///     This room is the default home room id of the user.
        ///     <para>
        ///         Nullable if home rooms are not active in the system. See also <seealso cref="ServerGeneralSettings.HomeRoomsActive"/>
        ///     </para>
        /// </summary>
        public long? HomeRoomId { get; internal set; }

        /// <summary>
        ///     Indicates if the user is locked.
        /// </summary>
        public bool IsLocked { get; internal set; }

        /// <summary>
        ///     IETF language tag.
        /// </summary>
        public string Language { get; internal set; }

        /// <summary>
        ///     Indicates if the user must set his <see cref="Email"/> at the first login.
        /// </summary>
        public bool MustSetEmail { get; internal set; }

        /// <summary>
        ///     Indicates if the user must accept the EULA.
        ///     <para>
        ///         Nullable if the eula is disabled in the system. See also <seealso cref="ServerGeneralSettings.EulaEnabled"/>
        ///     </para>
        /// </summary>
        public bool? NeedsToAcceptEULA { get; internal set; }

        /// <summary>
        ///     The phone number of the user.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Phone { get; internal set; }

        /// <summary>
        ///     The groups of which the user is member of.
        /// </summary>
        public List<UserGroup> UserGroups { get; internal set; }
    }
}