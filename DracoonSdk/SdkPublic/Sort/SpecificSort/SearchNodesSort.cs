namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/SearchNodesSort/*'/>
    public class SearchNodesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/UpdatedAt/*'/>
        public static SortField<SearchNodesSort> UpdatedAt {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "updatedAt");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Size/*'/>
        public static SortField<SearchNodesSort> Size {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "size");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreatedAt/*'/>
        public static SortField<SearchNodesSort> CreatedAt {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "createdAd");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/Name/*'/>
        public static SortField<SearchNodesSort> Name {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "name");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/ModificationTimestamp/*'/>
        public static SortField<SearchNodesSort> ModificationTimestamp {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampModification");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="searchNodesSort"]/CreationTimestamp/*'/>
        public static SortField<SearchNodesSort> CreationTimestamp {
            get {
                return new SortField<SearchNodesSort>(new SearchNodesSort(), "timestampCreation");
            }
        }
    }
}