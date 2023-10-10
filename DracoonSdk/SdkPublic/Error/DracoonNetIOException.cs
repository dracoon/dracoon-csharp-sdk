using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a network IO error.
    /// </summary>
    [Serializable]
    public class DracoonNetIOException : DracoonException {
        /// <summary>
        ///     Constructs a new network IO exception without any additional informations.
        /// </summary>
        public DracoonNetIOException() { }

        /// <summary>
        ///     Constructs a new network IO exception with the specified error message.
        /// </summary>
        /// <param name="message">The error message</param>
        public DracoonNetIOException(string message) : base(message) { }

        /// <summary>
        ///     Constructs a new network IO exception with the specified error message and the causing exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="cause">The error causing exception</param>
        public DracoonNetIOException(string message, Exception cause) : base(message, cause) { }

        /// <inheritdoc />
        protected DracoonNetIOException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}