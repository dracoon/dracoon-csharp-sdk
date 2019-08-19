using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="deletePreviousVersionsRequest"]/DeletePreviousVersionsRequest/*'/>
    public class DeletePreviousVersionsRequest {
        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="deletePreviousVersionsRequest"]/VersionIds/*'/>
        public List<long> VersionIds { get; private set; }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="deletePreviousVersionsRequest"]/DeletePreviousVersionsRequestConstructor/*'/>
        public DeletePreviousVersionsRequest(List<long> versionIds) {
            VersionIds = versionIds;
        }
    }
}