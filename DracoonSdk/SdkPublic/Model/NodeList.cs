using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="nodeList"]/NodeList/*'/>
    public class NodeList {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="nodeList"]/Offset/*'/>
        public long Offset { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="nodeList"]/Limit/*'/>
        public long Limit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="nodeList"]/Total/*'/>
        public long Total { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="nodeList"]/Items/*'/>
        public List<Node> Items { get; internal set; }
    }
}