namespace Dracoon.Sdk.Model {
    /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/UserAuthData/*'/>
    public class UserAuthData {
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/Method/*'/>
        public UserAuthMethod Method { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/Login/*'/>
        public string Login { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/Password/*'/>
        public string Password { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/Login/*'/>
        public bool? MustChangePassword { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/ADConfigId/*'/>
        public int? ADConfigId { get; internal set; }
        /// <include file = "ModelDoc.xml" path='docs/members[@name="userAuthData"]/OIDConfigId/*'/>
        public int? OIDConfigId { get; internal set; }
    }
}
