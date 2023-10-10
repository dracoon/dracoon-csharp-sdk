using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Informations about the node which should be copied.
    /// </summary>
    public class CopyNode {

        /// <summary>
        ///     The id of the node which should be copied.
        /// </summary>
        public long NodeId { get; private set; }

        /// <summary>
        ///     A new name for the copied node.
        ///     <para>
        ///         Nullable. If not set, the copied node has the same name as the source node.
        ///     </para>
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        ///     The external creation time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? ModificationTime { get; set; }

        /// <summary>
        ///     Constructs a new copy node information.
        /// </summary>
        /// <param name="nodeId"><see cref="NodeId"/></param>
        /// <param name="newName"><see cref="NewName"/></param>
        /// <param name="creationTime"><see cref="CreationTime"/></param>
        /// <param name="modificationTime"><see cref="ModificationTime"/></param>
        public CopyNode(long nodeId, string newName = null, DateTime? creationTime = null, DateTime? modificationTime = null) {
            NodeId = nodeId;
            NewName = newName;
            CreationTime = creationTime;
            this.ModificationTime = modificationTime;
        }
    }
}