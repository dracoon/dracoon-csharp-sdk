namespace Dracoon.Sdk.Filter {
    /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/GetUploadSharesFilter/*'/>
    public class GetUploadSharesFilter : DracoonFilter {

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/Name/*'/>
        public static NameFilter Name => new NameFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/UserId/*'/>
        public static UserIdFilter UserId => new UserIdFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/CreatedBy/*'/>
        public static CreatedByFilter CreatedBy => new CreatedByFilter();

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/NodeId/*'/>
        public static NodeIdFilter NodeId => new NodeIdFilter("targetId");

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/AddNameFilter/*'/>
        public void AddNameFilter(DracoonFilterType<NameFilter> nameFilter) {
            CheckFilter(nameFilter, nameof(nameFilter));
            filtersList.Add(nameFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/AddUserIdFilter/*'/>
        public void AddUserIdFilter(DracoonFilterType<UserIdFilter> userIdFilter) {
            CheckFilter(userIdFilter, nameof(userIdFilter));
            filtersList.Add(userIdFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/AddCreatedByFilter/*'/>
        public void AddCreatedByFilter(DracoonFilterType<CreatedByFilter> createdByFilter) {
            CheckFilter(createdByFilter, nameof(createdByFilter));
            filtersList.Add(createdByFilter);
        }

        /// <include file="SpecificFilterDoc.xml" path='docs/members[@name="getUploadSharesFilter"]/AddNodeIdFilter/*'/>
        public void AddNodeIdFilter(DracoonFilterType<NodeIdFilter> nodeIdFilter) {
            CheckFilter(nodeIdFilter, nameof(nodeIdFilter));
            filtersList.Add(nodeIdFilter);
        }
    }
}