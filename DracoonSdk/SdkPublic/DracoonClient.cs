using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using System;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/DracoonClient/*'/>
    public class DracoonClient {

        #region Class-Members

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/ServerUri/*'/>
        public Uri ServerUri {
            get; private set;
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Auth/*'/>
        public DracoonAuth Auth {
            get {
                return OAuthClient.dracoonAuth;
            }
            set {
                OAuthClient.dracoonAuth = value;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/EncryptionPassword/*'/>
        public string EncryptionPassword {
            get; set;
        }

        #region Internal

        internal DracoonHttpConfig HttpConfig {
            get; private set;
        }

        internal ILog Log {
            get; private set;
        }

        internal DracoonRequestBuilder RequestBuilder {
            get; private set;
        }

        internal DracoonRequestExecuter RequestExecutor {
            get; private set;
        }

        internal DracoonErrorParser ApiErrorParser {
            get; private set;
        }

        internal OAuthClient OAuthClient {
            get; private set;
        }

        #endregion

        #region Public interfaces

        internal DracoonAccountImpl AccountImpl {
            get; private set;
        }

        internal DracoonNodesImpl NodesImpl {
            get; private set;
        }

        internal DracoonSharesImpl SharesImpl {
            get; private set;
        }

        internal DracoonServerImpl ServerImpl {
            get; private set;
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Account/*'/>
        public IAccount Account {
            get {
                return AccountImpl;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Server/*'/>
        public IServer Server {
            get {
                return ServerImpl;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Nodes/*'/>
        public INodes Nodes {
            get {
                return NodesImpl;
            }
        }

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/Shares/*'/>
        public IShares Shares {
            get {
                return SharesImpl;
            }
        }

        #endregion

        #endregion

        /// <include file = "SdkPublicDoc.xml" path='docs/members[@name="dracoonClient"]/DracoonClientConstructor/*'/>
        public DracoonClient(Uri serverUri, DracoonAuth auth = null, string encryptionPassword = null, ILog logger = null, DracoonHttpConfig httpConfig = null) {
            serverUri.MustBeValid(nameof(serverUri));

            ServerUri = serverUri;
            EncryptionPassword = encryptionPassword;
            Log = logger ?? new EmptyLog();
            HttpConfig = httpConfig ?? new DracoonHttpConfig();

            #region init internal

            OAuthClient = new OAuthClient(this, auth);
            RequestBuilder = new DracoonRequestBuilder(this);
            RequestExecutor = new DracoonRequestExecuter(this);
            ApiErrorParser = new DracoonErrorParser(this);

            #endregion

            #region init public interfaces

            AccountImpl = new DracoonAccountImpl(this);
            ServerImpl = new DracoonServerImpl(this);
            NodesImpl = new DracoonNodesImpl(this);
            SharesImpl = new DracoonSharesImpl(this);

            #endregion
        }


    }
}
