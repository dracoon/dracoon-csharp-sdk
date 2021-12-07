using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to create a new folder.
    /// </summary>
    public class CreateFolderRequest {

        /// <summary>
        ///     The parent node id under which the new folder should be created.
        /// </summary>
        public long ParentId { get; private set; }

        /// <summary>
        ///     The name of the new folder.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     The notes of the new folder.
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
        public DateTime? TimestampCreation { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? TimestampModification { get; set; }

        /// <summary>
        ///     Constructs a new create folder request.
        /// </summary>
        /// <param name="parentId"><inheritdoc cref="ParentId"/></param>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="notes"><inheritdoc cref="Notes"/></param>
        /// <param name="classification"><inheritdoc cref="Classification"/></param>
        /// <param name="timestampCreation"><inheritdoc cref="TimestampCreation"/></param>
        /// <param name="timestampModificaiton"><inheritdoc cref="TimestampModification"/></param>
        public CreateFolderRequest(long parentId, string name, string notes = null, Classification? classification = null, DateTime? timestampCreation = null, DateTime? timestampModificaiton = null) {
            ParentId = parentId;
            Name = name;
            Notes = notes;
            Classification = classification;
            TimestampCreation = timestampCreation;
            TimestampModification = timestampModificaiton;
        }
    }
}