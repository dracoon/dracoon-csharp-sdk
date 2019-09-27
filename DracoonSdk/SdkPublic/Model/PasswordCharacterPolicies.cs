using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    public class PasswordCharacterPolicies {
        public List<PasswordCharacterSet> PredefinedCharacterSets { get; internal set; }
        public int NumberOfMustContainCharacteristics { get; internal set; }
    }
}