namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of the resolution strategies.
    /// </summary>
    public enum ResolutionStrategy {
        /// <summary>
        ///     If a conflict exists on e.g. uploading a new file and the file name already exists, the new uploaded file will be automatically renamed.
        /// </summary>
        AutoRename,
        /// <summary>
        ///     If a conflict exists on e.g. uploading a new file and the file name already exists, the old file will be overwritten with the new.
        /// </summary>
        Overwrite,
        /// <summary>
        ///     If a conflict exists do nothing automatically and a error is thrown.
        /// </summary>
        Fail
    }
}