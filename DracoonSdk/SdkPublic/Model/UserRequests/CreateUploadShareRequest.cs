using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/CreateUploadShareRequest/*'/>
    public class CreateUploadShareRequest {
        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/NodeId/*'/>
        public long NodeId { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/Name/*'/>
        public string Name { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/AccessPassword/*'/>
        public string AccessPassword { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/Expiration/*'/>
        public DateTime? Expiration { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/UploadedFilesExpirationPeriod/*'/>
        public int? UploadedFilesExpirationPeriod { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/NotifyCreator/*'/>
        public bool NotifyCreator { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/ShowUploadedFiles/*'/>
        public bool ShowUploadedFiles { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/MaxAllowedUploads/*'/>
        public int? MaxAllowedUploads { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/MaxAllowedTotalSizeOverAllUploadedFiles/*'/>
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/EmailRecipients/*'/>
        public List<string> EmailRecipients { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/EmailSubject/*'/>
        public string EmailSubject { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/EmailBody/*'/>
        public string EmailBody { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/SmsRecipients/*'/>
        public List<string> SmsRecipients { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="createUploadShareRequest"]/CreateUploadShareRequestConstructor/*'/>
        public CreateUploadShareRequest(long nodeId, string name, bool notifyCreator = false, bool showUploadedFiles = false,
            string accessPassword = null, DateTime? expiration = null, int? uploadedFileExpirationPeriod = null, string notes = null,
            int? maxAllowedUploads = null, long? maxAllowedTotalSizeOverAllUploadedFiles = null, List<string> emailRecipients = null,
            string emailSubject = null, string emailBody = null, List<string> smsRecipients = null) {
            NodeId = nodeId;
            Name = name;
            AccessPassword = accessPassword;
            Expiration = expiration;
            UploadedFilesExpirationPeriod = uploadedFileExpirationPeriod;
            Notes = notes;
            NotifyCreator = notifyCreator;
            ShowUploadedFiles = showUploadedFiles;
            MaxAllowedUploads = maxAllowedUploads;
            MaxAllowedTotalSizeOverAllUploadedFiles = maxAllowedTotalSizeOverAllUploadedFiles;
            EmailRecipients = emailRecipients;
            EmailSubject = emailSubject;
            EmailBody = emailBody;
            SmsRecipients = smsRecipients;
        }
    }
}