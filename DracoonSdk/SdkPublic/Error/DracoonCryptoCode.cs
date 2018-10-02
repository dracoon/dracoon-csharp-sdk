
namespace Dracoon.Sdk.Error {
    /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonCryptoCode"]/DracoonCryptoCode/*'/>
    public class DracoonCryptoCode {

        public static readonly DracoonCryptoCode INVALID_PASSWORD_ERROR = new DracoonCryptoCode(1, "The provided password is invalid.");
        public static readonly DracoonCryptoCode BAD_FILE_ERROR = new DracoonCryptoCode(2, "The file integrity check failed. It may have been modified.");

        public static readonly DracoonCryptoCode INTERNAL_ERROR = new DracoonCryptoCode(3, "A internal error occurred.");
        public static readonly DracoonCryptoCode SYSTEM_ERROR = new DracoonCryptoCode(4, "A system error occurred.");
        public static readonly DracoonCryptoCode UNKNOWN_ERROR = new DracoonCryptoCode(5, "A unknown error occurred.");

        /// <summary>
        /// The error message.
        /// </summary>
        public string Text {
            get; private set;
        }

        /// <summary>
        /// The error code.
        /// </summary>
        public int Code {
            get; private set;
        }

        private DracoonCryptoCode(int code, string text) {
            Code = code;
            Text = text;
        }
        /// <include file = "ErrorDoc.xml" path='docs/members[@name="dracoonApiCode"]/ToString/*'/>
        public override string ToString() {
            return Code + " " + Text;
        }
    }
}
