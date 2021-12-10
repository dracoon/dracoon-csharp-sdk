using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class FolderMapperTest {
        #region ToApiCreateFolderRequest

        [Fact]
        public void ToApiCreateFolderRequest() {
            // ARRANGE
            ApiCreateFolderRequest expected = FactoryFolder.ApiCreateFolderRequest;

            CreateFolderRequest param = new CreateFolderRequest(expected.ParentId, expected.Name) {
                Notes = expected.Notes,
                Classification = Classification.Internal,
                CreationTime = expected.TimestampCreation,
                ModificationTime = expected.TimestampModification
            };

            // ACT
            ApiCreateFolderRequest actual = FolderMapper.ToApiCreateFolderRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCreateFolderRequestComparer());
        }

        #endregion

        #region ToApiUpdateFolderRequest

        [Fact]
        public void ToApiUpdateFolderRequest() {
            // ARRANGE
            ApiUpdateFolderRequest expected = FactoryFolder.ApiUpdateFolderRequest;

            UpdateFolderRequest param = new UpdateFolderRequest(346) {
                Name = expected.Name,
                Notes = expected.Notes,
                Classification = Classification.Internal,
                CreationTime = expected.TimestampCreation,
                ModificationTime = expected.TimestampModification
            };

            // ACT
            ApiUpdateFolderRequest actual = FolderMapper.ToApiUpdateFolderRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiUpdateFolderRequestComparer());
        }

        #endregion
    }
}