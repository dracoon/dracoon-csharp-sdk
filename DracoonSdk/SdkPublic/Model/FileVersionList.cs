using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores a list of file versions. The list may be a paginated response.
    ///     <para>
    ///         <see cref = "FileVersionList.Offset"/> and <see cref="FileVersionList.Limit"/> can be used to get the start and length of the page.
    ///     </para>
    /// </summary>
    public class FileVersionList {
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
        ///     The returned file version items. See also <seealso cref="FileVersion"/>
        /// </summary>
        public List<FileVersion> Items { get; internal set; }
    }
}
