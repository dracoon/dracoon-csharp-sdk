using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Node/*'/>
    public class Node {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Id/*'/>
        public long Id { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Type/*'/>
        public NodeType Type { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/ParentId/*'/>
        public long? ParentId { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/ParentPath/*'/>
        public string ParentPath { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Name/*'/>
        public string Name { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Extension/*'/>
        public string Extension { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/MediaType/*'/>
        public string MediaType { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/MediaToken/*'/>
        public string MediaToken { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Size/*'/>
        public long? Size { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Quota/*'/>
        public long? Quota { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Classification/*'/>
        public Classification? Classification { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Notes/*'/>
        public string Notes { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Hash/*'/>
        public string Hash { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/ExpireAt/*'/>
        public DateTime? ExpireAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CreatedAt/*'/>
        public DateTime? CreatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CreatedBy/*'/>
        public UserInfo CreatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/UpdatedAt/*'/>
        public DateTime? UpdatedAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/UpdatedBy/*'/>
        public UserInfo UpdatedBy { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/HasInheritPermissions/*'/>
        public bool? HasInheritPermissions { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/Permissions/*'/>
        public NodePermissions Permissions { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/IsFavorite/*'/>
        public bool? IsFavorite { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/IsEncrypted/*'/>
        public bool? IsEncrypted { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountChildren/*'/>
        public int? CountChildren { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountRooms/*'/>
        public int? CountRooms { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountFolders/*'/>
        public int? CountFolders { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountFiles/*'/>
        public int? CountFiles { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountDeletedVersions/*'/>
        public int? CountDeletedVersions { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/RecycleBinRetentionPeriod/*'/>
        public int? RecycleBinRetentionPeriod { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountDownloadShares/*'/>
        public int? CountDownloadShares { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/CountUploadShares/*'/>
        public int? CountUploadShares { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="node"]/BranchVersion/*'/>
        public long? BranchVersion { get; internal set; }
    }
}