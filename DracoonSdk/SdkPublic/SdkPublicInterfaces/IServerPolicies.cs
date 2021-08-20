using Dracoon.Sdk.Model;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerPolicies"]/IServerPolicies/*'/>
    public interface IServerPolicies {

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerPolicies"]/GetEncryptionPasswordPolicies/*'/>
        PasswordEncryptionPolicies GetEncryptionPasswordPolicies();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerPolicies"]/GetSharesPasswordPolicies/*'/>
        PasswordSharePolicies GetSharesPasswordPolicies();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerPolicies"]/GetClassificationPolicies/*'/>
        ClassificationPolicies GetClassificationPolicies();
    }
}
