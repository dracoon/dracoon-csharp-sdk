namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/UpdatedAtSort/*'/>
    public class UpdatedAtSort<T> : DracoonSortOrder<T> where T : DracoonSort {

        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/UpdatedAtSortConstructor/*'/>
        public UpdatedAtSort(T p) : base(p) {
            parent.sortString += "updatedAt";
        }
    }

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SizeSort/*'/>
    public class SizeSort<T> : DracoonSortOrder<T> where T : DracoonSort {

        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SizeSortConstructor/*'/>
        public SizeSort(T p) : base(p) {
            parent.sortString += "size";
        }
    }

}
