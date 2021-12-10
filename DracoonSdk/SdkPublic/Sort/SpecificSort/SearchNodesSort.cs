namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/SearchNodesSort/*'/>
    public class SearchNodesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/UpdatedAt/*'/>
        public static SortField<SearchNodesSort> UpdatedAt => new SortField<SearchNodesSort>(new SearchNodesSort(), "updatedAt");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Size/*'/>
        public static SortField<SearchNodesSort> Size => new SortField<SearchNodesSort>(new SearchNodesSort(), "size");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreatedAt/*'/>
        public static SortField<SearchNodesSort> CreatedAt => new SortField<SearchNodesSort>(new SearchNodesSort(), "createdAt");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Name/*'/>
        public static SortField<SearchNodesSort> Name => new SortField<SearchNodesSort>(new SearchNodesSort(), "name");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/ModificationTimestamp/*'/>
        public static SortField<SearchNodesSort> ModificationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampModification");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreationTimestamp/*'/>
        public static SortField<SearchNodesSort> CreationTimestamp => new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampCreation");
    }
}