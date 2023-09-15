using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a DRACOON REST API error.
    ///     <para>The exception stores an error code which can be used to determine the error cause.</para>
    /// </summary>
    [Serializable]
    public class DracoonApiException : DracoonException {
        /// <summary>
        ///     Describes what caused the error. See also <seealso cref="Dracoon.Sdk.Error.DracoonApiCode"/>
        /// </summary>
        public DracoonApiCode ErrorCode { get; }

        /// <summary>
        ///     Constructs a new exception with an unknown error code.
        /// </summary>
        public DracoonApiException() {
            ErrorCode = DracoonApiCode.SERVER_UNKNOWN_ERROR;
        }

        /// <summary>
        ///     Constructs a new exception with a specified error code.
        /// </summary>
        /// <param name="errorCode"><see cref="ErrorCode"/></param>
        public DracoonApiException(DracoonApiCode errorCode) : base(errorCode.Text) {
            ErrorCode = errorCode;
        }

        /// <inheritdoc />
        protected DracoonApiException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}