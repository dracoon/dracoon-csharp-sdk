
using System;

namespace Dracoon.Sdk.SdkInternal.OAuth {
    internal class OAuthConfig {

        internal class OAuthGrantTypes {
            public static readonly OAuthGrantTypes AUTHORIZATION_CODE = new OAuthGrantTypes("authorization_code");
            public static readonly OAuthGrantTypes REFRESH_TOKEN = new OAuthGrantTypes("refresh_token");

            public string Text {
                get; private set;
            }
            private OAuthGrantTypes(string text) {
                Text = text;
            }
        }

        internal const string OAuthPrefix = "oauth";
        internal const string OAuthAuthorizePath = "/authorize";
        internal const string AuthorizationType = "Bearer ";
        internal const string OAuthFlow = "code";
        internal static TimeSpan AuthorizationRefreshInterval = new TimeSpan(0, 59, 0); // 60 minutes substracted with 60 seconds tolerance

        #region POST

        internal const string OAuthPostAuthToken = OAuthPrefix + "/token";
        internal const string OAuthPostRefreshToken = OAuthPrefix + "/token";

        #endregion
    }
}
