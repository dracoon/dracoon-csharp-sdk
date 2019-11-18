using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Validator;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonUsersImpl : IUsers {
        internal const string Logtag = nameof(DracoonUsersImpl);
        private readonly IInternalDracoonClient _client;

        internal DracoonUsersImpl(IInternalDracoonClient client) {
            _client = client;
        }

        public Image GetUserAvatar(long userId, string avatarUuid) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            userId.MustPositive(nameof(userId));
            avatarUuid.MustNotNullOrEmptyOrWhitespace(nameof(avatarUuid));

            #endregion

            IRestRequest request = _client.Builder.GetUserAvatar(userId, avatarUuid);
            ApiAvatarInfo apiAvatarInfo = _client.Executor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.GetResourcesAvatar);

            using (WebClient avatarClient = _client.Builder.ProvideAvatarDownloadWebClient()) {
                byte[] avatarImageBytes =
                    _client.Executor.ExecuteWebClientDownload(avatarClient, new Uri(apiAvatarInfo.AvatarUri), RequestType.GetResourcesAvatar);
                MemoryStream ms = new MemoryStream(avatarImageBytes);
                return Image.FromStream(ms);
            }
        }
    }
}