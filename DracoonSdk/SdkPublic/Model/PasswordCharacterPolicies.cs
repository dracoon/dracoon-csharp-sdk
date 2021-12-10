using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores the policy for the character containment.
    /// </summary>
    public class PasswordCharacterPolicies {

        /// <summary>
        ///     List of character sets which must be contained.
        /// </summary>
        public List<PasswordCharacterSet> PredefinedCharacterSets { get; internal set; }

        /// <summary>
        ///     Defines how much characteristics must be contained. 
        ///     This means if value is 2 and "PredefinedCharacterSets" hast 3 lists then the password must contain characters of 2 lists of the retrieved 3.
        /// </summary>
        public int NumberOfMustContainCharacteristics { get; internal set; }
    }
}