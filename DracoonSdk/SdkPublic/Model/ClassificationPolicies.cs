namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about policies for the classifications.
    /// </summary>
    public class ClassificationPolicies {

        /// <summary>
        ///     The policies for classifications if sharing nodes.
        /// </summary>
        public ShareClassificationPolicy ShareClassificationPolicy { get; internal set; }
    }
}
