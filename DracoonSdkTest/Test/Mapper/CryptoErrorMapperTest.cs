using Dracoon.Crypto.Sdk;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.Mapper;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class CryptoErrorMapperTest {

        #region ParseCause

        public static IEnumerable<object[]> DataParseCause => new List<object[]> {
            new object[] { new InvalidPasswordException(), DracoonCryptoCode.INVALID_PASSWORD_ERROR},
            new object[] { new BadFileException(), DracoonCryptoCode.BAD_FILE_ERROR},
            new object[] { new InvalidKeyPairException(), DracoonCryptoCode.INTERNAL_ERROR},
            new object[] { new InvalidFileKeyException(), DracoonCryptoCode.INTERNAL_ERROR},
            new object[] { new CryptoSystemException(), DracoonCryptoCode.SYSTEM_ERROR},
            new object[] { new InvalidCastException(), DracoonCryptoCode.UNKNOWN_ERROR }
        };

        [Theory]
        [MemberData(nameof(DataParseCause))]
        public void ParseCause(Exception param, DracoonCryptoCode expected) {
            // ARRANGE

            // ACT
            DracoonCryptoCode actual = CryptoErrorMapper.ParseCause(param);
            
            // ASSERT
            Assert.Equal(expected.Code, actual.Code);
            Assert.Equal(expected.Text, actual.Text);
        }

        #endregion

    }
}
