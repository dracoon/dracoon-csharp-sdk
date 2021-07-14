using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOException/*'/>
    [Serializable]
    public class DracoonNetIOException : DracoonException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorOne/*'/>
        public DracoonNetIOException() { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorTwo/*'/>
        public DracoonNetIOException(string message) : base(message) { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorThree/*'/>
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