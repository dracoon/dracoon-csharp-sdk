using RestSharp;
using System;
using System.Net;
using System.Threading;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IRequestExecutor {
        void CheckApiServerVersion(string minVersionForCheck = ApiConfig.MinimumApiVersion);

        T DoSyncApiCall<T>(IRestRequest request, DracoonRequestExecutor.RequestType requestType, int sendTry = 0) where T : class, new();

        byte[] ExecuteWebClientDownload(WebClient requestClient, Uri target, DracoonRequestExecutor.RequestType type, Thread asyncThread = null,
            int sendTry = 0);

        byte[] ExecuteWebClientChunkUpload(WebClient requestClient, Uri target, byte[] data,
            DracoonRequestExecutor.RequestType type, Thread asyncThread = null, int sendTry = 0);
    }
}