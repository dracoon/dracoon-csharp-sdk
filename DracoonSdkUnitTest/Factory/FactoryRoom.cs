using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryRoom {
        internal static ApiCreateRoomRequest ApiCreateRoomRequest => new ApiCreateRoomRequest {
            ParentId = 132,
            Name = "Room1",
            Quota = 45867456,
            Notes = "Some notes!",
            RecycleBinRetentionPeriod = 5,
            InheritPermissions = true,
            Classification = 2,
            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0),
            HasActivitiesLog = true,
            AdminIds = new List<long> {
                        5,
                        2
                    },
            AdminGroupIds = new List<long> {
                        1
                    },
            NewGroupMemberAcceptance = "autoallow"
        };

        internal static CreateRoomRequest CreateRoomRequest => new CreateRoomRequest("Room1", 132) {
            Quota = 45867456,
            Notes = "Some notes!",
            RecycleBinRetentionPeriod = 5,
            HasInheritPermissions = true,
            Classification = Classification.Internal,
            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0),
            HasActivitiesLog = true,
            AdminUserIds = new List<long> {
                        5,
                        2
                    },
            AdminGroupIds = new List<long> {
                        1
                    },
            NewGroupMemberAcceptance = GroupMemberAcceptance.AutoAllow
        };

        internal static ApiUpdateRoomRequest ApiUpdateRoomRequest => new ApiUpdateRoomRequest {
            Name = "Room1_rename",
            Quota = 3456345,
            Notes = "Some other notes."
        };

        internal static UpdateRoomRequest UpdateRoomRequest => new UpdateRoomRequest(215) {
            Name = "Room1_rename",
            Quota = 3456345,
            Notes = "Some other notes."
        };

        internal static ApiEnableRoomEncryptionRequest ApiEnableRoomEncryptionRequest => new ApiEnableRoomEncryptionRequest {
            DataRoomRescueKey = FactoryUser.ApiUserKeyPair_2048,
            IsEncryptionEnabled = true,
            UseDataSpaceRescueKey = false
        };

        internal static EnableRoomEncryptionRequest EnableRoomEncryptionRequest => new EnableRoomEncryptionRequest(1254, true) {
            UseDataSpaceRescueKey = false,
            DataRoomRescueKeyPassword = "Pass1234!",
            DataRoomRescueKeyPairAlgorithm = Crypto.Sdk.UserKeyPairAlgorithm.RSA2048
        };
    }
}