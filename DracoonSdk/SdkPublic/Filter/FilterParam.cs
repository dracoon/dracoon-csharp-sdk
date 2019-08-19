namespace Dracoon.Sdk.Filter {
    /// <include file="FilterDoc.xml" path='docs/members[@name="filterParam"]/FilterParam/*'/>
    public class FilterParam<T, TV> {
        internal T Parent;
        internal TV Superclass;

        internal FilterParam(T parent, TV superclass) {
            Parent = parent;
            Superclass = superclass;
        }

        /// <include file="FilterDoc.xml" path='docs/members[@name="filterParam"]/Build/*'/>    
        public TV Build() {
            return Superclass;
        }
    }
}