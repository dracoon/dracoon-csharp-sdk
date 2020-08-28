using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Validator;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;
using Attribute = Dracoon.Sdk.Model.Attribute;

namespace Dracoon.Sdk.SdkInternal {
    internal class DracoonAccountImpl : IAccount {
        internal const string Logtag = nameof(DracoonAccountImpl);
        private readonly IInternalDracoonClient _client;

        internal DracoonAccountImpl(IInternalDracoonClient client) {
            _client = client;
        }

        internal void AssertUserKeyPairAlgorithmSupported(UserKeyPairAlgorithm algorithm) {
            string minAlgorithmVersion = "4.0.0";
            switch (algorithm) {
                case UserKeyPairAlgorithm.RSA2048:
                    return;
                case UserKeyPairAlgorithm.RSA4096:
                    minAlgorithmVersion = ApiConfig.ApiVersionMin_Algorithm_UserKeyPair_RSA4096;
                    break;
            }

            try {
                _client.Executor.CheckApiServerVersion(minAlgorithmVersion);
            } catch (DracoonApiException) {
                throw new DracoonApiException(new DracoonApiCode(DracoonApiCode.SERVER_CRYPTO_VERSION_NOT_SUPPORTED.Code, "Algorithm " + algorithm.GetStringValue() + " requires minimum api version of " + minAlgorithmVersion + "."));
            }
        }

        public UserAccount GetUserAccount() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetUserAccount();
            ApiUserAccount result = _client.Executor.DoSyncApiCall<ApiUserAccount>(request, RequestType.GetUserAccount);
            return UserMapper.FromApiUserAccount(result);
        }

