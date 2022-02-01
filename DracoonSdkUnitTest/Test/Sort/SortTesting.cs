using Dracoon.Sdk.Sort;
using System.Collections.Generic;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Sort {
    public class SortTesting {

        public static IEnumerable<object[]> GetNodesSorts => new List<object[]>{
            new object[] { GetNodesSort.Name.Ascending(), "name:asc" },
            new object[] { GetNodesSort.Name.Descending(), "name:desc" },
            new object[] { GetNodesSort.Classification.Ascending(), "classification:asc" },
            new object[] { GetNodesSort.Classification.Descending(), "classification:desc" },
            new object[] { GetNodesSort.CreatedAt.Ascending(), "createdAt:asc" },
            new object[] { GetNodesSort.CreatedAt.Descending(), "createdAt:desc" },
            new object[] { GetNodesSort.CreatedBy.Ascending(), "createdBy:asc" },
            new object[] { GetNodesSort.CreatedBy.Descending(), "createdBy:desc" },
            new object[] { GetNodesSort.UpdatedAt.Ascending(), "updatedAt:asc" },
            new object[] { GetNodesSort.UpdatedAt.Descending(), "updatedAt:desc" },
            new object[] { GetNodesSort.UpdatedBy.Ascending(), "updatedBy:asc" },
            new object[] { GetNodesSort.UpdatedBy.Descending(), "updatedBy:desc" },
            new object[] { GetNodesSort.FileType.Ascending(), "fileType:asc" },
            new object[] { GetNodesSort.FileType.Descending(), "fileType:desc" },
            new object[] { GetNodesSort.Size.Ascending(), "size:asc" },
            new object[] { GetNodesSort.Size.Descending(), "size:desc" },
            new object[] { GetNodesSort.CountPreviousVersions.Ascending(), "cntDeletedVersions:asc" },
            new object[] { GetNodesSort.CountPreviousVersions.Descending(), "cntDeletedVersions:desc" },
            new object[] { GetNodesSort.CreationTimestamp.Ascending(), "timestampCreation:asc" },
            new object[] { GetNodesSort.CreationTimestamp.Descending(), "timestampCreation:desc" },
            new object[] { GetNodesSort.ModificationTimestamp.Ascending(), "timestampModification:asc" },
            new object[] { GetNodesSort.ModificationTimestamp.Descending(), "timestampModification:desc" }
        };

        [Theory, MemberData(nameof(GetNodesSorts))]
        public void TestSortMappingGetNodes(DracoonSort sort, string filterString) {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Equal(sort.ToString(), filterString);
        }

        public static IEnumerable<object[]> SearchNodesSorts => new List<object[]>{
            new object[] { SearchNodesSort.Name.Ascending(), "name:asc" },
            new object[] { SearchNodesSort.Name.Descending(), "name:desc" },
            new object[] { SearchNodesSort.Classification.Ascending(), "classification:asc" },
            new object[] { SearchNodesSort.Classification.Descending(), "classification:desc" },
            new object[] { SearchNodesSort.CreatedAt.Ascending(), "createdAt:asc" },
            new object[] { SearchNodesSort.CreatedAt.Descending(), "createdAt:desc" },
            new object[] { SearchNodesSort.CreatedBy.Ascending(), "createdBy:asc" },
            new object[] { SearchNodesSort.CreatedBy.Descending(), "createdBy:desc" },
            new object[] { SearchNodesSort.UpdatedAt.Ascending(), "updatedAt:asc" },
            new object[] { SearchNodesSort.UpdatedAt.Descending(), "updatedAt:desc" },
            new object[] { SearchNodesSort.UpdatedBy.Ascending(), "updatedBy:asc" },
            new object[] { SearchNodesSort.UpdatedBy.Descending(), "updatedBy:desc" },
            new object[] { SearchNodesSort.FileType.Ascending(), "fileType:asc" },
            new object[] { SearchNodesSort.FileType.Descending(), "fileType:desc" },
            new object[] { SearchNodesSort.Size.Ascending(), "size:asc" },
            new object[] { SearchNodesSort.Size.Descending(), "size:desc" },
            new object[] { SearchNodesSort.CountPreviousVersions.Ascending(), "cntDeletedVersions:asc" },
            new object[] { SearchNodesSort.CountPreviousVersions.Descending(), "cntDeletedVersions:desc" },
            new object[] { SearchNodesSort.CreationTimestamp.Ascending(), "timestampCreation:asc" },
            new object[] { SearchNodesSort.CreationTimestamp.Descending(), "timestampCreation:desc" },
            new object[] { SearchNodesSort.ModificationTimestamp.Ascending(), "timestampModification:asc" },
            new object[] { SearchNodesSort.ModificationTimestamp.Descending(), "timestampModification:desc" },
            new object[] { SearchNodesSort.ParentPath.Ascending(), "parentPath:asc" },
            new object[] { SearchNodesSort.ParentPath.Descending(), "parentPath:desc" },
            new object[] { SearchNodesSort.Type.Ascending(), "type:asc" },
            new object[] { SearchNodesSort.Type.Descending(), "type:desc" }
        };

        [Theory, MemberData(nameof(SearchNodesSorts))]
        public void TestSortMappingSearchNodes(DracoonSort sort, string filterString) {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Equal(sort.ToString(), filterString);
        }

        public static IEnumerable<object[]> SharesSorts => new List<object[]>{
            new object[] { SharesSort.CreatedAt.Ascending(), "createdAt:asc" },
            new object[] { SharesSort.CreatedAt.Descending(), "createdAt:desc" },
            new object[] { SharesSort.ExpireAt.Ascending(), "expireAt:asc" },
            new object[] { SharesSort.ExpireAt.Descending(), "expireAt:desc" },
            new object[] { SharesSort.Name.Ascending(), "name:asc" },
            new object[] { SharesSort.Name.Descending(), "name:desc" }
        };

        [Theory, MemberData(nameof(SharesSorts))]
        public void TestSortMappingShares(DracoonSort sort, string filterString) {
            // ARRANGE

            // ACT

            // ASSERT
            Assert.Equal(sort.ToString(), filterString);
        }
    }
}
