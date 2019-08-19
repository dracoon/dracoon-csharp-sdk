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
            get {
                return _oAuth.Auth;
            }
            set {
                _oAuth.Auth = value;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/EncryptionPassword/*'/>
        public string EncryptionPassword { get; set; }

        #region Internal

        private static DracoonHttpConfig _httpConfig;

        internal static DracoonHttpConfig HttpConfig {
            get {
                return _httpConfig ?? (_httpConfig = new DracoonHttpConfig());
            }
        }


        private static ILog _logger;

        internal static ILog Log {
            get {
                return _logger ?? (_logger = new EmptyLog());
            }
        }

        private readonly IRequestBuilder _builder;

        IRequestBuilder IInternalDracoonClient.Builder {
            get {
                return _builder;
            }
        }

        private readonly IRequestExecutor _executor;

        IRequestExecutor IInternalDracoonClient.Executor {
            get {
                return _executor;
            }
        }

        private readonly IOAuth _oAuth;

        IOAuth IInternalDracoonClient.OAuth {
            get {
                return _oAuth;
            }
        }

        #endregion

        #region Public interfaces

        private readonly DracoonAccountImpl _account;
        private readonly DracoonNodesImpl _nodes;
        private readonly DracoonSharesImpl _shares;
        private readonly DracoonServerImpl _server;
        private readonly DracoonUsersImpl _users;

        DracoonAccountImpl IInternalDracoonClient.AccountImpl {
            get {
                return _account;
            }
        }

        DracoonNodesImpl IInternalDracoonClient.NodesImpl {
            get {
                return _nodes;
            }
        }

        DracoonSharesImpl IInternalDracoonClient.SharesImpl {
            get {
                return _shares;
            }
        }

        DracoonServerImpl IInternalDracoonClient.ServerImpl {
            get {
                return _server;
            }
        }

        DracoonUsersImpl IInternalDracoonClient.UsersImpl {
            get {
                return _users;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Account/*'/>
        public IAccount Account {
            get {
                return _account;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Server/*'/>
        public IServer Server {
            get {
                return _server;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Nodes/*'/>
        public INodes Nodes {
            get {
                return _nodes;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Shares/*'/>
        public IShares Shares {
            get {
                return _shares;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Users/*'/>
        public IUsers Users {
            get {
                return _users;
            }
        }

        #endregion

        #endregion

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/DracoonClientConstructor/*'/>
        public DracoonClient(Uri serverUri, DracoonAuth auth = null, string encryptionPassword = null, ILog logger = null,
            DracoonHttpConfig httpConfig = null) {
            serverUri.MustBeValid(nameof(serverUri));

            ServerUri = serverUri;
            EncryptionPassword = encryptionPassword;
            _logger = logger;
            _httpConfig = httpConfig;

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