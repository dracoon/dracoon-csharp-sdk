namespace Dracoon.Sdk.Filter {
    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/GetNodesFilter/*'/>
    public class GetNodesFilter : DracoonFilter {

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/Type/*'/>
        public static NodeTypeFilter Type => new NodeTypeFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/Name/*'/>
        public static NameFilter Name => new NameFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/IsEncrypted/*'/>
        public static NodeIsEncryptedFilter IsEncrypted => new NodeIsEncryptedFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNodeTypeFilter/*'/>
        public void AddNodeTypeFilter(DracoonFilterType<NodeTypeFilter> typeFilter) {
            CheckFilter(typeFilter, nameof(typeFilter));
            filtersList.Add(typeFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNameFilter/*'/>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            filtersList.Add(nameFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getNodesFilter"]/AddNodeIsEncryptedFilter/*'/>
        public void AddNodeIsEncryptedFilter(DracoonFilterType<NodeIsEncryptedFilter> isEncryptedFilter) {
            CheckFilter(isEncryptedFilter, nameof(isEncryptedFilter));
            filtersList.Add(isEncryptedFilter);
        }
    }

}
