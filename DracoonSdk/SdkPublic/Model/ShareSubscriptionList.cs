using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores a list of upload or download share subscriptions. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "ShareSubscriptionList.Offset"/> and <see cref="ShareSubscriptionList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class ShareSubscriptionList {

        /// <summary>
        ///     The index of the first returned item of the possible total list.
        /// </summary>
        public long Offset { get; internal set; }

        /// <summary>
        ///     The number of returned items.
        /// </summary>
        public long Limit { get; internal set; }

        /// <summary>
        ///     The total number of items which can be requested.
        /// </summary>
        public long Total { get; internal set; }

        /// <summary>
        ///     The returned share subscription items. See also <seealso cref="ShareSubscription"/>
        /// </summary>
        public List<ShareSubscription> Items { get; internal set; }
    }
}
