using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Util;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class NodeMapper {
        internal static NodeList FromApiNodeList(ApiNodeList apiNodeList) {
            if (apiNodeList == null) {
                return null;
            }

            NodeList nodeList = new NodeList {
                Offset = apiNodeList.Range.Offset,
                Limit = apiNodeList.Range.Limit,
                Total = apiNodeList.Range.Total,
                Items = new List<Node>()
            };
            foreach (ApiNode currentNode in apiNodeList.Items) {
                nodeList.Items.Add(FromApiNode(currentNode));
            }

            return nodeList;
        }

        internal static Node FromApiNode(ApiNode apiNode) {
            if (apiNode == null) {
                return null;
            }

            int? countChildrens = 0;
            if (apiNode.CountFiles.HasValue) {
                countChildrens += apiNode.CountFiles.Value;
            }

            if (apiNode.CountFolders.HasValue) {
                countChildrens += apiNode.CountFolders.Value;
            }

            if (apiNode.CountRooms.HasValue) {
                countChildrens += apiNode.CountRooms.Value;
            }

            Node node = new Node {
                Id = apiNode.Id,
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiNode.Type),
                ParentId = apiNode.ParentId,
                ParentPath = apiNode.ParentPath,
                Name = apiNode.Name,
                Extension = apiNode.FileType,
                MediaType = apiNode.MediaType,
                MediaToken = apiNode.MediaToken,
                Size = apiNode.Size,
                Quota = apiNode.Quota,
                Classification = EnumConverter.ConvertValueToClassificationEnum(apiNode.Classification),
                Notes = apiNode.Notes,
                Hash = apiNode.Hash,
                ExpireAt = apiNode.ExpireAt,
                CreatedAt = apiNode.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiNode.CreatedBy),
                UpdatedAt = apiNode.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiNode.UpdatedBy),
                HasInheritPermissions = apiNode.InheritPermissions,
                Permissions = FromApiNodePermissions(apiNode.Permissions),
                IsFavorite = apiNode.IsFavorite,
                IsEncrypted = apiNode.IsEncrypted,
                CountChildren = countChildrens,
                CountRooms = apiNode.CountRooms,
                CountFolders = apiNode.CountFolders,
                CountFiles = apiNode.CountFiles,
                CountDeletedVersions = apiNode.CountDeletedVersions,
                RecycleBinRetentionPeriod = apiNode.RecycleBinRetentionPeriod,
                CountDownloadShares = apiNode.CountDownloadShares,
                CountUploadShares = apiNode.CountUploadShares,
                BranchVersion = apiNode.BranchVersion
            };
            return node;
        }

        internal static NodePermissions FromApiNodePermissions(ApiNodePermissions apiNodePermissions) {
            if (apiNodePermissions == null) {
                return null;
            }

            NodePermissions nodePermissions = new NodePermissions {
                Manage = apiNodePermissions.Manage,
                Read = apiNodePermissions.Read,
                Create = apiNodePermissions.Create,
                Change = apiNodePermissions.Change,
                Delete = apiNodePermissions.Delete,
                ManageDownloadShare = apiNodePermissions.ManageDownloadShare,
                ManageUploadShare = apiNodePermissions.ManageUploadShare,
                CanReadRecycleBin = apiNodePermissions.ReadRecycleBin,
                CanRestoreRecycleBin = apiNodePermissions.RestoreRecycleBin,
                CanDeleteRecycleBin = apiNodePermissions.DeleteRecycleBin
            };
            return nodePermissions;
        }

        internal static ApiDeleteNodesRequest ToApiDeleteNodesRequest(DeleteNodesRequest request) {
            ApiDeleteNodesRequest apiDeleteNodesRequest = new ApiDeleteNodesRequest {
                NodeIds = request.Ids
            };
            return apiDeleteNodesRequest;
        }

        internal static ApiCopyNodesRequest ToApiCopyNodesRequest(CopyNodesRequest request) {
            List<ApiCopyNode> copyNodeList = new List<ApiCopyNode>();
            foreach (CopyNode currentCopyNode in request.NodesToBeCopied) {
                ApiCopyNode apiCopyNode = new ApiCopyNode {
                    NodeId = currentCopyNode.NodeId,
                    NewName = currentCopyNode.NewName
                };
                copyNodeList.Add(apiCopyNode);
            }

            ApiCopyNodesRequest apiCopyNodesRequest = new ApiCopyNodesRequest {
                Nodes = copyNodeList,
                ResolutionStrategy = EnumConverter.ConvertResolutionStrategyToValue(request.ResolutionStrategy),
                KeepShareLinks = request.KeepShareLinks
            };
            return apiCopyNodesRequest;
        }

        internal static ApiMoveNodesRequest ToApiMoveNodesRequest(MoveNodesRequest request) {
            List<ApiMoveNode> moveNodesList = new List<ApiMoveNode>();
            foreach (MoveNode currentMoveNode in request.NodesToBeMoved) {
                ApiMoveNode apiMoveNode = new ApiMoveNode {
                    NodeId = currentMoveNode.NodeId,
                    NewName = currentMoveNode.NewName
                };
                moveNodesList.Add(apiMoveNode);
            }

            ApiMoveNodesRequest apiMoveNodesRequest = new ApiMoveNodesRequest {
                Nodes = moveNodesList,
                ResolutionStrategy = EnumConverter.ConvertResolutionStrategyToValue(request.ResolutionStrategy),
                KeepShareLinks = request.KeepShareLinks
            };
            return apiMoveNodesRequest;
        }

        internal static RecycleBinItemList FromApiDeletedNodeSummaryList(ApiDeletedNodeSummaryList apiNodeList) {
            if (apiNodeList == null) {
                return null;
            }

            RecycleBinItemList nodeList = new RecycleBinItemList {
                Offset = apiNodeList.Range.Offset,
                Limit = apiNodeList.Range.Limit,
                Total = apiNodeList.Range.Total,
                Items = new List<RecycleBinItem>()
            };
            foreach (ApiDeletedNodeSummary currentNode in apiNodeList.Items) {
                nodeList.Items.Add(FromApiDeletedNodeSummary(currentNode));
            }

            return nodeList;
        }

        internal static RecycleBinItem FromApiDeletedNodeSummary(ApiDeletedNodeSummary apiNode) {
            if (apiNode == null) {
                return null;
            }

            RecycleBinItem node = new RecycleBinItem {
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiNode.Type),
                ParentId = apiNode.ParentId,
                ParentPath = apiNode.ParentPath,
                Name = apiNode.Name,
                FirstDeletedAt = apiNode.FirstDeletedAt,
                LastDeletedAt = apiNode.LastDeletedAt,
                LastDeletedNodeId = apiNode.LastDeletedNodeId,
                VersionsCount = apiNode.CntVersions
            };
            return node;
        }

        internal static PreviousVersionList FromApiDeletedNodeVersionsList(ApiDeletedNodeVersionsList apiNodeList) {
            if (apiNodeList == null) {
                return null;
            }

            PreviousVersionList nodeList = new PreviousVersionList {
                Offset = apiNodeList.Range.Offset,
                Limit = apiNodeList.Range.Limit,
                Total = apiNodeList.Range.Total,
                Items = new List<PreviousVersion>()
            };
            foreach (ApiDeletedNodeVersion currentNode in apiNodeList.Items) {
                nodeList.Items.Add(FromApiDeletedNodeVersion(currentNode));
            }

            return nodeList;
        }

        internal static PreviousVersion FromApiDeletedNodeVersion(ApiDeletedNodeVersion apiNode) {
            if (apiNode == null) {
                return null;
            }

            PreviousVersion node = new PreviousVersion {
                Type = EnumConverter.ConvertValueToNodeTypeEnum(apiNode.Type),
                ParentId = apiNode.ParentId,
                ParentPath = apiNode.ParentPath,
                Name = apiNode.Name,
                AccessedAt = apiNode.AccessedAt,
                Classification = EnumConverter.ConvertValueToClassificationEnum(apiNode.Classification),
                CreatedAt = apiNode.CreatedAt,
                CreatedBy = UserMapper.FromApiUserInfo(apiNode.CreatedBy),
                DeletedAt = apiNode.DeletedAt,
                DeletedBy = UserMapper.FromApiUserInfo(apiNode.DeletedBy),
                ExpireAt = apiNode.ExpireAt,
                Id = apiNode.Id,
                IsEncrypted = apiNode.IsEncrypted,
                Notes = apiNode.Notes,
                Size = apiNode.Size,
                UpdatedAt = apiNode.UpdatedAt,
                UpdatedBy = UserMapper.FromApiUserInfo(apiNode.UpdatedBy)
            };
            return node;
        }

        internal static ApiRestorePreviousVersionsRequest ToApiRestorePreviousVersionsRequest(RestorePreviousVersionsRequest request) {
            ApiRestorePreviousVersionsRequest apiRequest = new ApiRestorePreviousVersionsRequest {
                DeletedNodeIds = request.RestoreVersionIds,
                KeepShareLinks = request.KeepShareLinks,
                ParentId = request.NewParentNodeId,
                ResolutionStrategy = EnumConverter.ConvertResolutionStrategyToValue(request.ResolutionStrategy)
            };
            return apiRequest;
        }

        internal static ApiDeletePreviousVersionsRequest ToApiDeletePreviousVersionsRequest(DeletePreviousVersionsRequest request) {
            ApiDeletePreviousVersionsRequest apiRequest = new ApiDeletePreviousVersionsRequest {
                VersionsToBeDeleted = request.VersionIds
            };
            return apiRequest;
        }
    }
}