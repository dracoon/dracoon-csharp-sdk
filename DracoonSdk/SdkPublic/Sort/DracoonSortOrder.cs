namespace Dracoon.Sdk.Sort {
    /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/DracoonSortOrder/*'/>
    public class DracoonSortOrder<T> where T : DracoonSort {

        internal T parent;

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/DracoonSortOrderConstructor/*'/>
        public DracoonSortOrder(T parent) {
            this.parent = parent;
        }

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/Ascending/*'/>
        public T Ascending() {
            parent.sortString += ":asc";
            return parent;
        }

        /// <include file="Sort.xml" path='docs/members[@name="dracoonSortOrder"]/Descending/*'/>
        public T Descending() {
            parent.sortString += ":desc";
            return parent;
        }

    }
}
