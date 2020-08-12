using Dracoon.Crypto.Sdk;

namespace Dracoon.Sdk.SdkPublic.Model {
    public class FileKeyAlgorithm {

        public EncryptedFileKeyAlgorithm Algorithm {
            get; internal set;
        }

        public AlgorithmState State {
            get; internal set;
        }

    }
}
