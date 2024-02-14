using System;
using System.Net;
using System.Reflection;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/DracoonHttpConfig/*'/>
    public class DracoonHttpConfig {
        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/RetryEnabled/*'/>
        public bool RetryEnabled { get; set; }

        /// <summary>
        ///     The HTTP timeout in milliseconds.
        ///     <para>
        ///         (Default: <c>15000</c>)
        ///     </para>
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        ///     The HTTP connection timeout in milliseconds.
        ///     <para>
        ///         (Default: <c>15000</c>)
        ///     </para>
        /// </summary>
        public int ConnectionTimeout { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/WebProxy/*'/>
        public IWebProxy WebProxy { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/UserAgent/*'/>
        public string UserAgent { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/ChunkSize/*'/>
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