        public CustomerAccount GetCustomerAccount() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetCustomerAccount();
            ApiCustomerAccount result = _client.Executor.DoSyncApiCall<ApiCustomerAccount>(request, RequestType.GetCustomerAccount);
            return CustomerMapper.FromApiCustomerAccount(result);
        }

        public void SetUserKeyPair(UserKeyPairAlgorithm algorithm) {
            _client.Executor.CheckApiServerVersion();

            AssertUserKeyPairAlgorithmSupported(algorithm);

            UserKeyPair cryptoPair = GenerateNewUserKeyPair(algorithm, _client.EncryptionPassword);
            ApiUserKeyPair apiUserKeyPair = UserMapper.ToApiUserKeyPair(cryptoPair);
            IRestRequest request = _client.Builder.SetUserKeyPair(apiUserKeyPair);
            _client.Executor.DoSyncApiCall<VoidResponse>(request, RequestType.SetUserKeyPair);
        }

        public bool CheckUserKeyPairPassword(UserKeyPairAlgorithm algorithm) {
            _client.Executor.CheckApiServerVersion();

            try {
                GetAndCheckUserKeyPair(algorithm);
                return true;
            } catch (DracoonCryptoException cryptoError) {
                if (cryptoError.ErrorCode.Code == DracoonCryptoCode.INVALID_PASSWORD_ERROR.Code) {
                    return false;
                }

                throw;
            }
        }

        public void DeleteUserKeyPair(UserKeyPairAlgorithm algorithm) {
            _client.Executor.CheckApiServerVersion();

            AssertUserKeyPairAlgorithmSupported(algorithm);

            string algorithmString = UserMapper.ToApiUserKeyPairVersion(algorithm);
            IRestRequest request = _client.Builder.DeleteUserKeyPair(algorithmString);
            _client.Executor.DoSyncApiCall<VoidResponse>(request, RequestType.DeleteUserKeyPair);
        }

        internal UserKeyPair GenerateNewUserKeyPair(UserKeyPairAlgorithm algorithm, string encryptionPassword) {
            try {
                return Crypto.Sdk.Crypto.GenerateUserKeyPair(algorithm, encryptionPassword);
            } catch (CryptoException ce) {
                DracoonClient.Log.Debug(Logtag, "Generation of user key pair failed with " + ce.Message);
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        internal UserKeyPair GetAndCheckUserKeyPair(UserKeyPairAlgorithm algorithm) {
            AssertUserKeyPairAlgorithmSupported(algorithm);

            try {
                string algorithmString = UserMapper.ToApiUserKeyPairVersion(algorithm);
                IRestRequest request = _client.Builder.GetUserKeyPair(algorithmString);
                ApiUserKeyPair result = _client.Executor.DoSyncApiCall<ApiUserKeyPair>(request, RequestType.GetUserKeyPair);
                UserKeyPair userKeyPair = UserMapper.FromApiUserKeyPair(result);
                CheckKeyPair(userKeyPair);
                return userKeyPair;
            } catch (CryptoException ce) {
                DracoonClient.Log.Debug(Logtag, $"Check of user key pair failed with '{ce.Message}'!");
                throw new DracoonCryptoException(CryptoErrorMapper.ParseCause(ce), ce);
            }
        }

        public List<UserKeyPairAlgorithm> GetUserKeyPairAlgorithms() {
            _client.Executor.CheckApiServerVersion();

            
            List<UserKeyPair> userKeyPairs = GetUserKeyPairs();
            List<UserKeyPairAlgorithm> result = new List<UserKeyPairAlgorithm>(userKeyPairs.Count);

            foreach(UserKeyPair current in userKeyPairs) {
                result.Add(current.UserPrivateKey.Version);
            }
            return result;
        }

        public void ValidateTokenValidity() {
            _client.Executor.CheckApiServerVersion();
            IRestRequest request = _client.Builder.GetAuthenticatedPing();
            _client.Executor.DoSyncApiCall<VoidResponse>(request, RequestType.GetAuthenticatedPing);
        }


        internal UserKeyPair GetPreferredUserKeyPair() {
            List<UserKeyPairAlgorithmData> algorithms = _client.ServerImpl.ServerSettings.GetAvailableUserKeyPairAlgorithms();

            algorithms = algorithms.OrderBy(x => x.State).ToList();

            List<UserKeyPair> keyPairs = GetAndCheckUserKeyPairs();

            foreach (UserKeyPairAlgorithmData currentAlgorithm in algorithms) {
                try {
                    // TODO check if function is correct
                    UserKeyPair found = keyPairs.Single(o => o.UserPublicKey.Version == currentAlgorithm.Algorithm);
                    if (found != null) {
                        return found;
                    }
                } catch { } // Next Key Pair
            }

            throw new DracoonApiException(DracoonApiCode.SERVER_USER_KEY_PAIR_NOT_FOUND);
        }

        internal UserKeyPairAlgorithm GetPreferredUserKeyPairAlgorithm() {
            List<UserKeyPairAlgorithmData> algorithms = _client.ServerImpl.ServerSettings.GetAvailableUserKeyPairAlgorithms();
            algorithms = algorithms.OrderBy(x => x.State).ToList();

            if (algorithms.Count > 0) {
                return algorithms[0].Algorithm;
            }

            return UserKeyPairAlgorithm.RSA2048;
        }

        private List<UserKeyPair> GetUserKeyPairs() {
            List<UserKeyPair> returnValue = new List<UserKeyPair>();
            try {
                // Check if api supports this api endpoint. If not only provide the keypair using the "old" api.
                _client.Executor.CheckApiServerVersion(ApiConfig.ApiGetUserKeyPairsMinimumVersion);

                IRestRequest request = _client.Builder.GetUserKeyPairs();
                List<ApiUserKeyPair> result = _client.Executor.DoSyncApiCall<List<ApiUserKeyPair>>(request, RequestType.GetUserKeyPairs);

                foreach (ApiUserKeyPair apiUserKeyPair in result) {
                    UserKeyPair userKeyPair = UserMapper.FromApiUserKeyPair(apiUserKeyPair);
                    returnValue.Add(userKeyPair);
                }
            } catch (DracoonApiException error) when (error.ErrorCode == DracoonApiCode.API_VERSION_NOT_SUPPORTED) {
                UserKeyPair keyPair = GetAndCheckUserKeyPair(UserKeyPairAlgorithm.RSA2048);
                returnValue.Add(keyPair);
            }
            return returnValue;
        }

        internal List<UserKeyPair> GetAndCheckUserKeyPairs() {
            List<UserKeyPair> userKeyPairs = GetUserKeyPairs();
            foreach (UserKeyPair userKeyPair in userKeyPairs) {
                CheckKeyPair(userKeyPair);
            }
            return userKeyPairs;
        }

        private void CheckKeyPair(UserKeyPair keyPair) {
            if (!Crypto.Sdk.Crypto.CheckUserKeyPair(keyPair, _client.EncryptionPassword)) {
                throw new DracoonCryptoException(DracoonCryptoCode.INVALID_PASSWORD_ERROR);
            }
        }

        #region Avatar functions

        public Image GetAvatar() {
            ApiAvatarInfo apiAvatarInfo = GetApiAvatarInfoInternally();

            using (WebClient avatarClient = _client.Builder.ProvideAvatarDownloadWebClient()) {
                byte[] avatarImageBytes =
                    _client.Executor.ExecuteWebClientDownload(avatarClient, new Uri(apiAvatarInfo.AvatarUri), RequestType.GetUserAvatar);
                MemoryStream ms = new MemoryStream(avatarImageBytes);
                return Image.FromStream(ms);
            }
        }

        public AvatarInfo GetAvatarInfo() {
            ApiAvatarInfo apiAvatarInfo = GetApiAvatarInfoInternally();
            return UserMapper.FromApiAvatarInfo(apiAvatarInfo);
        }

        private ApiAvatarInfo GetApiAvatarInfoInternally() {
            _client.Executor.CheckApiServerVersion();

            IRestRequest request = _client.Builder.GetAvatar();
            ApiAvatarInfo apiAvatarInfo = _client.Executor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.GetUserAvatar);
            return apiAvatarInfo;
        }

        public AvatarInfo ResetAvatar() {
            _client.Executor.CheckApiServerVersion();

            IRestRequest request = _client.Builder.DeleteAvatar();
            ApiAvatarInfo defaultAvatarInfo = _client.Executor.DoSyncApiCall<ApiAvatarInfo>(request, RequestType.DeleteUserAvatar);
            return UserMapper.FromApiAvatarInfo(defaultAvatarInfo);
        }

        public AvatarInfo UpdateAvatar(Image newAvatar) {
            _client.Executor.CheckApiServerVersion();

            byte[] avatarBytes = null;
            using (MemoryStream ms = new MemoryStream()) {
                newAvatar.Save(ms, newAvatar.RawFormat);
                avatarBytes = ms.ToArray();
            }

            #region Build multipart

            ImageCodecInfo info = ImageCodecInfo.GetImageDecoders().First(c => c.FormatID == newAvatar.RawFormat.Guid);

            string formDataBoundary = "---------------------------" + Guid.NewGuid();
            byte[] packageHeader = ApiConfig.ENCODING.GetBytes(
                $"--{formDataBoundary}\r\nContent-Disposition: form-data; name=\"{"file"}\"; filename=\"{"avatarImage"}\"\r\n\r\n");
            byte[] packageFooter = ApiConfig.ENCODING.GetBytes(string.Format("\r\n--" + formDataBoundary + "--"));
            byte[] multipartFormatedChunkData = new byte[packageHeader.Length + packageFooter.Length + avatarBytes.Length];
            Buffer.BlockCopy(packageHeader, 0, multipartFormatedChunkData, 0, packageHeader.Length);
            Buffer.BlockCopy(avatarBytes, 0, multipartFormatedChunkData, packageHeader.Length, avatarBytes.Length);
            Buffer.BlockCopy(packageFooter, 0, multipartFormatedChunkData, packageHeader.Length + avatarBytes.Length, packageFooter.Length);

            #endregion

            ApiAvatarInfo resultAvatarInfo;
            using (WebClient avatarClient = _client.Builder.ProvideAvatarUploadWebClient(formDataBoundary)) {
                byte[] resultAvatarInfoBytes = _client.Executor.ExecuteWebClientChunkUpload(avatarClient,
                    new Uri(_client.ServerUri, ApiConfig.ApiPostAvatar), multipartFormatedChunkData, RequestType.PostUserAvatar);
                resultAvatarInfo = JsonConvert.DeserializeObject<ApiAvatarInfo>(Encoding.UTF8.GetString(resultAvatarInfoBytes));
            }

            return UserMapper.FromApiAvatarInfo(resultAvatarInfo);
        }

        #endregion

        #region Profile-Attributes

        public AttributeList GetUserProfileAttributeList() {
            _client.Executor.CheckApiServerVersion();

            IRestRequest request = _client.Builder.GetUserProfileAttributes();
            ApiAttributeList apiAttributeList = _client.Executor.DoSyncApiCall<ApiAttributeList>(request, RequestType.GetUserProfileAttributes);
            return AttributeMapper.FromApiAttributeList(apiAttributeList);
        }

        public Attribute GetUserProfileAttribute(string attributeKey) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            attributeKey.MustNotNullOrEmptyOrWhitespace(nameof(attributeKey));

            #endregion

            IRestRequest request = _client.Builder.GetUserProfileAttribute(attributeKey);
            ApiAttributeList apiAttributeList = _client.Executor.DoSyncApiCall<ApiAttributeList>(request, RequestType.GetUserProfileAttributes);
            if (apiAttributeList.Range.Total == 0) {
                throw new DracoonApiException(DracoonApiCode.SERVER_ATTRIBUTE_NOT_FOUND);
            }

            return AttributeMapper.FromApiAttributeList(apiAttributeList).Items[0];
        }

        public void AddOrUpdateUserProfileAttributes(List<Attribute> attributes) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            List<string> attributeKeys = new List<string>();
            foreach (Attribute currentAttribute in attributes) {
                currentAttribute.Key.MustNotNullOrEmptyOrWhitespace(nameof(currentAttribute.Key));
                currentAttribute.Value.MustNotNull(nameof(currentAttribute.Value));
                if (attributeKeys.Contains(currentAttribute.Key)) {
                    throw new ArgumentException("List cannot contain attributes with same attribute key.");
                }

                attributeKeys.Add(currentAttribute.Key);
            }

            #endregion

            ApiAddOrUpdateAttributeRequest apiAttributes = AttributeMapper.ToApiAddOrUpdateAttributeRequest(attributes);
            IRestRequest request = _client.Builder.PutUserProfileAttributes(apiAttributes);
            _client.Executor.DoSyncApiCall<VoidResponse>(request, RequestType.PutUserProfileAttributes);
        }

        public void DeleteProfileAttribute(string attributeKey) {
            _client.Executor.CheckApiServerVersion();

            #region Parameter Validation

            attributeKey.MustNotNullOrEmptyOrWhitespace(nameof(attributeKey));

            #endregion

            IRestRequest request = _client.Builder.DeleteUserProfileAttributes(attributeKey);
            _client.Executor.DoSyncApiCall<VoidResponse>(request, RequestType.DeleteUserProfileAttributes);
        }

        #endregion
    }
}