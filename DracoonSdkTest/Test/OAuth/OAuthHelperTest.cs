using System;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.OAuth;
using Dracoon.Sdk.SdkInternal.Validator;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.OAuth {
    public class OAuthHelperTest {
        [Fact]
        public void CreateAuthorizationUrl() {
            // ARRANGE
            string clientId = "clientId1";
            string state = "state1";
            string baseUri = "https://dracoon.team";
            Uri expected = new Uri(baseUri + "/oauth/authorize?response_type=code&client_id=" + clientId + "&state=" + state);
            Mock.Arrange(() => Arg.IsAny<Uri>().MustBeValid(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(2);

            // ACT
            Uri actual = OAuthHelper.CreateAuthorizationUrl(new Uri(baseUri), clientId, state);

            // ASSERT
            Assert.Equal(expected.ToString(), actual.ToString());
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Uri>().MustBeValid(Arg.AnyString));
        }

        [Fact]
        public void ExtractAuthorizationStateFromUri() {
            // ARRANGE
            string expectedCode = "code1";
            string expectedState = "state1";
            Mock.Arrange(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Uri param = new Uri("https://dracoon.team/oauth/callback?code=" + expectedCode + "&state=" + expectedState);

            // ACT
            string actual = OAuthHelper.ExtractAuthorizationStateFromUri(param);

            // ASSERT
            Assert.Equal(expectedState, actual);
            Mock.Assert(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString));
        }

        [Fact]
        public void ExtractAuthorizationCodeFromUri() {
            // ARRANGE
            string expectedCode = "code1";
            string expectedState = "state1";
            Mock.Arrange(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Uri param = new Uri("https://dracoon.team/oauth/callback?code=" + expectedCode + "&state=" + expectedState);

            // ACT
            string actual = OAuthHelper.ExtractAuthorizationCodeFromUri(param);

            // ASSERT
            Assert.Equal(expectedCode, actual);
            Mock.Assert(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString));
        }

        [Fact]
        public void ExtractAuthorizationStateFromUri_Error() {
            // ARRANGE
            Mock.Arrange(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => OAuthErrorParser.ParseError(Arg.AnyString)).Throws(new DracoonApiException());
            Uri param = new Uri("https://dracoon.team/oauth/callback?error=someOccuredError");

            // ACT - ASSERT
            Assert.Throws<DracoonApiException>(() => OAuthHelper.ExtractAuthorizationStateFromUri(param));
            Mock.Assert(() => Arg.IsAny<Uri>().MustNotNull(Arg.AnyString));
        }
    }
}