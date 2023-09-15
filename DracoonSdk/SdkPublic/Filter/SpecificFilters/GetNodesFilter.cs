namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     This class provides filters for <see cref="Dracoon.Sdk.INodes.GetNodes(long, long?, long?, GetNodesFilter, Sort.GetNodesSort)"/>.
    /// </summary>
    public class GetNodesFilter : DracoonFilter {
        /// <summary>
        ///     Gets a new filter for the Type field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NodeTypeFilter"/>
        ///     </para>
        /// </summary>
        public static NodeTypeFilter Type => new NodeTypeFilter();

        /// <summary>
        ///     Gets a new filter for the Name field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NameFilter"/>
        ///     </para>
        /// </summary>
        public static NameFilter Name => new NameFilter();

        /// <summary>
        ///     Gets a new filter for the IsEncrypted field of a node (<see cref="Dracoon.Sdk.Model.Node"/>).
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.Filter.NodeIsEncryptedFilter"/>
        ///     </para>
        /// </summary>
        public static NodeIsEncryptedFilter IsEncrypted => new NodeIsEncryptedFilter();

        /// <summary>
        ///     Adds a type filter to the get nodes filter.
        /// </summary>
        /// <param name="typeFilter">The defined type filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNodeTypeFilter(DracoonFilterType<NodeTypeFilter> typeFilter) {
            CheckFilter(typeFilter, nameof(typeFilter));
            FiltersList.Add(typeFilter);
        }

        /// <summary>
        ///     Adds a name filter to the get nodes filter.
        /// </summary>
        /// <param name="nameFilter">The defined name filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            FiltersList.Add(nameFilter);
        }

        /// <summary>
        ///     Adds a is encrypted filter to the get nodes filter.
        /// </summary>
        /// <param name="isEncryptedFilter">The defined is encrypted filter.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public void AddNodeIsEncryptedFilter(DracoonFilterType<NodeIsEncryptedFilter> isEncryptedFilter) {
            CheckFilter(isEncryptedFilter, nameof(isEncryptedFilter));
            FiltersList.Add(isEncryptedFilter);
        }
    }
}