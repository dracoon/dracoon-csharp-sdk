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

        /// <summary>
        ///     Get a list of download share ids where the user has subscriptions on.
        /// </summary>
        /// <param name="offset">
        ///     The range offset. (Zero-based index; must be 0 or positive if set)
        /// </param>
        /// <param name="limit">
        ///     The range limit. (Number of returned records; must be positive if set)
        /// </param>
        /// <returns>
        ///     The list of the download share subscriptions.
        /// </returns>
        ShareSubscriptionList GetDownloadShareSubscriptions(long? offset = null, long? limit = null);

        /// <summary>
        ///     Remove the subscription on a download share.
        /// </summary>
        /// <param name="shareId">
        ///     The id of the download share where the subscription should be removed.
        /// </param>
        void RemoveDownloadShareSubscription(long shareId);

        /// <summary>
        ///     Add a subscription on a download share.
        /// </summary>
        /// <param name="shareId">
        ///     The id of the download share where the subscription should be added.
        /// </param>
        ShareSubscription AddDownloadShareSubscription(long shareId);

        /// <summary>
        ///     Get a list of upload share ids where the user has subscriptions on.
        /// </summary>
        /// <param name="offset">
        ///     The range offset. (Zero-based index; must be 0 or positive if set)
        /// </param>
        /// <param name="limit">
        ///     The range limit. (Number of returned records; must be positive if set)
        /// </param>
        /// <returns>
        ///     The list of the upload share subscriptions.
        /// </returns>
        ShareSubscriptionList GetUploadShareSubscriptions(long? offset = null, long? limit = null);

        /// <summary>
        ///     Remove the subscription on a upload share.
        /// </summary>
        /// <param name="shareId">
        ///     The id of the upload share where the subscription should be added.
        /// </param>
        void RemoveUploadShareSubscription(long shareId);

        /// <summary>
        ///     Add a subscription on a upload share.
        /// </summary>
        /// <param name="shareId">
        ///     The id of the upload share where the subscription should be added.
        /// </param>
        ShareSubscription AddUploadShareSubscription(long shareId);
    }
}
