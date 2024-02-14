using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.OAuth;
using RestSharp;
using System.Net;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.OAuth {
    public class OAuthErrorParserTest {
        private string GenerateJsonError(string error) {
            return "{\"error\": \"" + error + "\",\"error_description\": \"Some message!\"}";
        }

        [Theory]
        [InlineData(RequestType.PostOAuthToken, "unsupported_grant_type", HttpStatusCode.BadRequest, 1106)]
        [InlineData(RequestType.PostOAuthRefresh, "unsupported_grant_type", HttpStatusCode.BadRequest, 1108)]
        [InlineData(RequestType.GetAuthenticatedPing, "unsupported_grant_type", HttpStatusCode.BadRequest, 1000)]
        [InlineData(RequestType.GetAuthenticatedPing, "invalid_client", HttpStatusCode.BadRequest, 1102)]
        [InlineData(RequestType.PostOAuthToken, "invalid_grant", HttpStatusCode.BadRequest, 1107)]
        [InlineData(RequestType.PostOAuthRefresh, "invalid_grant", HttpStatusCode.BadRequest, 1109)]
        [InlineData(RequestType.GetAuthenticatedPing, "invalid_grant", HttpStatusCode.BadRequest, 1000)]
        [InlineData(RequestType.GetAuthenticatedPing, "invalid_request", HttpStatusCode.BadRequest, 1000)]
        [InlineData(RequestType.GetAuthenticatedPing, "invalid_request", HttpStatusCode.Unauthorized, 1101)]
        [InlineData(RequestType.GetAuthenticatedPing, "invalid_request", HttpStatusCode.ServiceUnavailable, 1000)]
        internal void ParseError_RestResponse(RequestType type, string content, HttpStatusCode statusCode, int expectedSdkErrorCode) {
            // ARRANGE
            RestResponse response = Mock.Create<RestResponse>();
            Mock.Arrange(() => response.Content).Returns(GenerateJsonError(content));
            Mock.Arrange(() => response.StatusCode).Returns(statusCode);

            try {
                // ACT
                OAuthErrorParser.ParseError(response, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            }
        }

        [Theory]
        [InlineData("unsupported_response_type", 1103)]
        [InlineData("invalid_client", 1100)]
        [InlineData("invalid_grant", 1102)]
        [InlineData("invalid_scope", 1104)]
        [InlineData("access_denied", 1105)]
        [InlineData("default", 1000)]
        internal void ParseError_String(string content, int expectedSdkErrorCode) {
            // ARRANGE

            try {
                // ACT
                OAuthErrorParser.ParseError(content);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            }
        }
    }
}