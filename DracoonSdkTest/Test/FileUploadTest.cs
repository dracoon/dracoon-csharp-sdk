using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Newtonsoft.Json;
using RestSharp;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test {
    public class FileUploadTest {
        #region RunSync

        [Fact]
        public void RunSync_KnownFileSize_Success() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, fileMock.Length);
            f.AddFileUploadCallback(callback);
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteUpload, 0))
                .Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(true).OccursAtLeast(1);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PostUploadChunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideChunkUploadWebClient(Arg.AnyInt, Arg.AnyLong, Arg.AnyString, Arg.AnyString)).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString, Arg.IsAny<Node>())).Occurs(1);

            // ACT
            Node actual = f.RunSync();
            s.Close();

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void RunSync_UnknownFileSize_Success() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, -1);
            f.AddFileUploadCallback(callback);
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteUpload, 0))
                .Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(true).OccursAtLeast(1);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PostUploadChunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideChunkUploadWebClient(Arg.AnyInt, Arg.AnyLong, Arg.AnyString, Arg.AnyString)).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString, Arg.IsAny<Node>())).Occurs(1);

            // ACT
            Node actual = f.RunSync();
            s.Close();

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void RunSync_WrongFileHash() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, -1);
            f.AddFileUploadCallback(callback);
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => c.Builder.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path"));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteUpload, 0))
                .Returns(FactoryNode.ApiNode);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(false);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PostUploadChunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideChunkUploadWebClient(Arg.AnyInt, Arg.AnyLong, Arg.AnyString, Arg.AnyString)).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);

            // ACT - ASSERT
            Assert.Throws<DracoonNetIOException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void RunSync_Fail() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, -1);
            f.AddFileUploadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartUpload").Throws(new DracoonException());
            Mock.Arrange(() => callback.OnFailed(Arg.AnyString, Arg.IsAny<DracoonException>())).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonException>(() => f.RunSync());
            s.Close();
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        [Fact]
        public void RunSync_Aborted() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, -1);
            f.AddFileUploadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartUpload").Throws(Mock.Create<ThreadAbortException>());
            Mock.Arrange(() => callback.OnCanceled(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();
            s.Close();

            // ASSERT
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        [Fact]
        public void RunSync_Interrupted() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, -1);
            f.AddFileUploadCallback(callback);
            Mock.NonPublic.Arrange(f, "StartUpload").Throws(Mock.Create<ThreadInterruptedException>());
            Mock.Arrange(() => callback.OnCanceled(Arg.AnyString)).Occurs(1);

            // ACT
            f.RunSync();
            s.Close();

            // ASSERT
            Mock.Assert(f);
            Mock.Assert(callback);
        }

        #endregion

        #region RunAsync

        [Fact]
        public void RunAsync_IOException() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .OnAllThreads();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).OnAllThreads();
            Mock.Arrange(() => c.Builder.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).OnAllThreads();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteUpload, 0))
                .Returns(FactoryNode.ApiNode).OnAllThreads();
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).OnAllThreads();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).OnAllThreads();
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(FactoryFile.ApiCreateFileUpload)
                .OnAllThreads();
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).OnAllThreads();
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(true).OnAllThreads();
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance().OnAllThreads();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PostUploadChunk, Arg.IsAny<Thread>(), Arg.AnyInt))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13])
                .OnAllThreads();
            Mock.Arrange(() => c.Builder.ProvideChunkUploadWebClient(Arg.AnyInt, Arg.AnyLong, Arg.AnyString, Arg.AnyString)).Returns(wc)
                .OnAllThreads();
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult)
                .OnAllThreads();
            Mock.Arrange(() => s.Read(Arg.IsAny<byte[]>(), Arg.AnyInt, Arg.AnyInt)).Throws(new IOException()).OnAllThreads();
            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnFailed(Arg.AnyString, Arg.IsAny<DracoonException>())).Occurs(1);

            // ACT
            f.RunAsync();
            while (f.RunningThread.IsAlive) { }

            s.Close();

            // ASSERT
            Mock.Assert(callback);
        }

        #endregion

        #region Encrypted Upload

        [Fact]
        public void Encrypted_RunSync_KnownFileSize_Success() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey).Occurs(1);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher).Occurs(1);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<PlainDataContainer>())).Returns(new EncryptedDataContainer(fileMock, fileMock))
                .OccursAtLeast(1);
            Mock.Arrange(() => cipher.DoFinal()).Returns(new EncryptedDataContainer(fileMock, fileMock)).OccursAtLeast(1);
            Mock.NonPublic.Arrange<ApiUploadChunkResult>(f, "UploadChunkWebClient", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<byte[]>(),
                ArgExpr.IsAny<long>(), ArgExpr.IsAny<int>()).Returns(FactoryFile.ApiUploadChunkResult).OccursAtLeast(1);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(true).OccursAtLeast(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()))
                .Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCompleteFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(FactoryFile.ApiCompleteFileUpload)
                .Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>())).Returns(FactoryFile.ApiFileKey);
            Mock.Arrange(() => c.Builder.PutCompleteFileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteUpload, 0))
                .Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString, Arg.IsAny<Node>())).Occurs(1);

            // ACT
            Node actual = f.RunSync();
            s.Close();

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>()));
            Mock.Assert(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString));
            Mock.Assert(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>()));
            Mock.Assert(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt));
            Mock.Assert(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()));
            Mock.Assert(() => FileMapper.ToApiCompleteFileUpload(Arg.IsAny<FileUploadRequest>()));
            Mock.Assert(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(callback);
            Mock.Assert(cipher);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            Mock.Assert(c.NodesImpl);
        }

        [Fact]
        public void Encrypted_RunSync_CreateFileKeyError() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Throws(new CryptoException("Error"));

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_EncryptFileKeyError() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<PlainDataContainer>())).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.Arrange(() => cipher.DoFinal()).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.NonPublic.Arrange<ApiUploadChunkResult>(f, "UploadChunkWebClient", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<byte[]>(),
                ArgExpr.IsAny<long>(), ArgExpr.IsAny<int>()).Returns(FactoryFile.ApiUploadChunkResult);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(true);
            Mock.Arrange(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()))
                .Throws(new CryptoException("Error"));

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_CreateFileEncryptionCipherError() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Throws(new CryptoException("Error"));

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_WrongFileHash() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<PlainDataContainer>())).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.Arrange(() => cipher.DoFinal()).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.NonPublic.Arrange<ApiUploadChunkResult>(f, "UploadChunkWebClient", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<byte[]>(),
                ArgExpr.IsAny<long>(), ArgExpr.IsAny<int>()).Returns(FactoryFile.ApiUploadChunkResult);
            Mock.Arrange(() => FileHash.CompareFileHashes(Arg.AnyString, Arg.IsAny<byte[]>(), Arg.AnyInt)).Returns(false);

            // ACT - ASSERT
            Assert.Throws<DracoonNetIOException>(() => f.RunSync());
            s.Close();
        }

        [Fact]
        public void Encrypted_RunSync_IOException() {
            // ARRANGE
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu);
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock());
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(FactoryServerSettings.ApiGeneralSettings).Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<PlainDataContainer>())).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.Arrange(() => cipher.DoFinal()).Returns(new EncryptedDataContainer(fileMock, fileMock));
            Mock.NonPublic.Arrange<ApiUploadChunkResult>(f, "UploadChunkWebClient", ArgExpr.IsAny<Uri>(), ArgExpr.IsAny<byte[]>(),
                ArgExpr.IsAny<long>(), ArgExpr.IsAny<int>()).Throws(new IOException());

            // ACT - ASSERT
            Assert.Throws<DracoonFileIOException>(() => f.RunSync());
            s.Close();
        }

        #endregion

        #region S3-Upload

        [Theory]
        [InlineData("done", 2356)]
        [InlineData("done", 27262976)]
        [InlineData("transfer", 2356)]
        [InlineData("finishing", 2356)]
        public void RunSync_S3_KnownFileSize_Success(string statusChecking, int fileSize) {
            // ARRANGE
            ApiS3Status resultStatus = FactoryFile.ApiS3Status;
            switch (statusChecking) {
                case "done":
                    resultStatus.Node = FactoryNode.ApiNode;
                    resultStatus.Status = "done";
                    resultStatus.ErrorInfo = null;
                    break;
                case "transfer":
                    resultStatus.Node = null;
                    resultStatus.Status = "transfer";
                    resultStatus.ErrorInfo = null;
                    break;
                case "finishing":
                    resultStatus.Node = null;
                    resultStatus.Status = "finishing";
                    resultStatus.ErrorInfo = null;
                    break;
            }
            byte[] fileMock = new byte[fileSize];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, fileMock.Length);
            f.AddFileUploadCallback(callback);
            ApiGeneralSettings generalSettings = FactoryServerSettings.ApiGeneralSettings;
            generalSettings.UseS3Storage = true;
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.PutCompleteS3FileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteS3Upload, 0)).DoNothing()
                .Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(generalSettings).Occurs(1);
            Mock.Arrange(() => c.Builder.PostGetS3Urls(Arg.AnyString, Arg.IsAny<ApiGetS3Urls>())).Returns(FactoryRestSharp.PostGetS3UrlsMock())
                .OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Urls>(Arg.IsAny<IRestRequest>(), RequestType.PostGetS3Urls, 0))
                .Returns(FactoryFile.ApiS3Urls).OccursAtLeast(1);
            Mock.Arrange(() => c.Builder.GetS3Status(Arg.AnyString)).Returns(FactoryRestSharp.GetS3StatusMock).OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Status>(Arg.IsAny<IRestRequest>(), RequestType.GetS3Status, 0))
                .Returns(resultStatus).OccursAtLeast(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCompleteFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(FactoryFile.ApiCompleteFileUpload);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PutUploadS3Chunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideS3ChunkUploadWebClient()).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);
            Mock.Arrange(() => new Stopwatch().Restart()).IgnoreInstance().DoInstead(() => {
                resultStatus.Node = FactoryNode.ApiNode;
                resultStatus.Status = "done";
                resultStatus.ErrorInfo = null;
            });

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString, Arg.IsAny<Node>())).Occurs(1);

            // ACT
            Node actual = f.RunSync();
            s.Close();

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void RunSync_S3_PollingError() {
            // ARRANGE
            ApiS3Status resultStatus = FactoryFile.ApiS3Status;
            resultStatus.Node = null;
            resultStatus.Status = "error";
            resultStatus.ErrorInfo = null;
            byte[] fileMock = new byte[1888];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            FileUpload f = new FileUpload(c, "id1", FactoryFile.UploadFileRequest, s, fileMock.Length);
            f.AddFileUploadCallback(callback);
            ApiGeneralSettings generalSettings = FactoryServerSettings.ApiGeneralSettings;
            generalSettings.UseS3Storage = true;
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.PutCompleteS3FileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteS3Upload, 0)).DoNothing()
                .Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(generalSettings).Occurs(1);
            Mock.Arrange(() => c.Builder.PostGetS3Urls(Arg.AnyString, Arg.IsAny<ApiGetS3Urls>())).Returns(FactoryRestSharp.PostGetS3UrlsMock())
                .OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Urls>(Arg.IsAny<IRestRequest>(), RequestType.PostGetS3Urls, 0))
                .Returns(FactoryFile.ApiS3Urls).OccursAtLeast(1);
            Mock.Arrange(() => c.Builder.GetS3Status(Arg.AnyString)).Returns(FactoryRestSharp.GetS3StatusMock).OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Status>(Arg.IsAny<IRestRequest>(), RequestType.GetS3Status, 0))
                .Returns(resultStatus).OccursAtLeast(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCompleteFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(FactoryFile.ApiCompleteFileUpload);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PutUploadS3Chunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideS3ChunkUploadWebClient()).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);
            Mock.Arrange(() => DracoonErrorParser.ParseError(Arg.IsAny<ApiErrorResponse>(), RequestType.GetS3Status)).DoNothing();

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFailed(Arg.AnyString, Arg.IsAny<DracoonException>())).Occurs(1);

            // ACT
            Assert.Throws<DracoonApiException>(() => f.RunSync());
            s.Close();

            // ASSERT
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void Encrypted_RunSync_S3_KnownFileSize_Success() {
            // ARRANGE
            byte[] fileMock = new byte[1564];
            new Random().NextBytes(fileMock);
            Stream s = new MemoryStream(fileMock);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            FileUploadCallbackMock callback = new FileUploadCallbackMock();
            EncFileUpload f = new EncFileUpload(c, "id1", FactoryFile.UploadFileRequest, s, FactoryUser.UserPublicKey, fileMock.Length);
            f.AddFileUploadCallback(callback);
            ApiGeneralSettings generalSettings = FactoryServerSettings.ApiGeneralSettings;
            generalSettings.UseS3Storage = true;
            ApiCreateFileUpload acfu = FactoryFile.ApiCreateFileUpload;
            acfu.Classification = null;
            Mock.Arrange(() => c.Builder.PostCreateFileUpload(Arg.IsAny<ApiCreateFileUpload>())).Returns(FactoryRestSharp.PostCreateFileUploadMock())
                .Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiUploadToken>(Arg.IsAny<IRestRequest>(), RequestType.PostUploadToken, 0))
                .Returns(FactoryFile.ApiUploadToken).Occurs(1);
            Mock.Arrange(() => c.Builder.PutCompleteS3FileUpload(Arg.AnyString, Arg.IsAny<ApiCompleteFileUpload>()))
                .Returns(FactoryRestSharp.PutCompleteFileUploadMock("path")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<IRestRequest>(), RequestType.PutCompleteS3Upload, 0)).DoNothing()
                .Occurs(1);
            Mock.Arrange(() => c.Builder.GetGeneralSettings())
                .Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetGeneralConfig, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiGeneralSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetGeneralSettings, 0))
                .Returns(generalSettings).Occurs(1);
            Mock.Arrange(() => c.Builder.PostGetS3Urls(Arg.AnyString, Arg.IsAny<ApiGetS3Urls>())).Returns(FactoryRestSharp.PostGetS3UrlsMock())
                .OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Urls>(Arg.IsAny<IRestRequest>(), RequestType.PostGetS3Urls, 0))
                .Returns(FactoryFile.ApiS3Urls).OccursAtLeast(1);
            Mock.Arrange(() => c.Builder.GetS3Status(Arg.AnyString)).Returns(FactoryRestSharp.GetS3StatusMock).OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiS3Status>(Arg.IsAny<IRestRequest>(), RequestType.GetS3Status, 0))
                .Returns(FactoryFile.ApiS3Status).OccursAtLeast(1);
            Mock.Arrange(() => FileMapper.ToApiCreateFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(acfu).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiCompleteFileUpload(Arg.IsAny<FileUploadRequest>())).Returns(FactoryFile.ApiCompleteFileUpload);
            DracoonWebClientExtension wc = Mock.Create<DracoonWebClientExtension>();
            Mock.Arrange(() => Mock.Create<UploadProgressChangedEventArgs>().BytesSent).IgnoreInstance();
            Mock.Arrange(() => c.Executor.ExecuteWebClientChunkUpload(Arg.IsAny<WebClient>(), Arg.IsAny<Uri>(), Arg.IsAny<byte[]>(),
                    RequestType.PutUploadS3Chunk, Arg.IsAny<Thread>(), Arg.AnyInt)).DoInstead(() => Thread.Sleep(250))
                .Raises(() => wc.UploadProgressChanged += null, null, Mock.Create<UploadProgressChangedEventArgs>()).Returns(new byte[13]);
            Mock.Arrange(() => c.Builder.ProvideS3ChunkUploadWebClient()).Returns(wc);
            Mock.Arrange(() => JsonConvert.DeserializeObject<ApiUploadChunkResult>(Arg.AnyString)).Returns(FactoryFile.ApiUploadChunkResult);

            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateFileKey(Arg.AnyString)).Returns(FactoryFile.PlainFileKey).Occurs(1);
            FileEncryptionCipher cipher = Mock.Create<FileEncryptionCipher>();
            Mock.Arrange(() => Crypto.Sdk.Crypto.CreateFileEncryptionCipher(Arg.IsAny<PlainFileKey>())).Returns(cipher).Occurs(1);
            Mock.Arrange(() => cipher.ProcessBytes(Arg.IsAny<PlainDataContainer>())).Returns(new EncryptedDataContainer(fileMock, fileMock))
                .OccursAtLeast(1);
            Mock.Arrange(() => cipher.DoFinal()).Returns(new EncryptedDataContainer(fileMock, fileMock)).OccursAtLeast(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()))
                .Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>())).Returns(FactoryFile.ApiFileKey);

            Mock.Arrange(() => callback.OnStarted(Arg.AnyString)).Occurs(1);
            Mock.Arrange(() => callback.OnRunning(Arg.AnyString, Arg.AnyLong, Arg.AnyLong)).OccursAtLeast(1);
            Mock.Arrange(() => callback.OnFinished(Arg.AnyString, Arg.IsAny<Node>())).Occurs(1);

            // ACT
            Node actual = f.RunSync();
            s.Close();

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(callback);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion
    }
}