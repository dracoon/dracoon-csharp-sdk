using System;

namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOException/*'/>
    public class DracoonNetIOException : DracoonException {
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorOne/*'/>
        public DracoonNetIOException() { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorTwo/*'/>
        public DracoonNetIOException(string message) : base(message) { }

        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonNetIOException"]/DracoonNetIOExceptionConstructorThree/*'/>
        public DracoonNetIOException(string message, Exception cause) : base(message, cause) { }
    }
}