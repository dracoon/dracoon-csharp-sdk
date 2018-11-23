
namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/SearchNodesSort/*'/>
    public class SearchNodesSort : DracoonSort {

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/UpdatedAt/*'/>
        public static UpdatedAtSort<SearchNodesSort> UpdatedAt => new UpdatedAtSort<SearchNodesSort>(new SearchNodesSort());

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Size/*'/>
        public static SizeSort<SearchNodesSort> Size => new SizeSort<SearchNodesSort>(new SearchNodesSort());

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreatedAt/*'/>
        public static CreatedAtSort<SearchNodesSort> CreatedAt => new CreatedAtSort<SearchNodesSort>(new SearchNodesSort());

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Name/*'/>
        public static NameSort<SearchNodesSort> Name => new NameSort<SearchNodesSort>(new SearchNodesSort());

    }
}
