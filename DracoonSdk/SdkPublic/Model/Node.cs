using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores information about a node.
    ///     <para>
    ///         Node is generic term for all file system objects in DRACOON.Rooms, folders and files are nodes.
    ///     </para>
    /// </summary>
    public class Node {

        /// <summary>
        ///     The id of the node.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     This id is identical across all versions of a file and is therefore only set on files-nodes.
        /// </summary>
        public long? ReferenceId { get; internal set; }

        /// <summary>
        ///     The type of the node. See also <seealso cref="NodeType"/>
        /// </summary>
        public NodeType Type { get; internal set; }

        /// <summary>
        ///     The parent id of the node.
        /// </summary>
        public long? ParentId { get; internal set; }

        /// <summary>
        ///     The path of the parent node.
        /// </summary>
        public string ParentPath { get; internal set; }

        /// <summary>
        ///     The name of the node.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     The file extension of the node. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public string Extension { get; internal set; }

        /// <summary>
        ///     The file media type like "image/jpeg", "application/pdf", ... (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public string MediaType { get; internal set; }

        /// <summary>
        ///     The media token to request a thumbnail from the media server. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public string MediaToken { get; internal set; }

        /// <summary>
        ///     The byte size of the node. If the node is a <see cref="NodeType.Room"/> or <see cref="NodeType.Folder"/> the total byte size of the underlying files.
        /// </summary>
        public long? Size { get; internal set; }

        /// <summary>
        ///     The quota in bytes. (Only if it is a <see cref="NodeType.Room"/>).
        /// </summary>
        public long? Quota { get; internal set; }

        /// <summary>
        ///     The classification of the node. See also <seealso cref="Model.Classification"/>
        /// </summary>
        public Classification? Classification { get; internal set; }

        /// <summary>
        ///     The notes for the node.
        /// </summary>
        public string Notes { get; internal set; }

        /// <summary>
        ///     The hash value of the node. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public string Hash { get; internal set; }

        /// <summary>
        ///     The expiration date of the node. (Only if it is a <see cref="NodeType.File"/>).
        /// </summary>
        public DateTime? ExpireAt { get; internal set; }

        /// <summary>
        ///     The creation date of the node.
        /// </summary>
        public DateTime? CreatedAt { get; internal set; }

        /// <summary>
        ///     The user which created the node. See also <seealso cref="UserInfo"/>
        /// </summary>
        public UserInfo CreatedBy { get; internal set; }

        /// <summary>
        ///     The update date of the node. Note: This date is also updated on meta data changes like node name or others.
        /// </summary>
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        ///     The user which updated the node. See also <seealso cref="UserInfo"/>
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }

        /// <summary>
        ///     The creation date of the physical file.
        /// </summary>
        public DateTime? CreationTime { get; internal set; }

        /// <summary>
        ///     The modification date of hte physical file. Note: This date is NOT changed on meta data changes.
        /// </summary>
        public DateTime? ModificationTime { get; internal set; }

        /// <summary>
        ///     Is set to <c>true</c> if the parent permissions are also applied to this node.
        /// </summary>
        public bool? HasInheritPermissions { get; internal set; }

        /// <summary>
        ///     The permissions for the node. See also <seealso cref="NodePermissions"/>
        /// </summary>
        public NodePermissions Permissions { get; internal set; }

        /// <summary>
        ///     Is set to <c>true</c> if you have ever set this node as favorite.
        /// </summary>
        public bool? IsFavorite { get; internal set; }

        /// <summary>
        ///     Indicates of this node is encrypted.
        /// </summary>
        public bool? IsEncrypted { get; internal set; }

        /// <summary>
        ///     The number of underlying nodes (no matter what node type they are).
        /// </summary>
        public int? CountChildren { get; internal set; }

        /// <summary>
        ///     The number of underlying rooms.
        /// </summary>
        public int? CountRooms { get; internal set; }

        /// <summary>
        ///     The number of underlying folders.
        /// </summary>
        public int? CountFolders { get; internal set; }

        /// <summary>
        ///     The number of comments given to this node.
        /// </summary>
        public int? CountComments { get; internal set; }

        /// <summary>
        ///     The number of underlying files.
        /// </summary>
        public int? CountFiles { get; internal set; }

        /// <summary>
        ///     The Number of deleted versions.
        /// </summary>
        public int? CountPreviousVersions { get; internal set; }

        /// <summary>
        ///     The retention period for deleted nodes.
        /// </summary>
        public int? RecycleBinRetentionPeriod { get; internal set; }

        /// <summary>
        ///     The number of download shares which referencing this node.
        /// </summary>
        public int? CountDownloadShares { get; internal set; }

        /// <summary>
        ///     The number of upload shares which referencing this node.
        /// </summary>
        public int? CountUploadShares { get; internal set; }

        /// <summary>
        ///     The Version of last change in this node or any underlying node.
        /// </summary>
        public long? BranchVersion { get; internal set; }

        /// <summary>
        ///     The room id which spezifies the config for this node. This can be the same id as this current node.
        /// </summary>
        public long? ConfigParentRoomId { get; internal set; }

        /// <summary>
        ///     Determines whether node is browsable by client (for rooms only).
        /// </summary>
        public bool? IsBrowsable { get; internal set; }

        /// <summary>
        ///     Indicates if this room has an activities log or not.
        /// </summary>
        public bool? HasActivitiesLog { get; internal set; }

        /// <summary>
        ///     The virus scanning informations for this node.
        /// </summary>
        public VirusProtectionInfo VirusProtectionInfo { get; internal set; }
    }
}