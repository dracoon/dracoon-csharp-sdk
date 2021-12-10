using System.Collections;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores a list of all nodes in the recycle bin of a room. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "RecycleBinItemList.Offset" /> and < see cref="RecycleBinItemList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class RecycleBinItemList {

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
        ///     The returned recycle bin items of a room. See also <seealso cref="RecycleBinItem"/>
        /// </summary>
        public List<RecycleBinItem> Items { get; internal set; }
    }
}