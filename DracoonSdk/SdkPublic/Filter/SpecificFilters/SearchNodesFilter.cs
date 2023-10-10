namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     This class provides filters for <see cref="Dracoon.Sdk.INodes.SearchNodes(string, long, long, long, SearchNodesFilter, Sort.SearchNodesSort)"/>.
    /// </summary>
    public class SearchNodesFilter : DracoonFilter {
        /// <summary>
        ///     Gets a new filter for the Type field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NodeTypeFilter"/>
        ///     </para>
        /// </summary>
        public static NodeTypeFilter Type => new NodeTypeFilter();

        /// <summary>
        ///     Gets a new filter for the IsFavorite field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.IsFavoriteFilter"/>
        ///     </para>
        /// </summary>
        public static IsFavoriteFilter IsFavorite => new IsFavoriteFilter();

        /// <summary>
        ///     Gets a new filter for the ParentPath field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.ParentPathFilter"/>
        ///     </para>
        /// </summary>
        public static ParentPathFilter ParentPath => new ParentPathFilter();

        /// <summary>
        ///     Gets a new filter for the UpdatedBy field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.UpdatedByFilter"/>
        ///     </para>
        /// </summary>
        public static UpdatedByFilter UpdatedBy => new UpdatedByFilter();

        /// <summary>
        ///     Gets a new filter for the UpdatedById field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.UpdatedByIdFilter"/>
        ///     </para>
        /// </summary>
        public static UpdatedByIdFilter UpdatedById => new UpdatedByIdFilter();

        /// <summary>
        ///     Gets a new filter for created by.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.CreatedByFilter"/>
        ///     </para>
        /// </summary>
        public static CreatedByFilter CreatedBy => new CreatedByFilter();

        /// <summary>
        ///     Gets a new filter for created by id.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.CreatedByIdFilter"/>
        ///     </para>
        /// </summary>
        public static CreatedByIdFilter CreatedById => new CreatedByIdFilter();

        /// <summary>
        ///     Gets a new file type filter.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.FileTypeFilter"/>
        ///     </para>
        /// </summary>
        public static FileTypeFilter FileType => new FileTypeFilter();

        /// <summary>
        ///     Gets a new classification filter.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.ClassificationFilter"/>
        ///     </para>
        /// </summary>
        public static ClassificationFilter Classification => new ClassificationFilter();

        /// <summary>
        ///     Gets a new timestamp filter for the time of modification of the file.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.TimestampFilter"/>
        ///     </para>
        /// </summary>
        public static TimestampFilter ModificationTimestamp => new TimestampFilter("timestampModification");

        /// <summary>
        ///     Gets a new timestamp filter for the time of creation of the file.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.TimestampFilter"/>
        ///     </para>
        /// </summary>
        public static TimestampFilter CreationTimestamp => new TimestampFilter("timestampCreation");

        /// <summary>
        ///     Adds a type filter to the search nodes filter.
        /// </summary>
        /// <param name="typeFilter">The defined type filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNodeTypeFilter(DracoonFilterType<NodeTypeFilter> typeFilter) {
            CheckFilter(typeFilter, nameof(typeFilter));
            FiltersList.Add(typeFilter);
        }

        /// <summary>
        ///     Adds a favorite filter to the search nodes filter.
        /// </summary>
        /// <param name="isFavoriteFilter">The defined favorite filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddIsFavoriteFilter(DracoonFilterType<IsFavoriteFilter> isFavoriteFilter) {
            CheckFilter(isFavoriteFilter, nameof(isFavoriteFilter));
            FiltersList.Add(isFavoriteFilter);
        }

        /// <summary>
        ///     Adds a parent path filter to the search nodes filter.
        /// </summary>
        /// <param name="parentPathFilter">The defined parent path filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddParentPathFilter(DracoonFilterType<ParentPathFilter> parentPathFilter) {
            CheckFilter(parentPathFilter, nameof(parentPathFilter));
            FiltersList.Add(parentPathFilter);
        }

        /// <summary>
        ///     Adds a updated by user filter to the search nodes filter.
        /// </summary>
        /// <param name="updatedByFilter">The defined updated by filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddUpdatedByFilter(DracoonFilterType<UpdatedByFilter> updatedByFilter) {
            CheckFilter(updatedByFilter, nameof(updatedByFilter));
            FiltersList.Add(updatedByFilter);
        }

        /// <summary>
        ///     Adds a updated by user id filter to the search nodes filter.
        /// </summary>
        /// <param name="updatedByIdFilter">The defined updated by id filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddUpdatedByIdFilter(DracoonFilterType<UpdatedByIdFilter> updatedByIdFilter) {
            CheckFilter(updatedByIdFilter, nameof(updatedByIdFilter));
            FiltersList.Add(updatedByIdFilter);
        }

        /// <summary>
        ///     Adds a file type filter to the search nodes filter.
        /// </summary>
        /// <param name="fileTypeFilter">The defined file type filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddFileTypeFilter(DracoonFilterType<FileTypeFilter> fileTypeFilter) {
            CheckFilter(fileTypeFilter, nameof(fileTypeFilter));
            FiltersList.Add(fileTypeFilter);
        }

        /// <summary>
        ///     Adds a classification filter to the search nodes filter.
        /// </summary>
        /// <param name="classificationFilter">The defined classification filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddClassificationFilter(DracoonFilterType<ClassificationFilter> classificationFilter) {
            CheckFilter(classificationFilter, nameof(classificationFilter));
            FiltersList.Add(classificationFilter);
        }

        /// <summary>
        ///     Adds a created by filter to the search nodes filter.
        /// </summary>
        /// <param name="createdByFilter">The defined created by filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddCreatedByFilter(DracoonFilterType<CreatedByFilter> createdByFilter) {
            CheckFilter(createdByFilter, nameof(createdByFilter));
            FiltersList.Add(createdByFilter);
        }

        /// <summary>
        ///     Adds a created by id filter to the search nodes filter.
        /// </summary>
        /// <param name="createdByIdFilter">The defined created by id filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddCreatedByIdFilter(DracoonFilterType<CreatedByIdFilter> createdByIdFilter) {
            CheckFilter(createdByIdFilter, nameof(createdByIdFilter));
            FiltersList.Add(createdByIdFilter);
        }

        /// <summary>
        ///     Adds a timestamp filter to the search nodes filter.
        /// </summary>
        /// <param name="timestampFilter">The defined timestamp filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddTimestampFilter(DracoonFilterType<TimestampFilter> timestampFilter) {
            CheckFilter(timestampFilter, nameof(timestampFilter));
            FiltersList.Add(timestampFilter);
        }
    }
}