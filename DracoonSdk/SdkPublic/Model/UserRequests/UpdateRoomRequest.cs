namespace Dracoon.Sdk.Model {
    /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/UpdateRoomRequest/*'/>
    public class UpdateRoomRequest {
        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/Id/*'/>
        public long Id { get; private set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/Name/*'/>
        public string Name { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/Quota/*'/>
        public long? Quota { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/Notes/*'/>
        public string Notes { get; set; }

        /// <include file = "UserRequestsDoc.xml" path='docs/members[@name="updateRoomRequest"]/UpdateRoomRequestConstructor/*'/>
        public UpdateRoomRequest(long id, string name = null, long? quota = null, string notes = null) {
            Id = id;
            Name = name;
            Quota = quota;
            Notes = notes;
        }
    }
}