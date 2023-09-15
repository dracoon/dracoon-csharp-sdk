namespace Dracoon.Sdk.Error {
    /// <summary>
    ///     Collection of DRACOON Crypto error codes.
    /// </summary>
    public class DracoonCryptoCode {
        /// <summary>
        ///     The provided password is invalid.
        /// </summary>
        public static readonly DracoonCryptoCode INVALID_PASSWORD_ERROR = new DracoonCryptoCode(1, "The provided password is invalid.");

        /// <summary>
        ///     The file integrity check failed. It may have been modified.
        /// </summary>
        public static readonly DracoonCryptoCode BAD_FILE_ERROR =
            new DracoonCryptoCode(2, "The file integrity check failed. It may have been modified.");

        /// <summary>
        ///     A internal error occurred.
        /// </summary>
        public static readonly DracoonCryptoCode INTERNAL_ERROR = new DracoonCryptoCode(3, "A internal error occurred.");

        /// <summary>
        ///     A system error occurred.
        /// </summary>
        public static readonly DracoonCryptoCode SYSTEM_ERROR = new DracoonCryptoCode(4, "A system error occurred.");

        /// <summary>
        ///     A unknown error occurred.
        /// </summary>
        public static readonly DracoonCryptoCode UNKNOWN_ERROR = new DracoonCryptoCode(5, "A unknown error occurred.");

        /// <summary>
        ///     Unknown crypto algorithm.
        /// </summary>
        public static readonly DracoonCryptoCode UNKNOWN_ALGORITHM_ERROR = new DracoonCryptoCode(6, "Unknown crypto algorithm.");

        /// <summary>
        ///     The error message.
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     The error code.
        /// </summary>
        public int Code { get; }

        internal DracoonCryptoCode(int code, string text) {
            Code = code;
            Text = text;
        }

        /// <summary>
        ///     Creates a string which contains the error number and the error message.
        /// </summary>
        /// <returns>
        ///     A string with: Code + " " + Text
        /// </returns>
        public override string ToString() {
            return Code + " " + Text;
        }
    }
}