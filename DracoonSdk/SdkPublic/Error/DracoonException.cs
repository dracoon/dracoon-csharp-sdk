using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a DRACOON SDK error.
    ///     <para>
    ///       <see cref="Dracoon.Sdk.Error.DracoonApiException" />
    ///     </para>
    ///     <para>
    ///       <see cref="Dracoon.Sdk.Error.DracoonCryptoException"/>
    ///     </para>
    ///     <para>
    ///       <see cref="Dracoon.Sdk.Error.DracoonFileIOException"/>
    ///     </para>
    ///     <para>
    ///       <see cref="Dracoon.Sdk.Error.DracoonNetIOException"/>
    ///     </para>
    /// </summary>
    [Serializable]
    public class DracoonException : Exception {
        /// <summary>
        ///     Constructs a new exception without any additional informations.
        /// </summary>
        internal DracoonException() { }

        /// <summary>
        ///     Constructs a new exception with the specified error message.
        /// </summary>
        /// <param name="message">The error message</param>
        internal DracoonException(string message) : base(message) { }

        /// <summary>
        ///     Constructs a new exception with the specified error message and the causing exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="cause">The error causing exception</param>
        internal DracoonException(string message, Exception cause) : base(message, cause) { }

        /// <inheritdoc />
        protected DracoonException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}