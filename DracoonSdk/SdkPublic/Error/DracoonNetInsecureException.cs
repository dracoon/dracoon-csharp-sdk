using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a insecure network connection.
    /// </summary>
    [Serializable]
    public class DracoonNetInsecureException : DracoonNetIOException {
        /// <summary>
        ///     Constructs a new insecure network exception without any additional informations.
        /// </summary>
        public DracoonNetInsecureException() { }

        /// <summary>
        ///     Constructs a new insecure network exception with the specified error message.
        /// </summary>
        /// <param name="message">The error message</param>
        public DracoonNetInsecureException(string message) : base(message) { }

        /// <summary>
        ///     Constructs a new insecure network exception with the specified error message and the causing exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="cause">The error causing exception</param>
        public DracoonNetInsecureException(string message, Exception cause) : base(message, cause) { }

        /// <inheritdoc />
        protected DracoonNetInsecureException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}