using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to update the meta data of a folder.
    /// </summary>
    public class UpdateFolderRequest {
        /// <summary>
        ///     The node id of the folder which should be updated.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     The new name of the folder.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The new notes of the folder.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     The classification for this node.
        ///     <para>
        ///         Nullable. If not set the parent room classification (or default if not available which is internal) is used.
        ///     </para>
        /// </summary>
        public Classification? Classification { get; set; }

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
        ///     Constructs a new update folder request.
        /// </summary>
        /// <param name="id"><inheritdoc cref="Id"/></param>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="notes"><inheritdoc cref="Notes"/></param>
        /// <param name="classification"><inheritdoc cref="Classification"/></param>
        /// <param name="creationTime"><inheritdoc cref="CreationTime"/></param>
        /// <param name="modificationTime"><inheritdoc cref="ModificationTime"/></param>
        public UpdateFolderRequest(long id, string name = null, string notes = null, Classification? classification = null, DateTime? creationTime = null, DateTime? modificationTime = null) {
            Id = id;
            Name = name;
            Notes = notes;
            Classification = classification;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
        }
    }
}