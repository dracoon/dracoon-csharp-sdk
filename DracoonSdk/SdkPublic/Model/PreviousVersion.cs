using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/PreviousVersion/*'/>
    public class PreviousVersion {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/ParentId/*'/>
        public long ParentId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/ParentPath/*'/>
        public string ParentPath { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Type/*'/>
        public NodeType Type { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Name/*'/>
        public string Name { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Id/*'/>
        public long? Id { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/ExpireAt/*'/>
        public DateTime? ExpireAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/AccessedAt/*'/>
        public DateTime? AccessedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/IsEncrypted/*'/>
        public bool? IsEncrypted { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Notes/*'/>
        public string Notes { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Size/*'/>
        public long? Size { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/Classification/*'/>
        public Classification? Classification { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/CreatedAt/*'/>
        public DateTime? CreatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/CreatedBy/*'/>
        public UserInfo CreatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/UpdatedAt/*'/>
        public DateTime? UpdatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/UpdatedBy/*'/>
        public UserInfo UpdatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/DeletedAt/*'/>
        public DateTime? DeletedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersion"]/DeletedBy/*'/>
        public UserInfo DeletedBy { get; internal set; }
    }
}