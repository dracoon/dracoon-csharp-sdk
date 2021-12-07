using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryFolder {
        internal static ApiCreateFolderRequest ApiCreateFolderRequest => new ApiCreateFolderRequest {
            ParentId = 345678,
            Name = "Folder1",
            Notes = "Some notes!",
            Classification = 2,
            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0)
        };

        internal static CreateFolderRequest CreateFolderRequest => new CreateFolderRequest(345678, "Folder1") {
            Notes = "Some notes!",
            Classification = Classification.Internal,
            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0)
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