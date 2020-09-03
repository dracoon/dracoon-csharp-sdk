namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="userKeyPairAlgorithmData"]/UserKeyPairAlgorithmData/*'/>
    public class UserKeyPairAlgorithmData {

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userKeyPairAlgorithmData"]/Algorithm/*'/>
        public Crypto.Sdk.UserKeyPairAlgorithm Algorithm {
            get; internal set;
        }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userKeyPairAlgorithmData"]/State/*'/>
        public AlgorithmState State {
            get; internal set;
        }

    }
}
