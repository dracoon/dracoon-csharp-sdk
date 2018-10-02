using Dracoon.Sdk.Model;

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
    }
}
