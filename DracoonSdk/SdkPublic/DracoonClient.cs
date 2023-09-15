using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using System;

namespace Dracoon.Sdk {
    /// <summary>
    ///     <para>
    ///         DracoonClient is the main class of the DRACOON SDK.It contains several handlers which group the functions of the SDK logically.
    ///     </para>
    ///     <list type = "bullet" >
    ///         <listheader>
    ///             <description>Following handlers are available:</description>
    ///         </listheader>
    ///         <item>
    ///             <term><see cref="Dracoon.Sdk.DracoonClient.Server"/>:</term>
    ///             <description><see cref="Dracoon.Sdk.IServer"/></description>
    ///         </item>
    ///         <item>
    ///             <term><see cref="Dracoon.Sdk.DracoonClient.Account"/>:</term>
    ///             <description><see cref="Dracoon.Sdk.IAccount"/></description>
    ///         </item>
    ///         <item>
    ///             <term><see cref="Dracoon.Sdk.DracoonClient.Nodes"/>:</term>
    ///             <description><see cref="Dracoon.Sdk.INodes"/></description>
    ///         </item>
    ///         <item>
    ///             <term><see cref= "Dracoon.Sdk.DracoonClient.Shares"/>:</term>
    ///             <description><see cref="Dracoon.Sdk.IShares"/></description>
    ///         </item>
    ///         <item>
    ///             <term><see cref= "Dracoon.Sdk.DracoonClient.Users"/>:</term>
    ///             <description><see cref="Dracoon.Sdk.IUsers"/></description>
    ///         </item>
    ///     </list>
    /// </summary>
    public class DracoonClient : IInternalDracoonClient {
        #region Class-Members

        /// <summary>
        ///     The used target server URI.
        /// </summary>
        public Uri ServerUri { get; }

        /// <summary>
        ///     The current authorization data. See also <seealso cref="Dracoon.Sdk.DracoonAuth"/>
        /// </summary>
        public DracoonAuth Auth {
            get => _oAuth.Auth;
            set => _oAuth.Auth = value;
        }

        /// <summary>
        ///     The client's encryption password.
        /// </summary>
        public string EncryptionPassword { get; set; }

        #region Internal

        private static DracoonHttpConfig _httpConfig;

        internal static DracoonHttpConfig HttpConfig {
            get => _httpConfig ?? (_httpConfig = new DracoonHttpConfig());
            set => _httpConfig = value;
        }


        private static ILog _logger;

        internal static ILog Log {
            get => _logger ?? (_logger = new EmptyLog());
            set => _logger = value;
        }

        private readonly IRequestBuilder _builder;

        IRequestBuilder IInternalDracoonClient.Builder => _builder;

        private readonly IRequestExecutor _executor;

        IRequestExecutor IInternalDracoonClient.Executor => _executor;

        private readonly IOAuth _oAuth;

        IOAuth IInternalDracoonClient.OAuth => _oAuth;

        #endregion

        #region Public interfaces

        private readonly DracoonAccountImpl _account;
        private readonly DracoonNodesImpl _nodes;
        private readonly DracoonSharesImpl _shares;
        private readonly DracoonServerImpl _server;
        private readonly DracoonUsersImpl _users;

        DracoonAccountImpl IInternalDracoonClient.AccountImpl => _account;

        DracoonNodesImpl IInternalDracoonClient.NodesImpl => _nodes;

        DracoonSharesImpl IInternalDracoonClient.SharesImpl => _shares;

        DracoonServerImpl IInternalDracoonClient.ServerImpl => _server;

        DracoonUsersImpl IInternalDracoonClient.UsersImpl => _users;

        /// <summary>
        ///     Get Account handler. See also <seealso cref="Dracoon.Sdk.IAccount"/>
        /// </summary>
        public IAccount Account => _account;

        /// <summary>
        ///     Get Server handler. See also <seealso cref="Dracoon.Sdk.IServer"/>
        /// </summary>
        public IServer Server => _server;

        /// <summary>
        ///     Get Nodes handler. See also <seealso cref="Dracoon.Sdk.INodes"/>
        /// </summary>
        public INodes Nodes => _nodes;

        /// <summary>
        ///     Get Shares handler. See also <seealso cref="Dracoon.Sdk.IShares"/>
        /// </summary>
        public IShares Shares => _shares;

        /// <summary>
        ///     Get Users handler. See also <seealso cref="Dracoon.Sdk.IUsers"/>
        /// </summary>
        public IUsers Users => _users;

        #endregion

        #endregion

        /// <summary>
        ///     Creates a new instance DRACOON client.
        /// </summary>
        /// <param name="serverUri">The used target server URI.</param>
        /// <param name="auth">The current authorization data. See also <seealso cref="Dracoon.Sdk.DracoonAuth"/></param>
        /// <param name="encryptionPassword">The client's encryption password.</param>
        /// <param name="logger">The logger which should be used. See also <seealso cref="Dracoon.Sdk.ILog"/></param>
        /// <param name="httpConfig">The self defined http configuration (otherwise the defaults of the DracoonHttpConfig is used). See also <seealso cref="Dracoon.Sdk.DracoonHttpConfig"/></param>
        public DracoonClient(Uri serverUri, DracoonAuth auth = null, string encryptionPassword = null, ILog logger = null,
            DracoonHttpConfig httpConfig = null) {
            serverUri.MustBeValid(nameof(serverUri));

            ServerUri = serverUri;
            EncryptionPassword = encryptionPassword;
            DracoonClient.Log = logger;
            DracoonClient.HttpConfig = httpConfig;

            #region init internal

            _oAuth = new OAuthClient(this, auth);
            _builder = new DracoonRequestBuilder(_oAuth);
            _executor = new DracoonRequestExecutor(_oAuth, this);

            #endregion

            #region init public interfaces

            _account = new DracoonAccountImpl(this);
            _server = new DracoonServerImpl(this);
            _nodes = new DracoonNodesImpl(this);
            _shares = new DracoonSharesImpl(this);
            _users = new DracoonUsersImpl(this);

            #endregion
        }
    }
}