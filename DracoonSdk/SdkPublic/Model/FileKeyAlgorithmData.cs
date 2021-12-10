using Dracoon.Crypto.Sdk;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about a algorithm for the file keys.
    /// </summary>
    public class FileKeyAlgorithmData {

        /// <summary>
        ///     The file key algorithm.
        /// </summary>
        public EncryptedFileKeyAlgorithm Algorithm { get; internal set; }

        /// <summary>
        ///     The state of the file key algorithm.
        /// </summary>
        public AlgorithmState State { get; internal set; }
    }
}
