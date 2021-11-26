namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iUsers"]/IUsers/*'/>
    public interface IUsers {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iUsers"]/GetUserAvatar/*'/>
        byte[] GetUserAvatar(long userId, string avatarUuid);
    }
}