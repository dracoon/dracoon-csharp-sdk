using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to update the meta data of a room.
    /// </summary>
    public class UpdateRoomRequest {
        /// <summary>
        ///     The node id of the room which should be updated.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     The new name of the room.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The new quota of the room.
        /// </summary>
        public long? Quota { get; set; }

        /// <summary>
        ///     The new notes of the room.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Notes { get; set; }

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
        ///     Constructs a new update room request.
        /// </summary>
        /// <param name="id"><inheritdoc cref="Id"/></param>
        /// <param name="name"><inheritdoc cref="Name"/></param>
        /// <param name="quota"><inheritdoc cref="Quota"/></param>
        /// <param name="notes"><inheritdoc cref="Notes"/></param>
        /// <param name="timestampCreation"><inheritdoc cref="TimestampCreation"/></param>
        /// <param name="timestampModification"><inheritdoc cref="TimestampModification"/></param>
        public UpdateRoomRequest(long id, string name = null, long? quota = null, string notes = null, DateTime? timestampCreation = null, DateTime? timestampModification = null) {
            Id = id;
            Name = name;
            Quota = quota;
            Notes = notes;
            TimestampCreation = timestampCreation;
            TimestampModification = timestampModification;
        }
    }
}