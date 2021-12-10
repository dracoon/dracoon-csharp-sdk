namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about policies for the classifications if sharing nodes.
    /// </summary>
    public class ShareClassificationPolicy {

        /// <summary>
        ///     For nodes which have this classification minimum a password is required (and higher classifications).
        /// </summary>
        public Classification? ClassificationMinimumForSharePasswort { get; internal set; }
    }
}
