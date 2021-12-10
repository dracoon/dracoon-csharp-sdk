namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the general configuration of the server.
    /// </summary>
    public class ServerGeneralSettings {
        /// <summary>
        ///     Is <c>true</c> if share passwords can be send via SMS. Otherwise <c>false</c>.
        /// </summary>
        public bool SharePasswordSmsEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if cliend-side cryptography is available for rooms. Otherwise <c>false</c>.
        /// </summary>
        public bool CryptoEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the email notification button is enabled. Otherwise <c>false</c>.
        /// </summary>
        public bool EmailNotificationButtonEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if each user has to confirm the EULA at first login. Otherwise <c>false</c>.
        /// </summary>
        public bool EulaEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if S3 is used as storage backend. Otherwise <c>false</c>.
        /// </summary>
        public bool UseS3Storage { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if S3 tags are enabled. Otherwise <c>false</c>.
        /// </summary>
        public bool S3TagsEnabled { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the home rooms are activated. Otherwise <c>false</c>.
        /// </summary>
        public bool HomeRoomsActive { get; internal set; }

        /// <summary>
        ///     The id of the root home room. Can be null if <see cref="HomeRoomsActive"/> is <c>false</c>.
        /// </summary>
        public long? HomeRoomParentId { get; internal set; }

        /// <summary>
        ///     The subscription plan of the customer.
        /// </summary>
        public SubscriptionPlan SubscriptionPlan { get; internal set; }
    }
}