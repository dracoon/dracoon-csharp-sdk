using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Error;

namespace Dracoon.Sdk.SdkInternal.Util {
    internal static class CryptoHelper {

        internal static UserKeyPairAlgorithm DetermineUserKeyPairVersion(EncryptedFileKeyAlgorithm algorithm) {
            switch (algorithm) {
                case EncryptedFileKeyAlgorithm.RSA2048_AES256GCM:
                    return UserKeyPairAlgorithm.RSA2048;
                case EncryptedFileKeyAlgorithm.RSA4096_AES256GCM:
                    return UserKeyPairAlgorithm.RSA4096;
                default:
                    throw new DracoonCryptoException(new DracoonCryptoCode(DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code, "Unknown encrypted file key algorithm: " + algorithm.GetStringValue() + "."));
            }
        }

        internal static EncryptedFileKeyAlgorithm DetermineEncryptedFileKeyVersion(UserKeyPairAlgorithm algorithm) {
            switch (algorithm) {
                case UserKeyPairAlgorithm.RSA2048:
                    return EncryptedFileKeyAlgorithm.RSA2048_AES256GCM;
                case UserKeyPairAlgorithm.RSA4096:
                    return EncryptedFileKeyAlgorithm.RSA4096_AES256GCM;
                default:
                    throw new DracoonCryptoException(new DracoonCryptoCode(DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code, "Unknown user key pair algorithm: " + algorithm.GetStringValue() + "."));
            }
        }

        internal static PlainFileKeyAlgorithm DeterminePlainFileKeyVersion(UserKeyPairAlgorithm algorithm) {
            switch (algorithm) {
                case UserKeyPairAlgorithm.RSA2048:
                    return PlainFileKeyAlgorithm.AES256GCM;
                case UserKeyPairAlgorithm.RSA4096:
                    return PlainFileKeyAlgorithm.AES256GCM;
                default:
                    throw new DracoonCryptoException(new DracoonCryptoCode(DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code, "Unknown plain file key algorithm: " + algorithm.GetStringValue() + "."));
            }
        }

    }
}
