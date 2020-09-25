using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.UnitTest.Factory;
using RestSharp;
using System.IO;
using System.Net;
using System.Text;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test {
    public class DracoonErrorParserTest {
        private static string GenerateJsonError(int httpCode, int errorCode) {
            return "{\"code\": " + httpCode + ",\"message\": \"Some message!\",\"debugInfo\": \"Some debug info!\",\"errorCode\": " + errorCode + "}";
        }

        private HttpWebResponse CreateMockedHttpWebResponse(int statusCode, string content, string[] headerNames, string[] headerValues) {
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            Stream responseStream = new MemoryStream();
            responseStream.Write(contentBytes, 0, contentBytes.Length);
            responseStream.Seek(0, SeekOrigin.Begin);

            WebHeaderCollection h = new WebHeaderCollection();
            if (headerNames != null) {
                for (int i = 0; i < headerNames.Length; i++) {
                    h.Add(headerNames[i], headerValues[i]);
                }
            }

            HttpWebResponse oNewResponse = (System.Net.HttpWebResponse) System.Activator.CreateInstance(typeof(HttpWebResponse));
            System.Reflection.PropertyInfo oInfo = oNewResponse.GetType().GetProperty("ResponseStream",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
            oInfo?.SetValue(oNewResponse, responseStream);
            System.Reflection.FieldInfo oFInfo = oNewResponse.GetType().GetField("m_HttpResponseHeaders",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
            oFInfo?.SetValue(oNewResponse, h);
            oFInfo = oNewResponse.GetType().GetField("m_StatusCode",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy);
            oFInfo?.SetValue(oNewResponse, statusCode);


            return oNewResponse;
        }

        [Fact]
        internal void TestSingleCodeWithIRestResponse() {
            // ARRANGE
            IRestResponse response = FactoryRestSharp.RestResponse;

            try {
                // ACT
                DracoonErrorParser.ParseError(response, RequestType.GetAuthenticatedPing);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(1301, dae.ErrorCode.Code);
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -10002, new string[] { }, new string[] { }, 3800)]
        [InlineData(RequestType.PostCopyNodes, -40001, new string[] { }, new string[] { }, 3119)]
        [InlineData(RequestType.PostMoveNodes, -40001, new string[] { }, new string[] { }, 3119)]
        [InlineData(RequestType.GetAuthenticatedPing, -40001, new string[] { }, new string[] { }, 3118)]
        [InlineData(RequestType.PostCopyNodes, -40002, new string[] { }, new string[] { }, 3120)]
        [InlineData(RequestType.PostMoveNodes, -40002, new string[] { }, new string[] { }, 3120)]
        [InlineData(RequestType.GetAuthenticatedPing, -40002, new string[] { }, new string[] { }, 3121)]
        [InlineData(RequestType.GetAuthenticatedPing, -40003, new string[] { }, new string[] { }, 3122)]
        [InlineData(RequestType.GetAuthenticatedPing, -40004, new string[] { }, new string[] { }, 3124)]
        [InlineData(RequestType.GetAuthenticatedPing, -40008, new string[] { }, new string[] { }, 3123)]
        [InlineData(RequestType.GetAuthenticatedPing, -40012, new string[] { }, new string[] { }, 3125)]
        [InlineData(RequestType.GetAuthenticatedPing, -40013, new string[] { }, new string[] { }, 3127)]
        [InlineData(RequestType.GetAuthenticatedPing, -40014, new string[] { }, new string[] { }, 3552)]
        [InlineData(RequestType.GetAuthenticatedPing, -40018, new string[] { }, new string[] { }, 3126)]
        [InlineData(RequestType.GetAuthenticatedPing, -40755, new string[] { }, new string[] { }, 3101)]
        [InlineData(RequestType.GetAuthenticatedPing, -40761, new string[] { }, new string[] { }, 3552)]
        [InlineData(RequestType.PostCopyNodes, -41052, new string[] { }, new string[] { }, 3114)]
        [InlineData(RequestType.PostMoveNodes, -41052, new string[] { }, new string[] { }, 3115)]
        [InlineData(RequestType.GetAuthenticatedPing, -41053, new string[] { }, new string[] { }, 3100)]
        [InlineData(RequestType.GetAuthenticatedPing, -41054, new string[] { }, new string[] { }, 3108)]
        [InlineData(RequestType.GetAuthenticatedPing, -41200, new string[] { }, new string[] { }, 3116)]
        [InlineData(RequestType.GetAuthenticatedPing, -41301, new string[] { }, new string[] { }, 3117)]
        [InlineData(RequestType.PostCopyNodes, -41302, new string[] { }, new string[] { }, 3109)]
        [InlineData(RequestType.PostCopyNodes, -41303, new string[] { }, new string[] { }, 3109)]
        [InlineData(RequestType.PostMoveNodes, -41302, new string[] { }, new string[] { }, 3110)]
        [InlineData(RequestType.PostMoveNodes, -41303, new string[] { }, new string[] { }, 3110)]
        [InlineData(RequestType.GetAuthenticatedPing, -70020, new string[] { }, new string[] { }, 3550)]
        [InlineData(RequestType.GetAuthenticatedPing, -70022, new string[] { }, new string[] { }, 3551)]
        [InlineData(RequestType.GetAuthenticatedPing, -70023, new string[] { }, new string[] { }, 3551)]
        [InlineData(RequestType.GetAuthenticatedPing, -80000, new string[] { }, new string[] { }, 3001)]
        [InlineData(RequestType.GetAuthenticatedPing, -80001, new string[] { }, new string[] { }, 3003)]
        [InlineData(RequestType.GetAuthenticatedPing, -80003, new string[] { }, new string[] { }, 3002)]
        [InlineData(RequestType.GetAuthenticatedPing, -80006, new string[] { }, new string[] { }, 3102)]
        [InlineData(RequestType.GetAuthenticatedPing, -80007, new string[] { }, new string[] { }, 3004)]
        [InlineData(RequestType.GetAuthenticatedPing, -80008, new string[] { }, new string[] { }, 3103)]
        [InlineData(RequestType.GetAuthenticatedPing, -80012, new string[] { }, new string[] { }, 3103)]
        [InlineData(RequestType.GetAuthenticatedPing, -80009, new string[] { }, new string[] { }, 3801)]
        [InlineData(RequestType.GetAuthenticatedPing, -80018, new string[] { }, new string[] { }, 3006)]
        [InlineData(RequestType.GetAuthenticatedPing, -80019, new string[] { }, new string[] { }, 3007)]
        [InlineData(RequestType.GetAuthenticatedPing, -80023, new string[] { }, new string[] { }, 3009)]
        [InlineData(RequestType.GetAuthenticatedPing, -80024, new string[] { }, new string[] { }, 3008)]
        [InlineData(RequestType.GetAuthenticatedPing, -80030, new string[] { }, new string[] { }, 5800)]
        [InlineData(RequestType.GetAuthenticatedPing, -80034, new string[] { }, new string[] { }, 3128)]
        [InlineData(RequestType.GetAuthenticatedPing, -80035, new string[] { }, new string[] { }, 3005)]
        [InlineData(RequestType.GetAuthenticatedPing, -80045, new string[] { }, new string[] { }, 3129)]
        [InlineData(RequestType.GetAuthenticatedPing, -90033, new string[] { }, new string[] { }, 5802)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 3000)]
        internal void TestBadRequestCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(400, GenerateJsonError(400, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -10006, new string[] { }, new string[] { }, 1150)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 1200)]
        internal void TestUnauthorizedCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(401, GenerateJsonError(401, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new[] {
            "X-Forbidden"
        }, new[] {
            "403"
        }, 5090)]
        [InlineData(RequestType.GetAuthenticatedPing, -10003, new string[] { }, new string[] { }, 1301)]
        [InlineData(RequestType.GetAuthenticatedPing, -10007, new string[] { }, new string[] { }, 1301)]
        [InlineData(RequestType.GetAuthenticatedPing, -10004, new string[] { }, new string[] { }, 1302)]
        [InlineData(RequestType.GetAuthenticatedPing, -10005, new string[] { }, new string[] { }, 1300)]
        [InlineData(RequestType.GetAuthenticatedPing, -70020, new string[] { }, new string[] { }, 5550)]
        [InlineData(RequestType.GetAuthenticatedPing, -40761, new string[] { }, new string[] { }, 5552)]
        [InlineData(RequestType.DeleteNodes, 0, new string[] { }, new string[] { }, 4104)]
        [InlineData(RequestType.PostMoveNodes, 0, new string[] { }, new string[] { }, 4103)]
        [InlineData(RequestType.PutRoom, 0, new string[] { }, new string[] { }, 4103)]
        [InlineData(RequestType.PutFolder, 0, new string[] { }, new string[] { }, 4103)]
        [InlineData(RequestType.PutFile, 0, new string[] { }, new string[] { }, 4103)]
        [InlineData(RequestType.GetNode, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.GetNodes, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.GetSearchNodes, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.PostDownloadToken, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.PostFavorite, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.DeleteFavorite, 0, new string[] { }, new string[] { }, 4101)]
        [InlineData(RequestType.PostRoom, 0, new string[] { }, new string[] { }, 4102)]
        [InlineData(RequestType.PostFolder, 0, new string[] { }, new string[] { }, 4102)]
        [InlineData(RequestType.PostCopyNodes, 0, new string[] { }, new string[] { }, 4102)]
        [InlineData(RequestType.PostCreateDownloadShare, 0, new string[] { }, new string[] { }, 4105)]
        [InlineData(RequestType.DeleteDownloadShare, 0, new string[] { }, new string[] { }, 4105)]
        [InlineData(RequestType.PostCreateUploadShare, 0, new string[] { }, new string[] { }, 4106)]
        [InlineData(RequestType.DeleteUploadShare, 0, new string[] { }, new string[] { }, 4106)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 4000)]
        internal void TestForbiddenCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(403, GenerateJsonError(403, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -20501, new string[] { }, new string[] { }, 5103)]
        [InlineData(RequestType.GetAuthenticatedPing, -40751, new string[] { }, new string[] { }, 5103)]
        [InlineData(RequestType.PostRoom, -41000, new string[] { }, new string[] { }, 5106)]
        [InlineData(RequestType.PostFolder, -41000, new string[] { }, new string[] { }, 5105)]
        [InlineData(RequestType.PutFolder, -41000, new string[] { }, new string[] { }, 5102)]
        [InlineData(RequestType.PutRoom, -41000, new string[] { }, new string[] { }, 5101)]
        [InlineData(RequestType.GetAuthenticatedPing, -41000, new string[] { }, new string[] { }, 5100)]
        [InlineData(RequestType.PostRoom, -40000, new string[] { }, new string[] { }, 5106)]
        [InlineData(RequestType.PostFolder, -40000, new string[] { }, new string[] { }, 5105)]
        [InlineData(RequestType.PutFolder, -40000, new string[] { }, new string[] { }, 5102)]
        [InlineData(RequestType.PutRoom, -40000, new string[] { }, new string[] { }, 5101)]
        [InlineData(RequestType.GetAuthenticatedPing, -41050, new string[] { }, new string[] { }, 5104)]
        [InlineData(RequestType.GetAuthenticatedPing, -41051, new string[] { }, new string[] { }, 5105)]
        [InlineData(RequestType.GetAuthenticatedPing, -41100, new string[] { }, new string[] { }, 5111)]
        [InlineData(RequestType.GetAuthenticatedPing, -60000, new string[] { }, new string[] { }, 5200)]
        [InlineData(RequestType.GetAuthenticatedPing, -60500, new string[] { }, new string[] { }, 5201)]
        [InlineData(RequestType.GetAuthenticatedPing, -70020, new string[] { }, new string[] { }, 5550)]
        [InlineData(RequestType.GetAuthenticatedPing, -70028, new string[] { }, new string[] { }, 5553)]
        [InlineData(RequestType.GetAuthenticatedPing, -70501, new string[] { }, new string[] { }, 5500)]
        [InlineData(RequestType.GetAuthenticatedPing, -70550, new string[] { }, new string[] { }, 5554)]
        [InlineData(RequestType.GetAuthenticatedPing, -90034, new string[] { }, new string[] { }, 5113)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5000)]
        internal void TestNotFoundCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(404, GenerateJsonError(404, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -41001, new string[] { }, new string[] { }, 3104)]
        [InlineData(RequestType.PostMoveNodes, -41304, new string[] { }, new string[] { }, 3113)]
        [InlineData(RequestType.PostCopyNodes, -41304, new string[] { }, new string[] { }, 3112)]
        [InlineData(RequestType.GetAuthenticatedPing, -40010, new string[] { }, new string[] { }, 3111)]
        [InlineData(RequestType.PostRoom, 0, new string[] { }, new string[] { }, 3105)]
        [InlineData(RequestType.PostFolder, 0, new string[] { }, new string[] { }, 3106)]
        [InlineData(RequestType.PutFolder, 0, new string[] { }, new string[] { }, 3106)]
        [InlineData(RequestType.PutRoom, 0, new string[] { }, new string[] { }, 3105)]
        [InlineData(RequestType.PutFile, 0, new string[] { }, new string[] { }, 3107)]
        [InlineData(RequestType.PostCreateUploadShare, 0, new string[] { }, new string[] { }, 3201)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5000)]
        internal void TestConflictCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(409, GenerateJsonError(409, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -10103, new string[] { }, new string[] { }, 2101)]
        [InlineData(RequestType.GetAuthenticatedPing, -10104, new string[] { }, new string[] { }, 2103)]
        [InlineData(RequestType.GetAuthenticatedPing, -10106, new string[] { }, new string[] { }, 2102)]
        [InlineData(RequestType.GetAuthenticatedPing, -90030, new string[] { }, new string[] { }, 2104)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 2000)]
        internal void TestPreconditionFailedCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues,
            int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(412, GenerateJsonError(412, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -90090, new string[] { }, new string[] { }, 5801)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5000)]
        internal void TestBadGatewayCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(502, GenerateJsonError(502, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, -90200, new string[] { }, new string[] { }, 5108)]
        [InlineData(RequestType.GetAuthenticatedPing, -40200, new string[] { }, new string[] { }, 5109)]
        [InlineData(RequestType.GetAuthenticatedPing, -50504, new string[] { }, new string[] { }, 5110)]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5107)]
        internal void TestInsufficentStorageCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues,
            int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(507, GenerateJsonError(507, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5090)]
        internal void TestCustomErrorCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(901, GenerateJsonError(901, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }

        [Fact]
        internal void TestCustomErrorCodesIRestRequest() {
            // ARRANGE
            IRestResponse response = FactoryRestSharp.RestResponse;
            response.Headers.Add(new Parameter("testHeader", "1234", ParameterType.HttpHeader));
            response.Headers.Add(new Parameter("X-Forbidden", "403", ParameterType.HttpHeader));

            try {
                // ACT
                DracoonErrorParser.ParseError(response, RequestType.GetAuthenticatedPing);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(5090, dae.ErrorCode.Code);
            }
        }

        [Theory]
        [InlineData(RequestType.GetAuthenticatedPing, 0, new string[] { }, new string[] { }, 5000)]
        internal void TestUnknownCodes(RequestType type, int apiCode, string[] headerNames, string[] headerValues, int expectedSdkErrorCode) {
            // ARRANGE
            HttpWebResponse r = CreateMockedHttpWebResponse(407, GenerateJsonError(407, apiCode), headerNames, headerValues);
            WebException we = new WebException("Some message!", null, WebExceptionStatus.ProtocolError, r);

            try {
                // ACT
                DracoonErrorParser.ParseError(we, type);
            } catch (DracoonApiException dae) {
                // ASSERT
                Assert.Equal(expectedSdkErrorCode, dae.ErrorCode.Code);
            } finally {
                r.Close();
            }
        }
    }
}