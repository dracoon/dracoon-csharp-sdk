using System;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test {
    public class DracoonAuthTest {
        #region Constructor 1

        [Fact]
        public void DracoonAuth_Ctor1() {
            // ARRANGE
            string expectedId = "id1";
            string expectedSecret = "secret1";
            string expectedCode = "code1";

            // ACT
            DracoonAuth actual = new DracoonAuth(expectedId, expectedSecret, expectedCode);

            // ASSERT
            Assert.Equal(expectedId, actual.ClientId);
            Assert.Equal(expectedSecret, actual.ClientSecret);
            Assert.Equal(expectedCode, actual.AuthorizationCode);
            Assert.Equal(DracoonAuth.Mode.AUTHORIZATION_CODE, actual.UsedMode);
        }

        [Theory]
        [InlineData(null, "secret1", "code1")]
        [InlineData("id1", null, "code1")]
        [InlineData("id1", "secret1", null)]
        public void DracoonAuth_Ctor1_ArgumentNullException(string expectedId, string expectedSecret, string expectedCode) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => new DracoonAuth(expectedId, expectedSecret, expectedCode));
        }

        [Theory]
        [InlineData("", "secret1", "code1")]
        [InlineData("id1", "", "code1")]
        [InlineData("id1", "secret1", "")]
        public void DracoonAuth_Ctor1_ArgumentException(string expectedId, string expectedSecret, string expectedCode) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => new DracoonAuth(expectedId, expectedSecret, expectedCode));
        }

        #endregion

        #region Constructor 2

        [Fact]
        public void DracoonAuth_Ctor2() {
            // ARRANGE
            string expectedToken = "token1";

            // ACT
            DracoonAuth actual = new DracoonAuth(expectedToken);

            // ASSERT
            Assert.Equal(expectedToken, actual.AccessToken);
            Assert.Equal(DracoonAuth.Mode.ACCESS_TOKEN, actual.UsedMode);
        }

        [Theory]
        [InlineData(null)]
        public void DracoonAuth_Ctor2_ArgumentNullException(string expectedToken) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => new DracoonAuth(expectedToken));
        }

        [Theory]
        [InlineData("")]
        public void DracoonAuth_Ctor2_ArgumentException(string expectedToken) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => new DracoonAuth(expectedToken));
        }

        #endregion

        #region Constructor 3

        [Fact]
        public void DracoonAuth_Ctor3() {
            // ARRANGE
            string expectedId = "id1";
            string expectedSecret = "secret1";
            string expectedToken = "token1";
            string expectedRefresh = "refresh1";

            // ACT
            DracoonAuth actual = new DracoonAuth(expectedId, expectedSecret, expectedToken, expectedRefresh);

            // ASSERT
            Assert.Equal(expectedId, actual.ClientId);
            Assert.Equal(expectedSecret, actual.ClientSecret);
            Assert.Equal(expectedToken, actual.AccessToken);
            Assert.Equal(expectedRefresh, actual.RefreshToken);
            Assert.Equal(DracoonAuth.Mode.ACCESS_REFRESH_TOKEN, actual.UsedMode);
        }

        [Theory]
        [InlineData(null, "secret1", "token1", "refresh1")]
        [InlineData("id1", null, "token1", "refresh1")]
        [InlineData("id1", "secret1", null, "refresh1")]
        public void DracoonAuth_Ctor3_ArgumentNullException(string expectedId, string expectedSecret, string expectedToken, string expectedRefresh) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => new DracoonAuth(expectedId, expectedSecret, expectedToken, expectedRefresh));
        }

        [Theory]
        [InlineData("", "secret1", "token1", "refresh1")]
        [InlineData("id1", "", "token1", "refresh1")]
        [InlineData("id1", "secret1", "", "refresh1")]
        [InlineData("id1", "secret1", "token1", "")]
        public void DracoonAuth_Ctor3_ArgumentException(string expectedId, string expectedSecret, string expectedToken, string expectedRefresh) {
            // ARRANGE

            // ACT - ASSERT
            Assert.Throws<ArgumentException>(() => new DracoonAuth(expectedId, expectedSecret, expectedToken, expectedRefresh));
        }

        #endregion
    }
}