using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to delete nodes.
    /// </summary>
    public class DeleteNodesRequest {
        /// <summary>
        ///     The node ids which should be deleted.
        /// </summary>
        public List<long> Ids { get; private set; }

        /// <summary>
        ///     Constructs a new delete nodes request.
        /// </summary>
        /// <param name="ids"><see cref="Ids"/></param>
        public DeleteNodesRequest(List<long> ids) {
            Ids = ids;
        }
    }
}