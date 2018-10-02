using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class FolderMapper {
        internal static ApiCreateFolderRequest ToApiCreateFolderRequest(CreateFolderRequest createFolderRequest) {
            ApiCreateFolderRequest apiCreateFolderRequest = new ApiCreateFolderRequest() {
                ParentId = createFolderRequest.ParentId,
                Name = createFolderRequest.Name,
                Notes = createFolderRequest.Notes
            };
            return apiCreateFolderRequest;
        }

        internal static ApiUpdateFolderRequest ToApiUpdateFolderRequest(UpdateFolderRequest updateFolderRequest) {
            ApiUpdateFolderRequest apiUpdateFolderRequest = new ApiUpdateFolderRequest() {
                Name = updateFolderRequest.Name,
                Notes = updateFolderRequest.Notes
            };
            return apiUpdateFolderRequest;
        }
    }
}
