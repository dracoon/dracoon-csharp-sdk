namespace Dracoon.Sdk.Sort {
    /// <summary>
    ///     Provides the functionality to set the sort as ascending or descending.
    /// </summary>
    /// <typeparam name="T">Is the specific definition for a request like <see cref="Dracoon.Sdk.Sort.SearchNodesSort"/>.</typeparam>
    public class DracoonSortOrder<T> where T : DracoonSort {
        internal T Parent;

        /// <summary>
        ///     Constructs a new sort order.
        /// </summary>
        /// <param name="parent">The parent instance like <see cref="Dracoon.Sdk.Sort.SearchNodesSort"/>.</param>
        public DracoonSortOrder(T parent) {
            Parent = parent;
        }

        /// <summary>
        ///     Sets the sort to ascending order. (smallest value first)
        /// </summary>
        /// <returns>The usable sort order.</returns>
        public T Ascending() {
            Parent.SortString += ":asc";
            return Parent;
        }

        /// <summary>
        ///     Sets the sort to descending order. (largest value first)
        /// </summary>
        /// <returns>The usable sort order.</returns>
        public T Descending() {
            Parent.SortString += ":desc";
            return Parent;
        }
    }
}