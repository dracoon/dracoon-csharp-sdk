using Dracoon.Sdk.Error;
using System;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Error {
    public class ExceptionTests {
        #region DracoonNetIOException

        [Fact]
        public void DracoonNetIOException_Constructor1() {
            // ARRANGE

            // ACT
            DracoonNetIOException exc = new DracoonNetIOException();

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
        }

        [Fact]
        public void DracoonNetIOException_Constructor2() {
            // ARRANGE
            string message = "A exception message";

            // ACT
            DracoonNetIOException exc = new DracoonNetIOException(message);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
        }

        [Fact]
        public void DracoonNetIOException_Constructor3() {
            // ARRANGE
            string message = "A exception message";
            Exception cause = new Exception("A other message");

            // ACT
            DracoonNetIOException exc = new DracoonNetIOException(message, cause);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
            Assert.Equal(cause, exc.InnerException);
        }

        #endregion

        #region DracoonNetInsecureException

        [Fact]
        public void DracoonNetInsecureException_Constructor1() {
            // ARRANGE

            // ACT
            DracoonNetInsecureException exc = new DracoonNetInsecureException();

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
        }

        [Fact]
        public void DracoonNetInsecureException_Constructor2() {
            // ARRANGE
            string message = "A exception message";

            // ACT
            DracoonNetInsecureException exc = new DracoonNetInsecureException(message);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
        }

        [Fact]
        public void DracoonNetInsecureException_Constructor3() {
            // ARRANGE
            string message = "A exception message";
            Exception cause = new Exception("A other message");

            // ACT
            DracoonNetInsecureException exc = new DracoonNetInsecureException(message, cause);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
            Assert.Equal(cause, exc.InnerException);
        }

        #endregion

        #region DracoonFileIOException

        [Fact]
        public void DracoonFileIOException_Constructor1() {
            // ARRANGE

            // ACT
            DracoonFileIOException exc = new DracoonFileIOException();

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
        }

        [Fact]
        public void DracoonFileIOException_Constructor2() {
            // ARRANGE
            string message = "A exception message";

            // ACT
            DracoonFileIOException exc = new DracoonFileIOException(message);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
        }

        [Fact]
        public void DracoonFileIOException_Constructor3() {
            // ARRANGE
            string message = "A exception message";
            Exception cause = new Exception("A other message");

            // ACT
            DracoonFileIOException exc = new DracoonFileIOException(message, cause);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(message, exc.Message);
            Assert.Equal(cause, exc.InnerException);
        }

        #endregion

        #region DracoonApiException

        [Fact]
        public void DracoonApiException_Constructor1() {
            // ARRANGE

            // ACT
            DracoonApiException exc = new DracoonApiException();

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(DracoonApiCode.SERVER_UNKNOWN_ERROR.Code, exc.ErrorCode.Code);
        }

        [Fact]
        public void DracoonApiException_Constructor2() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.SERVER_USER_NOT_FOUND;

            // ACT
            DracoonApiException exc = new DracoonApiException(code);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(code.Code, exc.ErrorCode.Code);
        }

        #endregion

        #region DracoonCryptoException

        [Fact]
        public void DracoonCryptoException_Constructor1() {
            // ARRANGE

            // ACT
            DracoonCryptoException exc = new DracoonCryptoException();

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(DracoonCryptoCode.UNKNOWN_ERROR.Code, exc.ErrorCode.Code);
        }

        [Fact]
        public void DracoonCryptoException_Constructor2() {
            // ARRANGE
            DracoonCryptoCode code = DracoonCryptoCode.INTERNAL_ERROR;

            // ACT
            DracoonCryptoException exc = new DracoonCryptoException(code);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(code.Code, exc.ErrorCode.Code);
        }

        [Fact]
        public void DracoonCryptoException_Constructor3() {
            // ARRANGE
            DracoonCryptoCode code = DracoonCryptoCode.INVALID_PASSWORD_ERROR;
            Exception cause = new Exception("A other message");

            // ACT
            DracoonCryptoException exc = new DracoonCryptoException(code, cause);

            // ASSERT
            Assert.IsAssignableFrom<DracoonException>(exc);
            Assert.IsAssignableFrom<Exception>(exc);
            Assert.Equal(code.Code, exc.ErrorCode.Code);
            Assert.Equal(cause, exc.InnerException);
        }

        #endregion

        #region DracoonApiCode

        [Fact]
        public void DracoonApiCode_AuthError() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.AUTH_OAUTH_AUTHORIZATION_REQUEST_INVALID;

            // ACT

            // ASSERT
            Assert.True(code.IsAuthError());
        }

        [Fact]
        public void DracoonApiCode_AuthError_Fail() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.False(code.IsAuthError());
        }

        [Fact]
        public void DracoonApiCode_PreconditionError() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.PRECONDITION_MUST_ACCEPT_EULA;

            // ACT

            // ASSERT
            Assert.True(code.IsPreconditionError());
        }

        [Fact]
        public void DracoonApiCode_PreconditionError_Fail() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.False(code.IsPreconditionError());
        }

        [Fact]
        public void DracoonApiCode_ValidationError() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.VALIDATION_BAD_FILE_NAME;

            // ACT

            // ASSERT
            Assert.True(code.IsValidationError());
        }

        [Fact]
        public void DracoonApiCode_ValidationError_Fail() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.False(code.IsValidationError());
        }

        [Fact]
        public void DracoonApiCode_PermissionError() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.PERMISSION_DELETE_ERROR;

            // ACT

            // ASSERT
            Assert.True(code.IsPermissionError());
        }

        [Fact]
        public void DracoonApiCode_PermissionError_Fail() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.False(code.IsPermissionError());
        }

        [Fact]
        public void DracoonApiCode_ServerError() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.SERVER_FILE_NOT_FOUND;

            // ACT

            // ASSERT
            Assert.True(code.IsServerError());
        }

        [Fact]
        public void DracoonApiCode_ServerError_Fail() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.False(code.IsServerError());
        }

        [Fact]
        public void DracoonApiCode_Equality_null() {
            // ARRANGE
            DracoonApiCode object1 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;
            object object2 = null;

            // ACT

            // ASSERT
            Assert.False(object1.Equals(object2));
        }

        [Fact]
        public void DracoonApiCode_Equality_SameInstance() {
            // ARRANGE
            DracoonApiCode object1 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;
            object object2 = object1;

            // ACT

            // ASSERT
            Assert.True(object1.Equals(object2));
        }

        [Fact]
        public void DracoonApiCode_Equality_OtherType() {
            // ARRANGE
            DracoonApiCode object1 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;
            object object2 = "string";

            // ACT

            // ASSERT
            Assert.False(object1.Equals(object2));
        }

        [Fact]
        public void DracoonApiCode_Equality_SameContent() {
            // ARRANGE
            DracoonApiCode object1 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;
            object object2 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;

            // ACT

            // ASSERT
            Assert.True(object1.Equals(object2));
        }

        [Fact]
        public void DracoonApiCode_Equality_NotSameContent() {
            // ARRANGE
            DracoonApiCode object1 = DracoonApiCode.API_VERSION_NOT_SUPPORTED;
            object object2 = DracoonApiCode.AUTH_USER_TEMPORARY_LOCKED;

            // ACT

            // ASSERT
            Assert.False(object1.Equals(object2));
        }

        [Fact]
        public void DracoonApiCode_ToString() {
            // ARRANGE
            DracoonApiCode code = DracoonApiCode.PERMISSION_CREATE_ERROR;
            string expected = code.Code + " " + code.Text;

            // ACT
            string actual = code.ToString();

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}