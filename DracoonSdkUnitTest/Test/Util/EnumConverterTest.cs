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


        #region ConvertValueToUserAuthMethodEnum

        [Theory]
        [InlineData("basic", UserAuthMethod.Basic)]
        [InlineData("active_directory", UserAuthMethod.ActiveDirectory)]
        [InlineData("radius", UserAuthMethod.Radius)]
        [InlineData("openid", UserAuthMethod.OpenID)]
        [InlineData("things", UserAuthMethod.Unknown)]
        public void ConvertValueToUserAuthMethodEnum(string value, UserAuthMethod expected) {
            // ARRANGE

            // ACT
            UserAuthMethod actual = EnumConverter.ConvertValueToUserAuthMethodEnum(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertUserAuthMethodEnumToValue

        [Theory]
        [InlineData(UserAuthMethod.Basic, "basic")]
        [InlineData(UserAuthMethod.ActiveDirectory, "active_directory")]
        [InlineData(UserAuthMethod.Radius, "radius")]
        [InlineData(UserAuthMethod.OpenID, "openid")]
        [InlineData(UserAuthMethod.Unknown, "unknown")]
        public void ConvertUserAuthMethodEnumToValue(UserAuthMethod value, string expected) {
            // ARRANGE

            // ACT
            string actual = EnumConverter.ConvertUserAuthMethodEnumToValue(value);

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

        #region ConvertValueToUserTypeEnum

        [Theory]
        [InlineData("internal", UserType.Internal)]
        [InlineData("external", UserType.External)]
        [InlineData("system", UserType.System)]
        [InlineData("deleted", UserType.Deleted)]
        public void ConvertValueToUserTypeEnum(string value, UserType expected) {
            // ARRANGE

            // ACT
            UserType actual = EnumConverter.ConvertValueToUserTypeEnum(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region ConvertValueToSubscriptionPlanEnum

        [Theory]
        [InlineData(0, SubscriptionPlan.Standard)]
        [InlineData(1, SubscriptionPlan.Premium)]
        [InlineData(2, SubscriptionPlan.Free)]
        public void ConvertValueToSubscriptionPlanEnum(int value, SubscriptionPlan expected) {
            // ARRANGE

            // ACT
            SubscriptionPlan actual = EnumConverter.ConvertValueToSubscriptionPlanEnum(value);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}