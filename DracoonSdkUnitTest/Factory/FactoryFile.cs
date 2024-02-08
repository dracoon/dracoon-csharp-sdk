﻿using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryFile {
        internal static ApiUpdateFileRequest ApiUpdateFileRequest => new ApiUpdateFileRequest {
            Classification = 2,
            Expiration = new ApiExpiration {
                ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                EnableExpiration = true
            },
            Name = "NewFile1",
            Notes = "Some new ",
            CreationTime = new DateTime(2020, 1, 1, 5, 10, 15),
            ModificationTime = new DateTime(2020, 1, 1, 10, 10, 15)
        };

        internal static UpdateFileRequest UpdateFileRequest => new UpdateFileRequest(254) {
            Classification = Classification.Internal,
            Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
            Name = "NewFile1",
            Notes = "Some new",
            CreationTime = new DateTime(2020, 1, 1, 5, 10, 15),
            ModificationTime = new DateTime(2020, 1, 1, 10, 10, 15)
        };

        internal static FileUploadRequest UploadFileRequest => new FileUploadRequest(1423, "file.file", Classification.Public) {
            Notes = "Some notes!",
            ResolutionStrategy = ResolutionStrategy.Overwrite,
            ExpirationDate = new DateTime(2000, 1, 1, 0, 0, 0)
        };

        internal static PlainFileKey PlainFileKey => new PlainFileKey {
            Iv = "PlainIv",
            Key = "PlainKey",
            Tag = "PlainTag",
            Version = Crypto.Sdk.PlainFileKeyAlgorithm.AES256GCM
        };

        internal static ApiFileKey ApiFileKey => new ApiFileKey {
            Iv = "TestIv",
            Key = "TestKey",
            Tag = "TestTag",
            Version = "A"
        };

        internal static EncryptedFileKey EncryptedFileKey {
            get {
                ApiFileKey fileKey = ApiFileKey;
                return new EncryptedFileKey {
                    Iv = fileKey.Iv,
                    Key = fileKey.Key,
                    Tag = fileKey.Tag,
                    Version = Crypto.Sdk.EncryptedFileKeyAlgorithm.RSA2048_AES256GCM
                };
            }
        }

        internal static ApiCreateFileUpload ApiCreateFileUpload => new ApiCreateFileUpload {
            Classification = 3,
            Expiration = new ApiExpiration {
                ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                EnableExpiration = true
            },
            Name = "FileName1",
            Notes = "Some notes!",
            ParentId = 436897,
            UseS3 = false,
            CreationTime = new DateTime(2020, 1, 1, 5, 10, 15),
            ModificationTime = new DateTime(2020, 1, 1, 10, 10, 15)
        };

        internal static ApiCompleteFileUpload ApiCompleteFileUpload => new ApiCompleteFileUpload {
            FileName = "FileName1",
            ResolutionStrategy = "overwrite",
            FileKey = ApiFileKey
        };

        internal static ApiCompleteFileUpload ApiCompleteS3FileUpload => new ApiCompleteFileUpload {
            FileName = "FileName1",
            ResolutionStrategy = "overwrite",
            FileKey = ApiFileKey,
            Parts = new List<ApiS3FileUploadPart> {
                        ApiS3FileUploadPart
                    }
        };

        internal static ApiSetUserFileKeysRequest ApiSetUserFileKeysRequest => new ApiSetUserFileKeysRequest {
            Items = new List<ApiSetUserFileKey> {
                        new ApiSetUserFileKey {
                            FileId = 73456,
                            UserId = 123,
                            FileKey = ApiFileKey
                        }
                    }
        };

        internal static ApiMissingFileKeys ApiMissingFileKeys => new ApiMissingFileKeys {
            Items = new List<ApiUserIdFileId> {
                        new ApiUserIdFileId {
                            FileId = 1543,
                            UserId = 53
                        }
                    },
            FileKeys = new List<ApiFileIdFileKey> {
                        new ApiFileIdFileKey {
                            FileId = 1543,
                            FileKeyContainer = ApiFileKey
                        }
                    },
            Range = new ApiRange {
                Limit = 1,
                Offset = 0,
                Total = 1
            },
            UserPublicKey = new List<ApiUserIdPublicKey> {
                        ApiUserIdPublicKey
                    }
        };

        internal static ApiUserIdPublicKey ApiUserIdPublicKey => new ApiUserIdPublicKey {
            UserId = 53,
            PublicKeyContainer = FactoryUser.ApiUserPublicKey_2048
        };

        internal static Dictionary<long, UserPublicKey> FileUserPublicKey => new Dictionary<long, UserPublicKey> {
                    {
                        53, FactoryUser.UserPublicKey_2048
                    }
                };

        internal static ApiUploadChunkResult ApiUploadChunkResult => new ApiUploadChunkResult {
            Hash = "HHD8FDH707D8GV",
            Size = 12345
        };

        internal static ApiUploadToken ApiUploadToken => new ApiUploadToken {
            UploadId = "uploadId",
            UploadUrl = "https://dracoon.team/uploadtoken"
        };

        internal static ApiGetS3Urls ApiGetS3UrlsRequest => new ApiGetS3Urls {
            Size = 3456,
            FirstPartNumber = 1,
            LastPartNumber = 5
        };

        internal static ApiS3Urls ApiS3Urls => new ApiS3Urls {
            Urls = new List<ApiS3Url> {
                        ApiS3Url
                    }
        };

        internal static ApiS3Url ApiS3Url => new ApiS3Url {
            PartNumber = 1,
            Url = "https://dracoon.s3.com"
        };

        internal static ApiS3Status ApiS3Status => new ApiS3Status {
            Node = FactoryNode.ApiNode,
            Status = "done",
            ErrorInfo = null
        };

        internal static ApiS3FileUploadPart ApiS3FileUploadPart => new ApiS3FileUploadPart {
            PartNumber = 1,
            PartEtag = "f78dfg67s7fd8s8f7"
        };
    }
}