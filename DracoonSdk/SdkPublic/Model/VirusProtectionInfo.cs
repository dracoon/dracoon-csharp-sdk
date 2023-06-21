using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about virus protection status.
    /// </summary>
    public class VirusProtectionInfo {
        /// <summary>
        ///     The verdict of the scanned file.
        /// </summary>
        public VirusProtectionVerdict Verdict { get; internal set; }

        /// <summary>
        ///     The timestamp were the file was last scanned.
        /// </summary>
        public DateTime? CheckedAt { get; internal set; }

        /// <summary>
        ///     The hash of the scanned file.
        /// </summary>
        public string Sha256 { get; internal set; }
    }
}
