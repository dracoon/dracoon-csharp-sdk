using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to move nodes.
    /// </summary>
    public class MoveNodesRequest {

        /// <summary>
        ///     The id of the node where the nodes should be moved to.
        /// </summary>
        public long TargetNodeId { get; private set; }

        /// <summary>
        ///     The information about the nodes which should be moved. See also <seealso cref="MoveNode"/>
        /// </summary>
        public List<MoveNode> NodesToBeMoved { get; private set; }

        /// <summary>
        ///     The conflict resolution strategy for the move operation. See also <seealso cref="Model.ResolutionStrategy"/>
        /// </summary>
        public ResolutionStrategy ResolutionStrategy { get; set; }

        /// <summary>
        ///     Set to <c>true</c> if any existing share link which references to the moved nodes should stay usable.
        /// </summary>
        public bool KeepShareLinks { get; set; }

        /// <summary>
        ///     Constructs a new move nodes request.
        /// </summary>
        /// <param name="targetNodeId"><inheritdoc cref="TargetNodeId"/></param>
        /// <param name="nodesToBeMoved"><inheritdoc cref="NodesToBeMoved"/></param>
        /// <param name="resolutionStrategy"><inheritdoc cref="ResolutionStrategy"/></param>
        /// <param name="keepShareLinks"><inheritdoc cref="KeepShareLinks"/></param>
        public MoveNodesRequest(long targetNodeId, List<MoveNode> nodesToBeMoved,
            ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename, bool keepShareLinks = false) {
            TargetNodeId = targetNodeId;
            NodesToBeMoved = nodesToBeMoved;
            ResolutionStrategy = resolutionStrategy;
            KeepShareLinks = keepShareLinks;
        }
    }
}