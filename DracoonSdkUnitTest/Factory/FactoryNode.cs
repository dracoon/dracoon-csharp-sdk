using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.Factory {
    internal static class FactoryNode {
        internal static NodePermissions NodePermissions => new NodePermissions {
            Manage = true,
            Read = false,
            Create = true,
            Change = false,
            Delete = true,
            ManageDownloadShare = false,
            ManageUploadShare = true,
            CanReadRecycleBin = false,
            CanRestoreRecycleBin = true,
            CanDeleteRecycleBin = false
        };

        internal static ApiNodePermissions ApiNodePermissions => new ApiNodePermissions {
            Manage = true,
            Read = false,
            Create = true,
            Change = false,
            Delete = true,
            ManageDownloadShare = false,
            ManageUploadShare = true,
            ReadRecycleBin = false,
            RestoreRecycleBin = true,
            DeleteRecycleBin = false
        };

        internal static VirusProtectionInfo VirusProtectionInfo => new VirusProtectionInfo {
            Verdict = VirusProtectionVerdict.Clean,
            CheckedAt = ApiVirusProtectionInfo.LastCheckedAt,
            Sha256 = ApiVirusProtectionInfo.Sha256,
        };

        internal static ApiVirusProtectionInfo ApiVirusProtectionInfo => new ApiVirusProtectionInfo {
            Verdict = "CLEAN",
            LastCheckedAt = new DateTime(2020, 1, 1, 0, 0, 0),
            Sha256 = "SDKJ23DFS874234D"
        };

        internal static Node Node => new Node {
            Id = 12,
            ReferenceId = 153,
            Type = NodeType.File,
            ParentId = 10,
            ParentPath = "/",
            Name = "Root",
            Extension = ".txt",
            MediaType = "media",
            MediaToken = "token",
            Size = 8349,
            Quota = 3 * 1024 * 1024,
            Classification = Classification.Internal,
            Notes = "Some notes.",
            Hash = "SDKJ23DFS874",
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.UserInfo,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            UpdatedBy = FactoryUser.UserInfo,
            CreationTime = new DateTime(1999, 1, 1, 0, 0, 0),
            ModificationTime = new DateTime(1999, 5, 1, 0, 0, 0),
            HasInheritPermissions = true,
            Permissions = NodePermissions,
            IsFavorite = false,
            IsEncrypted = true,
            CountChildren = 5,
            CountFiles = 1,
            CountRooms = 2,
            CountFolders = 2,
            CountPreviousVersions = 11,
            CountComments = 2,
            RecycleBinRetentionPeriod = 15,
            CountDownloadShares = 6,
            CountUploadShares = 4,
            BranchVersion = 12358239758,
            ConfigParentRoomId = 8,
            IsBrowsable = true,
            HasActivitiesLog = true,
            VirusProtectionInfo = VirusProtectionInfo
        };

        internal static NodeList NodeList => new NodeList {
            Offset = 1,
            Limit = 2,
            Total = 3,
            Items = new List<Node> {
                        Node
                    }
        };

        internal static ApiNode ApiNode => new ApiNode {
            Id = 12,
            ReferenceId = 153,
            Type = "file",
            ParentId = 10,
            ParentPath = "/",
            Name = "Root",
            MediaType = "media",
            MediaToken = "token",
            Size = 12354,
            Quota = 12345,
            Classification = 2,
            Notes = "Some notes.",
            Hash = "SDKJ23DFS874",
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0),
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.ApiUserInfo,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            UpdatedBy = FactoryUser.ApiUserInfo,
            CreationTimestamp = new DateTime(1999, 1, 1, 0, 0, 0),
            ModificationTimestamp = new DateTime(1999, 5, 1, 0, 0, 0),
            InheritPermissions = true,
            Permissions = ApiNodePermissions,
            IsFavorite = false,
            IsEncrypted = true,
            CountFiles = 1,
            CountRooms = 2,
            CountFolders = 2,
            CountDeletedVersions = 11,
            RecycleBinRetentionPeriod = 15,
            CountDownloadShares = 6,
            CountUploadShares = 4,
            CountComments = 2,
            BranchVersion = 12358239758,
            FileType = ".txt",
            ConfigParentRoomId = 8,
            IsBrowsable = true,
            HasActivitiesLog = true,
            VirusProtectionInfo = ApiVirusProtectionInfo
        };

        internal static ApiNodeList ApiNodeList => new ApiNodeList {
            Items = new List<ApiNode> {
                        ApiNode
                    },
            Range = new ApiRange {
                Limit = 2,
                Offset = 1,
                Total = 3
            }
        };

        internal static ApiDeleteNodesRequest ApiDeleteNodesRequest => new ApiDeleteNodesRequest {
            NodeIds = new List<long> {
                        12,
                        4567,
                        3254678
                    }
        };

        internal static DeleteNodesRequest DeleteNodesRequest => new DeleteNodesRequest(new List<long> {
                    12,
                    4567,
                    3254678
                });

        internal static ApiCopyNodesRequest ApiCopyNodesRequest => new ApiCopyNodesRequest {
            KeepShareLinks = true,
            ResolutionStrategy = "overwrite",
            Nodes = new List<ApiCopyNode> {
                        new ApiCopyNode {
                            NodeId = 13575,
                            NewName = "Name1",
                            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
                            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0)
                        },
                        new ApiCopyNode {
                            NodeId = 36584,
                            NewName = "Name2",
                            TimestampCreation = new DateTime(2003, 1, 1, 0, 0, 1),
                            TimestampModification = new DateTime(2004, 1, 1, 0, 0, 0)
                        }
                    }
        };

        internal static CopyNodesRequest CopyNodesRequest => new CopyNodesRequest(256, new List<CopyNode> {
                    new CopyNode(13575, "Name1", new DateTime(2001, 1, 1, 0, 0, 1), new DateTime(2002, 1, 1, 0, 0, 0)),
                    new CopyNode(36584, "Name2", new DateTime(2003, 1, 1, 0, 0, 1), new DateTime(2004, 1, 1, 0, 0, 0))
                }) {
            KeepShareLinks = true,
            ResolutionStrategy = ResolutionStrategy.Overwrite
        };

        internal static ApiMoveNodesRequest ApiMoveNodesRequest => new ApiMoveNodesRequest {
            KeepShareLinks = true,
            ResolutionStrategy = "overwrite",
            Nodes = new List<ApiMoveNode> {
                        new ApiMoveNode {
                            NodeId = 5634,
                            NewName = "Name1",
                            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
                            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0)
                        },
                        new ApiMoveNode {
                            NodeId = 435678,
                            NewName = "Name2",
                            TimestampCreation = new DateTime(2003, 1, 1, 0, 0, 1),
                            TimestampModification = new DateTime(2004, 1, 1, 0, 0, 0)
                        }
                    }
        };

        internal static MoveNodesRequest MoveNodesRequest => new MoveNodesRequest(235, new List<MoveNode> {
                    new MoveNode(5634, "Name1", new DateTime(2001, 1, 1, 0, 0, 1), new DateTime(2002, 1, 1, 0, 0, 0)),
                    new MoveNode(435678, "Name2", new DateTime(2003, 1, 1, 0, 0, 1), new DateTime(2004, 1, 1, 0, 0, 0))
                }) {
            ResolutionStrategy = ResolutionStrategy.Overwrite,
            KeepShareLinks = true
        };

        internal static RecycleBinItem RecycleBinItem => new RecycleBinItem {
            Type = NodeType.Folder,
            ParentId = 12,
            ParentPath = "/root",
            Name = "deletedNodeName1",
            FirstDeletedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            LastDeletedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            LastDeletedNodeId = 123452,
            VersionsCount = 12563,
            CreationTime = new DateTime(2001, 1, 1, 0, 0, 1),
            ModificationTime = new DateTime(2002, 1, 1, 0, 0, 0)
        };

        internal static RecycleBinItemList RecycleBinItemList => new RecycleBinItemList {
            Offset = 1,
            Limit = 2,
            Total = 3,
            Items = new List<RecycleBinItem> {
                        RecycleBinItem
                    }
        };

        internal static ApiDeletedNodeSummaryList ApiDeletedNodeSummaryList => new ApiDeletedNodeSummaryList {
            Items = new List<ApiDeletedNodeSummary> {
                        ApiDeletedNodeSummary
                    },
            Range = new ApiRange {
                Offset = 1,
                Limit = 2,
                Total = 3
            }
        };

        internal static ApiDeletedNodeSummary ApiDeletedNodeSummary => new ApiDeletedNodeSummary {
            Type = "folder",
            ParentId = 12,
            ParentPath = "/root",
            Name = "deletedNodeName1",
            FirstDeletedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            LastDeletedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            LastDeletedNodeId = 123452,
            CntVersions = 12563,
            TimestampCreation = new DateTime(2001, 1, 1, 0, 0, 1),
            TimestampModification = new DateTime(2002, 1, 1, 0, 0, 0)
        };

        internal static PreviousVersion PreviousVersion => new PreviousVersion {
            Type = NodeType.File,
            ParentId = 1534,
            ParentPath = "/root/second",
            Name = "Things",
            AccessedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            Classification = Classification.StrictlyConfidential,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.UserInfo,
            DeletedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            DeletedBy = FactoryUser.UserInfo,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(3),
            Id = 324657,
            IsEncrypted = true,
            Notes = "Some notes!",
            Size = 8657456,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(4),
            UpdatedBy = FactoryUser.UserInfo
        };

        internal static PreviousVersionList PreviousVersionList => new PreviousVersionList {
            Offset = 0,
            Limit = 1,
            Total = 4,
            Items = new List<PreviousVersion> {
                        PreviousVersion
                    }
        };

        internal static ApiDeletedNodeVersion ApiDeletedNodeVersion => new ApiDeletedNodeVersion {
            Type = "file",
            ParentId = 1534,
            ParentPath = "/root/second",
            Name = "Things",
            AccessedAt = new DateTime(2000, 1, 1, 0, 0, 0),
            Classification = 4,
            CreatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(1),
            CreatedBy = FactoryUser.ApiUserInfo,
            DeletedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(2),
            DeletedBy = FactoryUser.ApiUserInfo,
            ExpireAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(3),
            Id = 324657,
            IsEncrypted = true,
            Notes = "Some notes!",
            Size = 8657456,
            UpdatedAt = new DateTime(2000, 1, 1, 0, 0, 0).AddDays(4),
            UpdatedBy = FactoryUser.ApiUserInfo
        };

        internal static ApiDeletedNodeVersionsList ApiDeletedNodeVersionsList => new ApiDeletedNodeVersionsList {
            Range = new ApiRange {
                Offset = 0,
                Limit = 1,
                Total = 4
            },
            Items = new List<ApiDeletedNodeVersion> {
                        ApiDeletedNodeVersion
                    }
        };

        internal static ApiRestorePreviousVersionsRequest ApiRestorePreviousVersionsRequest => new ApiRestorePreviousVersionsRequest {
            DeletedNodeIds = new List<long> {
                        1234,
                        6534
                    },
            KeepShareLinks = true,
            ParentId = 5476,
            ResolutionStrategy = "overwrite"
        };

        internal static RestorePreviousVersionsRequest RestorePreviousVersionsRequest => new RestorePreviousVersionsRequest(new List<long> {
                    1234,
                    6534
                }) {
            KeepShareLinks = true,
            NewParentNodeId = 5476,
            ResolutionStrategy = ResolutionStrategy.Overwrite
        };

        internal static ApiDeletePreviousVersionsRequest ApiDeletePreviousVersionsRequest => new ApiDeletePreviousVersionsRequest {
            VersionsToBeDeleted = new List<long> {
                        35468,
                        235897
                    }
        };

        internal static DeletePreviousVersionsRequest DeletePreviousVersionsRequest => new DeletePreviousVersionsRequest(new List<long> {
                    35468,
                    235897
                });

        internal static ApiDownloadToken ApiDownloadToken => new ApiDownloadToken {
            DownloadUrl = "https://dracoon.com"
        };

    }
}