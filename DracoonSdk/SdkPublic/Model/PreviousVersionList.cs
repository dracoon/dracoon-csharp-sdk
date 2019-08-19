using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersionList"]/PreviousVersionList/*'/>
    public class PreviousVersionList {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersionList"]/Offset/*'/>
        public long Offset { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersionList"]/Limit/*'/>
        public long Limit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersionList"]/Total/*'/>
        public long Total { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="previousVersionList"]/Items/*'/>
        public List<PreviousVersion> Items { get; internal set; }
    }
}