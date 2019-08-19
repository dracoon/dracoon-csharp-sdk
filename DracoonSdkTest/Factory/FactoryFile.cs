using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;
using Dracoon.Sdk.Model;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryFile {
        internal static ApiUpdateFileRequest ApiUpdateFileRequest {
            get {
                return new ApiUpdateFileRequest {
                    Classification = 2,
                    Expiration = new ApiExpiration {
                        ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                        EnableExpiration = true
                    },
                    Name = "NewFile1",
                    Notes = "Some new "
                };
            }
        }

        internal static UpdateFileRequest UpdateFileRequest {
            get {
                return new UpdateFileRequest(254) {
                    Classification = Classification.Internal,
                    Expiration = new DateTime(2000, 1, 1, 0, 0, 0),
                    Name = "NewFile1",
                    Notes = "Some new"
                };
            }
        }

        internal static FileUploadRequest UploadFileRequest {
            get {
                return new FileUploadRequest(1423, "file.file", Classification.Public) {
                    Notes = "Some notes!",
                    ResolutionStrategy = ResolutionStrategy.Overwrite,
                    ExpirationDate = new DateTime(2000, 1, 1, 0, 0, 0)
                };
            }
        }

        internal static PlainFileKey PlainFileKey {
            get {
                return new PlainFileKey {
                    Iv = "PlainIv",
                    Key = "PlainKey",
                    Tag = "PlainTag",
                    Version = "PlainVersion"
                };
            }
        }

        internal static ApiFileKey ApiFileKey {
            get {
                return new ApiFileKey {
                    Iv = "TestIv",
                    Key = "TestKey",
                    Tag = "TestTag",
                    Version = "A"
                };
            }
        }

        internal static EncryptedFileKey EncryptedFileKey {
            get {
                ApiFileKey fileKey = ApiFileKey;
                return new EncryptedFileKey {
                    Iv = fileKey.Iv,
                    Key = fileKey.Key,
                    Tag = fileKey.Tag,
                    Version = fileKey.Version
                };
            }
        }

        internal static ApiCreateFileUpload ApiCreateFileUpload {
            get {
                return new ApiCreateFileUpload {
                    Classification = 3,
                    Expiration = new ApiExpiration {
                        ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
                        EnableExpiration = true
                    },
                    Name = "FileName1",
                    Notes = "Some notes!",
                    ParentId = 436897
                };
            }
        }

        internal static ApiCompleteFileUpload ApiCompleteFileUpload {
            get {
                return new ApiCompleteFileUpload {
                    FileName = "FileName1",
                    ResolutionStrategy = "overwrite",
                    FileKey = ApiFileKey
                };
            }
        }

        internal static ApiSetUserFileKeysRequest ApiSetUserFileKeysRequest {
            get {
                return new ApiSetUserFileKeysRequest {
                    Items = new List<ApiSetUserFileKey> {
                        new ApiSetUserFileKey {
                            FileId = 73456,
                            UserId = 123,
                            FileKey = ApiFileKey
                        }
                    }
                };
            }
        }

        internal static ApiMissingFileKeys ApiMissingFileKeys {
            get {
                return new ApiMissingFileKeys {
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
            }
        }

        internal static ApiUserIdPublicKey ApiUserIdPublicKey {
            get {
                return new ApiUserIdPublicKey {
                    UserId = 53,
                    PublicKeyContainer = FactoryUser.ApiUserPublicKey
                };
            }
        }

        internal static Dictionary<long, UserPublicKey> FileUserPublicKey {
            get {
                return new Dictionary<long, UserPublicKey> {
                    {
                        53, FactoryUser.UserPublicKey
                    }
                };
            }
        }

        internal static ApiUploadChunkResult ApiUploadChunkResult {
            get {
                return new ApiUploadChunkResult {
                    Hash = "HHD8FDH707D8GV",
                    Size = 12345
                };
            }
        }

        internal static ApiUploadToken ApiUploadToken {
            get {
                return new ApiUploadToken {
                    UploadUrl = "https://dracoon.team/uploadtoken"
                };
            }
        }
    }
}