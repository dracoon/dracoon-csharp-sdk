﻿using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to create a new download share.
    /// </summary>
    public class CreateDownloadShareRequest {

        /// <summary>
        ///     The id of the shared node.
        /// </summary>
        public long NodeId { get; private set; }

        /// <summary>
        ///     The name of the new share.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The password of the new download share which is either used for protecting the share or for using an encrypted share.
        /// </summary>
        public char[] Password { get; set; }

        /// <summary>
        ///     The expiration date of the new share.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        ///     The notes of the new share.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     The internal notes of the new share.
        /// </summary>
        public string InternalNotes { get; set; }

        /// <summary>
        ///     If set to <c>true</c> the creators name for the new share will be shown.
        /// </summary>
        public bool ShowCreatorName { get; set; }

        /// <summary>
        ///     If set to <c>true</c> the creators user name for the new share will be shown.
        /// </summary>
        public bool ShowCreatorUserName { get; set; }

        /// <summary>
        ///     Limit the maximum allowed usages of the new download share.
        /// </summary>
        public int? MaxAllowedDownloads { get; set; }

        /// <summary>
        ///     The IETF language tag for the text message language which will be sent to the text message receivers. See also <seealso cref="TextMessageRecipients"/>
        /// </summary>
        public string ReceiverLanguage { get; set; }

        /// <summary>
        ///     The list of the text message recipients. E.123 / E.164 Formatted
        /// </summary>
        public List<string> TextMessageRecipients { get; set; }

        /// <summary>
        ///     Constructs a new create download share request.
        /// </summary>
        /// <param name="nodeId"><see cref="NodeId"/></param>
        /// <param name="showCreatorName"><see cref="ShowCreatorName"/></param>
        /// <param name="showCreatorUserName"><see cref="ShowCreatorUserName"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="expiration"><see cref="Expiration"/></param>
        /// <param name="notes"><see cref="Notes"/></param>
        /// <param name="internalNotes"><see cref="InternalNotes"/></param>
        /// <param name="maxAllowedDownloads"><see cref="MaxAllowedDownloads"/></param>
        /// <param name="receiverLanguage"><see cref="ReceiverLanguage"/></param>
        /// <param name="textMessageRecipients"><see cref="TextMessageRecipients"/></param>
        public CreateDownloadShareRequest(long nodeId, bool showCreatorName = false, bool showCreatorUserName = false,
            string name = null, char[] password = null, DateTime? expiration = null, string notes = null,
            string internalNotes = null, int? maxAllowedDownloads = null, string receiverLanguage = null, List<string> textMessageRecipients = null) {
            NodeId = nodeId;
            Name = name;
            Password = password;
            Expiration = expiration;
            Notes = notes;
            InternalNotes = internalNotes;
            ShowCreatorName = showCreatorName;
            ShowCreatorUserName = showCreatorUserName;
            MaxAllowedDownloads = maxAllowedDownloads;
            ReceiverLanguage = receiverLanguage;
            TextMessageRecipients = textMessageRecipients;
        }
    }
}