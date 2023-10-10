using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using System.Collections.Generic;
using Telerik.JustMock;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class NodeMapperTest {
        #region FromApiNodeList

        [Fact]
        public void FromApiNodeList() {
            // ARRANGE
            Classification expectedClassification = Classification.Confidential;
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";

            NodeList expected = FactoryNode.NodeList;

            ApiNodeList param = new ApiNodeList {
                Range = new ApiRange {
                    Offset = expected.Offset,
                    Limit = expected.Limit,
                    Total = expected.Total
                },
                Items = new List<ApiNode>(expected.Items.Count)
            };

            foreach (Node current in expected.Items) {
                current.Type = expectedType;
                current.Classification = expectedClassification;
                ApiNode currentApi = new ApiNode {
                    Id = current.Id,
                    Type = expectedTypeValue,
                    ParentId = current.ParentId,
                    ParentPath = current.ParentPath,
                    Name = current.Name,
                    MediaType = current.MediaType,
                    MediaToken = current.MediaToken,
                    Size = current.Size,
                    Quota = current.Quota,
                    Classification = (int)current.Classification,
                    Notes = current.Notes,
                    Hash = current.Hash,
                    ExpireAt = current.ExpireAt,
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        Id = current.CreatedBy.Id,
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        UserName = current.CreatedBy.UserName,
                        Email = current.CreatedBy.Email,
                        FirstName = current.CreatedBy.FirstName,
                        LastName = current.CreatedBy.LastName,
                        UserType = "internal"
                    },
                    UpdatedAt = current.UpdatedAt,
                    UpdatedBy = new ApiUserInfo {
                        Id = current.UpdatedBy.Id,
                        AvatarUuid = current.UpdatedBy.AvatarUUID,
                        UserName = current.UpdatedBy.UserName,
                        Email = current.UpdatedBy.Email,
                        FirstName = current.UpdatedBy.FirstName,
                        LastName = current.UpdatedBy.LastName,
                        UserType = "internal"
                    },
                    CreationTimestamp = current.CreationTime,
                    ModificationTimestamp = current.ModificationTime,
                    InheritPermissions = current.HasInheritPermissions,
                    Permissions = new ApiNodePermissions {
                        Manage = current.Permissions.Manage,
                        Read = current.Permissions.Read,
                        Create = current.Permissions.Create,
                        Change = current.Permissions.Change,
                        Delete = current.Permissions.Delete,
                        ManageDownloadShare = current.Permissions.ManageDownloadShare,
                        ManageUploadShare = current.Permissions.ManageUploadShare,
                        ReadRecycleBin = current.Permissions.CanReadRecycleBin,
                        RestoreRecycleBin = current.Permissions.CanRestoreRecycleBin,
                        DeleteRecycleBin = current.Permissions.CanDeleteRecycleBin
                    },
                    IsFavorite = current.IsFavorite,
                    IsEncrypted = current.IsEncrypted,
                    CountFiles = current.CountFiles,
                    CountRooms = current.CountRooms,
                    CountFolders = current.CountFolders,
                    CountDeletedVersions = current.CountPreviousVersions,
                    CountComments = current.CountComments,
                    RecycleBinRetentionPeriod = current.RecycleBinRetentionPeriod,
                    CountDownloadShares = current.CountDownloadShares,
                    CountUploadShares = current.CountUploadShares,
                    BranchVersion = current.BranchVersion,
                    FileType = current.Extension,
                    ConfigParentRoomId = current.ConfigParentRoomId,
                    IsBrowsable = current.IsBrowsable,
                    HasActivitiesLog = current.HasActivitiesLog
                };
                param.Items.Add(currentApi);
                Mock.Arrange(() => NodeMapper.FromApiNode(currentApi)).Returns(current);
            }

            // ACT
            NodeList actual = NodeMapper.FromApiNodeList(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodeListComparer());
        }

        [Fact]
        public void FromApiNodeList_Null() {
            // ARRANGE
            NodeList expected = null;

            ApiNodeList param = null;

            // ACT
            NodeList actual = NodeMapper.FromApiNodeList(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodeListComparer());
        }

        #endregion

        #region FromApiNode

        [Fact]
        public void FromApiNode() {
            // ARRANGE
            Classification expectedClassification = Classification.Confidential;
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";
            VirusProtectionVerdict expectedVirusProtectionVerdict = VirusProtectionVerdict.Clean;
            string expectedVirusProtectionVerdictValue = "CLEAN";

            Node expected = FactoryNode.Node;
            expected.Type = expectedType;
            expected.Classification = expectedClassification;

            ApiNode param = new ApiNode {
                Id = expected.Id,
                Type = expectedTypeValue,
                ParentId = expected.ParentId,
                ParentPath = expected.ParentPath,
                Name = expected.Name,
                MediaType = expected.MediaType,
                MediaToken = expected.MediaToken,
                Size = expected.Size,
                Quota = expected.Quota,
                Classification = (int)expected.Classification,
                Notes = expected.Notes,
                Hash = expected.Hash,
                ExpireAt = expected.ExpireAt,
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    Id = expected.CreatedBy.Id,
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    UserName = expected.CreatedBy.UserName,
                    Email = expected.CreatedBy.Email,
                    FirstName = expected.CreatedBy.FirstName,
                    LastName = expected.CreatedBy.LastName,
                    UserType = "internal"
                },
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = new ApiUserInfo {
                    Id = expected.UpdatedBy.Id,
                    AvatarUuid = expected.UpdatedBy.AvatarUUID,
                    UserName = expected.UpdatedBy.UserName,
                    Email = expected.UpdatedBy.Email,
                    FirstName = expected.UpdatedBy.FirstName,
                    LastName = expected.UpdatedBy.LastName,
                    UserType = "internal"
                },
                CreationTimestamp = expected.CreationTime,
                ModificationTimestamp = expected.ModificationTime,
                InheritPermissions = expected.HasInheritPermissions,
                Permissions = new ApiNodePermissions {
                    Manage = expected.Permissions.Manage,
                    Read = expected.Permissions.Read,
                    Create = expected.Permissions.Create,
                    Change = expected.Permissions.Change,
                    Delete = expected.Permissions.Delete,
                    ManageDownloadShare = expected.Permissions.ManageDownloadShare,
                    ManageUploadShare = expected.Permissions.ManageUploadShare,
                    ReadRecycleBin = expected.Permissions.CanReadRecycleBin,
                    RestoreRecycleBin = expected.Permissions.CanRestoreRecycleBin,
                    DeleteRecycleBin = expected.Permissions.CanDeleteRecycleBin
                },
                IsFavorite = expected.IsFavorite,
                IsEncrypted = expected.IsEncrypted,
                CountFiles = expected.CountFiles,
                CountRooms = expected.CountRooms,
                CountFolders = expected.CountFolders,
                CountDeletedVersions = expected.CountPreviousVersions,
                CountComments = expected.CountComments,
                RecycleBinRetentionPeriod = expected.RecycleBinRetentionPeriod,
                CountDownloadShares = expected.CountDownloadShares,
                CountUploadShares = expected.CountUploadShares,
                BranchVersion = expected.BranchVersion,
                FileType = expected.Extension,
                ConfigParentRoomId = expected.ConfigParentRoomId,
                IsBrowsable = expected.IsBrowsable,
                HasActivitiesLog = expected.HasActivitiesLog,
                VirusProtectionInfo = new ApiVirusProtectionInfo {
                    Verdict = expectedVirusProtectionVerdictValue,
                    LastCheckedAt = expected.VirusProtectionInfo.CheckedAt,
                    Sha256 = expected.VirusProtectionInfo.Sha256
                }
            };

            Mock.Arrange(() => EnumConverter.ConvertValueToNodeTypeEnum(expectedTypeValue)).Returns(expectedType);
            Mock.Arrange(() => EnumConverter.ConvertValueToVirusProtectionVerdictEnum(expectedVirusProtectionVerdictValue)).Returns(expectedVirusProtectionVerdict);
            Mock.Arrange(() => EnumConverter.ConvertValueToClassificationEnum((int)expectedClassification)).Returns(expectedClassification);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.CreatedBy)).Returns(expected.CreatedBy);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.UpdatedBy)).Returns(expected.UpdatedBy);

            // ACT
            Node actual = NodeMapper.FromApiNode(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodeComparer());
        }

        [Fact]
        public void FromApiNode_Null() {
            // ARRANGE
            Node expected = null;
            ApiNode param = null;

            // ACT
            Node actual = NodeMapper.FromApiNode(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodeComparer());
        }

        #endregion

        #region FromApiNodePermissions

        [Fact]
        public void FromApiNodePermissions() {
            // ARRANGE
            NodePermissions expected = FactoryNode.NodePermissions;

            ApiNodePermissions param = new ApiNodePermissions {
                Manage = expected.Manage,
                Read = expected.Read,
                Create = expected.Create,
                Change = expected.Change,
                Delete = expected.Delete,
                ManageDownloadShare = expected.ManageDownloadShare,
                ManageUploadShare = expected.ManageUploadShare,
                ReadRecycleBin = expected.CanReadRecycleBin,
                RestoreRecycleBin = expected.CanRestoreRecycleBin,
                DeleteRecycleBin = expected.CanDeleteRecycleBin
            };

            // ACT
            NodePermissions actual = NodeMapper.FromApiNodePermissions(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodePermissionsComparer());
        }

        [Fact]
        public void FromApiNodePermissions_Null() {
            // ARRANGE
            NodePermissions expected = null;

            ApiNodePermissions param = null;

            // ACT
            NodePermissions actual = NodeMapper.FromApiNodePermissions(param);

            // ASSERT
            Assert.Equal(expected, actual, new NodePermissionsComparer());
        }

        #endregion

        #region FromApiVirusProtectionInfo

        [Fact]
        public void FromApiVirusProtectionInfo() {
            // ARRANGE
            VirusProtectionInfo expected = FactoryNode.VirusProtectionInfo;

            ApiVirusProtectionInfo param = new ApiVirusProtectionInfo {
                Verdict = "CLEAN",
                LastCheckedAt = expected.CheckedAt,
                Sha256 = expected.Sha256
            };

            // ACT
            VirusProtectionInfo actual = NodeMapper.FromApiVirusProtectionInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new VirusProtectionInfoComparer());
        }

        [Fact]
        public void FromApiVirusProtectionInfo_Null() {
            // ARRANGE
            VirusProtectionInfo expected = null;

            ApiVirusProtectionInfo param = null;

            // ACT
            VirusProtectionInfo actual = NodeMapper.FromApiVirusProtectionInfo(param);

            // ASSERT
            Assert.Equal(expected, actual, new VirusProtectionInfoComparer());
        }

        #endregion

        #region ToApiDeleteNodesRequest

        [Fact]
        public void ToApiDeleteNodesRequest() {
            // ARRANGE
            ApiDeleteNodesRequest expected = FactoryNode.ApiDeleteNodesRequest;

            DeleteNodesRequest param = new DeleteNodesRequest(expected.NodeIds);

            // ACT
            ApiDeleteNodesRequest actual = NodeMapper.ToApiDeleteNodesRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiDeleteNodesRequestComparer());
        }

        #endregion

        #region ToApiCopyNodesRequest

        [Fact]
        public void ToApiCopyNodesRequest() {
            // ARRANGE
            ApiCopyNodesRequest expected = FactoryNode.ApiCopyNodesRequest;
            expected.ResolutionStrategy = "overwrite";

            CopyNodesRequest param = new CopyNodesRequest(2,
                new List<CopyNode>(expected.Nodes.Count),
                ResolutionStrategy.Overwrite,
                expected.KeepShareLinks);

            foreach (ApiCopyNode currentApi in expected.Nodes) {
                param.NodesToBeCopied.Add(new CopyNode(currentApi.NodeId, currentApi.NewName, currentApi.TimestampCreation, currentApi.TimestampModification));
            }

            Mock.Arrange(() => EnumConverter.ConvertResolutionStrategyToValue(param.ResolutionStrategy)).Returns(expected.ResolutionStrategy);

            // ACT
            ApiCopyNodesRequest actual = NodeMapper.ToApiCopyNodesRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiCopyNodesRequestComparer());
        }

        #endregion

        #region ToApiMoveNodesRequest

        [Fact]
        public void ToApiMoveNodesRequest() {
            // ARRANGE
            ApiMoveNodesRequest expected = FactoryNode.ApiMoveNodesRequest;
            expected.ResolutionStrategy = "overwrite";

            MoveNodesRequest param = new MoveNodesRequest(12,
                new List<MoveNode>(expected.Nodes.Count),
                ResolutionStrategy.Overwrite,
                expected.KeepShareLinks);

            foreach (ApiMoveNode currentApi in expected.Nodes) {
                param.NodesToBeMoved.Add(new MoveNode(currentApi.NodeId, currentApi.NewName, currentApi.TimestampCreation, currentApi.TimestampModification));
            }

            Mock.Arrange(() => EnumConverter.ConvertResolutionStrategyToValue(param.ResolutionStrategy)).Returns(expected.ResolutionStrategy);

            // ACT
            ApiMoveNodesRequest actual = NodeMapper.ToApiMoveNodesRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiMoveNodesRequestComparer());
        }

        #endregion

        #region FromApiDeletedNodeSummaryList

        [Fact]
        public void FromApiDeletedNodeSummaryList() {
            // ARRANGE
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";

            RecycleBinItemList expected = FactoryNode.RecycleBinItemList;

            ApiDeletedNodeSummaryList param = new ApiDeletedNodeSummaryList {
                Range = new ApiRange {
                    Offset = expected.Offset,
                    Limit = expected.Limit,
                    Total = expected.Total
                },
                Items = new List<ApiDeletedNodeSummary>(expected.Items.Count)
            };

            foreach (RecycleBinItem current in expected.Items) {
                current.Type = expectedType;
                ApiDeletedNodeSummary currentApi = new ApiDeletedNodeSummary {
                    Type = expectedTypeValue,
                    CntVersions = current.VersionsCount,
                    FirstDeletedAt = current.FirstDeletedAt,
                    LastDeletedAt = current.LastDeletedAt,
                    LastDeletedNodeId = current.LastDeletedNodeId,
                    Name = current.Name,
                    ParentId = current.ParentId,
                    ParentPath = current.ParentPath
                };
                param.Items.Add(currentApi);
                Mock.Arrange(() => NodeMapper.FromApiDeletedNodeSummary(currentApi)).Returns(current);
            }

            // ACT
            RecycleBinItemList actual = NodeMapper.FromApiDeletedNodeSummaryList(param);

            // ASSERT
            Assert.Equal(expected, actual, new RecycleBinItemListComparer());
        }

        [Fact]
        public void FromApiDeletedNodeSummaryList_Null() {
            // ARRANGE
            RecycleBinItemList expected = null;
            ApiDeletedNodeSummaryList param = null;

            // ACT
            RecycleBinItemList actual = NodeMapper.FromApiDeletedNodeSummaryList(param);

            // ASSERT
            Assert.Equal(expected, actual, new RecycleBinItemListComparer());
        }

        #endregion

        #region FromApiDeletedNodeSummary

        [Fact]
        public void FromApiDeletedNodeSummary() {
            // ARRANGE
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";

            RecycleBinItem expected = FactoryNode.RecycleBinItem;
            expected.Type = expectedType;

            ApiDeletedNodeSummary param = new ApiDeletedNodeSummary() {
                Type = expectedTypeValue,
                ParentId = expected.ParentId,
                ParentPath = expected.ParentPath,
                Name = expected.Name,
                FirstDeletedAt = expected.FirstDeletedAt,
                LastDeletedAt = expected.LastDeletedAt,
                LastDeletedNodeId = expected.LastDeletedNodeId,
                CntVersions = expected.VersionsCount,
                TimestampCreation = expected.CreationTime,
                TimestampModification = expected.ModificationTime
            };

            Mock.Arrange(() => EnumConverter.ConvertValueToNodeTypeEnum(expectedTypeValue)).Returns(expectedType);

            // ACT
            RecycleBinItem actual = NodeMapper.FromApiDeletedNodeSummary(param);

            // ASSERT
            Assert.Equal(expected, actual, new RecycleBinItemComparer());
        }

        [Fact]
        public void FromApiDeletedNodeSummary_Null() {
            // ARRANGE
            RecycleBinItem expected = null;
            ApiDeletedNodeSummary param = null;

            // ACT
            RecycleBinItem actual = NodeMapper.FromApiDeletedNodeSummary(param);

            // ASSERT
            Assert.Equal(expected, actual, new RecycleBinItemComparer());
        }

        #endregion

        #region FromApiDeletedNodeVersionsList

        [Fact]
        public void FromApiDeletedNodeVersionsList() {
            // ARRANGE
            Classification expectedClassification = Classification.Confidential;
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";

            PreviousVersionList expected = FactoryNode.PreviousVersionList;

            ApiDeletedNodeVersionsList param = new ApiDeletedNodeVersionsList {
                Range = new ApiRange {
                    Offset = expected.Offset,
                    Limit = expected.Limit,
                    Total = expected.Total
                },
                Items = new List<ApiDeletedNodeVersion>(expected.Items.Count)
            };

            foreach (PreviousVersion current in expected.Items) {
                current.Classification = expectedClassification;
                current.Type = expectedType;
                ApiDeletedNodeVersion currentApi = new ApiDeletedNodeVersion() {
                    Type = expectedTypeValue,
                    ParentId = current.ParentId,
                    ParentPath = current.ParentPath,
                    Name = current.Name,
                    AccessedAt = current.AccessedAt,
                    Classification = (int)expectedClassification,
                    CreatedAt = current.CreatedAt,
                    CreatedBy = new ApiUserInfo {
                        Id = current.CreatedBy.Id,
                        UserName = current.CreatedBy.UserName,
                        AvatarUuid = current.CreatedBy.AvatarUUID,
                        Email = current.CreatedBy.Email,
                        FirstName = current.CreatedBy.FirstName,
                        LastName = current.CreatedBy.LastName,
                        UserType = "internal"
                    },
                    DeletedAt = current.DeletedAt,
                    DeletedBy = new ApiUserInfo {
                        Id = current.DeletedBy.Id,
                        UserName = current.DeletedBy.UserName,
                        AvatarUuid = current.DeletedBy.AvatarUUID,
                        Email = current.DeletedBy.Email,
                        FirstName = current.DeletedBy.FirstName,
                        LastName = current.DeletedBy.LastName,
                        UserType = "internal"
                    },
                    ExpireAt = current.ExpireAt,
                    Id = current.Id,
                    IsEncrypted = current.IsEncrypted,
                    Notes = current.Notes,
                    Size = current.Size,
                    UpdatedAt = current.UpdatedAt,
                    UpdatedBy = new ApiUserInfo {
                        Id = current.UpdatedBy.Id,
                        UserName = current.UpdatedBy.UserName,
                        AvatarUuid = current.UpdatedBy.AvatarUUID,
                        Email = current.UpdatedBy.Email,
                        FirstName = current.UpdatedBy.FirstName,
                        LastName = current.UpdatedBy.LastName,
                        UserType = "internal"
                    }
                };
                param.Items.Add(currentApi);
                Mock.Arrange(() => NodeMapper.FromApiDeletedNodeVersion(currentApi)).Returns(current);
            }

            // ACT
            PreviousVersionList actual = NodeMapper.FromApiDeletedNodeVersionsList(param);

            // ASSERT
            Assert.Equal(expected, actual, new PreviousVersionListComparer());
        }

        [Fact]
        public void FromApiDeletedNodeVersionsList_Null() {
            // ARRANGE
            PreviousVersionList expected = null;

            ApiDeletedNodeVersionsList param = null;

            // ACT
            PreviousVersionList actual = NodeMapper.FromApiDeletedNodeVersionsList(param);

            // ASSERT
            Assert.Equal(expected, actual, new PreviousVersionListComparer());
        }

        #endregion

        #region FromApiDeletedNodeVersion

        [Fact]
        public void FromApiDeletedNodeVersion() {
            // ARRANGE
            Classification expectedClassification = Classification.Confidential;
            NodeType expectedType = NodeType.File;
            string expectedTypeValue = "file";

            PreviousVersion expected = FactoryNode.PreviousVersion;
            expected.Classification = expectedClassification;
            expected.Type = expectedType;

            ApiDeletedNodeVersion param = new ApiDeletedNodeVersion {
                Type = expectedTypeValue,
                ParentId = expected.ParentId,
                ParentPath = expected.ParentPath,
                Name = expected.Name,
                AccessedAt = expected.AccessedAt,
                Classification = (int)expectedClassification,
                CreatedAt = expected.CreatedAt,
                CreatedBy = new ApiUserInfo {
                    Id = expected.CreatedBy.Id,
                    UserName = expected.CreatedBy.UserName,
                    AvatarUuid = expected.CreatedBy.AvatarUUID,
                    Email = expected.CreatedBy.Email,
                    FirstName = expected.CreatedBy.FirstName,
                    LastName = expected.CreatedBy.LastName,
                    UserType = "internal"
                },
                DeletedAt = expected.DeletedAt,
                DeletedBy = new ApiUserInfo {
                    Id = expected.DeletedBy.Id,
                    UserName = expected.DeletedBy.UserName,
                    AvatarUuid = expected.DeletedBy.AvatarUUID,
                    Email = expected.DeletedBy.Email,
                    FirstName = expected.DeletedBy.FirstName,
                    LastName = expected.DeletedBy.LastName,
                    UserType = "internal"
                },
                ExpireAt = expected.ExpireAt,
                Id = expected.Id,
                IsEncrypted = expected.IsEncrypted,
                Notes = expected.Notes,
                Size = expected.Size,
                UpdatedAt = expected.UpdatedAt,
                UpdatedBy = new ApiUserInfo {
                    Id = expected.UpdatedBy.Id,
                    UserName = expected.UpdatedBy.UserName,
                    AvatarUuid = expected.UpdatedBy.AvatarUUID,
                    Email = expected.UpdatedBy.Email,
                    FirstName = expected.UpdatedBy.FirstName,
                    LastName = expected.UpdatedBy.LastName,
                    UserType = "internal"
                }
            };

            Mock.Arrange(() => EnumConverter.ConvertValueToNodeTypeEnum(expectedTypeValue)).Returns(expectedType);
            Mock.Arrange(() => EnumConverter.ConvertValueToClassificationEnum((int)expectedClassification)).Returns(expectedClassification);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.CreatedBy)).Returns(expected.CreatedBy);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.DeletedBy)).Returns(expected.DeletedBy);
            Mock.Arrange(() => UserMapper.FromApiUserInfo(param.UpdatedBy)).Returns(expected.UpdatedBy);

            // ACT
            PreviousVersion actual = NodeMapper.FromApiDeletedNodeVersion(param);

            // ASSERT
            Assert.Equal(expected, actual, new PreviousVersionComparer());
        }

        [Fact]
        public void FromApiDeletedNodeVersion_Null() {
            // ARRANGE
            PreviousVersion expected = null;

            ApiDeletedNodeVersion param = null;

            // ACT
            PreviousVersion actual = NodeMapper.FromApiDeletedNodeVersion(param);

            // ASSERT
            Assert.Equal(expected, actual, new PreviousVersionComparer());
        }

        #endregion

        #region ToApiRestorePreviousVersionsRequest

        [Fact]
        public void ToApiRestorePreviousVersionsRequest() {
            // ARRANGE
            string expectedStrategyValue = "overwrite";
            ResolutionStrategy expectedStrategy = ResolutionStrategy.Overwrite;

            ApiRestorePreviousVersionsRequest expected = FactoryNode.ApiRestorePreviousVersionsRequest;
            expected.ResolutionStrategy = expectedStrategyValue;

            RestorePreviousVersionsRequest param = new RestorePreviousVersionsRequest(expected.DeletedNodeIds) {
                KeepShareLinks = expected.KeepShareLinks,
                NewParentNodeId = expected.ParentId,
                ResolutionStrategy = expectedStrategy
            };

            Mock.Arrange(() => EnumConverter.ConvertResolutionStrategyToValue(expectedStrategy)).Returns(expectedStrategyValue);

            // ACT
            ApiRestorePreviousVersionsRequest actual = NodeMapper.ToApiRestorePreviousVersionsRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiRestorePreviousVersionsRequestComparer());
        }

        #endregion

        #region ToApiDeletePreviousVersionsRequest

        [Fact]
        public void ToApiDeletePreviousVersionsRequest() {
            // ARRANGE
            ApiDeletePreviousVersionsRequest expected = FactoryNode.ApiDeletePreviousVersionsRequest;

            DeletePreviousVersionsRequest param = new DeletePreviousVersionsRequest(expected.VersionsToBeDeleted);

            // ACT
            ApiDeletePreviousVersionsRequest actual = NodeMapper.ToApiDeletePreviousVersionsRequest(param);

            // ASSERT
            Assert.Equal(expected, actual, new ApiDeletePreviousVersionsRequestComparer());
        }

        #endregion
    }
}