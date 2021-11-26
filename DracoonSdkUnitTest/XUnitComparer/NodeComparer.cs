using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using System;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class NodeComparer : IEqualityComparer<Node> {
        public bool Equals(Node x, Node y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CreatedBy, y.CreatedBy, new UserInfoComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());
            Assert.Equal(x.Permissions, y.Permissions, new NodePermissionsComparer());

            return x.Id == y.Id &&
                x.Type == y.Type &&
                x.ParentId == y.ParentId &&
                string.Equals(x.ParentPath, y.ParentPath) &&
                string.Equals(x.Name, y.Name) &&
                string.Equals(x.Extension, y.Extension) &&
                string.Equals(x.MediaType, y.MediaType) &&
                string.Equals(x.MediaToken, y.MediaToken) &&
                x.Size == y.Size &&
                x.Quota == y.Quota &&
                x.Classification == y.Classification &&
                string.Equals(x.Notes, y.Notes) &&
                string.Equals(x.Hash, y.Hash) &&
                x.ExpireAt == y.ExpireAt &&
                x.CreatedAt == y.CreatedAt &&
                x.UpdatedAt == y.UpdatedAt &&
                x.CreationTimestamp == y.CreationTimestamp &&
                x.ModificationTimestamp == y.ModificationTimestamp &&
                x.HasInheritPermissions == y.HasInheritPermissions &&
                x.IsFavorite == y.IsFavorite &&
                x.IsEncrypted == y.IsEncrypted &&
                x.CountChildren == y.CountChildren &&
                x.CountRooms == y.CountRooms &&
                x.CountFolders == y.CountFolders &&
                x.CountFiles == y.CountFiles &&
                x.CountDeletedVersions == y.CountDeletedVersions &&
                x.RecycleBinRetentionPeriod == y.RecycleBinRetentionPeriod &&
                x.CountDownloadShares == y.CountDownloadShares &&
                x.CountUploadShares == y.CountUploadShares &&
                x.BranchVersion == y.BranchVersion;
        }

        public int GetHashCode(Node obj) {
            throw new NotImplementedException();
        }
    }

    internal class NodePermissionsComparer : IEqualityComparer<NodePermissions> {
        public bool Equals(NodePermissions x, NodePermissions y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Manage == y.Manage &&
                x.Read == y.Read &&
                x.Create == y.Create &&
                x.Change == y.Change &&
                x.Delete == y.Delete &&
                x.ManageDownloadShare == y.ManageDownloadShare &&
                x.ManageUploadShare == y.ManageUploadShare &&
                x.CanReadRecycleBin == y.CanReadRecycleBin &&
                x.CanRestoreRecycleBin == y.CanRestoreRecycleBin &&
                x.CanDeleteRecycleBin == y.CanDeleteRecycleBin;
        }

        public int GetHashCode(NodePermissions obj) {
            throw new NotImplementedException();
        }
    }

    internal class NodeListComparer : IEqualityComparer<NodeList> {
        public bool Equals(NodeList x, NodeList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                x.Limit == y.Limit &&
                x.Total == y.Total &&
                CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(NodeList obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiDeleteNodesRequestComparer : IEqualityComparer<ApiDeleteNodesRequest> {
        public bool Equals(ApiDeleteNodesRequest x, ApiDeleteNodesRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return CompareHelper.ListIsEqual(x.NodeIds, y.NodeIds);
        }

        public int GetHashCode(ApiDeleteNodesRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiCopyNodesRequestComparer : IEqualityComparer<ApiCopyNodesRequest> {
        public bool Equals(ApiCopyNodesRequest x, ApiCopyNodesRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.KeepShareLinks == y.KeepShareLinks &&
                string.Equals(x.ResolutionStrategy, y.ResolutionStrategy) &&
                CompareHelper.ListIsEqual(x.Nodes, x.Nodes);
        }

        public int GetHashCode(ApiCopyNodesRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiCopyNodeComparer : IEqualityComparer<ApiCopyNode> {
        public bool Equals(ApiCopyNode x, ApiCopyNode y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.NodeId == y.NodeId &&
                string.Equals(x.NewName, y.NewName);
        }

        public int GetHashCode(ApiCopyNode obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiMoveNodesRequestComparer : IEqualityComparer<ApiMoveNodesRequest> {
        public bool Equals(ApiMoveNodesRequest x, ApiMoveNodesRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.KeepShareLinks == y.KeepShareLinks &&
                string.Equals(x.ResolutionStrategy, y.ResolutionStrategy) &&
                CompareHelper.ListIsEqual(x.Nodes, x.Nodes);
        }

        public int GetHashCode(ApiMoveNodesRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiMoveNodeComparer : IEqualityComparer<ApiMoveNode> {
        public bool Equals(ApiMoveNode x, ApiMoveNode y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.NodeId == y.NodeId &&
                string.Equals(x.NewName, y.NewName);
        }

        public int GetHashCode(ApiMoveNode obj) {
            throw new NotImplementedException();
        }
    }

    internal class RecycleBinItemListComparer : IEqualityComparer<RecycleBinItemList> {
        public bool Equals(RecycleBinItemList x, RecycleBinItemList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                x.Limit == y.Limit &&
                x.Total == y.Total &&
                CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(RecycleBinItemList obj) {
            throw new NotImplementedException();
        }
    }

    internal class RecycleBinItemComparer : IEqualityComparer<RecycleBinItem> {
        public bool Equals(RecycleBinItem x, RecycleBinItem y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.FirstDeletedAt == y.FirstDeletedAt &&
                x.LastDeletedAt == y.LastDeletedAt &&
                x.LastDeletedNodeId == y.LastDeletedNodeId &&
                string.Equals(x.Name, y.Name) &&
                x.ParentId == y.ParentId &&
                string.Equals(x.ParentPath, y.ParentPath) &&
                x.Type == y.Type &&
                x.VersionsCount == y.VersionsCount;
        }

        public int GetHashCode(RecycleBinItem obj) {
            throw new NotImplementedException();
        }
    }

    internal class PreviousVersionListComparer : IEqualityComparer<PreviousVersionList> {
        public bool Equals(PreviousVersionList x, PreviousVersionList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                x.Limit == y.Limit &&
                x.Total == y.Total &&
                CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(PreviousVersionList obj) {
            throw new NotImplementedException();
        }
    }

    internal class PreviousVersionComparer : IEqualityComparer<PreviousVersion> {
        public bool Equals(PreviousVersion x, PreviousVersion y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            Assert.Equal(x.CreatedBy, y.CreatedBy, new UserInfoComparer());
            Assert.Equal(x.DeletedBy, y.DeletedBy, new UserInfoComparer());
            Assert.Equal(x.UpdatedBy, y.UpdatedBy, new UserInfoComparer());
            return x.AccessedAt == y.AccessedAt &&
                x.Classification == y.Classification &&
                x.CreatedAt == y.CreatedAt &&
                string.Equals(x.Name, y.Name) &&
                x.ParentId == y.ParentId &&
                string.Equals(x.ParentPath, y.ParentPath) &&
                x.Type == y.Type &&
                x.DeletedAt == y.DeletedAt &&
                x.ExpireAt == y.ExpireAt &&
                x.Id == y.Id &&
                x.IsEncrypted == y.IsEncrypted &&
                string.Equals(x.Notes, y.Notes) &&
                x.Size == y.Size &&
                x.UpdatedAt == y.UpdatedAt;
        }

        public int GetHashCode(PreviousVersion obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiRestorePreviousVersionsRequestComparer : IEqualityComparer<ApiRestorePreviousVersionsRequest> {
        public bool Equals(ApiRestorePreviousVersionsRequest x, ApiRestorePreviousVersionsRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.KeepShareLinks == y.KeepShareLinks &&
                x.ParentId == y.ParentId &&
                string.Equals(x.ResolutionStrategy, y.ResolutionStrategy) &&
                CompareHelper.ListIsEqual(x.DeletedNodeIds, y.DeletedNodeIds);
        }

        public int GetHashCode(ApiRestorePreviousVersionsRequest obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiDeletePreviousVersionsRequestComparer : IEqualityComparer<ApiDeletePreviousVersionsRequest> {
        public bool Equals(ApiDeletePreviousVersionsRequest x, ApiDeletePreviousVersionsRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return CompareHelper.ListIsEqual(x.VersionsToBeDeleted, y.VersionsToBeDeleted);
        }

        public int GetHashCode(ApiDeletePreviousVersionsRequest obj) {
            throw new NotImplementedException();
        }
    }
}
