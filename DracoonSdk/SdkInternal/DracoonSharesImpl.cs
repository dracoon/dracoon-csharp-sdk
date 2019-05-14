using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.Sort;
using RestSharp;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonSharesImpl : IShares {

        internal static readonly string LOGTAG = typeof(DracoonSharesImpl).Name;
        private DracoonClient client;

        internal DracoonSharesImpl(DracoonClient client) {
            this.client = client;
        }

        #region Download-Share services

        public DownloadShare CreateDownloadShare(CreateDownloadShareRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));

            Node targetNode = client.NodesImpl.GetNode(request.NodeId);
            // Node id is still checked in previous called getNode()
            // To save much effort throw this restriction instantly and not let the rest api throw this error
            if (targetNode.IsEncrypted.GetValueOrDefault(false) && targetNode.Type != NodeType.File) {
                throw new DracoonApiException(DracoonApiCode.VALIDATION_DL_SHARE_CANNOT_CREATE_ON_ENCRYPTED_ROOM_FOLDER);
            }
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name), true);
            request.MaxAllowedDownloads.MustPositive(nameof(request.MaxAllowedDownloads));
            if (targetNode.IsEncrypted.GetValueOrDefault(false) && String.IsNullOrWhiteSpace(request.EncryptionPassword) && !String.IsNullOrWhiteSpace(request.AccessPassword)) {
                throw new ArgumentException("Download share of a encrypted node must have a encryption password and no access password.");
            } else if (!targetNode.IsEncrypted.GetValueOrDefault(false) && String.IsNullOrWhiteSpace(request.AccessPassword) && !String.IsNullOrWhiteSpace(request.EncryptionPassword)) {
                throw new ArgumentException("Download share of a not encrypted node must have a access password and no encryption password.");
            }
            if (targetNode.IsEncrypted.GetValueOrDefault(false) && String.IsNullOrWhiteSpace(request.EncryptionPassword)) {
                throw new ArgumentException("Download share of a encrypted node must have a encryption password.");
            } else if (!targetNode.IsEncrypted.GetValueOrDefault(false) && request.AccessPassword != null) {
                request.AccessPassword.MustNotNullOrEmptyOrWhitespace(nameof(request.AccessPassword));
            }
            if (request.EmailRecipients != null) {
                request.EmailRecipients.EnumerableMustNotNullOrEmpty(nameof(request.EmailRecipients));
                request.EmailRecipients.ForEach(Current => Current.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailRecipients) + " element"));
                request.EmailBody.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailBody));
                request.EmailSubject.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailSubject));
            }
            if (request.SmsRecipients != null) {
                request.SmsRecipients.EnumerableMustNotNullOrEmpty(nameof(request.SmsRecipients));
                request.SmsRecipients.ForEach(Current => Current.MustNotNullOrEmptyOrWhitespace(nameof(request.SmsRecipients) + " element"));
                if (String.IsNullOrEmpty(request.AccessPassword)) {
                    throw new ArgumentException("If a SMS should be sent, a access password must be set.");
                }
            }
            #endregion

            ApiCreateDownloadShareRequest apiRequest = ShareMapper.ToUnencryptedApiCreateDownloadShareRequest(request);
            if (targetNode.IsEncrypted.GetValueOrDefault(false)) {
                UserKeyPair creatorKeyPair = client.AccountImpl.GetAndCheckUserKeyPair();
                EncryptedFileKey creatorEncryptedFileKey = client.NodesImpl.GetEncryptedFileKey(request.NodeId);
                PlainFileKey plainFileKey = client.NodesImpl.DecryptFileKey(creatorEncryptedFileKey, creatorKeyPair.UserPrivateKey, request.NodeId);
                UserKeyPair newGeneratedKeyPair = client.AccountImpl.GenerateNewUserKeyPair(request.EncryptionPassword);
                EncryptedFileKey newEncryptedFileKey = client.NodesImpl.EncryptFileKey(plainFileKey, newGeneratedKeyPair.UserPublicKey, request.NodeId);

                apiRequest.KeyPair = UserMapper.ToApiUserKeyPair(newGeneratedKeyPair);
                apiRequest.FileKey = FileMapper.ToApiFileKey(newEncryptedFileKey);
            }
            RestRequest restRequest = client.RequestBuilder.PostCreateDownloadShare(apiRequest);
            ApiDownloadShare resultShare = client.RequestExecutor.DoSyncApiCall<ApiDownloadShare>(restRequest, DracoonRequestExecuter.RequestType.PostCreateDownloadShare);
            return ShareMapper.FromApiDownloadShare(resultShare);
        }

        public void DeleteDownloadShare(long shareId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            shareId.MustPositive(nameof(shareId));
            #endregion

            RestRequest restRequest = client.RequestBuilder.DeleteDownloadShare(shareId);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeleteDownloadShare);
        }

        public DownloadShareList GetDownloadShares(long? offset = null, long? limit = null, GetDownloadSharesFilter filter = null, SharesSort sort = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetDownloadShares(offset, limit, filter, sort);
            ApiDownloadShareList result = client.RequestExecutor.DoSyncApiCall<ApiDownloadShareList>(restRequest, DracoonRequestExecuter.RequestType.GetDownloadShares);
            return ShareMapper.FromApiDownloadShareList(result);
        }

        #endregion

        #region Upload-Share services

        public UploadShare CreateUploadShare(CreateUploadShareRequest request) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            request.MustNotNull(nameof(request));
            request.NodeId.MustPositive(nameof(request.NodeId));
            request.Name.MustNotNullOrEmptyOrWhitespace(nameof(request.Name));
            request.MaxAllowedUploads.MustPositive(nameof(request.MaxAllowedUploads));
            request.MaxAllowedTotalSizeOverAllUploadedFiles.MustPositive(nameof(request.MaxAllowedTotalSizeOverAllUploadedFiles));
            request.UploadedFilesExpirationPeriod.MustPositive(nameof(request.UploadedFilesExpirationPeriod));
            if (request.EmailRecipients != null) {
                request.EmailRecipients.EnumerableMustNotNullOrEmpty(nameof(request.EmailRecipients));
                request.EmailRecipients.ForEach(Current => Current.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailRecipients) + " element"));
                request.EmailBody.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailBody));
                request.EmailSubject.MustNotNullOrEmptyOrWhitespace(nameof(request.EmailSubject));
            }
            if (request.SmsRecipients != null) {
                request.SmsRecipients.EnumerableMustNotNullOrEmpty(nameof(request.SmsRecipients));
                request.SmsRecipients.ForEach(Current => Current.MustNotNullOrEmptyOrWhitespace(nameof(request.SmsRecipients) + " element"));
                if (String.IsNullOrEmpty(request.AccessPassword)) {
                    throw new ArgumentException("If a SMS should be sent, a access password must be set.");
                }
            }
            #endregion

            ApiCreateUploadShareRequest apiRequest = ShareMapper.ToApiCreateUploadShareRequest(request);
            RestRequest restRequest = client.RequestBuilder.PostCreateUploadShare(apiRequest);
            ApiUploadShare resultShare = client.RequestExecutor.DoSyncApiCall<ApiUploadShare>(restRequest, DracoonRequestExecuter.RequestType.PostCreateUploadShare);
            return ShareMapper.FromApiUploadShare(resultShare);
        }

        public void DeleteUploadShare(long shareId) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            shareId.MustPositive(nameof(shareId));
            #endregion
            ;
            RestRequest restRequest = client.RequestBuilder.DeleteUploadShare(shareId);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(restRequest, DracoonRequestExecuter.RequestType.DeleteUploadShare);
        }

        public UploadShareList GetUploadShares(long? offset = null, long? limit = null, GetUploadSharesFilter filter = null, SharesSort sort = null) {
            client.RequestExecutor.CheckApiServerVersion();
            #region Parameter Validation
            offset.MustNotNegative(nameof(offset));
            limit.MustPositive(nameof(limit));
            #endregion

            RestRequest restRequest = client.RequestBuilder.GetUploadShares(offset, limit, filter, sort);
            ApiUploadShareList result = client.RequestExecutor.DoSyncApiCall<ApiUploadShareList>(restRequest, DracoonRequestExecuter.RequestType.GetUploadShares);
            return ShareMapper.FromApiUploadShareList(result);
        }

        #endregion
    }
}
