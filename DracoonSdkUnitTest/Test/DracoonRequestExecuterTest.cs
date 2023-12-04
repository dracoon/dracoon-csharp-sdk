using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.UnitTest.Factory;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test {
    public class DracoonRequestExecuterTest {
        #region CheckApiServerVersion

        [Fact]
        public void CheckApiServerVersion_SdkVersionFail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(c.OAuth, c);
            Mock.Arrange(() => c.Builder.GetServerVersion()).Returns(FactoryRestSharp.GetServerVersionMock()).Occurs(1);
            Mock.Arrange(() => exec.DoSyncApiCall<ApiServerVersion>(Arg.IsAny<IRestRequest>(), RequestType.GetServerVersion, 0)).Returns(new ApiServerVersion {
                ServerVersion = "4.5.0",
                RestApiVersion = "4.5.0",
                BuildDate = DateTime.Now
            }).Occurs(1);

            try {
                // ACT
                exec.CheckApiServerVersion();
            } catch (DracoonApiException e) {
                // ASSERT
                Assert.Equal(DracoonApiCode.API_VERSION_NOT_SUPPORTED, e.ErrorCode);
                Mock.Assert(exec);
                Mock.Assert(c.Builder);
            }
        }

        [Fact]
        public void CheckApiServerVersion_FeatureVersionFail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(c.OAuth, c);
            Mock.Arrange(() => c.Builder.GetServerVersion()).Returns(FactoryRestSharp.GetServerVersionMock()).Occurs(1);
            Mock.Arrange(() => exec.DoSyncApiCall<ApiServerVersion>(Arg.IsAny<IRestRequest>(), RequestType.GetServerVersion, 0)).Returns(new ApiServerVersion {
                ServerVersion = "4.12.0",
                RestApiVersion = "4.12.0",
                BuildDate = DateTime.Now
            }).Occurs(1);

            try {
                // ACT
                exec.CheckApiServerVersion("4.13.0");
            } catch (DracoonApiException e) {
                // ASSERT
                Assert.Equal(0, e.ErrorCode.Code);
                Mock.Assert(exec);
                Mock.Assert(c.Builder);
            }
        }

        [Fact]
        public void CheckApiServerVersion_SdkVersionSuccess() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(c.OAuth, c);
            Mock.Arrange(() => c.Builder.GetServerVersion()).Returns(FactoryRestSharp.GetServerVersionMock());
            Mock.Arrange(() => exec.DoSyncApiCall<ApiServerVersion>(Arg.IsAny<IRestRequest>(), RequestType.GetServerVersion, 0)).Returns(new ApiServerVersion {
                ServerVersion = "5.99.0",
                RestApiVersion = "5.99.0",
                BuildDate = DateTime.Now
            });

            // ACT
            exec.CheckApiServerVersion();
            exec.CheckApiServerVersion(); // Double check it because of caching

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void CheckApiServerVersion_FeatureVersionSuccess() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(c.OAuth, c);
            Mock.Arrange(() => c.Builder.GetServerVersion()).Returns(FactoryRestSharp.GetServerVersionMock());
            Mock.Arrange(() => exec.DoSyncApiCall<ApiServerVersion>(Arg.IsAny<IRestRequest>(), RequestType.GetServerVersion, 0)).Returns(new ApiServerVersion {
                ServerVersion = "4.13.0",
                RestApiVersion = "4.13.0",
                BuildDate = DateTime.Now
            });

            // ACT
            exec.CheckApiServerVersion("4.13.0");

            // ASSERT
            // No exception should be thrown
        }

        #endregion

        #region DoSyncApiCall

        [Fact]
        public void DoSyncApiCall_Success() {
            // ARRANGE
            string version = "4.13.0";
            DateTime buildTime = new DateTime(2019, 1, 1, 0, 0, 0);

            ApiServerVersion expected = new ApiServerVersion {
                ServerVersion = version,
                RestApiVersion = version,
                BuildDate = buildTime
            };

            RestResponse response = FactoryRestSharp.RestResponse;
            response.Content = JsonConvert.SerializeObject(expected);
            response.StatusCode = HttpStatusCode.OK;
            Mock.NonPublic.Arrange<bool>(response, "IsSuccessful").Returns(true);
            IRestRequest request = FactoryRestSharp.GetServerVersionMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig {
                WebProxy = new WebProxy()
            });
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT
            ApiServerVersion actual = exec.DoSyncApiCall<ApiServerVersion>(request, RequestType.GetServerVersion);

            // ASSERT
            Assert.Equal(expected.BuildDate, actual.BuildDate);
            Assert.Equal(expected.RestApiVersion, actual.RestApiVersion);
            Assert.Equal(expected.ServerVersion, actual.ServerVersion);
        }

        [Fact]
        public void DoSyncApiCall_Success_VoidResponse() {
            // ARRANGE
            RestResponse response = FactoryRestSharp.RestResponse;
            response.StatusCode = HttpStatusCode.OK;
            Mock.NonPublic.Arrange<bool>(response, "IsSuccessful").Returns(true);
            IRestRequest request = FactoryRestSharp.GetAuthenticatedPingMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT
            exec.DoSyncApiCall<VoidResponse>(request, RequestType.GetServerVersion);

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void DoSyncApiCall_WebException_Fail() {
            // ARRANGE
            WebException we = new WebException();
            RestResponse response = FactoryRestSharp.RestResponse;
            response.ErrorException = we;
            IRestRequest request = FactoryRestSharp.GetServerVersionMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetServerVersion)).Throws(new DracoonApiException());

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.DoSyncApiCall<ApiServerVersion>(request, RequestType.GetServerVersion));
        }

        [Fact]
        public void DoSyncApiCall_OAuth_Fail() {
            // ARRANGE
            RestResponse response = FactoryRestSharp.RestResponse;
            IRestRequest request = FactoryRestSharp.PostOAuthTokenMock("id1", "secret1", "grant", "code1");
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);
            Mock.Arrange(() => OAuthErrorParser.ParseError(response, RequestType.PostOAuthToken)).Throws(new DracoonApiException());

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.DoSyncApiCall<ApiOAuthToken>(request, RequestType.PostOAuthToken));
        }

        [Fact]
        public void DoSyncApiCall_Fail() {
            // ARRANGE
            RestResponse response = FactoryRestSharp.RestResponse;
            IRestRequest request = FactoryRestSharp.GetServerVersionMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);
            Mock.Arrange(() => DracoonErrorParser.ParseError(response, RequestType.GetServerVersion)).Throws(new DracoonApiException());

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.DoSyncApiCall<ApiServerVersion>(request, RequestType.GetServerVersion));
        }

        [Fact]
        public void DoSyncApiCall_Fail_Retry() {
            // ARRANGE
            RestResponse response = FactoryRestSharp.RestResponse;
            IRestRequest request = FactoryRestSharp.GetAuthenticatedPingMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);
            Mock.Arrange(() => FactoryClients.OAuthMock.RefreshAccessToken()).IgnoreInstance().DoNothing();
            Mock.Arrange(() => FactoryClients.OAuthMock.BuildAuthString()).IgnoreInstance().Returns("AuthTest");
            Mock.Arrange(() => DracoonErrorParser.ParseError(response, RequestType.GetAuthenticatedPing))
                .Throws(new DracoonApiException(DracoonApiCode.AUTH_UNAUTHORIZED));

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.DoSyncApiCall<VoidResponse>(request, RequestType.GetAuthenticatedPing));
        }

        [Fact]
        public void DoSyncApiCall_TooManyRequestsFail_Retry() {
            // ARRANGE
            RestResponse response = FactoryRestSharp.RestResponse;
            IRestRequest request = FactoryRestSharp.GetAuthenticatedPingMock();
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig());
            Mock.Arrange(() => new RestClient().Execute(request)).IgnoreInstance().Returns(response);
            Mock.Arrange(() => FactoryClients.OAuthMock.RefreshAccessToken()).IgnoreInstance().DoNothing();
            Mock.Arrange(() => FactoryClients.OAuthMock.BuildAuthString()).IgnoreInstance().Returns("AuthTest");
            Mock.Arrange(() => DracoonErrorParser.ParseError(response, RequestType.GetAuthenticatedPing))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_TOO_MANY_REQUESTS));

            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.DoSyncApiCall<VoidResponse>(request, RequestType.GetAuthenticatedPing));
        }

        #endregion

        #region ExecuteWebClientDownload

        [Fact]
        public void ExecuteWebClientDownload_Success() {
            // ARRANGE
            byte[] expected = Encoding.UTF8.GetBytes("test");
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Returns(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT
            byte[] actual = exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteWebClientDownload_SecureChannelFailure_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance()
                .Throws(new AggregateException(new WebException("Test", WebExceptionStatus.SecureChannelFailure)));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonNetInsecureException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk));
        }

        [Fact]
        public void ExecuteWebClientDownload_RequestCancel_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance()
                .Throws(new AggregateException(new WebException("Test", WebExceptionStatus.RequestCanceled)));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<ThreadInterruptedException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk));
        }

        [Fact]
        public void ExecuteWebClientDownload_ProtocolError_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ProtocolError);
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetDownloadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk));
        }

        [Fact]
        public void ExecuteWebClientDownload_ProtocolError_TooManyRequests_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ProtocolError);
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetDownloadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_TOO_MANY_REQUESTS));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk));
        }

        [Fact]
        public void ExecuteWebClientDownload_ConnectFailure_Retry_Sync_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ConnectFailure);
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetDownloadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR));
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig(true));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk));
        }

        [Fact]
        public void ExecuteWebClientDownload_ConnectFailure_Retry_Async_Fail() {
            // ARRANGE
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ConnectFailure);
            Mock.Arrange(() => new WebClient().DownloadDataTaskAsync(uri)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig(true));
            Thread t = Mock.Create<Thread>(Behavior.CallOriginal);
            Mock.Arrange(() => t.ThreadState).Returns(ThreadState.Aborted);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<ThreadInterruptedException>(() => exec.ExecuteWebClientDownload(new WebClient(), uri, RequestType.GetDownloadChunk, t));
        }

        #endregion

        #region ExecuteWebClientChunkUpload

        [Fact]
        public void ExecuteWebClientChunkUpload_Success() {
            // ARRANGE
            byte[] expected = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", expected)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Returns(expected);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT
            byte[] actual = exec.ExecuteWebClientChunkUpload(new WebClient(), uri, expected, RequestType.PostUploadChunk);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_SecureChannelFailure_Fail() {
            // ARRANGE
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance()
                .Throws(new AggregateException(new WebException("Test", WebExceptionStatus.SecureChannelFailure)));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonNetInsecureException>(() =>
                exec.ExecuteWebClientChunkUpload(new WebClient(), uri, chunk, RequestType.PostUploadChunk));
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_RequestCancel_Fail() {
            // ARRANGE
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance()
                .Throws(new AggregateException(new WebException("Test", WebExceptionStatus.RequestCanceled)));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<ThreadInterruptedException>(
                () => exec.ExecuteWebClientChunkUpload(new WebClient(), uri, chunk, RequestType.PostUploadChunk));
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_ProtocolError_Fail() {
            // ARRANGE
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ProtocolError);
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetDownloadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientChunkUpload(new WebClient(), uri, chunk, RequestType.PostUploadChunk));
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_ProtocolError_TooManyRequests_Fail() {
            // ARRANGE
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ProtocolError);
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.GetDownloadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_TOO_MANY_REQUESTS));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientChunkUpload(new WebClient(), uri, chunk, RequestType.PostUploadChunk));
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_ConnectFailure_Retry_Sync_Fail() {
            // ARRANGE
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ConnectFailure);
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonErrorParser.ParseError(we, RequestType.PostUploadChunk))
                .Throws(new DracoonApiException(DracoonApiCode.SERVER_UNKNOWN_ERROR));
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig(true));
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => exec.ExecuteWebClientChunkUpload(new WebClient(), uri, chunk, RequestType.PostUploadChunk));
        }

        [Fact]
        public void ExecuteWebClientChunkUpload_ConnectFailure_Retry_Async_Fail() {
            // ARRANGE
            WebClient wc = new WebClient();
            byte[] chunk = Encoding.UTF8.GetBytes("OK");
            Uri uri = new Uri("https://dracoon.team");
            WebException we = new WebException("Test", WebExceptionStatus.ConnectFailure);
            Mock.Arrange(() => new WebClient().UploadDataTaskAsync(uri, "POST", chunk)).IgnoreInstance().Returns(Mock.Create<Task<byte[]>>());
            Mock.Arrange(() => Mock.Create<Task<byte[]>>().Result).IgnoreInstance().Throws(new AggregateException(we));
            Mock.Arrange(() => DracoonClient.HttpConfig).Returns(new DracoonHttpConfig(true));
            Thread t = Mock.Create<Thread>(Behavior.CallOriginal);
            Mock.Arrange(() => t.ThreadState).Returns(ThreadState.Aborted);
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock();
            IRequestExecutor exec = new DracoonRequestExecutor(FactoryClients.OAuthMock, c);

            // ACT - ASSERT
            Assert.Throws<ThreadInterruptedException>(() => exec.ExecuteWebClientChunkUpload(wc, uri, chunk, RequestType.PostUploadChunk, t));
        }

        #endregion
    }
}