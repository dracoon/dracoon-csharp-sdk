namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/UpdatedAtSort/*'/>
    public class UpdatedAtSort<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/UpdatedAtSortConstructor/*'/>
        public UpdatedAtSort(T p) : base(p) {
            Parent.SortString += "updatedAt";
        }
    }

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SizeSort/*'/>
    public class SizeSort<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SizeSortConstructor/*'/>
        public SizeSort(T p) : base(p) {
            Parent.SortString += "size";
        }
    }

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/ExpireAtSort/*'/>
    public class ExpireAtSort<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/ExpireAtSortConstructor/*'/>
        public ExpireAtSort(T p) : base(p) {
            Parent.SortString += "expireAt";
        }
    }

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/CreatedAtSort/*'/>
    public class CreatedAtSort<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/CreatedAtSortConstructor/*'/>
        public CreatedAtSort(T p) : base(p) {
            Parent.SortString += "createdAt";
        }
    }

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/NameSort/*'/>
    public class NameSort<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/NameSortConstructor/*'/>
        public NameSort(T p) : base(p) {
            Parent.SortString += "name";
        }
    }
}