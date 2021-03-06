﻿using System;
using System.Net;
using System.Threading;
using RestSharp;

namespace Dracoon.Sdk.SdkInternal {
    internal interface IRequestExecutor {
        void CheckApiServerVersion(string minVersionForCheck = ApiConfig.MinimumApiVersion);

        T DoSyncApiCall<T>(IRestRequest request, DracoonRequestExecutor.RequestType requestType, int authTry = 0) where T : class, new();

        byte[] ExecuteWebClientDownload(WebClient requestClient, Uri target, DracoonRequestExecutor.RequestType type, Thread asyncThread = null,
            int sendTry = 0);

        byte[] ExecuteWebClientChunkUpload(WebClient requestClient, Uri target, byte[] multipartFormatedChunk,
            DracoonRequestExecutor.RequestType type, Thread asyncThread = null, int sendTry = 0);
    }
}