using Dracoon.Sdk.Model;
using System.Collections.Generic;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to query server settings information.
    /// </summary>
    public interface IServerSettings {

        /// <summary>
        ///     Retrieves the server's general settings.
        /// </summary>
        /// <returns>The server general settings. See also <seealso cref="Dracoon.Sdk.Model.ServerGeneralSettings"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        ServerGeneralSettings GetGeneral();

        /// <summary>
        ///     Retrieves the server's infrastructure settings.
        /// </summary>
        /// <returns>The server infrastructure settings. See also <seealso cref="Dracoon.Sdk.Model.ServerInfrastructureSettings"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        ServerInfrastructureSettings GetInfrastructure();

        /// <summary>
        ///     Retrieves the server's default settings (a server defined default for e.g. the expiration period of a download share if the share expires, <see cref="Dracoon.Sdk.Model.CreateDownloadShareRequest.Expiration"/>).
        /// </summary>
        /// <returns>The server default settings. See also <seealso cref="Dracoon.Sdk.Model.ServerDefaultSettings"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        ServerDefaultSettings GetDefault();

        /// <summary>
        ///     Get a list of crypto algorithms for the user key pair which are supported by the server.
        /// </summary>
        /// <returns>List of key pair algorithm data.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        List<UserKeyPairAlgorithmData> GetAvailableUserKeyPairAlgorithms();

        /// <summary>
        ///     Get a list of crypto algorithms for the file key which are supported by the server.
        /// </summary>
        /// <returns>List of file key algorithm data.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        List<FileKeyAlgorithmData> GetAvailableFileKeyAlgorithms();
    }
}