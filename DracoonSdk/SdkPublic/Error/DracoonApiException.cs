namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiException"]/DracoonApiException/*'/>
    public class DracoonApiException : DracoonException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiException"]/ErrorCode/*'/>
        public DracoonApiCode ErrorCode { get; }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiException"]/DracoonApiExceptionConstructorOne/*'/>
        public DracoonApiException() {
            ErrorCode = DracoonApiCode.SERVER_UNKNOWN_ERROR;
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiException"]/DracoonApiExceptionConstructorTwo/*'/>
        public DracoonApiException(DracoonApiCode errorCode) : base(errorCode.Text) {
            ErrorCode = errorCode;
        }
    }
}