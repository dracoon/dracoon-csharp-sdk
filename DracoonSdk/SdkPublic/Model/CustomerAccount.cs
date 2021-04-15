namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/CustomerAccount/*'/>
    public class CustomerAccount {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/Id/*'/>
        public long Id { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/Name/*'/>
        public string Name { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/AccountsUsed/*'/>
        public int AccountsUsed { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/AccountsLimit/*'/>
        public int AccountsLimit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/SpaceUsed/*'/>
        public long SpaceUsed { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/SpaceLimit/*'/>
        public long SpaceLimit { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="customerAccount"]/HasEncryptionEnabled/*'/>
        public bool HasEncryptionEnabled { get; internal set; }
    }
}