namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about a share subscription.
    /// </summary>
    public class ShareSubscription {
        /// <summary>
        ///     The id of the share where this subscription is on.
        /// </summary>
        public long ShareId { get; internal set; }

        /// <summary>
        ///     The associated parent room id where the target node of this share is managed on.
        /// </summary>
        public long? AuthParentRoomId { get; internal set; }
    }
}
