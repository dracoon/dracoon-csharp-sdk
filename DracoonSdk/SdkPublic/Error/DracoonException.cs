using System;
using System.Runtime.Serialization;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonException/*'/>
    [Serializable]
    public class DracoonException : Exception {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorOne/*'/>
        internal DracoonException() { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorTwo/*'/>
        internal DracoonException(string message) : base(message) { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorThree/*'/>
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