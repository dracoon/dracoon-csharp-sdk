using RestSharp;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthClient : IOAuth {

        private readonly IInternalDracoonClient _client;
        private DracoonAuth _auth;

        DracoonAuth IOAuth.Auth {
            get => _auth;
            set => _auth = value;
        }

        internal OAuthClient(IInternalDracoonClient client, DracoonAuth auth) {
            _client = client;
            _auth = auth;
        }

        string IOAuth.BuildAuthString() {
            if (_auth == null) {
                return "";
            }

            if (_auth.UsedMode == DracoonAuth.Mode.AUTHORIZATION_CODE) {
                RequestAccessToken();
            }

            // If the auth mode is "ACCESS_TOKEN" then this is not null and the refresh of the token will not be done because there is no refresh token
            return OAuthConfig.AuthorizationType + _auth.AccessToken;
        }

        private void RequestAccessToken() {
            RestRequest request = _client.Builder.PostOAuthToken(_auth.ClientId, _auth.ClientSecret,
                OAuthConfig.OAuthGrantTypes.AUTHORIZATION_CODE.Text, _auth.AuthorizationCode);
            ApiOAuthToken token = _client.Executor.DoSyncApiCall<ApiOAuthToken>(request, RequestType.PostOAuthToken);
            _auth.AccessToken = token.AccessToken;
            _auth.RefreshToken = token.RefreshToken;
            _auth.UsedMode = DracoonAuth.Mode.ACCESS_REFRESH_TOKEN;
        }

        void IOAuth.RefreshAccessToken() {
            if (!string.IsNullOrWhiteSpace(_auth.RefreshToken)) {
                RestRequest request = _client.Builder.PostOAuthRefresh(_auth.ClientId, _auth.ClientSecret,
                    OAuthConfig.OAuthGrantTypes.REFRESH_TOKEN.Text, _auth.RefreshToken);
                ApiOAuthToken token = _client.Executor.DoSyncApiCall<ApiOAuthToken>(request, RequestType.PostOAuthRefresh);
                _auth.AccessToken = token.AccessToken;
                _auth.RefreshToken = token.RefreshToken;
            }
        }
    }
}