namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/UserInfo/*'/>
    public class UserInfo {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/Id/*'/>
        public long? Id { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/DisplayName/*'/>
        public string DisplayName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/AvatarUUID/*'/>
        public string AvatarUUID { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/FirstName/*'/>
        public string FirstName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/LastName/*'/>
        public string LastName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/Email/*'/>
        public string Email { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/Title/*'/>
        public string Title { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userInfo"]/UserType/*'/>
        public UserType UserType { get; internal set; }
    }
}