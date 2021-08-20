using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetInsecureException"]/DracoonNetInsecureException/*'/>
    [Serializable]
    public class DracoonNetInsecureException : DracoonNetIOException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetInsecureException"]/DracoonNetInsecureExceptionConstructorOne/*'/>
        public DracoonNetInsecureException() { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetInsecureException"]/DracoonNetInsecureExceptionConstructorTwo/*'/>
        public DracoonNetInsecureException(string message) : base(message) { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetInsecureException"]/DracoonNetInsecureExceptionConstructorThree/*'/>
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