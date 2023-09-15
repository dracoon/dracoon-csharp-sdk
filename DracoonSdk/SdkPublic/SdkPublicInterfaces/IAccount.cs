using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Model;
using System.Collections.Generic;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to query and update the user account.
    /// </summary>
    public interface IAccount {

        /// <summary>
        ///     Retrieves user account information.
        /// </summary>
        /// <returns>User account information. See also <seealso cref="Dracoon.Sdk.Model.UserAccount"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        UserAccount GetUserAccount();

        /// <summary>
        ///     Retrieves customer account information.
        /// </summary>
        /// <returns>Customer account information. See also <seealso cref="Dracoon.Sdk.Model.CustomerAccount"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        CustomerAccount GetCustomerAccount();

        /// <summary>
        ///      Sets the user's encryption key pair.
        /// </summary>
        /// <param name="algorithm">The algorithm for which a key pair should be set.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        void SetUserKeyPair(UserKeyPairAlgorithm algorithm);

        /// <summary>
        ///     Checks if the user's encryption key pair can be unlocked with the provided encryption password.
        /// </summary>
        /// <param name="algorithm">The algorithm for which the password should be checked.</param>
        /// <returns><c>true</c> if key pair could be unlocked, <c>false</c> otherwise</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        bool CheckUserKeyPairPassword(UserKeyPairAlgorithm algorithm);

        /// <summary>
        ///     Deletes the user's encryption key pair.
        /// </summary>
        /// <param name="algorithm">The algorithm definition for which the key pair should be deleted.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        void DeleteUserKeyPair(UserKeyPairAlgorithm algorithm);

        /// <summary>
        ///     Checks if the current token is still valid.
        /// </summary>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        void ValidateTokenValidity();

        /// <summary>
        ///     Get the avatar image for the current user.
        /// </summary>
        /// <returns>The avatar image.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        byte[] GetAvatar();

        /// <summary>
        ///     Get the informations about the current set avatar image of the current user.
        /// </summary>
        /// <returns>The avatar image informations. See also <seealso cref="Dracoon.Sdk.Model.AvatarInfo"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        AvatarInfo GetAvatarInfo();

        /// <summary>
        ///     Delete (Reset to default) the avatar of the current user.
        /// </summary>
        /// <returns>The new default (on server generated) avatar image informations. See also <seealso cref="Dracoon.Sdk.Model.AvatarInfo"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        AvatarInfo ResetAvatar();

        /// <summary>
        ///     Update the avatar image of the current user.
        /// </summary>
        /// <param name="newAvatar">The image bytes for the new avatar.</param>
        /// <returns>The new avatar image informations. See also <seealso cref="Dracoon.Sdk.Model.AvatarInfo"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        AvatarInfo UpdateAvatar(byte[] newAvatar);

        /// <summary>
        ///     Get the custom attributes for the current user.
        /// </summary>
        /// <returns>List of custom attributes. See also <seealso cref="Dracoon.Sdk.Model.AttributeList"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        AttributeList GetUserProfileAttributeList();

        /// <summary>
        ///     Get a custom attribute for the current user.
        /// </summary>
        /// <param name="attributeKey">The key of the attribute. (attributeKey must be not null or empty or whitespace)</param>
        /// <returns>The custom attribute for the current user given by the attribute key. See also <seealso cref="Dracoon.Sdk.Model.Attribute"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Attribute GetUserProfileAttribute(string attributeKey);

        /// <summary>
        ///     Add a new custom attribute for the current user or update an existing attribute with the same key.
        /// </summary>
        /// <param name="attributes">The attributes which should be added or updated.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void AddOrUpdateUserProfileAttributes(List<Attribute> attributes);

        /// <summary>
        ///     Delete a custom attribute with the given key.
        /// </summary>
        /// <param name="attributeKey">The key of the attribute which should be deleted.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DeleteProfileAttribute(string attributeKey);

        /// <summary>
        ///     Get the used crypto algorithms of the user.
        /// </summary>
        /// <returns>List of crypto algorithms for the user key pair.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        List<UserKeyPairAlgorithm> GetUserKeyPairAlgorithms();

        /// <summary>
        ///     Get a list of download share ids where the user has subscriptions on.
        /// </summary>
        /// <param name="offset"> The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <returns>The list of the download share subscriptions.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        ShareSubscriptionList GetDownloadShareSubscriptions(long? offset = null, long? limit = null);

        /// <summary>
        ///     Remove the subscription on a download share.
        /// </summary>
        /// <param name="shareId">The id of the download share where the subscription should be removed.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        void RemoveDownloadShareSubscription(long shareId);

        /// <summary>
        ///     Add a subscription on a download share.
        /// </summary>
        /// <param name="shareId">The id of the download share where the subscription should be added.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        ShareSubscription AddDownloadShareSubscription(long shareId);

        /// <summary>
        ///     Get a list of upload share ids where the user has subscriptions on.
        /// </summary>
        /// <param name="offset">The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <returns>The list of the upload share subscriptions.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        ShareSubscriptionList GetUploadShareSubscriptions(long? offset = null, long? limit = null);

        /// <summary>
        ///     Remove the subscription on a upload share.
        /// </summary>
        /// <param name="shareId">The id of the upload share where the subscription should be added.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        void RemoveUploadShareSubscription(long shareId);

        /// <summary>
        ///     Add a subscription on a upload share.
        /// </summary>
        /// <param name="shareId">The id of the upload share where the subscription should be added.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        ShareSubscription AddUploadShareSubscription(long shareId);
    }
}
