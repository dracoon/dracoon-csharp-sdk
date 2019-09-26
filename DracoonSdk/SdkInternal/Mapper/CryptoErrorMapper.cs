using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Error;
using System;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal class CryptoErrorMapper {
        internal static DracoonCryptoCode ParseCause(Exception e) {
            if (e.GetType() == typeof(InvalidPasswordException)) {
                return DracoonCryptoCode.INVALID_PASSWORD_ERROR;
            }

            if (e.GetType() == typeof(BadFileException)) {
                return DracoonCryptoCode.BAD_FILE_ERROR;
            }

            if (e.GetType() == typeof(InvalidKeyPairException) || e.GetType() == typeof(InvalidFileKeyException)) {
                return DracoonCryptoCode.INTERNAL_ERROR;
            }

            return e.GetType() == typeof(CryptoSystemException) ? DracoonCryptoCode.SYSTEM_ERROR : DracoonCryptoCode.UNKNOWN_ERROR;
        }
    }
}