namespace Dracoon.Sdk.Sort {
    /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/DracoonSortOrder/*'/>
    public class DracoonSortOrder<T> where T : DracoonSort {
        internal T Parent;

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/DracoonSortOrderConstructor/*'/>
        public DracoonSortOrder(T parent) {
            Parent = parent;
        }

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/Ascending/*'/>
        public T Ascending() {
            Parent.SortString += ":asc";
            return Parent;
        }

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/Descending/*'/>
        public T Descending() {
            Parent.SortString += ":desc";
            return Parent;
        }
    }
}