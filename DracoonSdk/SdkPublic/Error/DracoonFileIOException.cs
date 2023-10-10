using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Signals a IO error.
    /// </summary>
    [Serializable]
    public class DracoonFileIOException : DracoonException {
        /// <summary>
        ///     Constructs a new IO exception without any additional informations.
        /// </summary>
        public DracoonFileIOException() { }

        /// <summary>
        ///     Constructs a new IO exception with the specified error message.
        /// </summary>
        /// <param name="message">The error message</param>
        public DracoonFileIOException(string message) : base(message) { }

        /// <summary>
        ///     Constructs a new IO exception with the specified error message and the causing exception.
        /// </summary>
        /// <param name="message">The error message</param>
        /// <param name="cause">The error causing exception</param>
        public DracoonFileIOException(string message, Exception cause) : base(message, cause) { }

        /// <inheritdoc />
        protected DracoonFileIOException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }

        /// <inheritdoc />
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            base.GetObjectData(info, context);
        }
    }
}