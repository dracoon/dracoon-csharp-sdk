
namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNode"]/CopyNode/*'/>
    public class CopyNode {

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNode"]/NodeId/*'/>
        public long NodeId {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNode"]/NewName/*'/>
        public string NewName {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNode"]/CopyNodeConstructor/*'/>
        public CopyNode(long nodeId, string newName = null) {
            NodeId = nodeId;
            NewName = newName;
        }
    }
}
