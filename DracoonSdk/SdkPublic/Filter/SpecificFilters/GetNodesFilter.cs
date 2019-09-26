namespace Dracoon.Sdk.Filter {
    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/GetNodesFilter/*'/>
    public class GetNodesFilter : DracoonFilter {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/Type/*'/>
        public static NodeTypeFilter Type {
            get {
                return new NodeTypeFilter();
            }
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/Name/*'/>
        public static NameFilter Name {
            get {
                return new NameFilter();
            }
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/IsEncrypted/*'/>
        public static NodeIsEncryptedFilter IsEncrypted {
            get {
                return new NodeIsEncryptedFilter();
            }
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNodeTypeFilter/*'/>
        public void AddNodeTypeFilter(DracoonFilterType<NodeTypeFilter> typeFilter) {
            CheckFilter(typeFilter, nameof(typeFilter));
            FiltersList.Add(typeFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNameFilter/*'/>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            FiltersList.Add(nameFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNodeIsEncryptedFilter/*'/>
        public void AddNodeIsEncryptedFilter(DracoonFilterType<NodeIsEncryptedFilter> isEncryptedFilter) {
            CheckFilter(isEncryptedFilter, nameof(isEncryptedFilter));
            FiltersList.Add(isEncryptedFilter);
        }
    }
}