using Dracoon.Sdk.Error;
using Dracoon.Sdk.SdkInternal.Validator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Validator {
    public class ValidatorExtensionsTest {
        #region MustNotNull

        [Fact]
        public void MustNotNull_NotNull() {
            // ARRANGE
            string param = "Test";

            // ACT
            param.MustNotNull(nameof(MustNotNull_NotNull));

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void MustNotNull_ArgumentNullException() {
            // ARRANGE
            string param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.MustNotNull(nameof(MustNotNull_NotNull)));
        }

        #endregion

        #region EnumerableMustNotNullOrEmpty

        [Fact]
        public void EnumerableMustNotNullOrEmpty_NotNull_NotEmpty() {
            // ARRANGE
            List<long> param = new List<long> {
                1
            };

            // ACT
            param.EnumerableMustNotNullOrEmpty(nameof(EnumerableMustNotNullOrEmpty_NotNull_NotEmpty));

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void EnumerableMustNotNullOrEmpty_NotNull_Empty() {
            // ARRANGE
            List<long> param = new List<long>(0);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.EnumerableMustNotNullOrEmpty(nameof(EnumerableMustNotNullOrEmpty_NotNull_Empty)));
        }

        [Fact]
        public void EnumerableMustNotNullOrEmpty_Null() {
            // ARRANGE
            List<long> param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.EnumerableMustNotNullOrEmpty(nameof(EnumerableMustNotNullOrEmpty_Null)));
        }

        #endregion

        #region CheckEnumerableNullOrEmpty

        public static List<object[]> DataCheckEnumerable =>
            new List<object[]> {
                new object[] {
                    null, true
                },
                new object[] {
                    new List<long>(), true
                },
                new object[] {
                    new List<long> {
                        1
                    },
                    false
                }
            };

        [Theory]
        [MemberData(nameof(DataCheckEnumerable))]
        public void CheckEnumerableNullOrEmpty(List<long> param, bool expected) {
            // ARRANGE

            // ACT
            bool actual = param.CheckEnumerableNullOrEmpty();

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region MustNotNullOrEmptyOrWhitespace

        [Fact]
        public void MustNotNullOrEmptyOrWhitespace_NotNull_NotEmpty_NoWhitespace() {
            // ARRANGE
            string param = "Test";

            // ACT
            param.MustNotNullOrEmptyOrWhitespace(nameof(MustNotNullOrEmptyOrWhitespace_NotNull_NotEmpty_NoWhitespace));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void MustNotNullOrEmptyOrWhitespace_NotNull_Empty_Whitespace(string param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() =>
                param.MustNotNullOrEmptyOrWhitespace(nameof(MustNotNullOrEmptyOrWhitespace_NotNull_NotEmpty_NoWhitespace)));
        }

        [Fact]
        public void MustNotNullOrEmptyOrWhitespace_Null() {
            // ARRANGE
            string param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.MustNotNullOrEmptyOrWhitespace(nameof(MustNotNullOrEmptyOrWhitespace_Null)));
        }

        [Fact]
        public void MustNotNullOrEmptyOrWhitespace_AllowedNull_Null() {
            // ARRANGE
            string param = null;

            // ACT
            param.MustNotNullOrEmptyOrWhitespace(nameof(MustNotNullOrEmptyOrWhitespace_AllowedNull_Null), true);

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void MustNotNullOrEmptyOrWhitespace_AllowedNull_Null_Empty_Whitespace(string param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() =>
                param.MustNotNullOrEmptyOrWhitespace(nameof(MustNotNullOrEmptyOrWhitespace_AllowedNull_Null_Empty_Whitespace), true));
        }

        #endregion

        #region MustNotNullOrEmpty

        [Fact]
        public void MustNotNullOrEmpty_NotNull_NotEmpty() {
            // ARRANGE
            byte[] param = Encoding.UTF8.GetBytes("Test");

            // ACT
            param.MustNotNullOrEmpty(nameof(MustNotNullOrEmpty_NotNull_NotEmpty));

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void MustNotNullOrEmpty_NotNull_Empty() {
            // ARRANGE
            byte[] param = new byte[0];

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() =>
                param.MustNotNullOrEmpty(nameof(MustNotNullOrEmpty_NotNull_Empty)));
        }

        [Fact]
        public void MustNotNullOrEmpty_Null() {
            // ARRANGE
            byte[] param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.MustNotNullOrEmpty(nameof(MustNotNullOrEmpty_Null)));
        }

        [Fact]
        public void MustNotNullOrEmpty_AllowedNull_Null() {
            // ARRANGE
            byte[] param = null;

            // ACT
            param.MustNotNullOrEmpty(nameof(MustNotNullOrEmpty_AllowedNull_Null), true);

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void MustNotNullOrEmpty_AllowedNull_Null_Empty() {
            // ARRANGE
            byte[] param = new byte[0];

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() =>
                param.MustNotNullOrEmpty(nameof(MustNotNullOrEmpty_AllowedNull_Null_Empty), true));
        }

        #endregion

        #region MustValidNodePath

        [Theory]
        [InlineData("/")]
        [InlineData("Root")]
        [InlineData("/Root/")]
        [InlineData("<>")]
        [InlineData("")]
        [InlineData("  ")]
        public void MustValidNodePath_InvalidPath(string param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustValidNodePath(nameof(MustValidNodePath_InvalidPath)));
        }

        [Fact]
        public void MustValidNodePath_Null() {
            // ARRANGE
            string param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.MustValidNodePath(nameof(MustValidNodePath_Null)));
        }

        [Fact]
        public void MustValidNodePath_InvalidPathLength() {
            // ARRANGE
            string param = new string('*', 4097);

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustValidNodePath(nameof(MustValidNodePath_InvalidPathLength)));
        }

        #endregion

        #region CheckStreamCanRead

        [Fact]
        public void CheckStreamCanRead_Success() {
            // ARRANGE
            Stream param = new MemoryStream(new byte[0], false);

            // ACT
            param.CheckStreamCanRead(nameof(CheckStreamCanRead_Success));
            param.Close();

            // ASSERT
            // No exception should be thrown
        }

        #endregion

        #region CheckStreamCanWrite

        [Fact]
        public void CheckStreamCanWrite_Success() {
            // ARRANGE
            Stream param = new MemoryStream(new byte[1], true);

            // ACT
            param.CheckStreamCanWrite(nameof(CheckStreamCanWrite_Success));
            param.Close();

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void CheckStreamCanWrite_CantWrite() {
            // ARRANGE
            Stream param = new MemoryStream(new byte[1], false);

            // ACT - ASSERT
            Assert.Throws<DracoonFileIOException>(() => param.CheckStreamCanWrite(nameof(CheckStreamCanWrite_CantWrite)));
            param.Close();
        }

        #endregion

        #region Uri MustBeValid

        public static List<object[]> DataUriMustBeValid =>
            new List<object[]> {
                new object[] {
                    new Uri("ftp://www.dracoon.com")
                },
                new object[] {
                    new UriBuilder("https://www.dracoon.com") {
                        UserName = "A",
                        Password = "B"
                    }.Uri
                },
                new object[] {
                    new Uri("https://www.dracoon.com?param=1")
                }
            };

        [Fact]
        public void UriMustBeValid_Success() {
            // ARRANGE
            Uri param = new Uri("https://www.dracoon.com");

            // ACT
            param.MustBeValid(nameof(UriMustBeValid_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void UriMustBeValid_Null() {
            // ARRANGE
            Uri param = null;

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => param.MustBeValid(nameof(UriMustBeValid_Null)));
        }

        [Theory]
        [MemberData(nameof(DataUriMustBeValid))]
        public void UriMustBeValid_Fail(Uri param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustBeValid(nameof(UriMustBeValid_Fail)));
        }

        #endregion

        #region Long MustPositive

        [Fact]
        public void Long_MustPositive_Success() {
            // ARRANGE
            long param = 5;

            // ACT
            param.MustPositive(nameof(Long_MustPositive_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void Long_MustPositive_Fail(long param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustPositive(nameof(Long_MustPositive_Fail)));
        }

        #endregion

        #region Integer MustPositive

        [Fact]
        public void Int_MustPositive_Success() {
            // ARRANGE
            int param = 5;

            // ACT
            param.MustPositive(nameof(Int_MustPositive_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void Int_MustPositive_Fail(int param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustPositive(nameof(Int_MustPositive_Fail)));
        }

        #endregion

        #region Long MustNotNegative

        [Theory]
        [InlineData(7)]
        [InlineData(0)]
        public void Long_MustNotNegative_Success(long param) {
            // ARRANGE

            // ACT
            param.MustNotNegative(nameof(Long_MustNotNegative_Fail));

            // ASSERT
            // No exception should be thrown
        }

        [Fact]
        public void Long_MustNotNegative_Fail() {
            // ARRANGE
            long param = -5;

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustNotNegative(nameof(Long_MustNotNegative_Fail)));
        }

        #endregion

        #region Integer MustNotNegative

        [Fact]
        public void Int_MustNotNegative_Fail() {
            // ARRANGE
            int param = -5;

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.MustNotNegative(nameof(Int_MustNotNegative_Fail)));
        }

        [Theory]
        [InlineData(7)]
        [InlineData(0)]
        public void Int_MustNotNegative_Success(int param) {
            // ARRANGE

            // ACT
            param.MustNotNegative(nameof(Int_MustNotNegative_Success));

            // ASSERT
            // No exception should be thrown
        }

        #endregion

        #region Long NullableMustPositive

        [Theory]
        [InlineData(5)]
        [InlineData(null)]
        public void NullableLong_MustPositive_Success(long? param) {
            // ARRANGE

            // ACT
            param.NullableMustPositive(nameof(NullableLong_MustPositive_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void NullableLong_MustPositive_Fail(long? param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.NullableMustPositive(nameof(NullableLong_MustPositive_Fail)));
        }

        #endregion

        #region Int NullableMustPositive

        [Theory]
        [InlineData(5)]
        [InlineData(null)]
        public void NullableInt_MustPositive_Success(int? param) {
            // ARRANGE

            // ACT
            param.NullableMustPositive(nameof(NullableInt_MustPositive_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        [InlineData(0)]
        public void NullableInt_MustPositive_Fail(int? param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.NullableMustPositive(nameof(NullableInt_MustPositive_Fail)));
        }

        #endregion

        #region Long NullableMustNotNegative

        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(null)]
        public void NullableLong_MustNotNegative_Success(long? param) {
            // ARRANGE

            // ACT
            param.NullableMustNotNegative(nameof(NullableLong_MustNotNegative_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        public void NullableLong_MustNotNegative_Fail(long? param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.NullableMustNotNegative(nameof(NullableLong_MustNotNegative_Fail)));
        }

        #endregion

        #region Int NullableMustNotNegative

        [Theory]
        [InlineData(5)]
        [InlineData(0)]
        [InlineData(null)]
        public void NullableInt_MustNotNegative_Success(int? param) {
            // ARRANGE

            // ACT
            param.NullableMustNotNegative(nameof(NullableInt_MustNotNegative_Success));

            // ASSERT
            // No exception should be thrown
        }

        [Theory]
        [InlineData(-5)]
        public void NullableInt_MustNotNegative_Fail(int? param) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => param.NullableMustNotNegative(nameof(NullableInt_MustNotNegative_Fail)));
        }

        #endregion
    }
}