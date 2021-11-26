using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ILog/*'/>
    public interface ILog {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/DebugOne/*'/>
        void Debug(string tag, string message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/DebugTwo/*'/>
        void Debug(string tag, string message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/InfoOne/*'/>
        void Info(string tag, string message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/InfoTwo/*'/>
        void Info(string tag, string message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/WarnOne/*'/>
        void Warn(string tag, string message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/WarnTwo/*'/>
        void Warn(string tag, string message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ErrorOne/*'/>
        void Error(string tag, string message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ErrorTwo/*'/>
        void Error(string tag, string message, Exception e);
    }
}