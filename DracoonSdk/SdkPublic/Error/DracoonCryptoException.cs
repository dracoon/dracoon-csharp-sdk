using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a DRACOON crypto error.
    ///     <para>The exception stores an error code which can be used to determine the error cause.</para>
    /// </summary>
    [Serializable]
    public class DracoonCryptoException : DracoonException {
        /// <summary>
        ///     Describes what caused the error. See also <seealso cref="Dracoon.Sdk.Error.DracoonCryptoCode"/>
        /// </summary>
        public DracoonCryptoCode ErrorCode { get; }

        /// <summary>
        ///     Constructs a new exception with an unkown error code.
        /// </summary>
        public DracoonCryptoException() {
            ErrorCode = DracoonCryptoCode.UNKNOWN_ERROR;
        }

        /// <summary>
        ///     Constructs a new exception with a specified error code.
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorCode"/></param>
        public DracoonCryptoException(DracoonCryptoCode errorCode) : base(errorCode.Text) {
            ErrorCode = errorCode;
        }

        /// <summary>
        ///     Constructs a new exception with a specified error code and cause.
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorCode"/></param>
        /// <param name="cause">The error causing exception</param>
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