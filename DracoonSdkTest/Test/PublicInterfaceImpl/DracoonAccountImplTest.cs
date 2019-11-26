using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Validator;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;
using Attribute = Dracoon.Sdk.Model.Attribute;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonAccountImplTest {
        #region GetUserAccount

        [Fact]
        public void GetUserAccount() {
            // ARRANGE
            UserAccount expected = FactoryUser.UserAccount;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetUserAccount()).Returns(FactoryRestSharp.GetUserAccountMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUserAccount>(Arg.IsAny<IRestRequest>(), RequestType.GetUserAccount, 0))
                    .Returns(FactoryUser.ApiUserAccount).Occurs(1);
            Mock.Arrange(() => UserMapper.FromApiUserAccount(Arg.IsAny<ApiUserAccount>())).Returns(FactoryUser.UserAccount).Occurs(1);

            // ACT
            UserAccount actual = a.GetUserAccount();

            // ASSERT
            Assert.Equal(expected, actual, new UserAccountComparer());
            Mock.Assert(() => UserMapper.FromApiUserAccount(Arg.IsAny<ApiUserAccount>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetCustomerAccount

        [Fact]
        public void GetCustomerAccount() {
            // ARRANGE
            CustomerAccount expected = FactoryCustomer.CustomerAccount;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetCustomerAccount()).Returns(FactoryRestSharp.GetCustomerAccountMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiCustomerAccount>(Arg.IsAny<IRestRequest>(), RequestType.GetCustomerAccount, 0))
                    .Returns(FactoryCustomer.ApiCustomerAccount).Occurs(1);
            Mock.Arrange(() => CustomerMapper.FromApiCustomerAccount(Arg.IsAny<ApiCustomerAccount>())).Returns(FactoryCustomer.CustomerAccount).Occurs(1);

            // ACT
            CustomerAccount actual = a.GetCustomerAccount();

            // ASSERT
            Assert.Equal(expected, actual, new CustomerAccountComparer());
            Mock.Assert(() => CustomerMapper.FromApiCustomerAccount(Arg.IsAny<ApiCustomerAccount>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region SetUserKeyPair

        [Fact]
        public void SetUserKeyPair() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => a.GenerateNewUserKeyPair(Arg.AnyString)).Returns(FactoryUser.UserKeyPair).Occurs(1);
            Mock.Arrange(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>())).Returns(FactoryUser.ApiUserKeyPair).Occurs(1);
            Mock.Arrange(() => c.Builder.SetUserKeyPair(Arg.IsAny<ApiUserKeyPair>())).Returns(FactoryRestSharp.SetUserKeyPairMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.SetUserKeyPair, 0)).DoNothing().Occurs(1);

            // ACT
            a.SetUserKeyPair();

            // ASSERT
            Mock.Assert(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>()));
            Mock.Assert(a);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region CheckUserKeyPairPassword

        [Fact]
        public void CheckUserKeyPairPassword_Success() {
            // ARRANGE
            bool expected = true;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => a.GetAndCheckUserKeyPair()).Returns(FactoryUser.UserKeyPair).Occurs(1);

            // ACT
            bool actual = a.CheckUserKeyPairPassword();

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(a);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CheckUserKeyPairPassword_WrongPassword() {
            // ARRANGE
            bool expected = false;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => a.GetAndCheckUserKeyPair()).Throws(new DracoonCryptoException(DracoonCryptoCode.INVALID_PASSWORD_ERROR)).Occurs(1);

            // ACT
            bool actual = a.CheckUserKeyPairPassword();

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(a);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CheckUserKeyPairPassword_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => a.GetAndCheckUserKeyPair()).Throws(new DracoonCryptoException()).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => a.CheckUserKeyPairPassword());
            Mock.Assert(a);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeleteUserKeyPair

        [Fact]
        public void DeleteUserKeyPair() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.DeleteUserKeyPair()).Returns(FactoryRestSharp.DeleteUserKeyPairMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.DeleteUserKeyPair, 0)).DoNothing().Occurs(1);

            // ACT
            a.DeleteUserKeyPair();

            // ASSERT
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GenerateNewUserKeyPair

        [Fact]
        public void GenerateNewUserKeyPair_Success() {
            // ARRANGE
            UserKeyPair expected = FactoryUser.UserKeyPair;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.AnyString)).Returns(FactoryUser.UserKeyPair).Occurs(1);

            // ACT
            UserKeyPair actual = a.GenerateNewUserKeyPair(c.EncryptionPassword);

            // ASSERT
            Assert.Equal(expected, actual, new UserKeyPairComparer());
            Mock.Assert(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.AnyString));
        }

        [Fact]
        public void GenerateNewUserKeyPair_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.AnyString)).Throws(new CryptoException()).Occurs(1);
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>())).Returns(DracoonCryptoCode.UNKNOWN_ERROR).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => a.GenerateNewUserKeyPair(c.EncryptionPassword));
            Mock.Assert(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.AnyString));
            Mock.Assert(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>()));
        }

        #endregion

        #region GetAndCheckUserKeyPair

        [Fact]
        public void GetAndCheckUserKeyPair_Success() {
            // ARRANGE
            UserKeyPair expected = FactoryUser.UserKeyPair;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(false);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetUserKeyPair()).Returns(FactoryRestSharp.GetUserKeyPairMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUserKeyPair>(Arg.IsAny<IRestRequest>(), RequestType.GetUserKeyPair, 0))
                .Returns(FactoryUser.ApiUserKeyPair).Occurs(1);
            Mock.Arrange(() => UserMapper.FromApiUserKeyPair(Arg.IsAny<ApiUserKeyPair>())).Returns(FactoryUser.UserKeyPair).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CheckUserKeyPair(Arg.IsAny<UserKeyPair>(), Arg.AnyString)).Returns(true).Occurs(1);

            // ACT
            UserKeyPair actual = a.GetAndCheckUserKeyPair();

            // ASSERT
            Assert.Equal(expected, actual, new UserKeyPairComparer());
            Mock.Assert(() => UserMapper.FromApiUserKeyPair(Arg.IsAny<ApiUserKeyPair>()));
            Mock.Assert(() => Crypto.Sdk.Crypto.CheckUserKeyPair(Arg.IsAny<UserKeyPair>(), Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void GetAndCheckUserKeyPair_WrongPassword() {
            // ARRANGE
            int expected = DracoonCryptoCode.INVALID_PASSWORD_ERROR.Code;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetUserKeyPair()).Returns(FactoryRestSharp.GetUserKeyPairMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUserKeyPair>(Arg.IsAny<IRestRequest>(), RequestType.GetUserKeyPair, 0))
                .Returns(FactoryUser.ApiUserKeyPair);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CheckUserKeyPair(Arg.IsAny<UserKeyPair>(), Arg.AnyString)).Returns(false);

            try {
                // ACT
                a.GetAndCheckUserKeyPair();
            } catch (DracoonCryptoException e) {
                // ASSERT
                Assert.Equal(expected, e.ErrorCode.Code);
            }
        }

        [Fact]
        public void GetAndCheckUserKeyPair_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetUserKeyPair()).Returns(FactoryRestSharp.GetUserKeyPairMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUserKeyPair>(Arg.IsAny<IRestRequest>(), RequestType.GetUserKeyPair, 0))
                .Returns(FactoryUser.ApiUserKeyPair);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CheckUserKeyPair(Arg.IsAny<UserKeyPair>(), Arg.AnyString)).Throws(new CryptoException());
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>())).Returns(DracoonCryptoCode.UNKNOWN_ERROR).Occurs(1);

            try {
                // ACT
                a.GetAndCheckUserKeyPair();
            } catch (DracoonCryptoException e) {
                // ASSERT
                Assert.NotEqual(DracoonCryptoCode.INVALID_PASSWORD_ERROR.Code, e.ErrorCode.Code);
                Mock.Assert(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>()));
            }
        }

        #endregion

        #region ValidateTokenValidity

        [Fact]
        public void ValidateTokenValidity() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetAuthenticatedPing()).Returns(FactoryRestSharp.GetAuthenticatedPingMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.GetAuthenticatedPing, 0)).DoNothing().Occurs(1);

            // ACT
            a.ValidateTokenValidity();

            // ASSERT
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetAvatar

        [Fact]
        public void GetAvatar() {
            // ARRANGE
            Bitmap image = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(image);
            imageData.DrawLine(new Pen(Color.Red), 0, 0, 50, 50);
            MemoryStream memoryStream = new MemoryStream();
            byte[] bitmapData;
            using (memoryStream) {
                image.Save(memoryStream, ImageFormat.Bmp);
                bitmapData = memoryStream.ToArray();
            }

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetAvatar()).Returns(FactoryRestSharp.GetAvatarMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAvatarInfo>(Arg.IsAny<IRestRequest>(), RequestType.GetUserAvatar, 0))
                    .Returns(FactoryUser.ApiAvatarInfo).Occurs(1);
            Mock.Arrange(() => c.Builder.ProvideAvatarDownloadWebClient()).Returns(() => {
                DracoonWebClientExtension wc = new DracoonWebClientExtension();
                wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                wc.SetHttpConfigParams(new DracoonHttpConfig());
                return wc;
            }).Occurs(1);
            Mock.Arrange(() => c.Executor.ExecuteWebClientDownload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), RequestType.GetUserAvatar, null, 0))
                .Returns(bitmapData).Occurs(1);

            // ACT
            Image actual = a.GetAvatar();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetAvatarInfo

        [Fact]
        public void GetAvatarInfo() {
            // ARRANGE
            AvatarInfo expected = FactoryUser.AvatarInfo;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetAvatar()).Returns(FactoryRestSharp.GetAvatarMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAvatarInfo>(Arg.IsAny<IRestRequest>(), RequestType.GetUserAvatar, 0))
                    .Returns(FactoryUser.ApiAvatarInfo).Occurs(1);
            Mock.Arrange(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>())).Returns(FactoryUser.AvatarInfo).Occurs(1);

            // ACT
            AvatarInfo actual = a.GetAvatarInfo();

            // ASSERT
            Assert.Equal(expected, actual, new AvatarInfoComparer());
            Mock.Assert(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region ResetAvatar

        [Fact]
        public void ResetAvatar() {
            // ARRANGE
            AvatarInfo expected = FactoryUser.AvatarInfo;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.DeleteAvatar()).Returns(FactoryRestSharp.DeleteAvatarMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAvatarInfo>(Arg.IsAny<IRestRequest>(), RequestType.DeleteUserAvatar, 0))
                    .Returns(FactoryUser.ApiAvatarInfo).Occurs(1);
            Mock.Arrange(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>())).Returns(FactoryUser.AvatarInfo).Occurs(1);

            // ACT
            AvatarInfo actual = a.ResetAvatar();

            // ASSERT
            Assert.Equal(expected, actual, new AvatarInfoComparer());
            Mock.Assert(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region UpdateAvatar

        [Fact]
        public void UpdateAvatar() {
            // ARRANGE
            Bitmap image = new Bitmap(50, 50);
            Graphics imageData = Graphics.FromImage(image);
            imageData.DrawLine(new Pen(Color.Red), 0, 0, 50, 50);
            MemoryStream ms = new MemoryStream();
            image.Save(ms, ImageFormat.Bmp);

            AvatarInfo expected = FactoryUser.AvatarInfo;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.ProvideAvatarUploadWebClient(Arg.AnyString)).Returns((string x) => {
                DracoonWebClientExtension wc = new DracoonWebClientExtension();
                wc.Headers.Add(HttpRequestHeader.ContentType, "multipart/form-data; boundary=" + x);
                wc.Headers.Add(ApiConfig.AuthorizationHeader, FactoryClients.OAuthMock.BuildAuthString());
                wc.Headers.Add(HttpRequestHeader.UserAgent, new DracoonHttpConfig().UserAgent);
                wc.SetHttpConfigParams(new DracoonHttpConfig());
                return wc;
            }).Occurs(1);
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(), RequestType.PostUserAvatar, null, 0))
                    .Returns(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(FactoryUser.ApiAvatarInfo))).Occurs(1);
            Mock.Arrange(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>())).Returns(FactoryUser.AvatarInfo).Occurs(1);

            // ACT
            AvatarInfo actual = a.UpdateAvatar(Image.FromStream(ms));

            // ASSERT
            Assert.Equal(expected, actual, new AvatarInfoComparer());
            Mock.Assert(() => UserMapper.FromApiAvatarInfo(Arg.IsAny<ApiAvatarInfo>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            ms.Close();
        }

        #endregion

        #region GetUserProfileAttributeList

        [Fact]
        public void GetUserProfileAttributeList() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => c.Builder.GetUserProfileAttributes()).Returns(FactoryRestSharp.GetUserProfileAttributes()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAttributeList>(Arg.IsAny<IRestRequest>(), RequestType.GetUserProfileAttributes, 0))
                .Returns(FactoryAttribute.ApiAttributeList);
            Mock.Arrange(() => AttributeMapper.FromApiAttributeList(Arg.IsAny<ApiAttributeList>())).Returns(FactoryAttribute.AttributeList).Occurs(1);

            // ACT
            AttributeList actual = a.GetUserProfileAttributeList();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => AttributeMapper.FromApiAttributeList(Arg.IsAny<ApiAttributeList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetUserProfileAttribute

        [Fact]
        public void GetUserProfileAttribute() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetUserProfileAttribute(Arg.AnyString)).Returns(FactoryRestSharp.GetUserProfileAttribute(FactoryAttribute.AttributeList.Items[0].Key)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiAttributeList>(Arg.IsAny<IRestRequest>(), RequestType.GetUserProfileAttributes, 0))
                .Returns(FactoryAttribute.ApiAttributeList);
            Mock.Arrange(() => AttributeMapper.FromApiAttributeList(Arg.IsAny<ApiAttributeList>())).Returns(FactoryAttribute.AttributeList).Occurs(1);

            // ACT
            Attribute actual = a.GetUserProfileAttribute(FactoryAttribute.AttributeList.Items[0].Key);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => AttributeMapper.FromApiAttributeList(Arg.IsAny<ApiAttributeList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region AddOrUpdateUserProfileAttributes

        [Fact]
        public void AddOrUpdateUserProfileAttributes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNull(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => c.Builder.PutUserProfileAttributes(Arg.IsAny<ApiAddOrUpdateAttributeRequest>()))
                .Returns(FactoryRestSharp.PutUserProfileAttributes()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PutUserProfileAttributes, 0))
                .DoNothing().Occurs(1);

            // ACT
            a.AddOrUpdateUserProfileAttributes(FactoryAttribute.AttributeList.Items);

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.AnyString.MustNotNull(Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeleteProfileAttribute

        [Fact]
        public void DeleteProfileAttribute() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonAccountImpl a = new DracoonAccountImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteUserProfileAttributes(Arg.AnyString)).Returns(FactoryRestSharp.DeleteUserProfileAttribute(FactoryAttribute.AttributeList.Items[0].Key)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.DeleteUserProfileAttributes, 0))
                .DoNothing().Occurs(1);

            // ACT
            a.DeleteProfileAttribute(FactoryAttribute.AttributeList.Items[0].Key);

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion
    }
}