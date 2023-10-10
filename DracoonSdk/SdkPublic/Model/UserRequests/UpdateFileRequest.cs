using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Request to update the meta data of a file.
    /// </summary>
    public class UpdateFileRequest {
        /// <summary>
        ///     The node id of the file which should be updated.
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        ///     The new name of the file.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The new classification of the file. See also <seealso cref="Dracoon.Sdk.Model.Classification"/>
        /// </summary>
        public Classification? Classification { get; set; }

        /// <summary>
        ///     The new notes of the file.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        ///     The new expiration date of the file.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        ///     The real creation time of the file.
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        ///     The last modification time of the file.
        /// </summary>
        public DateTime? ModificationTime { get; set; }

        /// <summary>
        ///     Constructs a new update file request.
        /// </summary>
        /// <param name="id"><see cref="Id"/></param>
        /// <param name="name"><see cref="Name"/></param>
        /// <param name="classification"><see cref="Classification"/></param>
        /// <param name="notes"><see cref="Notes"/></param>
        /// <param name="expiration"><see cref="Expiration"/></param>
        /// <param name="creationTime"><see cref="CreationTime"/></param>
        /// <param name="modificationTime"><see cref="ModificationTime"/></param>
        public UpdateFileRequest(long id, string name = null, Classification? classification = null, string notes = null,
            DateTime? expiration = null, DateTime? creationTime = null, DateTime? modificationTime = null) {
            Id = id;
            Name = name;
            Classification = classification;
            Notes = notes;
            Expiration = expiration;
            CreationTime = creationTime;
            ModificationTime = modificationTime;
        }
    }
}