using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="restorePreviousVersionsRequest"]/RestorePreviousVersionsRequest/*'/>
    public class RestorePreviousVersionsRequest {

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="restorePreviousVersionsRequest"]/NewParentNodeId/*'/>
        public long? NewParentNodeId {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="restorePreviousVersionsRequest"]/RestoreVersionIds/*'/>
        public List<long> RestoreVersionIds {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="restorePreviousVersionsRequest"]/ResolutionStrategy/*'/>
        public ResolutionStrategy ResolutionStrategy {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="restorePreviousVersionsRequest"]/KeepShareLinks/*'/>
        public bool KeepShareLinks {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="copyNodesRequest"]/RestorePreviousVersionsRequestConstructor/*'/>
        public RestorePreviousVersionsRequest(List<long> restoreVersionIds, ResolutionStrategy resolutionStrategy = ResolutionStrategy.AutoRename, bool keepShareLinks = false, long? newParentNodeId = null) {
            RestoreVersionIds = restoreVersionIds;
            ResolutionStrategy = resolutionStrategy;
            NewParentNodeId = newParentNodeId;
            KeepShareLinks = keepShareLinks;
        }

    }
}
