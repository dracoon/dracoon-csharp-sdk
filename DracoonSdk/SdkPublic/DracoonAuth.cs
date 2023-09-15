using System;

namespace Dracoon.Sdk {
    /// <summary>
    ///     <para>
    ///         The DRACOON SDK uses OAuth 2.0 for client authorization.See <see ref="https://tools.ietf.org/html/rfc6749"/> for a detailed description of OAuth 2.0. Because OAuth can be difficult to implement
    ///         for beginners, the DRACOON SDK can handle the OAuth authorization steps to obtain and refresh tokens.
    ///    </para>
    ///    <para>
    ///         This class is used to configure which steps of the OAuth authorization are made by the DRACOON SDK.
    ///    </para>
    ///    <list type="bullet">
    ///         <listheader>
    ///             <description>Following three modes are supported:</description>
    ///         </listheader>
    ///         <item>
    ///             <term>Authorization Code Mode:</term>
    ///             <description><see cref="Mode.AUTHORIZATION_CODE"/></description>
    ///         </item>
    ///         <item>
    ///             <term>Access Token Mode:</term>
    ///             <description><see cref="Mode.ACCESS_TOKEN"/></description>
    ///         </item>
    ///         <item>
    ///             <term>Access and Refresh Token Mode:</term>
    ///             <description><see cref="Mode.ACCESS_REFRESH_TOKEN"/></description>
    ///         </item>
    ///     </list>
    /// </summary>
    public class DracoonAuth {
        /// <summary>
        ///     Enumeration of authorization modes.
        /// </summary>
        public enum Mode {
            /// <summary>
            ///     <para>
            ///         This is the most common mode.Your application must request authorization and obtain an authorization code.
            ///         The retrieval of the access and refresh tokens with the authorization code as well as the automatic token refresh is handled by the DRACOON SDK.
            ///     </para>
            ///     <para>
            ///         The authorization is done within the user's browser or web view. After the user has logged in and authorized your application you receive the authorization code via
            ///         a callback to a pre-defined redirect URI.Depending on the type of your application you must open a local TCP port, register the redirect URI at your OS or provide an HTTP
            ///         endpoint which receives the callback.
            ///     </para>
            ///     (You can use <see cref="Dracoon.Sdk.OAuthHelper"/> to create the authorization URI which must be opened in the user's browser or web view and to extract the state and code from the redirect URI).
            /// </summary>
            AUTHORIZATION_CODE,
            /// <summary>
            ///     This is a simple mode.You can use it at the development or for terminal applications and scripts where a specific user account is used.
            /// </summary>
            ACCESS_TOKEN,
            /// <summary>
            ///     This mode can be used to obtain access and refresh token by yourself.
            /// </summary>
            ACCESS_REFRESH_TOKEN
        }

        /// <summary>
        ///     The used authorization mode. See also <seealso cref="Dracoon.Sdk.DracoonAuth.Mode"/>.
        /// </summary>
        public Mode UsedMode { get; internal set; }

        /// <summary>
        ///     The used OAuth client ID.
        /// </summary>
        public string ClientId { get; internal set; }

        /// <summary>
        ///     The used OAuth client secret.
        /// </summary>
        public string ClientSecret { get; internal set; }

        /// <summary>
        ///     The used OAuth authorization code.
        /// </summary>
        public string AuthorizationCode { get; internal set; }

        /// <summary>
        ///     The used OAuth access token.
        /// </summary>
        public string AccessToken { get; internal set; }

        /// <summary>
        ///     The used OAuth refresh token.
        /// </summary>
        public string RefreshToken { get; internal set; }

        /// <summary>
        ///     Constructs a new configuration for the authorization code mode.
        /// </summary>
        /// <param name="clientId">The OAuth client ID.</param>
        /// <param name="clientSecret">The OAuth client secret.</param>
        /// <param name="authorizationCode">The OAuth authorization code.</param>
        public DracoonAuth(string clientId, string clientSecret, string authorizationCode) {
            UsedMode = Mode.AUTHORIZATION_CODE;
            ValidateParameters(nameof(clientId), clientId);
            ClientId = clientId;
            ValidateParameters(nameof(clientSecret), clientSecret);
            ClientSecret = clientSecret;
            ValidateParameters(nameof(authorizationCode), authorizationCode);
            AuthorizationCode = authorizationCode;
        }

        /// <summary>
        ///     Constructs a new configuration for the access token mode.
        /// </summary>
        /// <param name="accessToken">The OAuth access token.</param>
        public DracoonAuth(string accessToken) {
            UsedMode = Mode.ACCESS_TOKEN;
            ValidateParameters(nameof(accessToken), accessToken);
            AccessToken = accessToken;
        }

        /// <summary>
        ///     Constructs a new configuration for the access and refresh token mode. If the an old access token is given, the SDK automatically refreshes the access token(implied that the refresh token is valid).
        /// </summary>
        /// <param name="clientId">The OAuth client ID.</param>
        /// <param name="clientSecret">The OAuth client secret.</param>
        /// <param name="accessToken">The OAuth access token.</param>
        /// <param name="refreshToken">The OAuth refresh token.</param>
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
                if (value == null) {
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