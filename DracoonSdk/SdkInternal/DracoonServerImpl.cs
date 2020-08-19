using Dracoon.Sdk.SdkInternal.ApiModel;
using RestSharp;
using System;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonServerImpl : IServer {
        internal const string Logtag = nameof(DracoonServerImpl);
        private readonly IInternalDracoonClient _client;

        public IServerSettings ServerSettings { get; }

        internal DracoonServerImpl(IInternalDracoonClient client) {
            _client = client;
            ServerSettings = new DracoonServerSettingsImpl(client);
        }

        public string GetVersion() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetServerVersion();
            ApiServerVersion result = _client.Executor.DoSyncApiCall<ApiServerVersion>(request, RequestType.GetServerVersion);
            return result.ServerVersion;
        }

        public DateTime? GetTime() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetServerTime();
            ApiServerTime result = _client.Executor.DoSyncApiCall<ApiServerTime>(request, RequestType.GetServerTime);
            return result.Time;
        }
    }
}