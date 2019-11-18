using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterPolicies"]/PasswordCharacterPolicies/*'/>
    public class PasswordCharacterPolicies {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterPolicies"]/PredefinedCharacterSets/*'/>
        public List<PasswordCharacterSet> PredefinedCharacterSets { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="passwordCharacterPolicies"]/NumberOfMustContainCharacteristics/*'/>
        public int NumberOfMustContainCharacteristics { get; internal set; }
    }
}