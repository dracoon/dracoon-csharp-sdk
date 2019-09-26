namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateFolderRequest"]/UpdateFolderRequest/*'/>
    public class UpdateFolderRequest {
        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateFolderRequest"]/Id/*'/>
        public long Id { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateFolderRequest"]/Name/*'/>
        public string Name { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateFolderRequest"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateFolderRequest"]/UpdateFolderRequestConstructor/*'/>
        public UpdateFolderRequest(long id, string name = null, string notes = null) {
            Id = id;
            Name = name;
            Notes = notes;
        }
    }
}