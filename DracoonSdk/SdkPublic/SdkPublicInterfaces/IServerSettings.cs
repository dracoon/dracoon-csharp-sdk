﻿using Dracoon.Sdk.Model;
using System.Collections.Generic;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/IServerSettings/*'/>
    public interface IServerSettings {

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/GetGeneral/*'/>
        ServerGeneralSettings GetGeneral();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/GetInfrastructure/*'/>
        ServerInfrastructureSettings GetInfrastructure();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/GetDefault/*'/>
        ServerDefaultSettings GetDefault();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/GetAvailableUserKeyPairAlgorithms/*'/>
        List<UserKeyPairAlgorithmData> GetAvailableUserKeyPairAlgorithms();

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iServerSettings"]/GetAvailableFileKeyAlgorithms/*'/>
        List<FileKeyAlgorithmData> GetAvailableFileKeyAlgorithms();
    }
}