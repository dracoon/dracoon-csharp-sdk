namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores information about the customer account.
    /// </summary>
    public class CustomerAccount {

        /// <summary>
        ///     The ID of the customer.
        /// </summary>
        public long Id { get; internal set; }

        /// <summary>
        ///     The name of the customer.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     The number of user accounts used by the customer.
        /// </summary>
        public int AccountsUsed { get; internal set; }

        /// <summary>
        ///     The number of user accounts available to the customer.
        /// </summary>
        public int AccountsLimit { get; internal set; }

        /// <summary>
        ///     The space used by the customer.
        /// </summary>
        public long SpaceUsed { get; internal set; }

        /// <summary>
        ///     The space available to the customer.
        /// </summary>
        public long SpaceLimit { get; internal set; }

        /// <summary>
        ///     If <c>true</c> the customer has encryption enabled, otherwise <c>false</c>.
        /// </summary>
        public bool HasEncryptionEnabled { get; internal set; }
    }
}