namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthConfig {
        internal class OAuthGrantTypes {
            public static readonly OAuthGrantTypes AUTHORIZATION_CODE = new OAuthGrantTypes("authorization_code");
            public static readonly OAuthGrantTypes REFRESH_TOKEN = new OAuthGrantTypes("refresh_token");

            public string Text { get; }

            private OAuthGrantTypes(string text) {
                Text = text;
            }
        }

        internal const string OAuthPrefix = "oauth";
        internal const string OAuthAuthorizePath = "/authorize";
        internal const string AuthorizationType = "Bearer ";
        internal const string OAuthFlow = "code";

        #region POST

        internal const string OAuthPostAuthToken = OAuthPrefix + "/token";
        internal const string OAuthPostRefreshToken = OAuthPrefix + "/token";

        #endregion
    }
}