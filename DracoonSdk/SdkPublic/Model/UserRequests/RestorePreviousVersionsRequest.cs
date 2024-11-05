using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to restore a previous version of a node.
    /// </summary>
    public class RestorePreviousVersionsRequest {
        /// <summary>
        ///     Set, if the node version which should be restored sould be placed on the defined new parent node. Otherwise the old place of the node version is used.
        /// </summary>
        public long? NewParentNodeId { get; set; }

        /// <summary>
        ///     The version ids which should be restored.
        /// </summary>
        public List<long> RestoreVersionIds { get; private set; }

        /// <summary>
        ///     The resolution strategy for raised conficts.
        /// </summary>
        public ResolutionStrategy ResolutionStrategy { get; set; }

        /// <summary>
        ///     Only for resolution strategy 'Overwrite'.
        ///     <para>
        ///         Set to <c>true</c> if the share link of the current version should reference the restored version. Otherwise the share link gets invalid.
        ///     </para>
        /// </summary>
        public bool KeepShareLinks { get; set; }

        /// <summary>
        ///     Constructs a new restore previous versions request.
        /// </summary>
        /// <param name="restoreVersionIds"><see cref="RestoreVersionIds"/></param>
        /// <param name="resolutionStrategy"><see cref="ResolutionStrategy"/></param>
        /// <param name="keepShareLinks"><see cref="KeepShareLinks"/></param>
        /// <param name="newParentNodeId"><see cref="NewParentNodeId"/></param>
        public RestorePreviousVersionsRequest(List<long> restoreVersionIds, ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename,
            bool keepShareLinks = false, long? newParentNodeId = null) {
            RestoreVersionIds = restoreVersionIds;
            ResolutionStrategy = resolutionStrategy;
            NewParentNodeId = newParentNodeId;
            KeepShareLinks = keepShareLinks;
        }
    }
}