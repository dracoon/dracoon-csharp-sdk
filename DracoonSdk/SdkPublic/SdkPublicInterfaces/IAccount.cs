using System.Collections.Generic;
using Dracoon.Sdk.Model;
using System.Drawing;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/IAccount/*'/>
    public interface IAccount {

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserAccount/*'/>
        UserAccount GetUserAccount();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetCustomerAccount/*'/>
        CustomerAccount GetCustomerAccount();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/SetUserKeyPair/*'/>
        void SetUserKeyPair();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/CheckUserKeyPairPassword/*'/>
        bool CheckUserKeyPairPassword();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/DeleteUserKeyPair/*'/>
        void DeleteUserKeyPair();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/ValidateTokenValidity/*'/>
        void ValidateTokenValidity();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetAvatar/*'/>
        Image GetAvatar();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetAvatarInfo/*'/>
        AvatarInfo GetAvatarInfo();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/ResetAvatar/*'/>
        AvatarInfo ResetAvatar();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/UpdateAvatar/*'/>
        AvatarInfo UpdateAvatar(Image newAvatar);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserProfileAttributeList/*'/>
        AttributeList GetUserProfileAttributeList();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserProfileAttribute/*'/>
        Attribute GetUserProfileAttribute(string attributeKey);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/AddOrUpdateUserProfileAttributes/*'/>
        void AddOrUpdateUserProfileAttributes(List<Attribute> attributes);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/DeleteProfileAttribute/*'/>
        void DeleteProfileAttribute(string attributeKey);
    }
}
