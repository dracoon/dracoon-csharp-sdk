using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores information about a version of a node.
    /// </summary>
    public class PreviousVersion {
        /// <summary>
        ///     The id of the parent node.
        /// </summary>
        public long ParentId { get; internal set; }

        /// <summary>
        ///     The path of the parent node.
        /// </summary>
        public string ParentPath { get; internal set; }

        /// <summary>
        ///     The type of the version node. See also <seealso cref="NodeType"/>
        /// </summary>
        public NodeType Type { get; internal set; }

        /// <summary>
        ///     The name of the version node.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     The id of the node.
        /// </summary>
        public long? Id { get; internal set; }

        /// <summary>
        ///     The expiration date of the node. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public DateTime? ExpireAt { get; internal set; }

        /// <summary>
        ///     The last access date of the node. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public DateTime? AccessedAt { get; internal set; }

        /// <summary>
        ///     Indicates of this node is encrypted.
        /// </summary>
        public bool? IsEncrypted { get; internal set; }

        /// <summary>
        ///     The notes for the node.
        /// </summary>
        public string Notes { get; internal set; }

        /// <summary>
        ///     The byte size of the node. If the node is a <see cref="NodeType.Room"/> or <see cref="NodeType.Folder"/> the total byte size of the underlying files.
        /// </summary>
        public long? Size { get; internal set; }

        /// <summary>
        ///     The classification of the node. See also <seealso cref="Model.Classification"/>
        /// </summary>
        public Classification? Classification { get; internal set; }

        /// <summary>
        ///     The creation date of the node.
        /// </summary>
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        ///     The user which created the node. See also <seealso cref="UserInfo"/>
        /// </summary>
        public UserInfo CreatedBy { get; internal set; }

        /// <summary>
        ///     The update date of the node.
        /// </summary>
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        ///     The user which updated the node. See also <seealso cref="UserInfo"/>
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }

        /// <summary>
        ///     The deletion date of the node.
        /// </summary>
        public DateTime? DeletedAt { get; internal set; }

        /// <summary>
        ///     The user which deleted the node. See also <seealso cref="UserInfo"/>
        /// </summary>
        public UserInfo DeletedBy { get; internal set; }
    }
}