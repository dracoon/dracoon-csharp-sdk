using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using System;
using System.Collections.Specialized;
using System.Web;

namespace Dracoon.Sdk {
    /// <summary>
    ///     <para>
    ///         The DRACOON SDK uses OAuth 2.0 for client authorization.See <see ref="https://tools.ietf.org/html/rfc6749"/> for a detailed description of OAuth 2.0.
    ///     </para>
    ///     <para>
    ///         OAuthHelper is a helper class for the first part of the OAuth authorization code flow.
    ///     </para>
    ///     <list type="bullet">
    ///         <listheader>The class provides methods to:</listheader>
    ///         <item>
    ///             <term><see cref="CreateAuthorizationUrl(Uri, string, string, string)"/>:</term>
    ///             <description>Creates the authorization URI which must be opened in the user's browser.</description>
    ///         </item>
    ///         <item>
    ///             <term><see cref="ExtractAuthorizationStateFromUri(Uri)"/>:</term>
    ///             <description>Extracts the authorization state from the called redirect URI.</description>
    ///         </item>
    ///         <item>
    ///             <term><see cref="ExtractAuthorizationCodeFromUri(Uri)"/>:</term>
    ///             <description>Extracts the authorization code from the called redirect URI.</description>
    ///         </item>
    ///     </list>
    /// </summary>
    public static class OAuthHelper {

        /// <summary>
        ///     Creates the authorization URI which must be opened in the user's browser.
        /// </summary>
        /// <param name="baseServerUri">The URI of the Dracoon server.</param>
        /// <param name="clientId">The ID of the OAuth client.</param>
        /// <param name="state">The state identifier which is used to track running authorizations.</param>
        /// <param name="userAgentInfo">The userAgentInfo can be used to provide information about the application or device.</param>
        /// <returns>The authorization URI</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static Uri CreateAuthorizationUrl(Uri baseServerUri, string clientId, string state, string userAgentInfo = null) {
            baseServerUri.MustBeValid(nameof(baseServerUri));
            clientId.MustNotNullOrEmptyOrWhitespace(nameof(clientId));
            state.MustNotNullOrEmptyOrWhitespace(nameof(state));

            string baseUrl = baseServerUri.AbsoluteUri;
            if (!baseServerUri.AbsoluteUri.EndsWith("/")) {
                baseUrl += "/";
            }

            baseUrl += OAuthConfig.OAuthPrefix + OAuthConfig.OAuthAuthorizePath;
            string query = "response_type=" + OAuthConfig.OAuthFlow + "&client_id=" + clientId + "&state=" + state;

            if (string.IsNullOrWhiteSpace(userAgentInfo)) {
                return new Uri(baseUrl + "?" + query);
            }

            string base64UserAgentInfo = Convert.ToBase64String(ApiConfig.ENCODING.GetBytes(userAgentInfo));
            query += "&user_agent_info=" + HttpUtility.UrlEncode(base64UserAgentInfo);

            return new Uri(baseUrl + "?" + query);
        }

        /// <summary>
        ///     Extracts the authorization state from the called redirect URI.
        /// </summary>
        /// <param name="uri">The called redirect URI.</param>
        /// <returns>The authorization state</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        public static string ExtractAuthorizationStateFromUri(Uri uri) {
            return ExtractAuthorizationDataFromUri(uri, "state");
        }

        /// <summary>
        ///     Extracts the authorization code from the called redirect URI.
        /// </summary>
        /// <param name="uri">The called redirect URI.</param>
        /// <returns>The authorization code</returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
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