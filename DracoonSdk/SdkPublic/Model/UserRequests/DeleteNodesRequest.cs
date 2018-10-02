using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="deleteNodesRequest"]/DeleteNodesRequest/*'/>
    public class DeleteNodesRequest {

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="deleteNodesRequest"]/Ids/*'/>
        public List<long> Ids {
            get; private set;
        }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="deleteNodesRequest"]/DeleteNodesRequestConstructor/*'/>
        public DeleteNodesRequest(List<long> ids) {
            Ids = ids;
        }
    }
}
