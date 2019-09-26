using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/CopyNodesRequest/*'/>
    public class CopyNodesRequest {
        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/TargetNodeId/*'/>
        public long TargetNodeId { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/NodesToBeCopied/*'/>
        public List<CopyNode> NodesToBeCopied { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/ResolutionStrategy/*'/>
        public ResolutionStrategy ResolutionStrategy { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/KeepShareLinks/*'/>
        public bool KeepShareLinks { get; set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/CopyNodesRequestConstructor/*'/>
        public CopyNodesRequest(long targetNodeId, List<CopyNode> nodesToBeCopied,
            ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename, bool keepShareLinks = false) {
            TargetNodeId = targetNodeId;
            NodesToBeCopied = nodesToBeCopied;
            ResolutionStrategy = resolutionStrategy;
            KeepShareLinks = keepShareLinks;
        }
    }
}