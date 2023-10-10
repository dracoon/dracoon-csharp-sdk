using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to delete specific node versions.
    /// </summary>
    public class DeletePreviousVersionsRequest {
        /// <summary>
        ///     The specific node version ids which should be deleted.
        /// </summary>
        public List<long> VersionIds { get; private set; }

        /// <summary>
        ///     Constructs a new delete previous versions request.
        /// </summary>
        /// <param name="versionIds"><see cref="VersionIds"/></param>
        public DeletePreviousVersionsRequest(List<long> versionIds) {
            VersionIds = versionIds;
        }
    }
}