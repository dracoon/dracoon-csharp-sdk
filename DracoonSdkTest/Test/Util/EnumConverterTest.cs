using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.Util;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Util {
    public class EnumConverterTest {
        #region ConvertValueToNodeTypeEnum

        [Theory]
        [InlineData("room", NodeType.Room)]
        [InlineData("folder", NodeType.Folder)]
        [InlineData("file", NodeType.File)]
        public void ConvertValueToNodeTypeEnum(string value, NodeType expected) {
            // ARRANGE

            // ACT
            NodeType actual = EnumConverter.ConvertValueToNodeTypeEnum(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertNodeTypeEnumToValue

        [Theory]
        [InlineData(NodeType.Room, "room")]
        [InlineData(NodeType.Folder, "folder")]
        [InlineData(NodeType.File, "file")]
        public void ConvertNodeTypeEnumToValue(NodeType value, string expected) {
            // ARRANGE

            // ACT
            string actual = EnumConverter.ConvertNodeTypeEnumToValue(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertValueToClassificationEnum

        [Theory]
        [InlineData(1, Classification.Public)]
        [InlineData(2, Classification.Internal)]
        [InlineData(3, Classification.Confidential)]
        [InlineData(4, Classification.StrictlyConfidential)]
        [InlineData(null, null)]
        public void ConvertValueToClassificationEnum(int? value, Classification? expected) {
            // ARRANGE

            // ACT
            Classification? actual = EnumConverter.ConvertValueToClassificationEnum(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertClassificationEnumToValue

        [Theory]
        [InlineData(Classification.Public, 1)]
        [InlineData(Classification.Internal, 2)]
        [InlineData(Classification.Confidential, 3)]
        [InlineData(Classification.StrictlyConfidential, 4)]
        [InlineData(null, null)]
        public void ConvertClassificationEnumToValue(Classification? value, int? expected) {
            // ARRANGE

            // ACT
            int? actual = EnumConverter.ConvertClassificationEnumToValue(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertGroupMemberAcceptanceToValue

        [Theory]
        [InlineData(GroupMemberAcceptance.AutoAllow, "autoallow")]
        [InlineData(GroupMemberAcceptance.Pending, "pending")]
        [InlineData(null, null)]
        public void ConvertGroupMemberAcceptanceToValue(GroupMemberAcceptance? value, string expected) {
            // ARRANGE

            // ACT
            string actual = EnumConverter.ConvertGroupMemberAcceptanceToValue(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertResolutionStrategyToValue

        [Theory]
        [InlineData(ResolutionStrategy.AutoRename, "autorename")]
        [InlineData(ResolutionStrategy.Fail, "fail")]
        [InlineData(ResolutionStrategy.Overwrite, "overwrite")]
        public void ConvertResolutionStrategyToValue(ResolutionStrategy value, string expected) {
            // ARRANGE

            // ACT
            string actual = EnumConverter.ConvertResolutionStrategyToValue(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}