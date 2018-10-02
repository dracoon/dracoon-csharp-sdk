
namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNode"]/MoveNode/*'/>
    public class MoveNode {

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNode"]/NodeId/*'/>
        public long NodeId {
            get; private set;
        }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNode"]/NewName/*'/>
        public string NewName {
            get; set;
        }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNode"]/MoveNodeConstructor/*'/>
        public MoveNode(long nodeId, string newName = null) {
            NodeId = nodeId;
            NewName = newName;
        }
    }
}
