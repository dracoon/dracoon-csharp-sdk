using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShareList"]/UploadShareList/*'/>
    public class UploadShareList {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShareList"]/Offset/*'/>
        public long Offset { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShareList"]/Limit/*'/>
        public long Limit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShareList"]/Total/*'/>
        public long Total { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="uploadShareList"]/Items/*'/>
        public List<UploadShare> Items { get; internal set; }
    }
}