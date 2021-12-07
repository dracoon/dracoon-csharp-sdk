using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Informations about the node which should be moved.
    /// </summary>
    public class MoveNode {

        /// <summary>
        ///     The id of the node which should be moved.
        /// </summary>
        public long NodeId { get; private set; }

        /// <summary>
        ///     A new name for the moved node.
        ///     <para>
        ///         Nullable. If not set, the moved node keeps his name.
        ///     </para>
        /// </summary>
        public string NewName { get; set; }

        /// <summary>
        ///     The external creation time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? TimestampCreation { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? TimestampModification { get; set; }

        /// <summary>
        ///     Constructs a new move node information.
        /// </summary>
        /// <param name="nodeId"><inheritdoc cref="NodeId"/></param>
        /// <param name="newName"><inheritdoc cref="NewName"/></param>
        /// <param name="timestampCreation"><inheritdoc cref="TimestampCreation"/></param>
        /// <param name="timestampModification"><inheritdoc cref="TimestampModification"/></param>
        public MoveNode(long nodeId, string newName = null, DateTime? timestampCreation = null, DateTime? timestampModification = null) {
            NodeId = nodeId;
            NewName = newName;
            TimestampCreation = timestampCreation;
            TimestampModification = timestampModification;
        }
    }
}