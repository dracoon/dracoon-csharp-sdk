using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores a list of download shares. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "Dracoon.Sdk.Model.DownloadShareList.Offset" /> and <see cref="Dracoon.Sdk.Model.DownloadShareList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class DownloadShareList {
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
        ///     The returned download share items. See also <seealso cref="Dracoon.Sdk.Model.DownloadShare"/>
        /// </summary>
        public List<DownloadShare> Items { get; internal set; }
    }
}