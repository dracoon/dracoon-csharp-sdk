namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     Class which defines the resulting filter and the possible build functions to get the final filter.
    /// </summary>
    /// <typeparam name="T">The filter type specification of the final filter class. e.g. <see cref="Dracoon.Sdk.Filter.NodeTypeFilter"/></typeparam>
    /// <typeparam name="TV">The definition of the final filter class. See also <seealso cref="Dracoon.Sdk.Filter.DracoonFilterType{T}"/></typeparam>
    public class FilterParam<T, TV> {
        internal T Parent;
        internal TV Superclass;

        internal FilterParam(T parent, TV superclass) {
            Parent = parent;
            Superclass = superclass;
        }

        /// <summary>
        ///     Builds the final filter class which can be put into the request filter class. e.g. <see cref="Dracoon.Sdk.Filter.GetNodesFilter.AddNodeTypeFilter(DracoonFilterType{NodeTypeFilter})"/>
        /// </summary>
        /// <returns>The current filter class</returns>
        public TV Build() {
            return Superclass;
        }
    }
}