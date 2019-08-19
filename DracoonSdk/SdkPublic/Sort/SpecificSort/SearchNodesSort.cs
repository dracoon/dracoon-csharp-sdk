namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/SearchNodesSort/*'/>
    public class SearchNodesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/UpdatedAt/*'/>
        public static UpdatedAtSort<SearchNodesSort> UpdatedAt {
            get {
                return new UpdatedAtSort<SearchNodesSort>(new SearchNodesSort());
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Size/*'/>
        public static SizeSort<SearchNodesSort> Size {
            get {
                return new SizeSort<SearchNodesSort>(new SearchNodesSort());
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreatedAt/*'/>
        public static CreatedAtSort<SearchNodesSort> CreatedAt {
            get {
                return new CreatedAtSort<SearchNodesSort>(new SearchNodesSort());
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Name/*'/>
        public static NameSort<SearchNodesSort> Name {
            get {
                return new NameSort<SearchNodesSort>(new SearchNodesSort());
            }
        }
    }
}