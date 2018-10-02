using System;

namespace Dracoon.Sdk.Model {
    /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/UpdateFileRequest/*'/>
    public class UpdateFileRequest {

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/Id/*'/>
        public long Id {
            get; private set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/Name/*'/>
        public string Name {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/Classification/*'/>
        public Classification? Classification {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/Notes/*'/>
        public string Notes {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/Expiration/*'/>
        public DateTime? Expiration {
            get; set;
        }

        /// <include file="UserRequestsDoc.xml" path='docs/members[@name="updateFileRequest"]/UpdateFileRequestConstructor/*'/>
        public UpdateFileRequest(long id, string name = null, Classification? classification = null, string notes = null, DateTime? expiration = null) {
            Id = id;
            Name = name;
            Classification = classification;
            Notes = notes;
            Expiration = expiration;
        }
    }
}
