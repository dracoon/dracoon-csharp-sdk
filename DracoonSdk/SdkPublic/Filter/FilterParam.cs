namespace Dracoon.Sdk.Filter {
    /// <include file="FilterDoc.xml" path='docs/members[@name="filterParam"]/FilterParam/*'/>
    public class FilterParam<T, V> {

        internal T parent;
        internal V superclass;

        internal FilterParam(T parent, V superclass) {
            this.parent = parent;
            this.superclass = superclass;
        }

        /// <include file="FilterDoc.xml" path='docs/members[@name="filterParam"]/Build/*'/>    
        public V Build() {
            return superclass;
        }
    }

}
