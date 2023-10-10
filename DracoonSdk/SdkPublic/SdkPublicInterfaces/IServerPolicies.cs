using Dracoon.Sdk.Model;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to query policy informations.
    /// </summary>
    public interface IServerPolicies {

        /// <summary>
        ///     Retrieves the password policies for encryption passwords used for encrypting file keys.
        /// </summary>
        /// <returns>The encryption password policies.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        PasswordEncryptionPolicies GetEncryptionPasswordPolicies();

        /// <summary>
        ///     Retrieves the password policies for creating shares.
        /// </summary>
        /// <returns>The share password policies.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        PasswordSharePolicies GetSharesPasswordPolicies();

        /// <summary>
        ///     Retrieves the classification policies.
        /// </summary>
        /// <returns>The classification policies.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        ClassificationPolicies GetClassificationPolicies();
    }
}
