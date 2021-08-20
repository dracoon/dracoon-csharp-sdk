using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOException/*'/>
     [Serializable]
    public class DracoonFileIOException : DracoonException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorOne/*'/>
        public DracoonFileIOException() { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorTwo/*'/>
        public DracoonFileIOException(string message) : base(message) { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorThree/*'/>
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