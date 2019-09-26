using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using System;
using System.Collections.Specialized;
using System.Web;
using Dracoon.Sdk.SdkInternal;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="oAuthHelper"]/OAuthHelper/*'/>
    public static class OAuthHelper {
        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="oAuthHelper"]/CreateAuthorizationUrl/*'/>
        public static Uri CreateAuthorizationUrl(Uri baseServerUri, string clientId, string state, string deviceName = null) {
            baseServerUri.MustBeValid(nameof(baseServerUri));
            clientId.MustNotNullOrEmptyOrWhitespace(nameof(clientId));
            state.MustNotNullOrEmptyOrWhitespace(nameof(state));

            string baseUrl = baseServerUri.AbsoluteUri;
            if (!baseServerUri.AbsoluteUri.EndsWith("/")) {
                baseUrl += "/";
            }

            baseUrl += OAuthConfig.OAuthPrefix + OAuthConfig.OAuthAuthorizePath;
            string query = "response_type=" + OAuthConfig.OAuthFlow + "&client_id=" + clientId + "&state=" + state;

            if (string.IsNullOrWhiteSpace(deviceName)) {
                return new Uri(baseUrl + "?" + query);
            }

            string base64DeviceName = Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(deviceName));
            query += "&user_agent_info=" + HttpUtility.UrlEncode(base64DeviceName);

            return new Uri(baseUrl + "?" + query);
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="oAuthHelper"]/ExtractAuthorizationStateFromUri/*'/>
        public static string ExtractAuthorizationStateFromUri(Uri uri) {
            return ExtractAuthorizationDataFromUri(uri, "state");
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="oAuthHelper"]/ExtractAuthorizationCodeFromUri/*'/>
        public static string ExtractAuthorizationCodeFromUri(Uri uri) {
            return ExtractAuthorizationDataFromUri(uri, "code");
        }

        private static string ExtractAuthorizationDataFromUri(Uri uri, string requestedType) {
            uri.MustNotNull(nameof(uri));

            NameValueCollection queryElements = HttpUtility.ParseQueryString(uri.Query);
            if (queryElements["error"] != null) {
                OAuthErrorParser.ParseError(queryElements["error"]);
            }

            return queryElements[requestedType];
        }
    }
}