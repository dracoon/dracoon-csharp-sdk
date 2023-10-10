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
        public DateTime? CreationTime { get; set; }

        /// <summary>
        ///     The content modification time of this node.
        ///     <para>
        ///         Nullable. If not set, the default is the current server time in UTC.
        ///     </para>
        /// </summary>
        public DateTime? ModificationTime { get; set; }

        /// <summary>
        ///     Constructs a new update room request.
        /// </summary>
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="quota"><see cref="Quota"/></param>
        /// <param name="notes"><see cref="Notes"/></param>
        /// <param name="creationTime"><see cref="CreationTime"/></param>
        /// <param name="modificationTime"><see cref="ModificationTime"/></param>
        public UpdateRoomRequest(long id, string name = null, long? quota = null, string notes = null, DateTime? creationTime = null, DateTime? modificationTime = null) {
            Id = id;
            Name = name;
            Quota = quota;
            Notes = notes;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
        }
    }
}