using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Util;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class RoomMapper {
        internal static ApiCreateRoomRequest ToApiCreateRoomRequest(CreateRoomRequest createRoomRequest) {
            ApiCreateRoomRequest apiCreateRoomRequest = new ApiCreateRoomRequest {
                ParentId = createRoomRequest.ParentId,
                Name = createRoomRequest.Name,
                Quota = createRoomRequest.Quota,
                Notes = createRoomRequest.Notes,
                RecycleBinRetentionPeriod = createRoomRequest.RecycleBinRetentionPeriod,
                InheritPermissions = createRoomRequest.HasInheritPermissions,
                AdminIds = createRoomRequest.AdminUserIds,
                AdminGroupIds = createRoomRequest.AdminGroupIds,
                NewGroupMemberAcceptance = EnumConverter.ConvertGroupMemberAcceptanceToValue(createRoomRequest.NewGroupMemberAcceptance)
            };
            return apiCreateRoomRequest;
        }

        internal static ApiUpdateRoomRequest ToApiUpdateRoomRequest(UpdateRoomRequest updateRoomRequest) {
            ApiUpdateRoomRequest apiUpdateRoomRequest = new ApiUpdateRoomRequest {
                Name = updateRoomRequest.Name,
                Quota = updateRoomRequest.Quota,
                Notes = updateRoomRequest.Notes
            };
            return apiUpdateRoomRequest;
        }

        internal static ApiEnableRoomEncryptionRequest ToApiEnableRoomEncryptionRequest(EnableRoomEncryptionRequest enableRoomEncryptionRequest,
            ApiUserKeyPair dataRoomRescueKey) {
            ApiEnableRoomEncryptionRequest apiEnableRoomEncryptionRequest = new ApiEnableRoomEncryptionRequest {
                IsEncryptionEnabled = enableRoomEncryptionRequest.IsEncryptionEnabled,
                UseDataSpaceRescueKey = enableRoomEncryptionRequest.UseDataSpaceRescueKey,
                DataRoomRescueKey = dataRoomRescueKey
            };
            return apiEnableRoomEncryptionRequest;
        }
    }
}