using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class RoomMapperTest {
        #region ToApiCreateRoomRequest

        [Fact]
        public void ToApiCreateRoomRequest() {
            // ARRANGE
            GroupMemberAcceptance paramGMA = GroupMemberAcceptance.AutoAllow;
            string expectedGMAValue = "autoallow";

            ApiCreateRoomRequest expected = FactoryRoom.ApiCreateRoomRequest;
            expected.NewGroupMemberAcceptance = expectedGMAValue;

            CreateRoomRequest param = new CreateRoomRequest(expected.Name) {
                ParentId = expected.ParentId.Value,
                Quota = expected.Quota,
                Notes = expected.Notes,
                RecycleBinRetentionPeriod = expected.RecycleBinRetentionPeriod,
                HasInheritPermissions = expected.InheritPermissions.Value,
                AdminUserIds = expected.AdminIds,
                AdminGroupIds = expected.AdminGroupIds,
                NewGroupMemberAcceptance = paramGMA,
                Classification = Classification.Internal,
                HasActivitiesLog = expected.HasActivitiesLog,
                CreationTime = expected.TimestampCreation,
                ModificationTime = expected.TimestampModification
            };

            Mock.Arrange(() => EnumConverter.ConvertGroupMemberAcceptanceToValue(paramGMA)).Returns(expectedGMAValue);

            // ACT
            ApiCreateRoomRequest actual = RoomMapper.ToApiCreateRoomRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCreateRoomRequestComparer());
        }

        #endregion

        #region ToApiUpdateRoomRequest

        [Fact]
        public void ToApiUpdateRoomRequest() {
            // ARRANGE
            ApiUpdateRoomRequest expected = FactoryRoom.ApiUpdateRoomRequest;

            UpdateRoomRequest param = new UpdateRoomRequest(12) {
                Name = expected.Name,
                Quota = expected.Quota,
                Notes = expected.Notes,
                CreationTime = expected.TimestampCreation,
                ModificationTime = expected.TimestampModification
            };

            // ACT
            ApiUpdateRoomRequest actual = RoomMapper.ToApiUpdateRoomRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiUpdateRoomRequestComparer());
        }

        #endregion

        #region ToApiEnableRoomEncryptionRequest

        [Fact]
        public void ToApiEnableRoomEncryptionRequest() {
            // ARRANGE
            ApiEnableRoomEncryptionRequest expected = FactoryRoom.ApiEnableRoomEncryptionRequest;

            EnableRoomEncryptionRequest param = new EnableRoomEncryptionRequest(1234, expected.IsEncryptionEnabled) {
                DataRoomRescueKeyPassword = "Pass12!".ToCharArray(),
                UseDataSpaceRescueKey = expected.UseDataSpaceRescueKey,
            };

            // ACT
            ApiEnableRoomEncryptionRequest actual = RoomMapper.ToApiEnableRoomEncryptionRequest(param, expected.DataRoomRescueKey);

            // ASSERT
            Assert.Equal(expected, actual, new ApiEnableRoomEncryptionRequestComparer());
        }

        #endregion
    }
}