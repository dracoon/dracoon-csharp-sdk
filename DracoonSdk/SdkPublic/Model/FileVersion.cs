namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores information about a file version.
    /// </summary>
    public class FileVersion {

        /// <summary>
        ///     The id of the file.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     This id is identical across all versions of a file and is therefore only set on files-nodes.
        /// </summary>
        public long ReferenceId { get; internal set; }

        /// <summary>
        ///     The parent id of the file.
        /// </summary>
        public long? ParentId { get; internal set; }

        /// <summary>
        ///     The name of the file.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Is set to <c>true</c> if it is the current version of the reference id otherwise it is an older version.
        /// </summary>
        public bool IsDeleted { get; internal set; }
    }
}
