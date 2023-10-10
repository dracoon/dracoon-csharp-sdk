using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores a list of upload shares. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "Dracoon.Sdk.Model.UploadShareList.Offset" /> and <see cref="Dracoon.Sdk.Model.UploadShareList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class UploadShareList {
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
        ///     The returned upload share items. See also <seealso cref="Dracoon.Sdk.Model.UploadShare"/>
        /// </summary>
        public List<UploadShare> Items { get; internal set; }
    }
}