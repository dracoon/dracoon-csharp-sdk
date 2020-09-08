using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/DracoonAuth/*'/>
    public class DracoonAuth {
        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/Mode/*'/>
        public enum Mode {
            /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/EnumAuthCode/*'/>
            AUTHORIZATION_CODE, /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/EnumAccessToken/*'/>
            ACCESS_TOKEN, /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/EnumAccessRefreshToken/*'/>
            ACCESS_REFRESH_TOKEN
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/UsedMode/*'/>
        public Mode UsedMode { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/ClientId/*'/>
        public string ClientId { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/ClientSecret/*'/>
        public string ClientSecret { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/AuthorizationCode/*'/>
        public string AuthorizationCode { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/AccessToken/*'/>
        public string AccessToken { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/RefreshToken/*'/>
        public string RefreshToken { get; internal set; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/DracoonAuthConstructorOne/*'/>
        public DracoonAuth(string clientId, string clientSecret, string authorizationCode) {
            UsedMode = Mode.AUTHORIZATION_CODE;
            ValidateParameters(nameof(clientId), clientId);
            ClientId = clientId;
            ValidateParameters(nameof(clientSecret), clientSecret);
            ClientSecret = clientSecret;
            ValidateParameters(nameof(authorizationCode), authorizationCode);
            AuthorizationCode = authorizationCode;
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/DracoonAuthConstructorTwo/*'/>
        public DracoonAuth(string accessToken) {
            UsedMode = Mode.ACCESS_TOKEN;
            ValidateParameters(nameof(accessToken), accessToken);
            AccessToken = accessToken;
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonAuth"]/DracoonAuthConstructorThree/*'/>
        public DracoonAuth(string clientId, string clientSecret, string accessToken, string refreshToken) {
            UsedMode = Mode.ACCESS_REFRESH_TOKEN;
            ValidateParameters(nameof(clientId), clientId);
            ClientId = clientId;
            ValidateParameters(nameof(clientSecret), clientSecret);
            ClientSecret = clientSecret;
            ValidateParameters(nameof(accessToken), accessToken);
            AccessToken = accessToken;
            ValidateParameters(nameof(refreshToken), refreshToken, true);
            RefreshToken = refreshToken;
        }

        private static void ValidateParameters(string name, string value, bool nullable = false) {
            if (string.IsNullOrWhiteSpace(value)) {
                if(value == null) {
                    if (!nullable) {
                        throw new ArgumentNullException(name);
                    }
                } else {
                    throw new ArgumentException(name + " cannot be empty or whitespaced.");
                }
            }
        }
    }
}