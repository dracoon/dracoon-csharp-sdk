using Dracoon.Sdk.SdkInternal.Util;
using System.Text;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Util {
    public class FileHashTest {
        #region CompareFileHashes

        [Theory]
        [InlineData("I'm a hash compare function.", "27e7ccdc3117544c57fb0c7acc8c5cef", true)]
        [InlineData("I'm a hash compare functio.", "27e7ccdc3117544c57fb0c7acc8c5cef", false)]
        [InlineData("I'm a hash compare function.", "27e7ccdc3117544c57fb0c7acc8c5ceg", false)]
        public void CompareFileHashes_Success(string hashText, string compareMd5Hash, bool expected) {
            // ARRANGE
            byte[] hashTextBytes = Encoding.UTF8.GetBytes(hashText);

            // ACT
            bool actual = FileHash.CompareFileHashes(compareMd5Hash, hashTextBytes, hashTextBytes.Length);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}