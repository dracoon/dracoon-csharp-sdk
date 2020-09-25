using Dracoon.Crypto.Sdk;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="fileKeyAlgorithmData"]/FileKeyAlgorithmData/*'/>
    public class FileKeyAlgorithmData {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="fileKeyAlgorithmData"]/Algorithm/*'/>
        public EncryptedFileKeyAlgorithm Algorithm {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="fileKeyAlgorithmData"]/State/*'/>
        public AlgorithmState State {
            get; internal set;
        }

    }
}
