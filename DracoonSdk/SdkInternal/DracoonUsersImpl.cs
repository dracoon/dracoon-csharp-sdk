using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Validator;
using RestSharp;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonUsersImpl : IUsers {

        internal static readonly string LOGTAG = typeof(DracoonUsersImpl).Name;
        private DracoonClient client;

        internal DracoonUsersImpl(DracoonClient client) {
            this.client = client;
        }

        public Image GetUserAvatar(long userId, string avatarUUID) {
            client.RequestExecutor.CheckApiServerVersion();
            client.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiAvatarFunctions);

            #region Parameter Validation

            userId.MustPositive(nameof(userId));
            avatarUUID.MustNotNullOrEmptyOrWhitespace(nameof(avatarUUID));

            #endregion

            RestRequest request = client.RequestBuilder.GetUserAvatar(userId, avatarUUID);
            ApiAvatarInfo apiAvatarInfo = client.RequestExecutor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.GetResourcesAvatar);

            using (WebClient avatarClient = client.RequestBuilder.ProvideAvatarDownloadWebClient()) {
                byte[] avatarImageBytes = client.RequestExecutor.ExecuteWebClientDownload(avatarClient, new Uri(apiAvatarInfo.AvatarUri), RequestType.GetResourcesAvatar);
                MemoryStream ms = new MemoryStream(avatarImageBytes);
                return Image.FromStream(ms);
            }
        }
    }
}
