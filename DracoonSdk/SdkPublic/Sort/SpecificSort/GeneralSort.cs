namespace Dracoon.Sdk.Sort {

    /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SortField/*'/>
    public class SortField<T> : DracoonSortOrder<T> where T : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="generalSort"]/SortFieldConstructor/*'/>
        public SortField(T p, string sortField) : base(p) {
            Parent.SortString += sortField;
        }
    }
}