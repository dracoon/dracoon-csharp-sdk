using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Model;
using System.Collections.Generic;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/IAccount/*'/>
    public interface IAccount {

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserAccount/*'/>
        UserAccount GetUserAccount();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetCustomerAccount/*'/>
        CustomerAccount GetCustomerAccount();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/SetUserKeyPair/*'/>
        void SetUserKeyPair(UserKeyPairAlgorithm algorithm);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/CheckUserKeyPairPassword/*'/>
        bool CheckUserKeyPairPassword(UserKeyPairAlgorithm algorithm);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/DeleteUserKeyPair/*'/>
        void DeleteUserKeyPair(UserKeyPairAlgorithm algorithm);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/ValidateTokenValidity/*'/>
        void ValidateTokenValidity();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetAvatar/*'/>
        byte[] GetAvatar();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetAvatarInfo/*'/>
        AvatarInfo GetAvatarInfo();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/ResetAvatar/*'/>
        AvatarInfo ResetAvatar();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/UpdateAvatar/*'/>
        AvatarInfo UpdateAvatar(byte[] newAvatar);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserProfileAttributeList/*'/>
        AttributeList GetUserProfileAttributeList();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserProfileAttribute/*'/>
        Attribute GetUserProfileAttribute(string attributeKey);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/AddOrUpdateUserProfileAttributes/*'/>
        void AddOrUpdateUserProfileAttributes(List<Attribute> attributes);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/DeleteProfileAttribute/*'/>
        void DeleteProfileAttribute(string attributeKey);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iAccount"]/GetUserKeyPairAlgorithms/*'/>
        List<UserKeyPairAlgorithm> GetUserKeyPairAlgorithms();
    }
}
