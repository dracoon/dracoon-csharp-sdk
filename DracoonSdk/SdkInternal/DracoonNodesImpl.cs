using Dracoon.Sdk.Model;
using RestSharp;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System.IO;
using System.Collections.Generic;
using Dracoon.Sdk.Error;
using Dracoon.Crypto.Sdk.Model;
using System;
using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Sort;
using Dracoon.Sdk.SdkInternal.Util;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonNodesImpl : INodes, IFileDownloadCallback, IFileUploadCallback {

        internal static readonly string LOGTAG = typeof(DracoonNodesImpl).Name;
        private Dictionary<string, FileDownload> runningDownloads = new Dictionary<string, FileDownload>();
        private Dictionary<string, FileUpload> runningUploads = new Dictionary<string, FileUpload>();
        private DracoonClient client;

        internal DracoonNodesImpl(DracoonClient client) {
            this.client = client;
        }

        #region Node services

        public NodeList GetNodes(long parentNodeId = 0, long? offset = null, long? limit = null, GetNodesFilter filter = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            parentNodeId.MustNotNegative(nameof(parentNodeId));
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetNodes(parentNodeId, offset, limit, filter: filter);
            ApiNodeList result = client.RequestExecutor.DoSyncApiCall<ApiNodeList>(restRequest, DracoonRequestExecuter.RequestType.GetNodes);
            return NodeMapper.FromApiNodeList(result);
        }

        public Node GetNode(long nodeId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            nodeId.MustPositive(nameof(nodeId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetNode(nodeId);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.GetNode);
            return NodeMapper.FromApiNode(result);
        }

        public Node GetNode(string nodePath) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            nodePath.MustValidNodePath(nameof(nodePath));
            #endregion

            string parentNodePath = nodePath.Substring(0, nodePath.LastIndexOf('/') + 1);
            string searchedNodeName = nodePath.Substring(nodePath.LastIndexOf('/') + 1);
            SearchNodesFilter snf = new SearchNodesFilter();
            snf.AddParentPathFilter(SearchNodesFilter.ParentPath.EqualTo(parentNodePath).Build());

            NodeList result = SearchNodes(searchedNodeName, filter: snf);

            foreach (Node currentNode in result.Items) {
                if (currentNode.Name.Equals(searchedNodeName)) {
                    return currentNode;
                }
            }
            DracoonApiCode errorCode = DracoonApiCode.SERVER_NODE_NOT_FOUND;
            string message = "Query of node " + nodePath + " failed with '" + errorCode.Text + "'";
            throw new DracoonApiException(errorCode);
        }

        internal bool IsNodeEncrypted(long nodeId) {
            Node node = GetNode(nodeId);
            return node.IsEncrypted.GetValueOrDefault(false);
        }

        public void DeleteNodes(DeleteNodesRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.Ids.EnumerableMustNotNullOrEmpty(nameof(request.Ids));
            request.Ids.ForEach(id => id.MustPositive(nameof(request.Ids) + " element"));
            #endregion

            ApiDeleteNodesRequest apiDeleteNodesRequest = NodeMapper.ToApiDeleteNodesRequest(request);
            RestRequest restRequest = client.RequestBuilder.DeleteNodes(apiDeleteNodesRequest);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeleteNodes);
        }

        public Node CopyNodes(CopyNodesRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.TargetNodeId.MustPositive(nameof(request.TargetNodeId));
            request.NodesToBeCopied.EnumerableMustNotNullOrEmpty(nameof(request.NodesToBeCopied));
            request.NodesToBeCopied.ForEach(Current => Current.NodeId.MustPositive(nameof(Current.NodeId)));
            #endregion

            ApiCopyNodesRequest apiCopyNodesRequest = NodeMapper.ToApiCopyNodesRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostCopyNodes(request.TargetNodeId, apiCopyNodesRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PostCopyNodes);
            return NodeMapper.FromApiNode(result);
        }

        public Node MoveNodes(MoveNodesRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.TargetNodeId.MustPositive(nameof(request.TargetNodeId));
            request.NodesToBeMoved.EnumerableMustNotNullOrEmpty(nameof(request.NodesToBeMoved));
            request.NodesToBeMoved.ForEach(Current => Current.NodeId.MustPositive(nameof(Current.NodeId)));
            #endregion

            ApiMoveNodesRequest apiMoveNodesRequest = NodeMapper.ToApiMoveNodesRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostMoveNodes(request.TargetNodeId, apiMoveNodesRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PostMoveNodes);
            return NodeMapper.FromApiNode(result);
        }

        public NodeList SearchNodes(string searchString, long parentNodeId = 0, long offset = 0, long limit = 500, SearchNodesFilter filter = null, SearchNodesSort sort = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            searchString.MustNotNullOrEmptyOrWhitespace(nameof(searchString));
            parentNodeId.MustNotNegative(nameof(parentNodeId));
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetSearchNodes(parentNodeId, searchString, offset, limit, filter: filter, sort: sort);
            ApiNodeList result = client.RequestExecutor.DoSyncApiCall<ApiNodeList>(restRequest, DracoonRequestExecuter.RequestType.GetSearchNodes);
            return NodeMapper.FromApiNodeList(result);
        }

        public Node SetNodeAsFavorite(long nodeId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            nodeId.MustPositive(nameof(nodeId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.PostFavorite(nodeId);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PostFavorite);
            return NodeMapper.FromApiNode(result);
        }

        public void DeleteNodeFromFavorites(long nodeId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            nodeId.MustPositive(nameof(nodeId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.DeleteFavorite(nodeId);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeleteFavorite);
        }


        public RecycleBinItemList GetRecycleBinItems(long parentRoomId, long? offset = null, long? limit = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            parentRoomId.MustPositive(nameof(parentRoomId));
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetRecycleBin(parentRoomId, offset, limit);
            ApiDeletedNodeSummaryList result = client.RequestExecutor.DoSyncApiCall<ApiDeletedNodeSummaryList>(restRequest, DracoonRequestExecuter.RequestType.GetRecycleBin);
            return NodeMapper.FromApiDeletedNodeSummaryList(result);
        }

        public void EmptyRecycleBin(long parentRoomId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            parentRoomId.MustPositive(nameof(parentRoomId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.DeleteRecycleBin(parentRoomId);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeleteRecycleBin);
        }

        public PreviousVersionList GetPreviousVersions(long parentId, NodeType type, string nodeName, long? offset = null, long? limit = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            parentId.MustPositive(nameof(parentId));
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            nodeName.MustNotNullOrEmptyOrWhitespace(nameof(nodeName));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetPreviousVersions(parentId, EnumConverter.ConvertNodeTypeEnumToValue(type), nodeName, offset, limit);
            ApiDeletedNodeVersionsList result = client.RequestExecutor.DoSyncApiCall<ApiDeletedNodeVersionsList>(restRequest, DracoonRequestExecuter.RequestType.GetPreviousVersions);
            return NodeMapper.FromApiDeletedNodeVersionsList(result);
        }

        public PreviousVersion GetPreviousVersion(long previousNodeId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            previousNodeId.MustPositive(nameof(previousNodeId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetPreviousVersion(previousNodeId);
            ApiDeletedNodeVersion result = client.RequestExecutor.DoSyncApiCall<ApiDeletedNodeVersion>(restRequest, DracoonRequestExecuter.RequestType.GetPreviousVersion);
            return NodeMapper.FromApiDeletedNodeVersion(result);
        }

        public void RestorePreviousVersion(RestorePreviousVersionsRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.RestoreVersionIds.EnumerableMustNotNullOrEmpty(nameof(request.RestoreVersionIds));
            request.NewParentNodeId.MustPositive(nameof(request.NewParentNodeId));
            #endregion

            ApiRestorePreviousVersionsRequest apiRequest = NodeMapper.ToApiRestorePreviousVersionsRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostRestoreNodeVersion(apiRequest);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.PostRestoreNodeVersion);
        }

        public void DeletePreviousVersions(DeletePreviousVersionsRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.VersionIds.EnumerableMustNotNullOrEmpty(nameof(request.VersionIds));
            request.VersionIds.ForEach(Id => Id.MustPositive((nameof(request.VersionIds) + " element")));
            #endregion

            ApiDeletePreviousVersionsRequest apiRequest = NodeMapper.ToApiDeletePreviousVersionsRequest(request);
            RestRequest restRequest = client.RequestBuilder.DeletePreviousVersion(apiRequest);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeletePreviousVersions);
        }

        #endregion

        #region Room services

        public Node CreateRoom(CreateRoomRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.Quota.MustNotNegative(nameof(request.Quota));
            request.RecycleBinRetentionPeriod.MustNotNegative(nameof(request.RecycleBinRetentionPeriod));
            if (request.AdminUserIds.CheckEnumerableNullOrEmpty() && request.AdminGroupIds.CheckEnumerableNullOrEmpty()) {
                throw new ArgumentNullException(nameof(request.AdminUserIds) + " | " + nameof(request.AdminGroupIds), "Room must have an admin user or admin group.");
            }
            if (request.AdminUserIds != null) {
                request.AdminUserIds.EnumerableMustNotNullOrEmpty(nameof(request.AdminUserIds));
                request.AdminUserIds.ForEach(Id => Id.MustPositive(nameof(request.AdminUserIds) + " element"));
            }
            if (request.AdminGroupIds != null) {
                request.AdminGroupIds.EnumerableMustNotNullOrEmpty(nameof(request.AdminGroupIds));
                request.AdminGroupIds.ForEach(Id => Id.MustPositive(nameof(request.AdminGroupIds) + " element"));
            }
            #endregion

            ApiCreateRoomRequest apiCreateRoomRequest = RoomMapper.ToApiCreateRoomRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostRoom(apiCreateRoomRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PostRoom);
            return NodeMapper.FromApiNode(result);
        }

        public Node UpdateRoom(UpdateRoomRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.Id.MustPositive(nameof(request.Id));
            request.Quota.MustNotNegative(nameof(request.Quota));
            #endregion

            ApiUpdateRoomRequest apiUpdateRoomRequest = RoomMapper.ToApiUpdateRoomRequest(request);
            RestRequest restRequest = client.RequestBuilder.PutRoom(request.Id, apiUpdateRoomRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PutRoom);
            return NodeMapper.FromApiNode(result);
        }

        public Node EnableRoomEncryption(EnableRoomEncryptionRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.DataRoomRescueKeyPassword.MustNotNullOrEmptyOrWhitespace(nameof(request.DataRoomRescueKeyPassword), true);
            request.Id.MustPositive(nameof(request.Id));
            #endregion

            ApiUserKeyPair apiDataRoomRescueKey = null;
            if (request.DataRoomRescueKeyPassword != null) {
                try {
                    UserKeyPair cryptoPair = Crypto.Sdk.Crypto.GenerateUserKeyPair(request.DataRoomRescueKeyPassword);
                    apiDataRoomRescueKey = UserMapper.ToApiUserKeyPair(cryptoPair);
                } catch (CryptoException ce) {
                    client.Log.Debug(LOGTAG, String.Format("Generation of user key pair failed with '{0}'!", ce.Message));
                    throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
                }
            }

            ApiEnableRoomEncryptionRequest apiEnableRoomEncryptionRequest = RoomMapper.ToApiEnableRoomEncryptionRequest(request, apiDataRoomRescueKey);
            RestRequest restRequest = client.RequestBuilder.PutEnableRoomEncryption(request.Id, apiEnableRoomEncryptionRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PutEnableRoomEncryption);
            return NodeMapper.FromApiNode(result);
        }

        #endregion

        #region Folder services

        public Node CreateFolder(CreateFolderRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            #endregion

            ApiCreateFolderRequest apiCreateFolderRequest = FolderMapper.ToApiCreateFolderRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostFolder(apiCreateFolderRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PostFolder);
            return NodeMapper.FromApiNode(result);
        }

        public Node UpdateFolder(UpdateFolderRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.Id.MustPositive(nameof(request.Id));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);
            #endregion

            ApiUpdateFolderRequest apiUpdateFolderRequest = FolderMapper.ToApiUpdateFolderRequest(request);
            RestRequest restRequest = client.RequestBuilder.PutFolder(request.Id, apiUpdateFolderRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PutFolder);
            return NodeMapper.FromApiNode(result);
        }

        #endregion

        #region File services

        public Node UpdateFile(UpdateFileRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.Id.MustPositive(nameof(request.Id));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);
            #endregion

            ApiUpdateFileRequest apiUpdateFileRequest = FileMapper.ToApiUpdateFileRequest(request);
            RestRequest restRequest = client.RequestBuilder.PutFile(request.Id, apiUpdateFileRequest);
            ApiNode result = client.RequestExecutor.DoSyncApiCall<ApiNode>(restRequest, DracoonRequestExecuter.RequestType.PutFile);
            return NodeMapper.FromApiNode(result);
        }

        public Node UploadFile(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            CheckUploadActionId(actionId);
            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            input.CheckStreamCanRead(nameof(input));
            #endregion

            FileUpload upload = null;
            if (client.NodesImpl.IsNodeEncrypted(request.ParentId)) {
                UserKeyPair keyPair = client.AccountImpl.GetAndCheckUserKeyPair();
                upload = new EncFileUpload(client, actionId, request, input, keyPair.UserPublicKey, fileSize);
            } else {
                upload = new FileUpload(client, actionId, request, input, fileSize);
            }
            runningUploads.Add(actionId, upload);
            upload.AddFileUploadCallback(callback);
            upload.AddFileUploadCallback(this);
            return upload.RunSync();
        }

        public void StartUploadFileAsync(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            CheckUploadActionId(actionId);
            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            input.CheckStreamCanRead(nameof(input));
            #endregion

            FileUpload upload = null;
            Node parentNode = GetNode(request.ParentId); // Validation will be done in "GetNode"
            if (parentNode.IsEncrypted.GetValueOrDefault(false)) {
                UserKeyPair keyPair = client.AccountImpl.GetAndCheckUserKeyPair();
                upload = new EncFileUpload(client, actionId, request, input, keyPair.UserPublicKey, fileSize);
            } else {
                upload = new FileUpload(client, actionId, request, input, fileSize);
            }
            runningUploads.Add(actionId, upload);
            upload.AddFileUploadCallback(callback);
            upload.AddFileUploadCallback(this);
            upload.RunAsync();
        }

        public void CancelUploadFileAsync(string actionId) {
            #region Parameter Validation
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            #endregion

            if (runningUploads.ContainsKey(actionId)) {
                runningUploads[actionId].CancelUpload();
            }
        }

        public void DownloadFile(string actionId, long nodeId, Stream output, IFileDownloadCallback callback) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            CheckDownloadActionId(actionId);
            nodeId.MustPositive(nameof(nodeId));
            output.CheckStreamCanWrite(nameof(output));
            #endregion

            FileDownload download = null;
            Node nodeToDownload = GetNode(nodeId); // Validation will be done in "GetNode"
            if (nodeToDownload.IsEncrypted.GetValueOrDefault(false)) {
                UserKeyPair keyPair = client.AccountImpl.GetAndCheckUserKeyPair();
                download = new EncFileDownload(client, actionId, nodeToDownload, output, keyPair.UserPrivateKey);
            } else {
                download = new FileDownload(client, actionId, nodeToDownload, output);
            }
            runningDownloads.Add(actionId, download);
            download.AddFileDownloadCallback(callback);
            download.AddFileDownloadCallback(this);
            download.RunSync();
        }

        public void StartDownloadFileAsync(string actionId, long nodeId, Stream output, IFileDownloadCallback callback) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            CheckDownloadActionId(actionId);
            nodeId.MustPositive(nameof(nodeId));
            output.CheckStreamCanWrite(nameof(output));
            #endregion

            FileDownload download = null;
            Node nodeToDownload = GetNode(nodeId); // Validation will be done in "GetNode"
            if (nodeToDownload.IsEncrypted.GetValueOrDefault(false)) {
                UserKeyPair keyPair = client.AccountImpl.GetAndCheckUserKeyPair();
                download = new EncFileDownload(client, actionId, nodeToDownload, output, keyPair.UserPrivateKey);
            } else {
                download = new FileDownload(client, actionId, nodeToDownload, output);
            }
            runningDownloads.Add(actionId, download);
            download.AddFileDownloadCallback(callback);
            download.AddFileDownloadCallback(this);
            download.RunAsync();
        }

        public void CancelDownloadFileAsync(string actionId) {
            #region Parameter Validation
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            #endregion

            if (runningDownloads.ContainsKey(actionId)) {
                runningDownloads[actionId].CancelDownload();
            }
        }

        private void CheckDownloadActionId(string actionId) {
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            if (runningDownloads.ContainsKey(actionId)) {
                throw new ArgumentException("Download action id " + actionId + " is still registered!");
            }
        }

        private void CheckUploadActionId(string actionId) {
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            if (runningUploads.ContainsKey(actionId)) {
                throw new ArgumentException("Upload action id " + actionId + " is still registered!");
            }
        }

        public void GenerateMissingFileKeys(long? nodeId = null, int limit = int.MaxValue) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            nodeId.MustPositive(nameof(nodeId));
            limit.MustPositive(nameof(limit));
            #endregion

            UserKeyPair userKeyPair = client.AccountImpl.GetAndCheckUserKeyPair();
            int currentBatchOffset = 0;
            int batchLimit = 10;
            while (currentBatchOffset < limit) {
                RestRequest currentBatchRequest = client.RequestBuilder.GetMissingFileKeys(nodeId, batchLimit, currentBatchOffset);
                ApiMissingFileKeys missingFileKeys = client.RequestExecutor.DoSyncApiCall<ApiMissingFileKeys>(currentBatchRequest, DracoonRequestExecuter.RequestType.GetMissingFileKeys);
                HandlePendingMissingFileKeys(missingFileKeys, userKeyPair.UserPrivateKey);
                currentBatchOffset += missingFileKeys.Items.Count;
                if (missingFileKeys == null || missingFileKeys.Items.Count < batchLimit) {
                    break;
                }
            }
        }

        private void HandlePendingMissingFileKeys(ApiMissingFileKeys missingFileKeys, UserPrivateKey thisUserPrivateKey) {
            if (missingFileKeys == null || missingFileKeys.Items.Count == 0) {
                return;
            }
            Dictionary<long, UserPublicKey> userPublicKeys = UserMapper.ConvertApiUserIdPublicKeys(missingFileKeys.UserPublicKey);
            Dictionary<long, PlainFileKey> plainFileKeys = GeneratePlainFileKeyMap(missingFileKeys.FileKeys, thisUserPrivateKey);
            ApiSetUserFileKeysRequest setUserFileKeysRequest = new ApiSetUserFileKeysRequest {
                Items = new List<ApiSetUserFileKey>(missingFileKeys.UserPublicKey.Count)
            };

            foreach (ApiUserIdFileId currentMissingFileKey in missingFileKeys.Items) {
                UserPublicKey currentUsersPublicKey = userPublicKeys[currentMissingFileKey.UserId];
                PlainFileKey currentPlainFileKey = plainFileKeys[currentMissingFileKey.FileId];

                EncryptedFileKey currentEncryptedFileKey = EncryptFileKey(currentPlainFileKey, currentUsersPublicKey, currentMissingFileKey.FileId);

                ApiSetUserFileKey newRequestEntry = new ApiSetUserFileKey {
                    FileId = currentMissingFileKey.FileId,
                    UserId = currentMissingFileKey.UserId,
                    FileKey = FileMapper.ToApiFileKey(currentEncryptedFileKey)
                };
                setUserFileKeysRequest.Items.Add(newRequestEntry);
            }
            RestRequest restRequest = client.RequestBuilder.PostMissingFileKeys(setUserFileKeysRequest);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.PostMissingFileKeys);
        }

        private Dictionary<long, PlainFileKey> GeneratePlainFileKeyMap(List<ApiFileIdFileKey> fileIdFileKeys, UserPrivateKey thisUserPrivateKey) {
            Dictionary<long, PlainFileKey> plainFileKeys = new Dictionary<long, PlainFileKey>(fileIdFileKeys.Count);
            foreach (ApiFileIdFileKey currentEncryptedFileKey in fileIdFileKeys) {
                EncryptedFileKey encryptedFileKey = FileMapper.FromApiFileKey(currentEncryptedFileKey.FileKeyContainer);
                PlainFileKey decryptedFileKey = DecryptFileKey(encryptedFileKey, thisUserPrivateKey, currentEncryptedFileKey.FileId);
                plainFileKeys.Add(currentEncryptedFileKey.FileId, decryptedFileKey);
            }
            return plainFileKeys;
        }

        internal EncryptedFileKey EncryptFileKey(PlainFileKey plainFileKey, UserPublicKey userPublicKey, long? nodeId = null) {
            try {
                return Crypto.Sdk.Crypto.EncryptFileKey(plainFileKey, userPublicKey);
            } catch (CryptoException ce) {
                string message = "Encryption file key for node " + (nodeId.HasValue ? nodeId.Value.ToString() : "NULL") + " failed with " + ce.Message;
                client.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        internal PlainFileKey DecryptFileKey(EncryptedFileKey encryptedFileKey, UserPrivateKey userPrivateKey, long? nodeId = null) {
            try {
                return Crypto.Sdk.Crypto.DecryptFileKey(encryptedFileKey, userPrivateKey, client.EncryptionPassword);
            } catch (CryptoException ce) {
                string message = "Decryption file key for node " + (nodeId.HasValue ? nodeId.Value.ToString() : "NULL") + " failed with " + ce.Message;
                client.Log.Debug(LOGTAG, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        internal EncryptedFileKey GetEncryptedFileKey(long nodeId) {
            RestRequest fileKeyRequest = client.RequestBuilder.GetFileKey(nodeId);
            return FileMapper.FromApiFileKey(client.RequestExecutor.DoSyncApiCall<ApiFileKey>(fileKeyRequest, DracoonRequestExecuter.RequestType.GetFileKey));
        }

        #region IFileDownloadCallback / IFileUploadCallback implementation

        public void OnStarted(string actionId) {
        }

        public void OnRunning(string actionId, long bytesDownloaded, long bytesTotal) {
        }

        public void OnFinished(string actionId) {
            runningDownloads.Remove(actionId);
        }

        public void OnFinished(string actionId, Node resultNode) {
            runningUploads.Remove(actionId);
        }

        public void OnCanceled(string actionId) {
            runningDownloads.Remove(actionId);
            runningUploads.Remove(actionId);
        }

        public void OnFailed(string actionId, DracoonException occuredError) {
            runningDownloads.Remove(actionId);
            runningUploads.Remove(actionId);
        }

        #endregion

        #endregion
    }
}
