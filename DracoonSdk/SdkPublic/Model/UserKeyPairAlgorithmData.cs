namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about a algorithm for the user key pair.
    /// </summary>
    public class UserKeyPairAlgorithmData {

        /// <summary>
        ///     The user key pair algorithm.
        /// </summary>
        public Crypto.Sdk.UserKeyPairAlgorithm Algorithm { get; internal set; }

        /// <summary>
        ///     The state of the user key pair algorithm.
        /// </summary>
        public AlgorithmState State { get; internal set; }
    }
}
