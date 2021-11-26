using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryFolder {
        internal static ApiCreateFolderRequest ApiCreateFolderRequest => new ApiCreateFolderRequest {
            ParentId = 345678,
            Name = "Folder1",
            Notes = "Some notes!"
        };

        internal static CreateFolderRequest CreateFolderRequest => new CreateFolderRequest(345678, "Folder1") {
            Notes = "Some notes!"
        };

        internal static ApiUpdateFolderRequest ApiUpdateFolderRequest => new ApiUpdateFolderRequest {
            Name = "NewFolder1",
            Notes = "Some new notes!"
        };

        internal static UpdateFolderRequest UpdateFolderRequest => new UpdateFolderRequest(2534) {
            Name = "NewFolder1",
            Notes = "Some new notes!"
        };
    }
}