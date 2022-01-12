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
        ///     Gets a new sort for the field 'UpdatedBy' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> UpdatedBy => new SortField<SearchNodesSort>(new SearchNodesSort(), "updatedBy");

        /// <summary>
        ///     Gets a new sort for the field 'Size' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Size => new SortField<SearchNodesSort>(new SearchNodesSort(), "size");

        /// <summary>
        ///     Gets a new sort for the field 'CreatedAt' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> CreatedAt => new SortField<SearchNodesSort>(new SearchNodesSort(), "createdAt");

        /// <summary>
        ///     Gets a new sort for the field 'CreatedBy' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> CreatedBy => new SortField<SearchNodesSort>(new SearchNodesSort(), "createdBy");

        /// <summary>
        ///     Gets a new sort for the field 'Name' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Name => new SortField<SearchNodesSort>(new SearchNodesSort(), "name");

        /// <summary>
        ///     Gets a new sort for the field 'Classification' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Classification => new SortField<SearchNodesSort>(new SearchNodesSort(), "classification");

        /// <summary>
        ///     Gets a new sort for the field 'Extension' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> FileType => new SortField<SearchNodesSort>(new SearchNodesSort(), "fileType");

        /// <summary>
        ///     Gets a new sort for the field 'CountPreviousVersions' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> CountPreviousVersions => new SortField<SearchNodesSort>(new SearchNodesSort(), "cntDeletedVersions");

        /// <summary>
        ///     Gets a new sort for the field 'ModificationTime' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> ModificationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampModification");

        /// <summary>
        ///     Gets a new sort for the field 'CreationTime' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> CreationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampCreation");

        /// <summary>
        ///     Gets a new sort for the field 'Type' (room, folder, file) of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> Type => new SortField<SearchNodesSort>(new SearchNodesSort(), "type");

        /// <summary>
        ///     Gets a new sort for the field 'ParentPath' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<SearchNodesSort> ParentPath => new SortField<SearchNodesSort>(new SearchNodesSort(), "parentPath");
    }
}