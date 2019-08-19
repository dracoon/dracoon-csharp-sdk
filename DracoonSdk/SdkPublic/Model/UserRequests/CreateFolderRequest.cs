namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="createFolderRequest"]/CreateFolderRequest/*'/>
    public class CreateFolderRequest {
        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="createFolderRequest"]/ParentId/*'/>
        public long ParentId { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="createFolderRequest"]/Name/*'/>
        public string Name { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="createFolderRequest"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="createFolderRequest"]/CreateFolderRequest/*'/>
        public CreateFolderRequest(long parentId, string name, string notes = null) {
            ParentId = parentId;
            Name = name;
            Notes = notes;
        }
    }
}