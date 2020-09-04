using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServer"]/IServer/*'/>
    public interface IServer {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServer"]/GetVersion/*'/>
        string GetVersion();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServer"]/GetTime/*'/>
        DateTime? GetTime();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServer"]/ServerSettings/*'/>
        IServerSettings ServerSettings { get; set; }

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServer"]/ServerPolicies/*'/>
        IServerPolicies ServerPolicies { get; set; }
    }
}