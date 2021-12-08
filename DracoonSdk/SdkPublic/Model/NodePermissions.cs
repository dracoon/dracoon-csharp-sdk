namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores information about the permission a user has on a node.
    /// </summary>
    public class NodePermissions {

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool Manage { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool Read { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool Create { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool Change { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool Delete { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool ManageDownloadShare { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool ManageUploadShare { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool CanReadRecycleBin { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool CanRestoreRecycleBin { get; internal set; }

        /// <summary>
        ///     Is <c>true</c> if the user has the permission on the data room. Otherwise <c>false</c>.
        /// </summary>
        public bool CanDeleteRecycleBin { get; internal set; }
    }
}