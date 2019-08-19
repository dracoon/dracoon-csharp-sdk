using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/CreateDownloadShareRequest/*'/>
    public class CreateDownloadShareRequest {
        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/NodeId/*'/>
        public long NodeId { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/Name/*'/>
        public string Name { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/AccessPassword/*'/>
        public string AccessPassword { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/EncryptionPassword/*'/>
        public string EncryptionPassword { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/Expiration/*'/>
        public DateTime? Expiration { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/ShowCreatorName/*'/>
        public bool ShowCreatorName { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/ShowCreatorUserName/*'/>
        public bool ShowCreatorUserName { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/NotifyCreator/*'/>
        public bool NotifyCreator { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/MaxAllowedDownloads/*'/>
        public int? MaxAllowedDownloads { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/EmailRecipients/*'/>
        public List<string> EmailRecipients { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/EmailSubject/*'/>
        public string EmailSubject { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/EmailBody/*'/>
        public string EmailBody { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/SmsRecipients/*'/>
        public List<string> SmsRecipients { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createDownloadShareRequest"]/CreateDownloadShareRequestConstructor/*'/>
        public CreateDownloadShareRequest(long nodeId, bool showCreatorName = false, bool showCreatorUserName = false, bool notifyCreator = false,
            string name = null, string accessPassword = null, string encryptionPassword = null, DateTime? expiration = null, string notes = null,
            int? maxAllowedDownloads = null, List<string> emailRecipients = null, string emailSubject = null, string emailBody = null,
            List<string> smsRecipients = null) {
            NodeId = nodeId;
            Name = name;
            AccessPassword = accessPassword;
            EncryptionPassword = encryptionPassword;
            Expiration = expiration;
            Notes = notes;
            ShowCreatorName = showCreatorName;
            ShowCreatorUserName = showCreatorUserName;
            NotifyCreator = notifyCreator;
            MaxAllowedDownloads = maxAllowedDownloads;
            EmailRecipients = emailRecipients;
            EmailSubject = emailSubject;
            EmailBody = emailBody;
            SmsRecipients = smsRecipients;
        }
    }
}