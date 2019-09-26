using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/CustomerAccount/*'/>
    public class DownloadShare {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/ShareId/*'/>
        public long ShareId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/NodeId/*'/>
        public long NodeId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/NodePath/*'/>
        public string NodePath { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/AccessKey/*'/>
        public string AccessKey { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/NotifyCreator/*'/>
        public bool NotifyCreator { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/CurrentDownloadsCount/*'/>
        public int CurrentDownloadsCount { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/CreatedAt/*'/>
        public DateTime CreatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/CreatedBy/*'/>
        public UserInfo CreatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/Name/*'/>
        public string Name { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/Classification/*'/>
        public Classification? Classification { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/Notes/*'/>
        public string Notes { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/ShowCreatorName/*'/>
        public bool? ShowCreatorName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/ShowCreatorUserName/*'/>
        public bool? ShowCreatorUserName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/IsProtected/*'/>
        public bool? IsProtected { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/ExpireAt/*'/>
        public DateTime? ExpireAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/MaxAllowedDownloads/*'/>
        public int? MaxAllowedDownloads { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShare"]/IsEncrypted/*'/>
        public bool? IsEncrypted { get; internal set; }
    }
}