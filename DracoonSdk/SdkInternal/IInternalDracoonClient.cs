using Dracoon.Sdk.SdkInternal.OAuth;
using System;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IInternalDracoonClient {
        byte[] EncryptionPassword { get; set; }
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