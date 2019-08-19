using System;
using Dracoon.Sdk.SdkInternal.OAuth;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IInternalDracoonClient {
        string EncryptionPassword { get; set; }
        Uri ServerUri { get; }
        IRequestBuilder Builder { get; }
        IRequestExecutor Executor { get; }
        IOAuth OAuth { get; }

        DracoonAccountImpl AccountImpl { get; }
        DracoonNodesImpl NodesImpl { get; }
        DracoonSharesImpl SharesImpl { get; }
        DracoonServerImpl ServerImpl { get; }
        DracoonUsersImpl UsersImpl { get; }
    }
}