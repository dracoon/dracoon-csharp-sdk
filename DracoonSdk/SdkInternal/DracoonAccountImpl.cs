using System;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using RestSharp;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecuter;
using Dracoon.Sdk.Error;
using System.Drawing;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using System.Linq;
using Newtonsoft.Json;
using System.Text;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonAccountImpl : IAccount {

        internal static readonly string LOGTAG = typeof(DracoonAccountImpl).Name;
        private DracoonClient client;

        internal DracoonAccountImpl(DracoonClient client) {
            this.client = client;
        }

        public UserAccount GetUserAccount() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetUserAccount();
            ApiUserAccount result = client.RequestExecutor.DoSyncApiCall<ApiUserAccount>(request, RequestType.GetUserAccount);
            return UserMapper.FromApiUserAccount(result);
        }

        public CustomerAccount GetCustomerAccount() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetCustomerAccount();
            ApiCustomerAccount result = client.RequestExecutor.DoSyncApiCall<ApiCustomerAccount>(request, RequestType.GetCustomerAccount);
            return CustomerMapper.FromApiCustomerAccount(result);
        }

        public void SetUserKeyPair() {
            client.RequestExecutor.CheckApiServerVersion();
            UserKeyPair cryptoPair;
            try {
                cryptoPair = Crypto.Sdk.Crypto.GenerateUserKeyPair(client.EncryptionPassword);
            } catch (CryptoException ce) {
                client.Log.Debug(LOGTAG, String.Format("Generation of user key pair failed with '{0}'!", ce.Message));
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }

            ApiUserKeyPair apiUserKeyPair = UserMapper.ToApiUserKeyPair(cryptoPair);
            RestRequest request = client.RequestBuilder.SetUserKeyPair(apiUserKeyPair);
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(request, RequestType.SetUserKeyPair);
        }

        public bool CheckUserKeyPairPassword() {
            client.RequestExecutor.CheckApiServerVersion();
            return GetAndCheckUserKeyPair() == null ? false : true;
        }

        public void DeleteUserKeyPair() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.DeleteUserKeyPair();
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(request, RequestType.DeleteUserKeyPair);
        }

        internal UserKeyPair GenerateNewUserKeyPair(string encryptionPassword) {
            try {
                return Crypto.Sdk.Crypto.GenerateUserKeyPair(encryptionPassword);
            } catch (CryptoException ce) {
                client.Log.Debug(LOGTAG, "Generation of user key pair failed with " + ce.Message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        internal UserKeyPair GetAndCheckUserKeyPair() {
            try {
                RestRequest request = client.RequestBuilder.GetUserKeyPair();
                ApiUserKeyPair result = client.RequestExecutor.DoSyncApiCall<ApiUserKeyPair>(request, RequestType.GetUserKeyPair);
                UserKeyPair userKeyPair = UserMapper.FromApiUserKeyPair(result);
                if (Crypto.Sdk.Crypto.CheckUserKeyPair(userKeyPair, client.EncryptionPassword)) {
                    return userKeyPair;
                } else {
                    throw new DracoonCryptoException(DracoonCryptoCode.INVALID_PASSWORD_ERROR);
                }
            } catch (CryptoException ce) {
                client.Log.Debug(LOGTAG, String.Format("Check of user key pair failed with '{0}'!", ce.Message));
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        public void ValidateTokenValidity() {
            client.RequestExecutor.CheckApiServerVersion();
            RestRequest request = client.RequestBuilder.GetAuthenticatedPing();
            client.RequestExecutor.DoSyncApiCall<VoidResponse>(request, RequestType.GetAuthenticatedPing);
        }

        #region Avatar functions

        public Image GetAvatar() {
            ApiAvatarInfo apiAvatarInfo = GetApiAvatarInfoInternally();

            using (WebClient avatarClient = client.RequestBuilder.ProvideAvatarDownloadWebClient()) {
                byte[] avatarImageBytes = client.RequestExecutor.ExecuteWebClientDownload(avatarClient, new Uri(apiAvatarInfo.AvatarUri), RequestType.GetUserAvatar);
                MemoryStream ms = new MemoryStream(avatarImageBytes);
                return Image.FromStream(ms);
            }
        }

        public AvatarInfo GetAvatarInfo() {
            ApiAvatarInfo apiAvatarInfo = GetApiAvatarInfoInternally();
            return UserMapper.FromApiAvatarInfo(apiAvatarInfo);
        }

        private ApiAvatarInfo GetApiAvatarInfoInternally() {
            client.RequestExecutor.CheckApiServerVersion();
            client.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiAvatarFunctions);

            RestRequest request = client.RequestBuilder.GetAvatar();
            ApiAvatarInfo apiAvatarInfo = client.RequestExecutor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.GetUserAvatar);
            return apiAvatarInfo;
        }

        public AvatarInfo ResetAvatar() {
            client.RequestExecutor.CheckApiServerVersion();
            client.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiAvatarFunctions);

            RestRequest request = client.RequestBuilder.DeleteAvatar();
            ApiAvatarInfo defaultAvatarInfo = client.RequestExecutor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.DeleteUserAvatar);
            return UserMapper.FromApiAvatarInfo(defaultAvatarInfo);
        }

        public AvatarInfo UpdateAvatar(Image newAvatar) {
            client.RequestExecutor.CheckApiServerVersion();
            client.RequestExecutor.CheckApiServerVersion(ApiConfig.ApiAvatarFunctions);

            byte[] avatarBytes = null;
            using (MemoryStream ms = new MemoryStream()) {
                newAvatar.Save(ms, newAvatar.RawFormat);
                avatarBytes = ms.ToArray();
            }

            #region Build multipart
            ImageCodecInfo info = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == newAvatar.RawFormat.Guid);

            string formDataBoundary = "---------------------------" + Guid.NewGuid();
            byte[] packageHeader = ApiConfig.encoding.GetBytes(string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\n\r\n", formDataBoundary, "file", "avatarImage"));
            byte[] packageFooter = ApiConfig.encoding.GetBytes(string.Format("\r\n--" + formDataBoundary + "--"));
            byte[] multipartFormatedChunkData = new byte[packageHeader.Length + packageFooter.Length + avatarBytes.Length];
            Buffer.BlockCopy(packageHeader, 0, multipartFormatedChunkData, 0, packageHeader.Length);
            Buffer.BlockCopy(avatarBytes, 0, multipartFormatedChunkData, packageHeader.Length, avatarBytes.Length);
            Buffer.BlockCopy(packageFooter, 0, multipartFormatedChunkData, packageHeader.Length + avatarBytes.Length, packageFooter.Length);
            #endregion

            ApiAvatarInfo resultAvatarInfo;
            using (WebClient avatarClient = client.RequestBuilder.ProvideAvatarUploadWebClient(formDataBoundary)) {
                byte[] resultAvatarInfoBytes = client.RequestExecutor.ExecuteWebClientChunkUpload(avatarClient, new Uri(client.ServerUri, ApiConfig.ApiPostAvatar), multipartFormatedChunkData, RequestType.PostUserAvatar);
                resultAvatarInfo = JsonConvert.DeserializeObject<ApiAvatarInfo>(Encoding.UTF8.GetString(resultAvatarInfoBytes));
            }
            return UserMapper.FromApiAvatarInfo(resultAvatarInfo);
        }

        #endregion
    }
}
