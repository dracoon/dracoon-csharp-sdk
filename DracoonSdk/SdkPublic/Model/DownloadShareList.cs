using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShareList"]/DownloadShareList/*'/>
    public class DownloadShareList {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShareList"]/Offset/*'/>
        public long Offset { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShareList"]/Limit/*'/>
        public long Limit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShareList"]/Total/*'/>
        public long Total { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="downloadShareList"]/Items/*'/>
        public List<DownloadShare> Items { get; internal set; }
    }
}