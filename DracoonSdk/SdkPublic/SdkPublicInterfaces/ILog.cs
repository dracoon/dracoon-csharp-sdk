using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ILog/*'/>
    public interface ILog {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/DebugOne/*'/>
        void Debug(String tag, String message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/DebugTwo/*'/>
        void Debug(String tag, String message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/InfoOne/*'/>
        void Info(String tag, String message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/InfoTwo/*'/>
        void Info(String tag, String message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/WarnOne/*'/>
        void Warn(String tag, String message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/WarnTwo/*'/>
        void Warn(String tag, String message, Exception e);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ErrorOne/*'/>
        void Error(String tag, String message);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iLog"]/ErrorTwo/*'/>
        void Error(String tag, String message, Exception e);
    }
}