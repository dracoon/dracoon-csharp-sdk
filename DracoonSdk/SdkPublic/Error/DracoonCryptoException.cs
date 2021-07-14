using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoException"]/DracoonCryptoException/*'/>
    [Serializable]
    public class DracoonCryptoException : DracoonException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoException"]/ErrorCode/*'/>
        public DracoonCryptoCode ErrorCode { get; }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoException"]/DracoonCryptoExceptionConstructorOne/*'/>
        public DracoonCryptoException() {
            ErrorCode = DracoonCryptoCode.UNKNOWN_ERROR;
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoException"]/DracoonCryptoExceptionConstructorTwo/*'/>
        public DracoonCryptoException(DracoonCryptoCode errorCode) : base(errorCode.Text) {
            ErrorCode = errorCode;
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoException"]/DracoonCryptoExceptionConstructorThree/*'/>
        public DracoonCryptoException(DracoonCryptoCode errorCode, Exception cause) : base(errorCode.Text, cause) {
            ErrorCode = errorCode;
        }

        /// <inheritdoc />
        protected DracoonCryptoException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}