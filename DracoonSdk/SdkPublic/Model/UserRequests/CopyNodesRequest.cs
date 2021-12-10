using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to copy nodes.
    /// </summary>
    public class CopyNodesRequest {

        /// <summary>
        ///     The id of the node where the nodes should be copied to.
        /// </summary>
        public long TargetNodeId { get; private set; }

        /// <summary>
        ///     The information about the nodes which should be copied. See also <seealso cref="CopyNode"/>
        /// </summary>
        public List<CopyNode> NodesToBeCopied { get; private set; }

        /// <summary>
        ///     The conflict resolution strategy for the copy operation. See also <seealso cref="Model.ResolutionStrategy"/>
        /// </summary>
        public ResolutionStrategy ResolutionStrategy { get; set; }

        /// <summary>
        ///     Set to <c>true</c> if any existing share link which references to the source copied nodes should now reference the new copied node.
        /// </summary>
        public bool KeepShareLinks { get; set; }

        /// <summary>
        ///     Constructs a new copy nodes request.
        /// </summary>
        /// <param name="targetNodeId"><inheritdoc cref="TargetNodeId"/></param>
        /// <param name="nodesToBeCopied"><inheritdoc cref="NodesToBeCopied"/></param>
        /// <param name="resolutionStrategy"><inheritdoc cref="ResolutionStrategy"/></param>
        /// <param name="keepShareLinks"><inheritdoc cref="KeepShareLinks"/></param>
        public CopyNodesRequest(long targetNodeId, List<CopyNode> nodesToBeCopied,
            ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename, bool keepShareLinks = false) {
            TargetNodeId = targetNodeId;
            NodesToBeCopied = nodesToBeCopied;
            ResolutionStrategy = resolutionStrategy;
            KeepShareLinks = keepShareLinks;
        }
    }
}