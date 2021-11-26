using Dracoon.Sdk.SdkInternal.ApiModel;
using System;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryServer {

        internal static ApiServerVersion ApiServerVersionMock => new ApiServerVersion {
            ServerVersion = "4.13.0",
            BuildDate = new DateTime(2000, 1, 1, 0, 0, 0),
            RestApiVersion = "4.13.0"
        };

        internal static ApiServerTime ApiServerTimeMock => new ApiServerTime {
            Time = new DateTime(2000, 1, 1, 0, 0, 0)
        };


    }
}
