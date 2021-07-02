using System;
using System.Security.Cryptography;

namespace Dracoon.Sdk.SdkInternal.Util {
    internal static class FileHash {
        public static bool CompareFileHashes(string serverHash, byte[] chunkBytes, int chunkValidByteCount) {
            using (MD5 md5Creator = MD5.Create()) {
                string localCalculatedHash =
                    BitConverter.ToString(md5Creator.ComputeHash(chunkBytes, 0, chunkValidByteCount)).Replace("-", "").ToLower();
                return serverHash.ToLower().Equals(localCalculatedHash);
            }
        }
    }
}