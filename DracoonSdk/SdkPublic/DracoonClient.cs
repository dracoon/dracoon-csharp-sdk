using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/DracoonClient/*'/>
    public class DracoonClient : IInternalDracoonClient {
        #region Class-Members

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/ServerUri/*'/>
        public Uri ServerUri { get; }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Auth/*'/>
        public DracoonAuth Auth {
            get => _oAuth.Auth;
            set => _oAuth.Auth = value;
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/EncryptionPassword/*'/>
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

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Account/*'/>
        public IAccount Account => _account;

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Server/*'/>
        public IServer Server => _server;

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Nodes/*'/>
        public INodes Nodes => _nodes;

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Shares/*'/>
        public IShares Shares => _shares;

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Users/*'/>
        public IUsers Users => _users;

        #endregion

        #endregion

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/DracoonClientConstructor/*'/>
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