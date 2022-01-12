namespace Dracoon.Sdk.Sort {
    /// <summary>
    ///     This class provides sorts for <see cref="Dracoon.Sdk.INodes.GetNodes(string, long?, long?, Filter.GetNodesFilter, GetNodesSort)"/>.
    /// </summary>
    public class GetNodesSort : DracoonSort {

        /// <summary>
        ///     Gets a new sort for the field 'Name' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> Name => new SortField<GetNodesSort>(new GetNodesSort(), "name");

        /// <summary>
        ///     Gets a new sort for the field 'CreatedAt' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> CreatedAt => new SortField<GetNodesSort>(new GetNodesSort(), "createdAt");

        /// <summary>
        ///     Gets a new sort for the field 'CreatedBy' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> CreatedBy => new SortField<GetNodesSort>(new GetNodesSort(), "createdBy");

        /// <summary>
        ///     Gets a new sort for the field 'UpdatedAt' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> UpdatedAt => new SortField<GetNodesSort>(new GetNodesSort(), "updatedAt");

        /// <summary>
        ///     Gets a new sort for the field 'UpdatedBy' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> UpdatedBy => new SortField<GetNodesSort>(new GetNodesSort(), "updatedBy");


        /// <summary>
        ///     Gets a new sort for the field 'Extension' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> FileType => new SortField<GetNodesSort>(new GetNodesSort(), "fileType");

        /// <summary>
        ///     Gets a new sort for the field 'Classification' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> Classification => new SortField<GetNodesSort>(new GetNodesSort(), "classification");

        /// <summary>
        ///     Gets a new sort for the field 'Size' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> Size => new SortField<GetNodesSort>(new GetNodesSort(), "size");

        /// <summary>
        ///     Gets a new sort for the field 'CountPreviousVersions' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> CountPreviousVersions => new SortField<GetNodesSort>(new GetNodesSort(), "cntDeletedVersions");

        /// <summary>
        ///     Gets a new sort for the field 'ModificationTime' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> ModificationTimestamp => new SortField<GetNodesSort>(new GetNodesSort(), "timestampModification");

        /// <summary>
        ///     Gets a new sort for the field 'CreationTime' of a <see cref="Dracoon.Sdk.Model.Node"/>.
        /// </summary>
        public static SortField<GetNodesSort> CreationTimestamp => new SortField<GetNodesSort>(new GetNodesSort(), "timestampCreation");
    }
}
