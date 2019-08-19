using System.Drawing;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iUsers"]/IUsers/*'/>
    public interface IUsers {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iUsers"]/GetUserAvatar/*'/>
        Image GetUserAvatar(long userId, string avatarUUID);
    }
}