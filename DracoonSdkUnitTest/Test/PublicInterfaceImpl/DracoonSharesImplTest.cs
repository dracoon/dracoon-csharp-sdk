using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.Sort;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using System;
using System.Collections.Generic;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonSharesImplTest {
        #region CreateDownloadShare

        [Fact]
        public void CreateDownloadShare_Success() {
            // ARRANGE
            Node node = FactoryNode.Node;
            CreateDownloadShareRequest req = FactoryShare.CreateDownloadShareRequest;
            req.TextMessageRecipients = null;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => ShareMapper.ToUnencryptedApiCreateDownloadShareRequest(Arg.IsAny<CreateDownloadShareRequest>()))
                .Returns(FactoryShare.ApiCreateDownloadShareRequest).Occurs(1);
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048).Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => c.NodesImpl.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<long?>())).Returns(FactoryFile.PlainFileKey).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetPreferredUserKeyPairAlgorithm()).Returns(UserKeyPairAlgorithm.RSA4096).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GenerateNewUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>(), Arg.AnyString)).Returns(FactoryUser.UserKeyPair_2048);
            Mock.Arrange(() => c.NodesImpl.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>(), Arg.IsAny<long?>())).Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>())).Returns(FactoryUser.ApiUserKeyPair_2048).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>())).Returns(FactoryFile.ApiFileKey).Occurs(1);
            Mock.Arrange(() => c.Builder.PostCreateDownloadShare(Arg.IsAny<ApiCreateDownloadShareRequest>())).Returns(FactoryRestSharp.PostCreateDownloadShareMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadShare>(Arg.IsAny<IRestRequest>(), RequestType.PostCreateDownloadShare, 0))
                    .Returns(FactoryShare.ApiDownloadShare).Occurs(1);
            Mock.Arrange(() => ShareMapper.FromApiDownloadShare(Arg.IsAny<ApiDownloadShare>())).Returns(FactoryShare.DownloadShare).Occurs(1);

            // ACT
            DownloadShare actual = s.CreateDownloadShare(req);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => ShareMapper.ToUnencryptedApiCreateDownloadShareRequest(Arg.IsAny<CreateDownloadShareRequest>()));
            Mock.Assert(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>()));
            Mock.Assert(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>()));
            Mock.Assert(() => ShareMapper.FromApiDownloadShare(Arg.IsAny<ApiDownloadShare>()));
            Mock.Assert(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>()));
            Mock.Assert(c.NodesImpl);
            Mock.Assert(c.AccountImpl);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateDownloadShare_EncryptedNoFile_Fail() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.IsEncrypted = true;
            node.Type = NodeType.Room;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);

            try {
                // ACT
                s.CreateDownloadShare(FactoryShare.CreateDownloadShareRequest);
            } catch (DracoonApiException e) {
                // ASSERT
                Assert.Equal(DracoonApiCode.VALIDATION_DL_SHARE_CANNOT_CREATE_ON_ENCRYPTED_ROOM_FOLDER, e.ErrorCode);
                Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
                Mock.Assert(c.NodesImpl);
                Mock.Assert(c.Executor);
            }
        }

        [Fact]
        public void CreateDownloadShare_EncryptedNoPw_Fail() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.IsEncrypted = true;
            node.Type = NodeType.File;
            CreateDownloadShareRequest req = FactoryShare.CreateDownloadShareRequest;
            req.Password = null;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateDownloadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(c.NodesImpl);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateDownloadShare_EncryptedTextMessage_Fail() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.IsEncrypted = true;
            node.Type = NodeType.File;
            CreateDownloadShareRequest req = FactoryShare.CreateDownloadShareRequest;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateDownloadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(c.NodesImpl);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateDownloadShare_EmptyRecipient_Fail() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.IsEncrypted = false;
            node.Type = NodeType.File;
            CreateDownloadShareRequest req = FactoryShare.CreateDownloadShareRequest;
            req.TextMessageRecipients = new List<string> { "" };
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateDownloadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(c.NodesImpl);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateDownloadShare_TextMessagePWEmpty_Fail() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.IsEncrypted = false;
            node.Type = NodeType.File;
            CreateDownloadShareRequest req = FactoryShare.CreateDownloadShareRequest;
            req.Password = null;
            req.TextMessageRecipients = new List<string> { "" };
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateDownloadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateDownloadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(c.NodesImpl);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeleteDownloadShare

        [Fact]
        public void DeleteDownloadShare() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteDownloadShare(Arg.AnyLong)).Returns(FactoryRestSharp.DeleteDownloadShareMock(123));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.DeleteDownloadShare, 0)).DoNothing().Occurs(1);

            // ACT
            s.DeleteDownloadShare(5);

            // ASSERT
            // No exception should be thrown
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetDownloadShares

        [Fact]
        public void GetDownloadShares() {
            // ARRANGE
            DownloadShareList expected = FactoryShare.DownloadShareList;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetDownloadShares(Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetDownloadSharesFilter>(), Arg.IsAny<SharesSort>()))
                    .Returns(FactoryRestSharp.GetDownloadSharesMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadShareList>(Arg.IsAny<IRestRequest>(), RequestType.GetDownloadShares, 0))
                    .Returns(FactoryShare.ApiDownloadShareList).Occurs(1);
            Mock.Arrange(() => ShareMapper.FromApiDownloadShareList(Arg.IsAny<ApiDownloadShareList>())).Returns(FactoryShare.DownloadShareList).Occurs(1);

            // ACT
            DownloadShareList actual = s.GetDownloadShares();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => ShareMapper.FromApiDownloadShareList(Arg.IsAny<ApiDownloadShareList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region CreateUploadShare

        [Fact]
        public void CreateUploadShare_Success() {
            // ARRANGE
            CreateUploadShareRequest req = FactoryShare.CreateUploadShareRequest;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().OccursAtLeast(3);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => ShareMapper.ToApiCreateUploadShareRequest(Arg.IsAny<CreateUploadShareRequest>()))
                .Returns(FactoryShare.ApiCreateUploadShareRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PostCreateUploadShare(Arg.IsAny<ApiCreateUploadShareRequest>())).Returns(FactoryRestSharp.PostCreateUploadShareMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadShare>(Arg.IsAny<IRestRequest>(), RequestType.PostCreateUploadShare, 0))
                    .Returns(FactoryShare.ApiUploadShare).Occurs(1);
            Mock.Arrange(() => ShareMapper.FromApiUploadShare(Arg.IsAny<ApiUploadShare>())).Returns(FactoryShare.UploadShare).Occurs(1);

            // ACT
            UploadShare actual = s.CreateUploadShare(req);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => ShareMapper.ToApiCreateUploadShareRequest(Arg.IsAny<CreateUploadShareRequest>()));
            Mock.Assert(() => ShareMapper.FromApiUploadShare(Arg.IsAny<ApiUploadShare>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateUploadShare_TextMessagePWEmpty_Fail() {
            // ARRANGE
            CreateUploadShareRequest req = FactoryShare.CreateUploadShareRequest;
            req.Password = null;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateUploadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateUploadShare_EmptyRecipient_Fail() {
            // ARRANGE
            CreateUploadShareRequest req = FactoryShare.CreateUploadShareRequest;
            req.TextMessageRecipients = new List<string> { "" };
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => s.CreateUploadShare(req));
            Mock.Assert(() => Arg.IsAny<CreateUploadShareRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeleteUploadShare

        [Fact]
        public void DeleteUploadShare() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteUploadShare(Arg.AnyLong)).Returns(FactoryRestSharp.DeleteUploadShareMock(2134)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.DeleteUploadShare, 0)).DoNothing().Occurs(1);

            // ACT
            s.DeleteUploadShare(5);

            // ASSERT
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetUploadShares

        [Fact]
        public void GetUploadShares() {
            // ARRANGE
            UploadShareList expected = FactoryShare.UploadShareList;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetUploadShares(Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetUploadSharesFilter>(), Arg.IsAny<SharesSort>())).Returns(FactoryRestSharp.GetUploadSharesMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadShareList>(Arg.IsAny<IRestRequest>(), RequestType.GetUploadShares, 0))
                .Returns(FactoryShare.ApiUploadShareList).Occurs(1);
            Mock.Arrange(() => ShareMapper.FromApiUploadShareList(Arg.IsAny<ApiUploadShareList>())).Returns(FactoryShare.UploadShareList).Occurs(1);

            // ACT
            UploadShareList actual = s.GetUploadShares();

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => ShareMapper.FromApiUploadShareList(Arg.IsAny<ApiUploadShareList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DownloadShareMail

        [Fact]
        public void SendDownloadShareMail_Success() {
            // ARRANGE

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<MailShareInfoRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().OccursAtLeast(3);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<string>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.PostMailDownloadShare(Arg.AnyLong, Arg.IsAny<ApiMailShareInfoRequest>())).Returns(FactoryRestSharp.PostMailDownloadShare(123)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PostMailDownloadShare, 0)).DoNothing().Occurs(1);
            Mock.Arrange(() => ShareMapper.ToApiMailShareInfoRequest(Arg.IsAny<MailShareInfoRequest>())).Returns(FactoryShare.ApiMailShareInfoRequest).Occurs(1);

            // ACT
            s.SendMailForDownloadShare(FactoryShare.MailShareInfoRequest);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<MailShareInfoRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<IEnumerable<string>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => ShareMapper.ToApiMailShareInfoRequest(Arg.IsAny<MailShareInfoRequest>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region UploadShareMail

        [Fact]
        public void SendUploadShareMail_Success() {
            // ARRANGE

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonSharesImpl s = new DracoonSharesImpl(c);
            Mock.Arrange(() => Arg.IsAny<MailShareInfoRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool)).DoNothing().OccursAtLeast(3);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<string>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.PostMailUploadShare(Arg.AnyLong, Arg.IsAny<ApiMailShareInfoRequest>())).Returns(FactoryRestSharp.PostMailUploadShare(123)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PostMailUploadShare, 0)).DoNothing().Occurs(1);
            Mock.Arrange(() => ShareMapper.ToApiMailShareInfoRequest(Arg.IsAny<MailShareInfoRequest>())).Returns(FactoryShare.ApiMailShareInfoRequest).Occurs(1);

            // ACT
            s.SendMailForUploadShare(FactoryShare.MailShareInfoRequest);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<MailShareInfoRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, Arg.AnyBool));
            Mock.Assert(() => Arg.IsAny<IEnumerable<string>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => ShareMapper.ToApiMailShareInfoRequest(Arg.IsAny<MailShareInfoRequest>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion
    }
}