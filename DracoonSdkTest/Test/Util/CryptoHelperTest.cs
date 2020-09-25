using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.SdkInternal.Util;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Util {
    public class CryptoHelperTest {

        #region DetermineUserKeyPairVersion

        [Fact]
        public void DetermineUserKeyPairVersion_2048() {
            // ARRANGE
            UserKeyPairAlgorithm expected = UserKeyPairAlgorithm.RSA2048;

            EncryptedFileKeyAlgorithm param = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM;

            // ACT
            UserKeyPairAlgorithm actual = CryptoHelper.DetermineUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DetermineUserKeyPairVersion_4096() {
            // ARRANGE
            UserKeyPairAlgorithm expected = UserKeyPairAlgorithm.RSA4096;

            EncryptedFileKeyAlgorithm param = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM;

            // ACT
            UserKeyPairAlgorithm actual = CryptoHelper.DetermineUserKeyPairVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region DetermineEncryptedFileKeyVersion

        [Fact]
        public void DetermineEncryptedFileKeyVersion_2048() {
            // ARRANGE
            EncryptedFileKeyAlgorithm expected = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM;

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA2048;

            // ACT
            EncryptedFileKeyAlgorithm actual = CryptoHelper.DetermineEncryptedFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DetermineEncryptedFileKeyVersion_4096() {
            // ARRANGE
            EncryptedFileKeyAlgorithm expected = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM;

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA4096;

            // ACT
            EncryptedFileKeyAlgorithm actual = CryptoHelper.DetermineEncryptedFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region DeterminePlainFileKeyVersion

        [Fact]
        public void DeterminePlainFileKeyVersion_2048() {
            // ARRANGE
            PlainFileKeyAlgorithm expected = PlainFileKeyAlgorithm.AES256GCM;

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA2048;

            // ACT
            PlainFileKeyAlgorithm actual = CryptoHelper.DeterminePlainFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DeterminePlainFileKeyVersion_4096() {
            // ARRANGE
            PlainFileKeyAlgorithm expected = PlainFileKeyAlgorithm.AES256GCM;

            UserKeyPairAlgorithm param = UserKeyPairAlgorithm.RSA4096;

            // ACT
            PlainFileKeyAlgorithm actual = CryptoHelper.DeterminePlainFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

    }
}
