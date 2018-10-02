using System;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/RecycleBinItem/*'/>
    public class RecycleBinItem {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/ParentId/*'/>
        public long ParentId {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/ParentPath/*'/>
        public string ParentPath {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/Name/*'/>
        public string Name {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/Type/*'/>
        public NodeType Type {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/VersionsCount/*'/>
        public int VersionsCount {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/FirstDeletedAt/*'/>
        public DateTime FirstDeletedAt {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/LastDeletedAt/*'/>
        public DateTime LastDeletedAt {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="recycleBinItem"]/LastDeletedNodeId/*'/>
        public long LastDeletedNodeId {
            get; internal set;
        }
    }
}
