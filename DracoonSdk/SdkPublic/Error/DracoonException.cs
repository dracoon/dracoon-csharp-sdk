using System;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonException/*'/>
    public class DracoonException : Exception {

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorOne/*'/>
        internal DracoonException() {
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorTwo/*'/>
        internal DracoonException(string message) : base(message) {
        }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonException"]/DracoonExceptionConstructorThree/*'/>
        internal DracoonException(string message, Exception cause) : base(message, cause) {

        }
    }
}
