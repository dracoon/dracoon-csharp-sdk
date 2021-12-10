using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores custom defined attributes. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "AttributeList.Offset" /> and < see cref="AttributeList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class AttributeList {

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
        ///     The returned upload share items. See also <seealso cref="UploadShare"/>
        /// </summary>
        public List<Attribute> Items { get; internal set; }
    }
}