namespace Dracoon.Sdk.Filter {
    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/GetDownloadSharesFilter/*'/>
    public class GetDownloadSharesFilter : DracoonFilter {
        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/UserId/*'/>
        public static UserIdFilter UserId => new UserIdFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/Name/*'/>
        public static NameFilter Name => new NameFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/CreatedBy/*'/>
        public static CreatedByFilter CreatedBy => new CreatedByFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/NodeId/*'/>
        public static NodeIdFilter NodeId => new NodeIdFilter("nodeId");

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/AddUserIdFilter/*'/>
        public void AddUserIdFilter(DracoonFilterType<UserIdFilter> userIdFilter) {
            CheckFilter(userIdFilter, nameof(userIdFilter));
            FiltersList.Add(userIdFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/AddNameFilter/*'/>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            FiltersList.Add(nameFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/AddCreatedByFilter/*'/>
        public void AddCreatedByFilter(DracoonFilterType<CreatedByFilter> createdByFilter) {
            CheckFilter(createdByFilter, nameof(createdByFilter));
            FiltersList.Add(createdByFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getDownloadSharesFilter"]/AddNodeIdFilter/*'/>
        public void AddNodeIdFilter(DracoonFilterType<NodeIdFilter> nodeIdFilter) {
            CheckFilter(nodeIdFilter, nameof(nodeIdFilter));
            FiltersList.Add(nodeIdFilter);
        }
    }
}