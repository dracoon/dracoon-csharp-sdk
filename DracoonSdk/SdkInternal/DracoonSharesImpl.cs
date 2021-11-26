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

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonSharesImpl : IShares {
        internal const string Logtag = nameof(DracoonSharesImpl);
        private readonly IInternalDracoonClient _client;

        internal DracoonSharesImpl(IInternalDracoonClient client) {
            _client = client;
        }

        #region Download-Share services

        public DownloadShare CreateDownloadShare(CreateDownloadShareRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));

            Node targetNode = _client.NodesImpl.GetNode(request.NodeId);
            // Node id is still checked in previous called getNode()
            // To save much effort throw this restriction instantly and not let the rest api throw this error
            if (targetNode.IsEncrypted.GetValueOrDefault(false) && targetNode.Type != NodeType.File) {
                throw new DracoonApiException(DracoonApiCode.VALIDATION_DL_SHARE_CANNOT_CREATE_ON_ENCRYPTED_ROOM_FOLDER);
            }

            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);
            request.MaxAllowedDownloads.NullableMustPositive(nameof(request.MaxAllowedDownloads));
            if (targetNode.IsEncrypted.GetValueOrDefault(false) && string.IsNullOrWhiteSpace(request.EncryptionPassword) &&
                !string.IsNullOrWhiteSpace(request.AccessPassword)) {
                throw new ArgumentException("Download share of a encrypted node must have a encryption password and no access password.");
            }

            if (!targetNode.IsEncrypted.GetValueOrDefault(false) && string.IsNullOrWhiteSpace(request.AccessPassword) &&
                !string.IsNullOrWhiteSpace(request.EncryptionPassword)) {
                throw new ArgumentException("Download share of a not encrypted node must have a access password and no encryption password.");
            }

            if (targetNode.IsEncrypted.GetValueOrDefault(false) && string.IsNullOrWhiteSpace(request.EncryptionPassword)) {
                throw new ArgumentException("Download share of a encrypted node must have a encryption password.");
            }

            if (!targetNode.IsEncrypted.GetValueOrDefault(false)) {
                request.AccessPassword?.MustNotNullOrEmptyOrWhitespace(nameof(request.AccessPassword));
            }

            if (request.EmailRecipients != null) {
                request.EmailRecipients.EnumerableMustNotNullOrEmpty(nameof(request.EmailRecipients));
                request.EmailRecipients.ForEach(current => current.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailRecipients) + " element"));
                request.EmailBody.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailBody));
                request.EmailSubject.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailSubject));
            }

            if (request.SmsRecipients != null) {
                request.SmsRecipients.EnumerableMustNotNullOrEmpty(nameof(request.SmsRecipients));
                request.SmsRecipients.ForEach(current => current.MustNotNullOrEmptyOrWhitespace(nameof(request.SmsRecipients) + " element"));
                if (string.IsNullOrEmpty(request.AccessPassword)) {
                    throw new ArgumentException("If a SMS should be sent, a access password must be set.");
                }
            }

            #endregion

            ApiCreateDownloadShareRequest apiRequest = ShareMapper.ToUnencryptedApiCreateDownloadShareRequest(request);
            if (targetNode.IsEncrypted.GetValueOrDefault(false)) {

                EncryptedFileKey creatorEncryptedFileKey = _client.NodesImpl.GetEncryptedFileKey(request.NodeId);
                UserKeyPair creatorKeyPair = _client.AccountImpl.GetAndCheckUserKeyPair(CryptoHelper.DetermineUserKeyPairVersion(creatorEncryptedFileKey.Version));
                PlainFileKey plainFileKey = _client.NodesImpl.DecryptFileKey(creatorEncryptedFileKey, creatorKeyPair.UserPrivateKey, request.NodeId);

                UserKeyPair newGeneratedKeyPair = _client.AccountImpl.GenerateNewUserKeyPair(_client.AccountImpl.GetPreferredUserKeyPairAlgorithm(), request.EncryptionPassword);
                EncryptedFileKey newEncryptedFileKey =
                    _client.NodesImpl.EncryptFileKey(plainFileKey, newGeneratedKeyPair.UserPublicKey, request.NodeId);

                apiRequest.KeyPair = UserMapper.ToApiUserKeyPair(newGeneratedKeyPair);
                apiRequest.FileKey = FileMapper.ToApiFileKey(newEncryptedFileKey);
            }

            IRestRequest restRequest = _client.Builder.PostCreateDownloadShare(apiRequest);
            ApiDownloadShare resultShare =
                _client.Executor.DoSyncApiCall<ApiDownloadShare>(restRequest, DracoonRequestExecutor.RequestType.PostCreateDownloadShare);
            return ShareMapper.FromApiDownloadShare(resultShare);
        }

        public void DeleteDownloadShare(long shareId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            shareId.MustPositive(nameof(shareId));

            #endregion

            IRestRequest restRequest = _client.Builder.DeleteDownloadShare(shareId);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecutor.RequestType.DeleteDownloadShare);
        }

        public DownloadShareList GetDownloadShares(long? offset = null, long? limit = null, GetDownloadSharesFilter filter = null,
            SharesSort sort = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            offset.NullableMustNotNegative(nameof(offset));
            limit.NullableMustPositive(nameof(limit));

            #endregion

            IRestRequest restRequest = _client.Builder.GetDownloadShares(offset, limit, filter, sort);
            ApiDownloadShareList result =
                _client.Executor.DoSyncApiCall<ApiDownloadShareList>(restRequest, DracoonRequestExecutor.RequestType.GetDownloadShares);
            return ShareMapper.FromApiDownloadShareList(result);
        }

        #endregion

        #region Upload-Share services

        public UploadShare CreateUploadShare(CreateUploadShareRequest request) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            request.MustNotNull(nameof(request));
            request.NodeId.MustPositive(nameof(request.NodeId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.MaxAllowedUploads.NullableMustPositive(nameof(request.MaxAllowedUploads));
            request.MaxAllowedTotalSizeOverAllUploadedFiles.NullableMustPositive(nameof(request.MaxAllowedTotalSizeOverAllUploadedFiles));
            request.UploadedFilesExpirationPeriod.NullableMustPositive(nameof(request.UploadedFilesExpirationPeriod));
            if (request.EmailRecipients != null) {
                request.EmailRecipients.EnumerableMustNotNullOrEmpty(nameof(request.EmailRecipients));
                request.EmailRecipients.ForEach(current => current.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailRecipients) + " element"));
                request.EmailBody.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailBody));
                request.EmailSubject.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailSubject));
            }

            if (request.SmsRecipients != null) {
                request.SmsRecipients.EnumerableMustNotNullOrEmpty(nameof(request.SmsRecipients));
                request.SmsRecipients.ForEach(current => current.MustNotNullOrEmptyOrWhitespace(nameof(request.SmsRecipients) + " element"));
                if (string.IsNullOrEmpty(request.AccessPassword)) {
                    throw new ArgumentException("If a SMS should be sent, a access password must be set.");
                }
            }

            #endregion

            ApiCreateUploadShareRequest apiRequest = ShareMapper.ToApiCreateUploadShareRequest(request);
            IRestRequest restRequest = _client.Builder.PostCreateUploadShare(apiRequest);
            ApiUploadShare resultShare =
                _client.Executor.DoSyncApiCall<ApiUploadShare>(restRequest, DracoonRequestExecutor.RequestType.PostCreateUploadShare);
            return ShareMapper.FromApiUploadShare(resultShare);
        }

        public void DeleteUploadShare(long shareId) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            shareId.MustPositive(nameof(shareId));

            #endregion

            IRestRequest restRequest = _client.Builder.DeleteUploadShare(shareId);
            _client.Executor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecutor.RequestType.DeleteUploadShare);
        }

        public UploadShareList GetUploadShares(long? offset = null, long? limit = null, GetUploadSharesFilter filter = null, SharesSort sort = null) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            offset.NullableMustNotNegative(nameof(offset));
            limit.NullableMustPositive(nameof(limit));

            #endregion

            IRestRequest restRequest = _client.Builder.GetUploadShares(offset, limit, filter, sort);
            ApiUploadShareList result =
                _client.Executor.DoSyncApiCall<ApiUploadShareList>(restRequest, DracoonRequestExecutor.RequestType.GetUploadShares);
            return ShareMapper.FromApiUploadShareList(result);
        }

        #endregion
    }
}