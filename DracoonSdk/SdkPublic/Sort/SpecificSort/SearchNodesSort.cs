namespace Dracoon.Sdk.Sort {
    /// <summary>
    ///     This class provides sorts for <see cref="Dracoon.Sdk.INodes.SearchNodes(string, long, long, long, Filter.SearchNodesFilter, SearchNodesSort)"/>.
    /// </summary>
    public class SearchNodesSort : DracoonSort {

        /// <summary>
        ///     Gets a new sort for the field 'UpdatedAt' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> UpdatedAt => new SortField<SearchNodesSort>(new SearchNodesSort(), "updatedAt");

        /// <summary>
        ///     Gets a new sort for the field 'Size' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Size => new SortField<SearchNodesSort>(new SearchNodesSort(), "size");

        /// <summary>
        ///     Gets a new sort for the field 'CreatedAt' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> CreatedAt => new SortField<SearchNodesSort>(new SearchNodesSort(), "createdAt");

        /// <summary>
        ///     Gets a new sort for the field 'Name' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Name => new SortField<SearchNodesSort>(new SearchNodesSort(), "name");

        /// <summary>
        ///     Gets a new sort for the field 'timestampModification'.
        /// </summary>
        public static SortField<SearchNodesSort> ModificationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampModification");

        /// <summary>
        ///     Gets a new sort for the field 'timestampCreation'.
        /// </summary>
        public static SortField<SearchNodesSort> CreationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampCreation");
    }
}