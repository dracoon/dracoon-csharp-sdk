namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterSet"]/PasswordCharacterSet/*'/>
    public class PasswordCharacterSet {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterSet"]/Set/*'/>
        public char[] Set { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterSet"]/Type/*'/>
        public PasswordCharacterSetType Type { get; internal set; }
    }
}