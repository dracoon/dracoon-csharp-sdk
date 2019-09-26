using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/MoveNodesRequest/*'/>
    public class MoveNodesRequest {
        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/TargetNodeId/*'/>
        public long TargetNodeId { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/NodesToBeMoved/*'/>
        public List<MoveNode> NodesToBeMoved { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/ResolutionStrategy/*'/>
        public ResolutionStrategy ResolutionStrategy { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/KeepShareLinks/*'/>
        public bool KeepShareLinks { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="moveNodesRequest"]/MoveNodesRequestConstructor/*'/>
        public MoveNodesRequest(long targetNodeId, List<MoveNode> nodesToBeMoved,
            ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename, bool keepShareLinks = false) {
            TargetNodeId = targetNodeId;
            NodesToBeMoved = nodesToBeMoved;
            ResolutionStrategy = resolutionStrategy;
            KeepShareLinks = keepShareLinks;
        }
    }
}