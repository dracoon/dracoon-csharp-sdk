using System;
using System.Net;
using System.Reflection;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/DracoonHttpConfig/*'/>
    public class DracoonHttpConfig {
        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/RetryEnabled/*'/>
        public bool RetryEnabled { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/ReadWriteTimeout/*'/>
        public int ReadWriteTimeout { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/ConnectionTimeout/*'/>
        public int ConnectionTimeout { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/WebProxy/*'/>
        public IWebProxy WebProxy { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/UserAgent/*'/>
        public string UserAgent { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/ChunkSize/*'/>
        public int ChunkSize { get; set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonHttpConfig"]/DracoonHttpConfigConstructor/*'/>
        public DracoonHttpConfig(bool retryEnabled = false, int readWriteTimeout = 15000, int connectionTimeout = 15000, IWebProxy webProxy = null,
            string ownUserAgent = null, int chunkSize = 2048) {
            RetryEnabled = retryEnabled;
            ReadWriteTimeout = readWriteTimeout;
            ConnectionTimeout = connectionTimeout;
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