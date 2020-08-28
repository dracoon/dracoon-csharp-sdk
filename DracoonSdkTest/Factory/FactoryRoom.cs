using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System.Collections.Generic;
using Dracoon.Sdk.Model;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal class FactoryRoom {
        internal static ApiCreateRoomRequest ApiCreateRoomRequest {
            get {
                return new ApiCreateRoomRequest {
                    ParentId = 132,
                    Name = "Room1",
                    Quota = 45867456,
                    Notes = "Some notes!",
                    RecycleBinRetentionPeriod = 5,
                    InheritPermissions = true,
                    AdminIds = new List<long> {
                        5,
                        2
                    },
                    AdminGroupIds = new List<long> {
                        1
                    },
                    NewGroupMemberAcceptance = "autoallow"
                };
            }
        }

        internal static CreateRoomRequest CreateRoomRequest {
            get {
                return new CreateRoomRequest("Room1", 132) {
                    Quota = 45867456,
                    Notes = "Some notes!",
                    RecycleBinRetentionPeriod = 5,
                    HasInheritPermissions = true,
                    AdminUserIds = new List<long> {
                        5,
                        2
                    },
                    AdminGroupIds = new List<long> {
                        1
                    },
                    NewGroupMemberAcceptance = GroupMemberAcceptance.AutoAllow
                };
            }
        }

        internal static ApiUpdateRoomRequest ApiUpdateRoomRequest {
            get {
                return new ApiUpdateRoomRequest {
                    Name = "Room1_rename",
                    Quota = 3456345,
                    Notes = "Some other notes."
                };
            }
        }

        internal static UpdateRoomRequest UpdateRoomRequest {
            get {
                return new UpdateRoomRequest(215) {
                    Name = "Room1_rename",
                    Quota = 3456345,
                    Notes = "Some other notes."
                };
            }
        }

        internal static ApiEnableRoomEncryptionRequest ApiEnableRoomEncryptionRequest {
            get {
                return new ApiEnableRoomEncryptionRequest {
                    DataRoomRescueKey = FactoryUser.ApiUserKeyPair_2048,
                    IsEncryptionEnabled = true,
                    UseDataSpaceRescueKey = false
                };
            }
        }

        internal static EnableRoomEncryptionRequest EnableRoomEncryptionRequest {
            get {
                return new EnableRoomEncryptionRequest(1254, true) {
                    UseDataSpaceRescueKey = false,
                    DataRoomRescueKeyPassword = "Pass1234!",
                    DataRoomRescueKeyPairAlgorithm = Crypto.Sdk.UserKeyPairAlgorithm.RSA2048
                };
            }
        }
    }
}