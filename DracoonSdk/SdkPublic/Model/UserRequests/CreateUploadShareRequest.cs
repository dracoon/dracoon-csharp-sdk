using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to create a new upload share.
    /// </summary>
    public class CreateUploadShareRequest {

        /// <summary>
        ///     The id of the node in which the files will be uploaded.
        /// </summary>
        public long NodeId { get; private set; }

        /// <summary>
        ///     The name of the new share.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     The access password of the new upload share.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///     The expiration date of the new share.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        ///     Number of days after which uploaded files expire.
        /// </summary>
        public int? UploadedFilesExpirationPeriod { get; set; }

        /// <summary>
        ///     The notes of the new share.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     The internal notes of the new share.
        /// </summary>
        public String InternalNotes { get; set; }

        /// <summary>
        ///     Allow display of already uploaded files.
        /// </summary>
        public bool ShowUploadedFiles { get; set; }

        /// <summary>
        ///     Maximal amount of files which can be uploaded with the new upload share.
        /// </summary>
        public int? MaxAllowedUploads { get; set; }

        /// <summary>
        ///     Maximal total size over all still uploaded files (in bytes).
        /// </summary>
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; set; }

        /// <summary>
        ///     The IETF language tag for the text message language which will be sent to the text message receivers. See also <seealso cref="TextMessageRecipients"/>
        /// </summary>
        public string ReceiverLanguage { get; set; }

        /// <summary>
        ///     The list of the text message recipients. E.123 / E.164 Formatted
        /// </summary>
        public List<string> TextMessageRecipients { get; set; }

        /// <summary>
        ///     If set to <c>true</c> the creators name for the new share will be shown.
        /// </summary>
        public bool ShowCreatorName { get; set; }

        /// <summary>
        ///     If set to <c>true</c> the creators user name for the new share will be shown.
        /// </summary>
        public bool ShowCreatorUsername { get; set; }

        /// <summary>
        ///     Constructs a new create upload share request.
        /// </summary>
        /// <param name="nodeId"><see cref="NodeId"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="showUploadedFiles"><see cref="ShowUploadedFiles"/></param>
        /// <param name="showCreatorName"><see cref="ShowCreatorName"/></param>
        /// <param name="showCreatorUsername"><see cref="ShowCreatorUsername"/></param>
        /// <param name="password"><see cref="Password"/></param>
        /// <param name="expiration"><see cref="Expiration"/></param>
        /// <param name="uploadedFileExpirationPeriod"><see cref="UploadedFilesExpirationPeriod"/></param>
        /// <param name="notes"><see cref="Notes"/></param>
        /// <param name="maxAllowedUploads"><see cref="MaxAllowedUploads"/></param>
        /// <param name="maxAllowedTotalSizeOverAllUploadedFiles"><see cref="MaxAllowedTotalSizeOverAllUploadedFiles"/></param>
        /// <param name="receiverLanguage"><see cref="ReceiverLanguage"/></param>
        /// <param name="textMessageRecipients"><see cref="TextMessageRecipients"/></param>
        public CreateUploadShareRequest(long nodeId, string name, bool showUploadedFiles = false, bool showCreatorName = false, bool showCreatorUsername = false, string password = null, DateTime? expiration = null,
            int? uploadedFileExpirationPeriod = null, string notes = null, int? maxAllowedUploads = null, long? maxAllowedTotalSizeOverAllUploadedFiles = null,
            string receiverLanguage = null, List<string> textMessageRecipients = null) {
            NodeId = nodeId;
            Name = name;
            Password = password;
            Expiration = expiration;
            UploadedFilesExpirationPeriod = uploadedFileExpirationPeriod;
            Notes = notes;
            ShowUploadedFiles = showUploadedFiles;
            MaxAllowedUploads = maxAllowedUploads;
            MaxAllowedTotalSizeOverAllUploadedFiles = maxAllowedTotalSizeOverAllUploadedFiles;
            ShowCreatorName = showCreatorName;
            ShowCreatorUsername = showCreatorUsername;
            ReceiverLanguage = receiverLanguage;
            TextMessageRecipients = textMessageRecipients;
        }
    }
}