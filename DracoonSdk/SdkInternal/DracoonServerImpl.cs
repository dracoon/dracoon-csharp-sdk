using Dracoon.Sdk.SdkInternal.ApiModel;
using RestSharp;
using System;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonServerImpl : IServer {

        internal static readonly string LOGTAG = typeof(DracoonServerImpl).Name;
        private DracoonClient client;

        public IServerSettings ServerSettings {
            get; set;
        }

        internal DracoonServerImpl(DracoonClient client) {
            this.client = client;
            ServerSettings = new DracoonServerSettingsImpl(client);
        }

        public string GetVersion() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetServerVersion();
            ApiServerVersion result = client.RequestExecutor.DoSyncApiCall<ApiServerVersion>(request, RequestType.GetServerVersion);
            return result.ServerVersion;
        }

        public DateTime? GetTime() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetServerTime();
            ApiServerTime result = client.RequestExecutor.DoSyncApiCall<ApiServerTime>(request, RequestType.GetServerTime);
            return result.Time;
        }

    }
}
