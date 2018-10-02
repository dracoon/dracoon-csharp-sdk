
namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/SearchNodesSort/*'/>
    public class SearchNodesSort : DracoonSort {

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/UpdatedAt/*'/>
        public static UpdatedAtSort<SearchNodesSort> UpdatedAt => new UpdatedAtSort<SearchNodesSort>(new SearchNodesSort());

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Size/*'/>
        public static SizeSort<SearchNodesSort> Size => new SizeSort<SearchNodesSort>(new SearchNodesSort());
    }
}
