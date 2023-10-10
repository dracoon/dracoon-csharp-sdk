namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     Enumeration of virus protection verdicts.
    /// </summary>
    public enum VirusProtectionVerdict {
        /// <summary>
        ///     The node hasn't fulfilled the scanning preconditions.
        /// </summary>
        NoScanning,
        /// <summary>
        ///     The node isn't scanned so far.
        /// </summary>
        InProgress,
        /// <summary>
        ///     The node isn't virus affected.
        /// </summary>
        Clean,
        /// <summary>
        ///     The node could be virus affected.
        /// </summary>
        Malicious
    }
}