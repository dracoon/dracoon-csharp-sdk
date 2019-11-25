using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="attributeList"]/AttributeList/*'/>
    public class AttributeList {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="attributeList"]/Offset/*'/>
        public long Offset { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="attributeList"]/Limit/*'/>
        public long Limit { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="attributeList"]/Total/*'/>
        public long Total { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="attributeList"]/Items/*'/>
        public List<Attribute> Items { get; internal set; }
    }
}