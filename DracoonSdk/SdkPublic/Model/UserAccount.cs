using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/UserAccount/*'/>
    public class UserAccount {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/Id/*'/>
        public long Id { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/LoginName/*'/>
        public string LoginName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/Title/*'/>
        public string Title { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/FirstName/*'/>
        public string FirstName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/LastName/*'/>
        public string LastName { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/Email/*'/>
        public string Email { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/HasEncryptionEnabled/*'/>
        public bool? HasEncryptionEnabled { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/HasManageableRooms/*'/>
        public bool HasManageableRooms { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/ExpireAt/*'/>
        public DateTime? ExpireAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/LastLoginSuccessAt/*'/>
        public DateTime? LastLoginSuccessAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/LastLoginFailAt/*'/>
        public DateTime? LastLoginFailAt { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/UserRoles/*'/>
        public List<UserRole> UserRoles { get; internal set; }

        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAccount"]/HomeRoomId/*'/>
        public long? HomeRoomId { get; internal set; }
    }
}