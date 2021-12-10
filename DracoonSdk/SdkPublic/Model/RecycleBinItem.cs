using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the versions of a node.
    /// </summary>
    public class RecycleBinItem {

        /// <summary>
        ///     The id of the parent node.
        /// </summary>
        public long ParentId { get; internal set; }

        /// <summary>
        ///     The path of the parent node.
        /// </summary>
        public string ParentPath { get; internal set; }

        /// <summary>
        ///     The name of the versioned node.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     The type of the versioned node. See also <seealso cref="NodeType"/>
        /// </summary>
        public NodeType Type { get; internal set; }

        /// <summary>
        ///     The number of versions which exists for the node.
        /// </summary>
        public int VersionsCount { get; internal set; }

        /// <summary>
        ///     The date on which the node was first versioned.
        /// </summary>
        public DateTime FirstDeletedAt { get; internal set; }

        /// <summary>
        ///     The date on which the node was last versioned.
        /// </summary>
        public DateTime LastDeletedAt { get; internal set; }

        /// <summary>
        ///     The node id of the last versioned node instance.
        /// </summary>
        public long LastDeletedNodeId { get; internal set; }

        /// <summary>
        ///     The external creation time of this node.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public DateTime? ModificationTime { get; set; }
    }
}