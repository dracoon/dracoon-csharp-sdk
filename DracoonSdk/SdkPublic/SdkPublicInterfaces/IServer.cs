using System;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to query server informations.
    /// </summary>
    public interface IServer {
        /// <summary>
        ///     Retrieves the server's version.
        /// </summary>
        /// <returns>The server version.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        string GetVersion();

        /// <summary>
        ///     Retrieves the server's time.
        /// </summary>
        /// <returns>The server time.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        DateTime? GetTime();

        /// <summary>
        ///     Handler to query server configuration informations.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.IServerSettings"/>
        ///     </para>
        /// </summary>
        IServerSettings ServerSettings { get; }

        /// <summary>
        ///         Handler to query server policy informations.
        ///     <para>
        ///         See also <seealso cref="Dracoon.Sdk.IServerPolicies"/>
        ///     </para>
        /// </summary>
        IServerPolicies ServerPolicies { get; }
    }
}