using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/UploadShare/*'/>
    public class UploadShare {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/ShareId/*'/>
        public long ShareId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/NodeId/*'/>
        public long NodeId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/Name/*'/>
        public string Name { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/IsProtected/*'/>
        public bool IsProtected { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/AccessKey/*'/>
        public string AccessKey { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/NotifyCreator/*'/>
        public bool NotifyCreator { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/CreatedAt/*'/>
        public DateTime CreatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/CreatedBy/*'/>
        public UserInfo CreatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/ExpireAt/*'/>
        public DateTime? ExpireAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/NodePath/*'/>
        public string NodePath { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/IsEncrypted/*'/>
        public bool? IsEncrypted { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/Notes/*'/>
        public string Notes { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/InternalNotes/*'/>
        public string InternalNotes { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/UploadedFilesExpirationPeriod/*'/>
        public int? UploadedFilesExpirationPeriod { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/CurrentDoneUploadsCount/*'/>
        public int? CurrentDoneUploadsCount { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/CurrentUploadedFilesCount/*'/>
        public int? CurrentUploadedFilesCount { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/ShowUploadedFiles/*'/>
        public bool? ShowUploadedFiles { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/MaxAllowedUploads/*'/>
        public int? MaxAllowedUploads { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/MaxAllowedTotalSizeOverAllUploadedFiles/*'/>
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShare"]/Type/*'/>
        public NodeType Type { get; internal set; }
    }
}