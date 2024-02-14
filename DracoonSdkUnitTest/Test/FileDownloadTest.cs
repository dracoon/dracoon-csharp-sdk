using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Threading;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test {
    public class FileDownloadTest {
        #region RunSync

        [Fact]
        public void RunSync_Success() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken).Occurs(1);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<DownloadProgressChangedEventArgs>().BytesReceived).IgnoreInstance().Returns(expected.Length);
            Mock.Arrange(() => c.Builder.ProvideChunkDownloadWebClient(Arg.AnyLong, Arg.AnyLong)).Returns(wc);
            Mock.Arrange(() => c.Executor.ExecuteWebClientDownload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(),
                    RequestType.GetDownloadChunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.DownloadProgressChanged += null, null, Mock.Create<DownloadProgressChangedEventArgs>()).Returns(expected);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();
            s.Position = 0;
            byte[] actual = new byte[expected.Length];
            s.Read(actual, 0, expected.Length);
            s.Close();

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void RunSync_Fail() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartDownload").Throws(new DracoonException());

            Mock.Arrange(() => callback.OnFailed(Arg.AnyString, Arg.IsAny<DracoonException>())).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonException>(() => f.RunSync());
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        [Fact]
        public void RunSync_Aborted() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartDownload").Throws(Mock.Create<ThreadAbortException>());
            Mock.Arrange(() => callback.OnCanceled(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();

            // ASSERT
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        [Fact]
        public void RunSync_Interrupted() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartDownload").Throws(Mock.Create<ThreadInterruptedException>());
            Mock.Arrange(() => callback.OnCanceled(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();

            // ASSERT
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        #endregion

        #region RunAsync

        [Fact]
        public void RunAsync_IOException_Interrupted() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(123)).OnAllThreads();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken).OnAllThreads();
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<DownloadProgressChangedEventArgs>().BytesReceived).IgnoreInstance().Returns(expected.Length).OnAllThreads();
            Mock.Arrange(() => c.Executor.ExecuteWebClientDownload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(),
                RequestType.GetDownloadChunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => { while (f.RunningThread.IsAlive) { } }).OnAllThreads();
            Mock.Arrange(() => c.Builder.ProvideChunkDownloadWebClient(Arg.AnyLong, Arg.AnyLong)).Returns(wc).OnAllThreads();
            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnCanceled(Arg.AnyString)).Occurs(1);


            // ACT
            f.RunAsync();
            Thread.Sleep(5000);
            f.CancelDownload();
            s.Close();

            // ASSERT
            Mock.Assert(callback);
        }

        [Fact]
        public void RunAsync_IOException() {
            // ARRANGE
            byte[] expected = new byte[FactoryNode.Node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            FileDownload f = new FileDownload(c, "id1", FactoryNode.Node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(123)).OnAllThreads();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken).OnAllThreads();
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<DownloadProgressChangedEventArgs>().BytesReceived).IgnoreInstance().Returns(expected.Length);
            Mock.Arrange(() => c.Executor.ExecuteWebClientDownload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(),
                    RequestType.GetDownloadChunk, Arg.IsAny<Thread>(), Arg.AnyInt))
                .Raises(() => wc.DownloadProgressChanged += null, null, Mock.Create<DownloadProgressChangedEventArgs>()).Returns(expected).OnAllThreads();
            Mock.Arrange(() => s.Write(Arg.IsAny<byte[]>(), Arg.AnyInt, Arg.AnyInt)).Throws(new IOException()).OnAllThreads();
            Mock.Arrange(() => c.Builder.ProvideChunkDownloadWebClient(Arg.AnyLong, Arg.AnyLong)).Returns(wc).OnAllThreads();
            Mock.Arrange(() => callback.OnFailed(Arg.AnyString, Arg.IsAny<DracoonException>())).Occurs(1);

            // ACT
            f.RunAsync();
            while (f.RunningThread.IsAlive) { }
            s.Close();

            // ASSERT
            Mock.Assert(callback);
        }

        #endregion

        #region Encrypted Download

        [Fact]
        public void Encrypted_RunSync_Success() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.Size = 1024;
            byte[] expected = new byte[node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            EncFileDownload f = new EncFileDownload(c, "id1", node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken).Occurs(1);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048).Occurs(1);
            FileDecryptionCipher cipher = Mock.Create<FileDecryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()))
                .Returns(FactoryFile.PlainFileKey).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileDecryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher).Occurs(1);
            Mock.NonPublic.Arrange<byte[]>(f, "DownloadChunk", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<long>(), ArgExpr.IsAny<long>()).Returns(expected);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<EncryptedDataContainer>())).Returns(new PlainDataContainer(expected)).OccursAtLeast(1);
            Mock.Arrange(() => cipher.DoFinal(Arg.IsAny<EncryptedDataContainer>())).Returns(new PlainDataContainer(new byte[0])).Occurs(1);
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048).Occurs(1);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();
            s.Position = 0;
            byte[] actual = new byte[expected.Length];
            s.Read(actual, 0, expected.Length);
            s.Close();

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>()));
            Mock.Assert(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()));
            Mock.Assert(() => Crypto.Sdk.Crypto.CreateFileDecryptionCipher(Arg.IsAny<PlainFileKey>()));
            Mock.Assert(() => c.AccountImpl);
            Mock.Assert(callback);
            Mock.Assert(cipher);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            Mock.Assert(c.NodesImpl);
        }

        [Fact]
        public void Encrypted_RunSync_DecryptFileKeyError() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.Size = 1024;
            byte[] expected = new byte[node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            EncFileDownload f = new EncFileDownload(c, "id1", node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey);
            FileDecryptionCipher cipher = Mock.Create<FileDecryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()))
                .Throws(new CryptoException("Error"));
            Mock.NonPublic.Arrange<byte[]>(f, "DownloadChunk", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<long>(), ArgExpr.IsAny<long>()).Returns(expected);
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048);

            // ACT
            Assert.Throws<DracoonCryptoException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_CreateFileDecryptionCipherError() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.Size = 1024;
            byte[] expected = new byte[node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            EncFileDownload f = new EncFileDownload(c, "id1", node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey);
            FileDecryptionCipher cipher = Mock.Create<FileDecryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()))
                .Returns(FactoryFile.PlainFileKey);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileDecryptionCipher(Arg.IsAny<PlainFileKey>())).Throws(new CryptoException("Error"));
            Mock.NonPublic.Arrange<byte[]>(f, "DownloadChunk", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<long>(), ArgExpr.IsAny<long>()).Returns(expected);
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048);

            // ACT
            Assert.Throws<DracoonCryptoException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_ProcessBytesError() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.Size = 1024;
            byte[] expected = new byte[node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            EncFileDownload f = new EncFileDownload(c, "id1", node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey);
            FileDecryptionCipher cipher = Mock.Create<FileDecryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()))
                .Returns(FactoryFile.PlainFileKey);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileDecryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher);
            Mock.NonPublic.Arrange<byte[]>(f, "DownloadChunk", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<long>(), ArgExpr.IsAny<long>()).Returns(expected);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<EncryptedDataContainer>())).Throws(new CryptoException("Error"));
            Mock.Arrange(() => cipher.DoFinal(Arg.IsAny<EncryptedDataContainer>())).Returns(new PlainDataContainer(new byte[0]));
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048);

            // ACT
            Assert.Throws<DracoonFileIOException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_ProcessBytesIOError() {
            // ARRANGE
            Node node = FactoryNode.Node;
            node.Size = 1024;
            byte[] expected = new byte[node.Size.Value];
            new Random().NextBytes(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            Stream s = new MemoryStream();
            FileDownloadCallbackMock callback = new FileDownloadCallbackMock();
            EncFileDownload f = new EncFileDownload(c, "id1", node, s);
            f.AddFileDownloadCallback(callback);
            Mock.Arrange(() => c.Builder.PostFileDownload(Arg.AnyLong)).Returns(FactoryRestSharp.PostFileDownloadMock(2354));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDownloadToken>(Arg.IsAny<RestRequest>(), RequestType.PostDownloadToken, 0))
                    .Returns(FactoryNode.ApiDownloadToken);
            Mock.Arrange(() => c.NodesImpl.GetEncryptedFileKey(Arg.AnyLong)).Returns(FactoryFile.EncryptedFileKey);
            FileDecryptionCipher cipher = Mock.Create<FileDecryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<byte[]>()))
                .Returns(FactoryFile.PlainFileKey);
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileDecryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher);
            Mock.NonPublic.Arrange<byte[]>(f, "DownloadChunk", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<long>(), ArgExpr.IsAny<long>()).Returns(expected);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<EncryptedDataContainer>())).Throws(new IOException("Error"));
            Mock.Arrange(() => cipher.DoFinal(Arg.IsAny<EncryptedDataContainer>())).Returns(new PlainDataContainer(new byte[0]));
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>())).Returns(FactoryUser.UserKeyPair_2048);

            // ACT
            Assert.Throws<DracoonFileIOException>(() => f.RunSync());
            s.Close();
        }

        #endregion
    }
}