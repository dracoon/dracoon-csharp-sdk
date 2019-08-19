using System;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class FileMapperTest {
        #region ToApiUpdateFileRequest

        [Fact]
        public void ToApiUpdateFileRequest() {
            // ARRANGE
            ApiUpdateFileRequest expected = FactoryFile.ApiUpdateFileRequest;
            expected.Classification = 2;

            UpdateFileRequest param = new UpdateFileRequest(24654) {
                Classification = (Classification) Enum.ToObject(typeof(Classification), expected.Classification),
                Expiration = expected.Expiration.ExpireAt,
                Name = expected.Name,
                Notes = expected.Notes
            };

            Mock.Arrange(() => EnumConverter.ConvertClassificationEnumToValue(param.Classification)).Returns(expected.Classification);

            // ACT
            ApiUpdateFileRequest actual = FileMapper.ToApiUpdateFileRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiUpdateFileRequestComparer());
        }

        #endregion

        #region ToApiFileKey

        [Fact]
        public void ToApiFileKey() {
            // ARRANGE
            ApiFileKey expected = FactoryFile.ApiFileKey;

            EncryptedFileKey param = new EncryptedFileKey {
                Iv = expected.Iv,
                Key = expected.Key,
                Tag = expected.Tag,
                Version = expected.Version
            };

            // ACT
            ApiFileKey actual = FileMapper.ToApiFileKey(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiFileKeyComparer());
        }

        #endregion

        #region FromApiFileKey

        [Fact]
        public void FromApiFileKey() {
            // ARRANGE
            EncryptedFileKey expected = FactoryFile.EncryptedFileKey;

            ApiFileKey param = new ApiFileKey {
                Iv = expected.Iv,
                Key = expected.Key,
                Tag = expected.Tag,
                Version = expected.Version
            };

            // ACT
            EncryptedFileKey actual = FileMapper.FromApiFileKey(param);

            // ASSERT
            Assert.Equal(expected, actual, new EncryptedFileKeyComparer());
        }

        #endregion

        #region ToApiCreateFileUpload

        [Fact]
        public void ToApiCreateFileUpload() {
            // ARRANGE
            ApiCreateFileUpload expected = FactoryFile.ApiCreateFileUpload;
            expected.Classification = 3;

            FileUploadRequest param = new FileUploadRequest(expected.ParentId,
                expected.Name,
                (Classification) Enum.ToObject(typeof(Classification), expected.Classification)) {
                ExpirationDate = expected.Expiration.ExpireAt,
                Notes = expected.Notes,
                ResolutionStrategy = ResolutionStrategy.Overwrite
            };

            Mock.Arrange(() => EnumConverter.ConvertClassificationEnumToValue(param.Classification)).Returns(expected.Classification);

            // ACT
            ApiCreateFileUpload actual = FileMapper.ToApiCreateFileUpload(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCreateFileUploadComparer());
        }

        #endregion

        #region ToApiCompleteFileUpload

        [Fact]
        public void ToApiCompleteFileUpload() {
            // ARRANGE
            ResolutionStrategy paramStrategy = ResolutionStrategy.Overwrite;
            string expectedStrategy = "overwrite";

            ApiCompleteFileUpload expected = FactoryFile.ApiCompleteFileUpload;
            expected.FileKey = null;
            expected.ResolutionStrategy = expectedStrategy;

            FileUploadRequest param = new FileUploadRequest(63534, expected.FileName) {
                ResolutionStrategy = paramStrategy
            };

            Mock.Arrange(() => EnumConverter.ConvertResolutionStrategyToValue(paramStrategy)).Returns(expectedStrategy);

            // ACT
            ApiCompleteFileUpload actual = FileMapper.ToApiCompleteFileUpload(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCompleteFileUploadComparer());
        }

        #endregion
    }
}