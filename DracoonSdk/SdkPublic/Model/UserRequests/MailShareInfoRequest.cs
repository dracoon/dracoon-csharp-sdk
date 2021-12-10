using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Send an email to specific recipients for existing Download Share.
    /// </summary>
    public class MailShareInfoRequest {

        /// <summary>
        ///     The id of the associated share.
        /// </summary>
        public long ShareId { get; private set; }

        /// <summary>
        ///     The recipients of the message.
        /// </summary>
        public List<string> Recipients { get; private set; }

        /// <summary>
        ///     The body of the message.
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        ///     The IETF language tag.
        /// </summary>
        public string ReceiverLanguage { get; set; }

        /// <summary>
        ///     Constructs a new create mail share info request.
        /// </summary>
        /// <param name="shareId"><inheritdoc cref="ShareId"/></param>
        /// <param name="body"><inheritdoc cref="Body"/></param>
        /// <param name="recipients"><inheritdoc cref="Recipients"/></param>
        /// <param name="receiverLanguage"><inheritdoc cref="ReceiverLanguage"/></param>
        public MailShareInfoRequest(long shareId, string body, List<string> recipients, string receiverLanguage = null) {
            ShareId = shareId;
            Body = body;
            Recipients = recipients;
            ReceiverLanguage = receiverLanguage;
        }
    }
}
