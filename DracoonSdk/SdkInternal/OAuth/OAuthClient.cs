using Dracoon.Sdk.SdkInternal.ApiModel;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthClient {

        private static readonly string LOGTAG = typeof(OAuthClient).Name;

        private DracoonClient dracoonClient;
        private OAuthErrorParser oauthErrorParser;
        internal DracoonAuth dracoonAuth;

        internal OAuthClient(DracoonClient dracoonClient, DracoonAuth auth) {
            this.dracoonClient = dracoonClient;
            dracoonAuth = auth;
            oauthErrorParser = new OAuthErrorParser(dracoonClient);
        }

        internal string BuildAuthString() {
            if (dracoonAuth == null) {
                return "";
            }

            if (dracoonAuth.UsedMode == DracoonAuth.Mode.AUTHORIZATION_CODE) {
                RequestAccessToken();
            }

            // If the auth mode is "ACCESS_TOKEN" then this is not null and the refresh of the token will not be done because there is no refresh token
            return OAuthConfig.AuthorizationType + dracoonAuth.AccessToken;
        }

        internal void RequestAccessToken() {
            RestRequest request = dracoonClient.RequestBuilder.PostOAuthToken(dracoonAuth.ClientId, dracoonAuth.ClientSecret,
                OAuthConfig.OAuthGrantTypes.AUTHORIZATION_CODE.Text, dracoonAuth.AuthorizationCode);
            ApiOAuthToken token = DoSyncApiCall<ApiOAuthToken>(request, RequestType.PostOAuthToken);
            dracoonAuth.AccessToken = token.AccessToken;
            dracoonAuth.RefreshToken = token.RefreshToken;
            dracoonAuth.UsedMode = DracoonAuth.Mode.ACCESS_REFRESH_TOKEN;
        }

        internal void RefreshAccessToken() {
            if (!String.IsNullOrWhiteSpace(dracoonAuth.RefreshToken)) {
                RestRequest request = dracoonClient.RequestBuilder.PostOAuthRefresh(dracoonAuth.ClientId, dracoonAuth.ClientSecret,
                    OAuthConfig.OAuthGrantTypes.REFRESH_TOKEN.Text, dracoonAuth.RefreshToken);
                ApiOAuthToken token = DoSyncApiCall<ApiOAuthToken>(request, RequestType.PostOAuthRefresh);
                dracoonAuth.AccessToken = token.AccessToken;
                dracoonAuth.RefreshToken = token.RefreshToken;
            }
        }

        private T DoSyncApiCall<T>(RestRequest request, RequestType requestType) where T : class, new() {
            RestClient client = new RestClient(dracoonClient.ServerUri) {
                UserAgent = dracoonClient.HttpConfig.UserAgent
            };
            var uri = client.BuildUri(request);
            if (dracoonClient.HttpConfig.WebProxy != null) {
                client.Proxy = dracoonClient.HttpConfig.WebProxy;
            }
            IRestResponse response = client.Execute(request);
            if (response.ErrorException != null && response.ErrorException is WebException we) { // It's an HTTP exception
                dracoonClient.ApiErrorParser.ParseError(we, requestType);
            }
            if (!response.IsSuccessful) { // It's an API exception
                oauthErrorParser.ParseError(response, requestType);
            }
            if (typeof(T) == typeof(VoidResponse)) {
                return new VoidResponse() as T;
            }
            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
