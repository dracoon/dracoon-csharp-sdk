using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.Sort;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonNodesImpl : INodes, IFileDownloadCallback, IFileUploadCallback {
        internal const string Logtag = nameof(DracoonNodesImpl);
        private readonly Dictionary<string, FileDownload> _runningDownloads = new Dictionary<string, FileDownload>();
        private readonly Dictionary<string, FileUpload> _runningUploads = new Dictionary<string, FileUpload>();
        private readonly IInternalDracoonClient _client;

        internal DracoonNodesImpl(IInternalDracoonClient client) {
            _client = client;
        }

        #region Node services

        public NodeList GetNodes(long parentNodeId = 0, long? offset = null, long? limit = null, GetNodesFilter filter = null, GetNodesSort sort = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            parentNodeId.MustNotNegative(nameof(parentNodeId));
            offset.NullableMustNotNegative(nameof(offset));
            limit.NullableMustPositive(nameof(limit));

            #endregion

            IRestRequest restRequest = _client.Builder.GetNodes(parentNodeId, offset, limit, filter: filter, sort: sort);
            ApiNodeList result = _client.Executor.DoSyncApiCall<ApiNodeList>(restRequest, RequestType.GetNodes);
            return NodeMapper.FromApiNodeList(result);
        }

        public Node GetNode(long nodeId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            nodeId.MustPositive(nameof(nodeId));

            #endregion

            IRestRequest restRequest = _client.Builder.GetNode(nodeId);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.GetNode);
            return NodeMapper.FromApiNode(result);
        }

        public Node GetNode(string nodePath) {
            _client.Executor.CheckApiServerVersion();

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

            throw new DracoonApiException(DracoonApiCode.SERVER_NODE_NOT_FOUND);
        }

        internal bool IsNodeEncrypted(long nodeId) {
            Node node = GetNode(nodeId);
            return node.IsEncrypted.GetValueOrDefault(false);
        }

        public void DeleteNodes(DeleteNodesRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.Ids.EnumerableMustNotNullOrEmpty(nameof(request.Ids));
            request.Ids.ForEach(id => id.MustPositive(nameof(request.Ids) + " element"));

            #endregion

            ApiDeleteNodesRequest apiDeleteNodesRequest = NodeMapper.ToApiDeleteNodesRequest(request);
            IRestRequest restRequest = _client.Builder.DeleteNodes(apiDeleteNodesRequest);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.DeleteNodes);
        }

        public Node CopyNodes(CopyNodesRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.TargetNodeId.MustPositive(nameof(request.TargetNodeId));
            request.NodesToBeCopied.EnumerableMustNotNullOrEmpty(nameof(request.NodesToBeCopied));
            request.NodesToBeCopied.ForEach(current => current.NodeId.MustPositive(nameof(current.NodeId)));

            #endregion

            ApiCopyNodesRequest apiCopyNodesRequest = NodeMapper.ToApiCopyNodesRequest(request);
            IRestRequest restRequest = _client.Builder.PostCopyNodes(request.TargetNodeId, apiCopyNodesRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PostCopyNodes);
            return NodeMapper.FromApiNode(result);
        }

        public Node MoveNodes(MoveNodesRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.TargetNodeId.MustPositive(nameof(request.TargetNodeId));
            request.NodesToBeMoved.EnumerableMustNotNullOrEmpty(nameof(request.NodesToBeMoved));
            request.NodesToBeMoved.ForEach(current => current.NodeId.MustPositive(nameof(current.NodeId)));

            #endregion

            ApiMoveNodesRequest apiMoveNodesRequest = NodeMapper.ToApiMoveNodesRequest(request);
            IRestRequest restRequest = _client.Builder.PostMoveNodes(request.TargetNodeId, apiMoveNodesRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PostMoveNodes);
            return NodeMapper.FromApiNode(result);
        }

        public NodeList SearchNodes(string searchString, long parentNodeId = 0, long offset = 0, long limit = 500, SearchNodesFilter filter = null,
            SearchNodesSort sort = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            searchString.MustNotNullOrEmptyOrWhitespace(nameof(searchString));
            parentNodeId.MustNotNegative(nameof(parentNodeId));
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));

            #endregion

            IRestRequest restRequest = _client.Builder.GetSearchNodes(parentNodeId, searchString, offset, limit, filter: filter, sort: sort);
            ApiNodeList result = _client.Executor.DoSyncApiCall<ApiNodeList>(restRequest, RequestType.GetSearchNodes);
            return NodeMapper.FromApiNodeList(result);
        }

        public Node SetNodeAsFavorite(long nodeId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            nodeId.MustPositive(nameof(nodeId));

            #endregion

            IRestRequest restRequest = _client.Builder.PostFavorite(nodeId);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PostFavorite);
            return NodeMapper.FromApiNode(result);
        }

        public void DeleteNodeFromFavorites(long nodeId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            nodeId.MustPositive(nameof(nodeId));

            #endregion

            IRestRequest restRequest = _client.Builder.DeleteFavorite(nodeId);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.DeleteFavorite);
        }


        public RecycleBinItemList GetRecycleBinItems(long parentRoomId, long? offset = null, long? limit = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            parentRoomId.MustPositive(nameof(parentRoomId));
            offset.NullableMustNotNegative(nameof(offset));
            limit.NullableMustPositive(nameof(limit));

            #endregion

            IRestRequest restRequest = _client.Builder.GetRecycleBin(parentRoomId, offset, limit);
            ApiDeletedNodeSummaryList result = _client.Executor.DoSyncApiCall<ApiDeletedNodeSummaryList>(restRequest, RequestType.GetRecycleBin);
            return NodeMapper.FromApiDeletedNodeSummaryList(result);
        }

        public void EmptyRecycleBin(long parentRoomId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            parentRoomId.MustPositive(nameof(parentRoomId));

            #endregion

            IRestRequest restRequest = _client.Builder.DeleteRecycleBin(parentRoomId);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.DeleteRecycleBin);
        }

        public PreviousVersionList GetPreviousVersions(long parentId, NodeType type, string nodeName, long? offset = null, long? limit = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            parentId.MustPositive(nameof(parentId));
            offset.NullableMustNotNegative(nameof(offset));
            limit.NullableMustPositive(nameof(limit));
            nodeName.MustNotNullOrEmptyOrWhitespace(nameof(nodeName));

            #endregion

            IRestRequest restRequest =
                _client.Builder.GetPreviousVersions(parentId, EnumConverter.ConvertNodeTypeEnumToValue(type), nodeName, offset, limit);
            ApiDeletedNodeVersionsList result =
                _client.Executor.DoSyncApiCall<ApiDeletedNodeVersionsList>(restRequest, RequestType.GetPreviousVersions);
            return NodeMapper.FromApiDeletedNodeVersionsList(result);
        }

        public PreviousVersion GetPreviousVersion(long previousNodeId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            previousNodeId.MustPositive(nameof(previousNodeId));

            #endregion

            IRestRequest restRequest = _client.Builder.GetPreviousVersion(previousNodeId);
            ApiDeletedNodeVersion result = _client.Executor.DoSyncApiCall<ApiDeletedNodeVersion>(restRequest, RequestType.GetPreviousVersion);
            return NodeMapper.FromApiDeletedNodeVersion(result);
        }

        public void RestorePreviousVersion(RestorePreviousVersionsRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.RestoreVersionIds.EnumerableMustNotNullOrEmpty(nameof(request.RestoreVersionIds));
            request.RestoreVersionIds.ForEach(current => current.MustPositive(nameof(request.RestoreVersionIds) + " element"));
            request.NewParentNodeId.NullableMustPositive(nameof(request.NewParentNodeId));

            #endregion

            ApiRestorePreviousVersionsRequest apiRequest = NodeMapper.ToApiRestorePreviousVersionsRequest(request);
            IRestRequest restRequest = _client.Builder.PostRestoreNodeVersion(apiRequest);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.PostRestoreNodeVersion);
        }

        public void DeletePreviousVersions(DeletePreviousVersionsRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.VersionIds.EnumerableMustNotNullOrEmpty(nameof(request.VersionIds));
            request.VersionIds.ForEach(id => id.MustPositive(nameof(request.VersionIds) + " element"));

            #endregion

            ApiDeletePreviousVersionsRequest apiRequest = NodeMapper.ToApiDeletePreviousVersionsRequest(request);
            IRestRequest restRequest = _client.Builder.DeletePreviousVersion(apiRequest);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.DeletePreviousVersions);
        }

        public Uri BuildMediaUrl(string mediaToken, int width, int height) {
            #region Parameter Validation

            mediaToken.MustNotNullOrEmptyOrWhitespace(nameof(mediaToken));
            width.MustPositive(nameof(width));
            height.MustPositive(nameof(height));

            #endregion

            Uri mediaUrl = new Uri(_client.ServerUri, string.Format(ApiConfig.MediaTokenTemplate, mediaToken, width, height));
            return mediaUrl;
        }

        #endregion

        #region Room services

        public Node CreateRoom(CreateRoomRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.ParentId.NullableMustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.Quota.NullableMustNotNegative(nameof(request.Quota));
            request.RecycleBinRetentionPeriod.NullableMustNotNegative(nameof(request.RecycleBinRetentionPeriod));
            if (request.AdminUserIds.CheckEnumerableNullOrEmpty() && request.AdminGroupIds.CheckEnumerableNullOrEmpty()) {
                throw new ArgumentNullException(nameof(request.AdminUserIds) + " | " + nameof(request.AdminGroupIds),
                    "Room must have an admin user or admin group.");
            }

            if (request.AdminUserIds != null) {
                request.AdminUserIds.EnumerableMustNotNullOrEmpty(nameof(request.AdminUserIds));
                request.AdminUserIds.ForEach(id => id.MustPositive(nameof(request.AdminUserIds) + " element"));
            }

            if (request.AdminGroupIds != null) {
                request.AdminGroupIds.EnumerableMustNotNullOrEmpty(nameof(request.AdminGroupIds));
                request.AdminGroupIds.ForEach(id => id.MustPositive(nameof(request.AdminGroupIds) + " element"));
            }

            #endregion

            ApiCreateRoomRequest apiCreateRoomRequest = RoomMapper.ToApiCreateRoomRequest(request);
            IRestRequest restRequest = _client.Builder.PostRoom(apiCreateRoomRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PostRoom);
            return NodeMapper.FromApiNode(result);
        }

        public Node UpdateRoom(UpdateRoomRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.Id.MustPositive(nameof(request.Id));
            request.Quota.NullableMustNotNegative(nameof(request.Quota));

            #endregion

            ApiUpdateRoomRequest apiUpdateRoomRequest = RoomMapper.ToApiUpdateRoomRequest(request);
            IRestRequest restRequest = _client.Builder.PutRoom(request.Id, apiUpdateRoomRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PutRoom);
            return NodeMapper.FromApiNode(result);
        }

        public Node EnableRoomEncryption(EnableRoomEncryptionRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.Id.MustPositive(nameof(request.Id));

            if (request.DataRoomRescueKeyPassword != null) {
                request.DataRoomRescueKeyPairAlgorithm.MustNotNull(nameof(request.DataRoomRescueKeyPairAlgorithm));
            }

            if (request.DataRoomRescueKeyPairAlgorithm != null) {
                request.DataRoomRescueKeyPassword.MustNotNullOrEmptyOrWhitespace(nameof(request.DataRoomRescueKeyPassword));
            }

            #endregion

            ApiUserKeyPair apiDataRoomRescueKey = null;
            if (request.DataRoomRescueKeyPassword != null) {
                try {
                    UserKeyPair cryptoPair = Crypto.Sdk.Crypto.GenerateUserKeyPair(request.DataRoomRescueKeyPairAlgorithm.Value, request.DataRoomRescueKeyPassword);
                    apiDataRoomRescueKey = UserMapper.ToApiUserKeyPair(cryptoPair);
                } catch (CryptoException ce) {
                    DracoonClient.Log.Debug(Logtag, $"Generation of user key pair failed with '{ce.Message}'!");
                    throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
                }
            }

            ApiEnableRoomEncryptionRequest apiEnableRoomEncryptionRequest =
                RoomMapper.ToApiEnableRoomEncryptionRequest(request, apiDataRoomRescueKey);
            IRestRequest restRequest = _client.Builder.PutEnableRoomEncryption(request.Id, apiEnableRoomEncryptionRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PutEnableRoomEncryption);
            return NodeMapper.FromApiNode(result);
        }

        #endregion

        #region Folder services

        public Node CreateFolder(CreateFolderRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));

            #endregion

            ApiCreateFolderRequest apiCreateFolderRequest = FolderMapper.ToApiCreateFolderRequest(request);
            IRestRequest restRequest = _client.Builder.PostFolder(apiCreateFolderRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PostFolder);
            return NodeMapper.FromApiNode(result);
        }

        public Node UpdateFolder(UpdateFolderRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.Id.MustPositive(nameof(request.Id));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);

            #endregion

            ApiUpdateFolderRequest apiUpdateFolderRequest = FolderMapper.ToApiUpdateFolderRequest(request);
            IRestRequest restRequest = _client.Builder.PutFolder(request.Id, apiUpdateFolderRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PutFolder);
            return NodeMapper.FromApiNode(result);
        }

        #endregion

        #region File services

        public Node UpdateFile(UpdateFileRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.Id.MustPositive(nameof(request.Id));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);

            #endregion

            ApiUpdateFileRequest apiUpdateFileRequest = FileMapper.ToApiUpdateFileRequest(request);
            IRestRequest restRequest = _client.Builder.PutFile(request.Id, apiUpdateFileRequest);
            ApiNode result = _client.Executor.DoSyncApiCall<ApiNode>(restRequest, RequestType.PutFile);
            return NodeMapper.FromApiNode(result);
        }

        public Node UploadFile(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null) {
            FileUpload upload = CreateFileUploadInternally(actionId, request, input, fileSize, callback);
            return upload.RunSync();
        }

        public void StartUploadFileAsync(string actionId, FileUploadRequest request, Stream input, long fileSize = -1,
            IFileUploadCallback callback = null) {
            FileUpload upload = CreateFileUploadInternally(actionId, request, input, fileSize, callback);
            upload.RunAsync();
        }

        private FileUpload CreateFileUploadInternally(string actionId, FileUploadRequest request, Stream input, long fileSize = -1,
            IFileUploadCallback callback = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            CheckUploadActionId(actionId);
            request.MustNotNull(nameof(request));
            request.ParentId.MustPositive(nameof(request.ParentId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            input.CheckStreamCanRead(nameof(input));

            #endregion

            FileUpload upload;
            if (IsNodeEncrypted(request.ParentId)) {
                UserKeyPair keyPair = _client.AccountImpl.GetPreferredUserKeyPair();
                upload = new EncFileUpload(_client, actionId, request, input, keyPair.UserPublicKey, fileSize);
            } else {
                upload = new FileUpload(_client, actionId, request, input, fileSize);
            }

            _runningUploads.Add(actionId, upload);
            upload.AddFileUploadCallback(callback);
            upload.AddFileUploadCallback(this);
            return upload;
        }

        public void CancelUploadFileAsync(string actionId) {
            #region Parameter Validation

            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));

            #endregion

            if (_runningUploads.ContainsKey(actionId)) {
                _runningUploads[actionId].CancelUpload();
            }
        }

        public void DownloadFile(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null) {
            FileDownload download = CreateFileDownloadInternally(actionId, nodeId, output, callback);
            download.RunSync();
        }

        public void StartDownloadFileAsync(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null) {
            FileDownload download = CreateFileDownloadInternally(actionId, nodeId, output, callback);
            download.RunAsync();
        }

        private FileDownload CreateFileDownloadInternally(string actionId, long nodeId, Stream output, IFileDownloadCallback callback) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            CheckDownloadActionId(actionId);
            output.CheckStreamCanWrite(nameof(output));

            #endregion

            FileDownload download = null;
            Node nodeToDownload = GetNode(nodeId); // Validation for nodeId will be done in "GetNode"
            if (nodeToDownload.IsEncrypted.GetValueOrDefault(false)) {
                download = new EncFileDownload(_client, actionId, nodeToDownload, output);
            } else {
                download = new FileDownload(_client, actionId, nodeToDownload, output);
            }

            _runningDownloads.Add(actionId, download);
            download.AddFileDownloadCallback(callback);
            download.AddFileDownloadCallback(this);
            return download;
        }

        public void CancelDownloadFileAsync(string actionId) {
            #region Parameter Validation

            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));

            #endregion

            if (_runningDownloads.ContainsKey(actionId)) {
                _runningDownloads[actionId].CancelDownload();
            }
        }

        private void CheckDownloadActionId(string actionId) {
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            if (_runningDownloads.ContainsKey(actionId)) {
                throw new ArgumentException("Download action id " + actionId + " is still registered!");
            }
        }

        private void CheckUploadActionId(string actionId) {
            actionId.MustNotNullOrEmptyOrWhitespace(nameof(actionId));
            if (_runningUploads.ContainsKey(actionId)) {
                throw new ArgumentException("Upload action id " + actionId + " is still registered!");
            }
        }

        public void GenerateMissingFileKeys(long? nodeId = null, int limit = int.MaxValue) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            nodeId.NullableMustPositive(nameof(nodeId));
            limit.MustPositive(nameof(limit));

            #endregion

            List<UserKeyPair> userKeyPairs = _client.AccountImpl.GetAndCheckUserKeyPairs();
            int currentBatchOffset = 0;
            const int batchLimit = 10;
            while (currentBatchOffset < limit) {
                IRestRequest currentBatchRequest = _client.Builder.GetMissingFileKeys(nodeId, batchLimit, currentBatchOffset);
                ApiMissingFileKeys missingFileKeys =
                    _client.Executor.DoSyncApiCall<ApiMissingFileKeys>(currentBatchRequest, RequestType.GetMissingFileKeys);
                HandlePendingMissingFileKeys(missingFileKeys, userKeyPairs);
                currentBatchOffset += missingFileKeys.Items.Count;
                if (missingFileKeys.Items.Count < batchLimit) {
                    break;
                }
            }
        }

        private void HandlePendingMissingFileKeys(ApiMissingFileKeys missingFileKeys, List<UserKeyPair> userKeyPairs) {
            if (missingFileKeys == null || missingFileKeys.Items.Count == 0) {
                return;
            }

            Dictionary<long, UserPublicKey> userPublicKeys = UserMapper.ConvertApiUserIdPublicKeys(missingFileKeys.UserPublicKey);
            Dictionary<long, PlainFileKey> plainFileKeys = GeneratePlainFileKeyMap(missingFileKeys.FileKeys, userKeyPairs);
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

            IRestRequest restRequest = _client.Builder.PostMissingFileKeys(setUserFileKeysRequest);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.PostMissingFileKeys);
        }

        private Dictionary<long, PlainFileKey> GeneratePlainFileKeyMap(List<ApiFileIdFileKey> fileIdFileKeys, List<UserKeyPair> userKeyPairs) {
            Dictionary<long, PlainFileKey> plainFileKeys = new Dictionary<long, PlainFileKey>(fileIdFileKeys.Count);
            foreach (ApiFileIdFileKey currentEncryptedFileKey in fileIdFileKeys) {
                EncryptedFileKey encryptedFileKey = FileMapper.FromApiFileKey(currentEncryptedFileKey.FileKeyContainer);

                try {
                    UserKeyPair found = userKeyPairs.Single(o => o.UserPublicKey.Version == CryptoHelper.DetermineUserKeyPairVersion(encryptedFileKey.Version));
                    if (found != null) {
                        PlainFileKey decryptedFileKey = DecryptFileKey(encryptedFileKey, found.UserPrivateKey, currentEncryptedFileKey.FileId);
                        plainFileKeys.Add(currentEncryptedFileKey.FileId, decryptedFileKey);
                    }
                } catch {
                    // Next File Key
                }
            }

            return plainFileKeys;
        }

        internal EncryptedFileKey EncryptFileKey(PlainFileKey plainFileKey, UserPublicKey userPublicKey, long? nodeId = null) {
            try {
                return Crypto.Sdk.Crypto.EncryptFileKey(plainFileKey, userPublicKey);
            } catch (CryptoException ce) {
                string message = "Encryption file key for node " + (nodeId.HasValue ? nodeId.Value.ToString() : "NULL") + " failed with " +
                                 ce.Message;
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce));
            }
        }

        internal PlainFileKey DecryptFileKey(EncryptedFileKey encryptedFileKey, UserPrivateKey userPrivateKey, long? nodeId = null) {
            try {
                return Crypto.Sdk.Crypto.DecryptFileKey(encryptedFileKey, userPrivateKey, _client.EncryptionPassword);
            } catch (CryptoException ce) {
                string message = "Decryption file key for node " + (nodeId.HasValue ? nodeId.Value.ToString() : "NULL") + " failed with " +
                                 ce.Message;
                DracoonClient.Log.Debug(Logtag, message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        internal EncryptedFileKey GetEncryptedFileKey(long nodeId) {
            IRestRequest fileKeyRequest = _client.Builder.GetFileKey(nodeId);
            return FileMapper.FromApiFileKey(
                _client.Executor.DoSyncApiCall<ApiFileKey>(fileKeyRequest, RequestType.GetFileKey));
        }

        public List<FileVirusProtectionInfo> GenerateVirusProtectionInfo(List<long> fileIds) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            fileIds.EnumerableMustNotNullOrEmpty(nameof(fileIds));
            fileIds.ForEach(currentId => currentId.MustPositive(nameof(fileIds)));

            #endregion

            ApiGenerateVirusProtectionInfoRequest apiRequest = new ApiGenerateVirusProtectionInfoRequest() {
                FileIds = fileIds
            };
            IRestRequest restRequest = _client.Builder.GenerateVirusProtectionInfo(apiRequest);
            List<ApiFileVirusProtectionInfo> result = _client.Executor.DoSyncApiCall<List<ApiFileVirusProtectionInfo>>(restRequest, RequestType.GenerateVirusProtectionInfo);

            List<FileVirusProtectionInfo> returnValue = new List<FileVirusProtectionInfo>(result.Count);
            foreach (ApiFileVirusProtectionInfo current in result) {
                returnValue.Add(FileMapper.FromApiFileVirusProtectionInfo(current));
            }
            return returnValue;
        }

        public void DeleteMaliciousFile(long fileId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            fileId.MustPositive(nameof(fileId));

            #endregion

            IRestRequest restRequest = _client.Builder.DeleteMaliciousFile(fileId);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, RequestType.DeleteMaliciousFile);
        }

        #region IFileDownloadCallback / IFileUploadCallback implementation

        public void OnStarted(string actionId) {
            // Nothing to do on this point
        }

        public void OnRunning(string actionId, long bytesDownloaded, long bytesTotal) {
            // Nothing to do on this point
        }

        public void OnFinished(string actionId) {
            _runningDownloads.Remove(actionId);
        }

        public void OnFinished(string actionId, Node resultNode) {
            _runningUploads.Remove(actionId);
        }

        public void OnCanceled(string actionId) {
            _runningDownloads.Remove(actionId);
            _runningUploads.Remove(actionId);
        }

        public void OnFailed(string actionId, DracoonException occuredError) {
            _runningDownloads.Remove(actionId);
            _runningUploads.Remove(actionId);
        }

        #endregion

        #endregion
    }
}