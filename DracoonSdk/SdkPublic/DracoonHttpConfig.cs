using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;

namespace Dracoon.Sdk {
    /// <summary>
    ///     <para>
    ///         DracoonHttpConfig is used to configure HTTP communication options.
    ///     </para>
    ///     <list type="bullet">
    ///         <listheader>
    ///             <description>Following options can be configured:</description>
    ///         </listheader>
    ///         <item>
    ///             <description><see cref="RetryEnabled"/></description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="Timeout"/></description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="WebProxy"/></description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="UserAgent"/></description>
    ///         </item>
    ///         <item>
    ///             <description><see cref="ChunkSize"/></description>
    ///         </item>
    ///    </list>
    /// </summary>
    public class DracoonHttpConfig {
        /// <summary>
        ///     Enables/Disables auto retry on failed request (up to 3 tries).
        ///     <para>
        ///         (Default: <c>false</c>)
        ///     </para>
        /// </summary>
        public bool RetryEnabled { get; set; }

        /// <summary>
        ///     The HTTP timeout in milliseconds.
        ///     <para>
        ///         (Default: <c>15000</c>)
        ///     </para>
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     The HTTP proxy settings.
        /// </summary>
        public IWebProxy WebProxy { get; set; }

        /// <summary>
        ///     The User-Agent string.
        ///     <para>
        ///         (Default: <c>CSharp-SDK|[Version]|[EnvironmentOS]|-|-</c>)
        ///     </para>
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        ///     The chunk size for upload/download in KiB.
        ///     <para>
        ///         (Default: 2048 KiB)
        ///     </para>
        /// </summary>
        public int ChunkSize { get; set; }

        /// <summary>
        ///     Constructs a HTTP configuration.
        /// </summary>
        /// <param name="retryEnabled"><see cref="RetryEnabled"/></param>
        /// <param name="timeout"><see cref="Timeout"/></param>
        /// <param name="webProxy"><see cref="WebProxy"/></param>
        /// <param name="ownUserAgent"><see cref="UserAgent"/></param>
        /// <param name="chunkSize"><see cref="ChunkSize"/></param>
        public DracoonHttpConfig(bool retryEnabled = false, int timeout = 15000, IWebProxy webProxy = null,
            string ownUserAgent = null, int chunkSize = 2048) {
            RetryEnabled = retryEnabled;
            Timeout = timeout;
            WebProxy = webProxy;
            UserAgent = ownUserAgent ?? BuildDefaultUserAgent();
            ChunkSize = chunkSize * 1024;
        }

        private static string BuildDefaultUserAgent() {
            AssemblyName assembly = Assembly.GetExecutingAssembly().GetName();
            return "CSharp-SDK|" + assembly.Version.Major + "." + assembly.Version.Minor + "." + assembly.Version.Revision + "|" +
                   Environment.OSVersion + "|-|-";
        }
    }
}