using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using System;
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
                Classification = (Classification)Enum.ToObject(typeof(Classification), expected.Classification),
                Expiration = expected.Expiration.ExpireAt,
                Name = expected.Name,
                Notes = expected.Notes,
                CreationTime = expected.CreationTime,
                ModificationTime = expected.ModificationTime
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
            Mock.Arrange(() => FileMapper.ToApiFileKeyVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(expected.Version).Occurs(1);

            EncryptedFileKey param = new EncryptedFileKey {
                Iv = expected.Iv,
                Key = expected.Key,
                Tag = expected.Tag,
                Version = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM
            };

            // ACT
            ApiFileKey actual = FileMapper.ToApiFileKey(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiFileKeyComparer());
            Mock.Assert(() => FileMapper.ToApiFileKeyVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>()));
        }

        #endregion

        #region FromApiFileKey

        [Fact]
        public void FromApiFileKey() {
            // ARRANGE
            EncryptedFileKey expected = FactoryFile.EncryptedFileKey;
            Mock.Arrange(() => FileMapper.FromApiFileKeyVersion(Arg.AnyString)).Returns(expected.Version).Occurs(1);

            ApiFileKey param = new ApiFileKey {
                Iv = expected.Iv,
                Key = expected.Key,
                Tag = expected.Tag,
                Version = "A"
            };

            // ACT
            EncryptedFileKey actual = FileMapper.FromApiFileKey(param);

            // ASSERT
            Assert.Equal(expected, actual, new EncryptedFileKeyComparer());
            Mock.Assert(() => FileMapper.FromApiFileKeyVersion(Arg.AnyString));
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
                (Classification)Enum.ToObject(typeof(Classification), expected.Classification)) {
                ExpirationDate = expected.Expiration.ExpireAt,
                Notes = expected.Notes,
                ResolutionStrategy = ResolutionStrategy.Overwrite,
                CreationTime = expected.CreationTime,
                ModificationTime = expected.ModificationTime
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
            expected.KeepShareLinks = true;

            FileUploadRequest param = new FileUploadRequest(63534, expected.FileName) {
                ResolutionStrategy = paramStrategy,
                KeepShareLinks = expected.KeepShareLinks
            };

            Mock.Arrange(() => EnumConverter.ConvertResolutionStrategyToValue(paramStrategy)).Returns(expectedStrategy);

            // ACT
            ApiCompleteFileUpload actual = FileMapper.ToApiCompleteFileUpload(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCompleteFileUploadComparer());
        }

        #endregion

        #region ToApiFileKeyVersion

        [Fact]
        public void ToApiFileKeyVersion_2048() {
            // ARRANGE
            string expected = "A";

            EncryptedFileKeyAlgorithm param = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM;

            // ACT
            string actual = FileMapper.ToApiFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ToApiFileKeyVersion_4096() {
            // ARRANGE
            string expected = "RSA-4096/AES-256-GCM";

            EncryptedFileKeyAlgorithm param = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM;

            // ACT
            string actual = FileMapper.ToApiFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

        #region FromApiFileKeyVersion

        [Fact]
        public void FromApiFileKeyVersion_2048() {
            // ARRANGE
            EncryptedFileKeyAlgorithm expected = EncryptedFileKeyAlgorithm.RSA2048_AES256GCM;

            string param = "A";

            // ACT
            EncryptedFileKeyAlgorithm actual = FileMapper.FromApiFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromApiFileKeyVersion_4096() {
            // ARRANGE
            EncryptedFileKeyAlgorithm expected = EncryptedFileKeyAlgorithm.RSA4096_AES256GCM;

            string param = "RSA-4096/AES-256-GCM";

            // ACT
            EncryptedFileKeyAlgorithm actual = FileMapper.FromApiFileKeyVersion(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromApiFileKeyVersion_Fail() {
            // ARRANGE
            int expected = DracoonCryptoCode.UNKNOWN_ALGORITHM_ERROR.Code;

            string param = "UnknownAlgorithm";

            try {
                // ACT
                FileMapper.FromApiFileKeyVersion(param);
            } catch (DracoonCryptoException e) {
                // ASSERT
                Assert.Equal(expected, e.ErrorCode.Code);
            }
        }

        #endregion

        #region FromApiFileVirusProtectionInfo

        [Fact]
        public void FromApiFileVirusProtectionInfo() {
            // ARRANGE
            FileVirusProtectionInfo expected = FactoryFile.FileVirusProtectionInfo;

            ApiFileVirusProtectionInfo param = new ApiFileVirusProtectionInfo() {
                Verdict = "CLEAN",
                LastCheckedAt = expected.CheckedAt,
                Sha256 = expected.Sha256,
                NodeId = expected.NodeId
            };

            // ACT
            FileVirusProtectionInfo actual = FileMapper.FromApiFileVirusProtectionInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new FileVirusProtectionInfoComparer());
        }

        [Fact]
        public void FromApiFileVirusProtectionInfo_null() {
            // ARRANGE
            FileVirusProtectionInfo expected = null;

            ApiFileVirusProtectionInfo param = null;

            // ACT
            FileVirusProtectionInfo actual = FileMapper.FromApiFileVirusProtectionInfo(param);

            // ASSERT
            Assert.Equal(expected, actual);
        }

        #endregion

    }
}