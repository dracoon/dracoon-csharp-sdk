using System;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOException/*'/>
    public class DracoonFileIOException : DracoonException {

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorOne/*'/>
        public DracoonFileIOException() {
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorTwo/*'/>
        public DracoonFileIOException(string message) : base(message) {
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonFileIOException"]/DracoonFileIOExceptionConstructorThree/*'/>
        public DracoonFileIOException(string message, Exception cause) : base(message, cause) {
        }
    }
}
