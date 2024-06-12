using Dracoon.Crypto.Sdk;
using Dracoon.Crypto.Sdk.Model;
using Dracoon.Sdk.Error;
using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.SdkInternal.Util;
using Dracoon.Sdk.SdkInternal.Validator;
using Dracoon.Sdk.Sort;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonNodesImplTest {
        #region GetNodes

        [Fact]
        public void GetNodes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long>().MustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetNodes(Arg.AnyLong, Arg.IsAny<long?>(), Arg.IsAny<long?>(), Arg.IsAny<GetNodesFilter>())).Returns(FactoryRestSharp.GetNodesMock(1)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNodeList>(Arg.IsAny<RestRequest>(), RequestType.GetNodes, 0)).Returns(FactoryNode.ApiNodeList).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNodeList(Arg.IsAny<ApiNodeList>())).Returns(FactoryNode.NodeList).Occurs(1);

            // ACT
            NodeList actual = n.GetNodes(1);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long>().MustNotNegative(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiNodeList(Arg.IsAny<ApiNodeList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetNode

        [Fact]
        public void GetNode_ById() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetNode(Arg.AnyLong)).Returns(FactoryRestSharp.GetNodeMock(1)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.GetNode, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.GetNode(123);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void GetNode_ByPath_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustValidNodePath(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => n.SearchNodes(Arg.AnyString, Arg.AnyLong, Arg.AnyLong, Arg.AnyLong, Arg.IsAny<SearchNodesFilter>(),
                Arg.IsAny<SearchNodesSort>())).Returns(FactoryNode.NodeList).Occurs(1);

            // ACT
            Node actual = n.GetNode("/Root");

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyString.MustValidNodePath(Arg.AnyString));
            Mock.Assert(n);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void GetNode_ByPath_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustValidNodePath(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => n.SearchNodes(Arg.AnyString, Arg.AnyLong, Arg.AnyLong, Arg.AnyLong, Arg.IsAny<SearchNodesFilter>(),
                Arg.IsAny<SearchNodesSort>())).Returns(FactoryNode.NodeList).Occurs(1);

            try {
                // ACT
                n.GetNode("/RootFail");
            } catch (DracoonApiException e) {
                // ASSERT
                Assert.Equal(DracoonApiCode.SERVER_NODE_NOT_FOUND, e.ErrorCode);
                Mock.Assert(() => Arg.AnyString.MustValidNodePath(Arg.AnyString));
                Mock.Assert(n);
                Mock.Assert(c.Executor);
            }
        }

        #endregion

        #region IsNodeEncrypted

        [Fact]
        public void IsNodeEncrypted() {
            // ARRANGE
            Node node = FactoryNode.Node;
            bool expected = node.IsEncrypted.Value;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => n.GetNode(Arg.AnyLong)).Returns(node).Occurs(1);

            // ACT
            bool actual = n.IsNodeEncrypted(1435);

            // ASSERT
            Assert.Equal(expected, actual);
            Mock.Assert(n);
        }

        #endregion

        #region DeleteNodes

        [Fact]
        public void DeleteNodes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<DeleteNodesRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => NodeMapper.ToApiDeleteNodesRequest(Arg.IsAny<DeleteNodesRequest>())).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteNodes(Arg.IsAny<ApiDeleteNodesRequest>())).Returns(FactoryRestSharp.DeleteNodesMock()).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.DeleteNodes, 0)).DoNothing().Occurs(1);

            // ACT
            n.DeleteNodes(FactoryNode.DeleteNodesRequest);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<DeleteNodesRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.ToApiDeleteNodesRequest(Arg.IsAny<DeleteNodesRequest>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region CopyNodes

        [Fact]
        public void CopyNodes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CopyNodesRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<CopyNode>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(2);
            Mock.Arrange(() => NodeMapper.ToApiCopyNodesRequest(Arg.IsAny<CopyNodesRequest>())).Returns(FactoryNode.ApiCopyNodesRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PostCopyNodes(Arg.AnyLong, Arg.IsAny<ApiCopyNodesRequest>())).Returns(FactoryRestSharp.CopyNodesMock(2)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PostCopyNodes, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.CopyNodes(FactoryNode.CopyNodesRequest);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<CopyNodesRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<CopyNode>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.ToApiCopyNodesRequest(Arg.IsAny<CopyNodesRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region MoveNodes

        [Fact]
        public void MoveNodes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<MoveNodesRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<MoveNode>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(2);
            Mock.Arrange(() => NodeMapper.ToApiMoveNodesRequest(Arg.IsAny<MoveNodesRequest>())).Returns(FactoryNode.ApiMoveNodesRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PostMoveNodes(Arg.AnyLong, Arg.IsAny<ApiMoveNodesRequest>())).Returns(FactoryRestSharp.MoveNodesMock(2134)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PostMoveNodes, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.MoveNodes(FactoryNode.MoveNodesRequest);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<MoveNodesRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<MoveNode>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.ToApiMoveNodesRequest(Arg.IsAny<MoveNodesRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region SearchNodes

        [Fact]
        public void SearchNodes() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustNotNegative(Arg.AnyString)).DoNothing().Occurs(2);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetSearchNodes(Arg.AnyLong, Arg.AnyString, Arg.AnyLong, Arg.AnyLong, Arg.AnyInt, Arg.IsAny<SearchNodesFilter>(),
                    Arg.IsAny<SearchNodesSort>())).Returns(FactoryRestSharp.GetSearchNodesMock(1234, "test", 0, 1)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNodeList>(Arg.IsAny<RestRequest>(), RequestType.GetSearchNodes, 0)).Returns(FactoryNode.ApiNodeList).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNodeList(Arg.IsAny<ApiNodeList>())).Returns(FactoryNode.NodeList).Occurs(1);

            // ACT
            NodeList actual = n.SearchNodes("things", 234);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.AnyLong.MustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiNodeList(Arg.IsAny<ApiNodeList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region SetNodeAsFavorite

        [Fact]
        public void SetNodeAsFavorite() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.PostFavorite(Arg.AnyLong)).Returns(FactoryRestSharp.PostFavoriteMock(123)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PostFavorite, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.SetNodeAsFavorite(2354);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeleteNodeFromFavorites

        [Fact]
        public void DeleteNodeFromFavorites() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteFavorite(Arg.AnyLong)).Returns(FactoryRestSharp.DeleteFavoriteMock(123)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.DeleteFavorite, 0)).DoNothing().Occurs(1);

            // ACT
            n.DeleteNodeFromFavorites(2345);

            // ASSERT
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetRecycleBinItems

        [Fact]
        public void GetRecycleBinItems() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetRecycleBin(Arg.AnyLong, Arg.IsAny<long?>(), Arg.IsAny<long?>())).Returns(FactoryRestSharp.GetRecycleBinItemsMock(213)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDeletedNodeSummaryList>(Arg.IsAny<RestRequest>(), RequestType.GetRecycleBin, 0))
                    .Returns(FactoryNode.ApiDeletedNodeSummaryList).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiDeletedNodeSummaryList(Arg.IsAny<ApiDeletedNodeSummaryList>()))
                .Returns(FactoryNode.RecycleBinItemList).Occurs(1);

            // ACT
            RecycleBinItemList actual = n.GetRecycleBinItems(234);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiDeletedNodeSummaryList(Arg.IsAny<ApiDeletedNodeSummaryList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region EmptyRecycleBin

        [Fact]
        public void EmptyRecycleBin() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.DeleteRecycleBin(Arg.AnyLong)).Returns(FactoryRestSharp.DeleteRecycleBinMock(12542)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.DeleteRecycleBin, 0)).DoNothing().Occurs(1);

            // ACT
            n.EmptyRecycleBin(243);

            // ASSERT
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetPreviousVersions

        [Fact]
        public void GetPreviousVersions() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetPreviousVersions(Arg.AnyLong, Arg.AnyString, Arg.AnyString, Arg.IsAny<long?>(), Arg.IsAny<long?>())).Returns(FactoryRestSharp.GetPreviousVersionsMock(124, "file", "name")).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDeletedNodeVersionsList>(Arg.IsAny<RestRequest>(), RequestType.GetPreviousVersions, 0))
                .Returns(FactoryNode.ApiDeletedNodeVersionsList).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiDeletedNodeVersionsList(Arg.IsAny<ApiDeletedNodeVersionsList>()))
                .Returns(FactoryNode.PreviousVersionList).Occurs(1);

            // ACT
            PreviousVersionList actual = n.GetPreviousVersions(12346, NodeType.File, "name");

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => NodeMapper.FromApiDeletedNodeVersionsList(Arg.IsAny<ApiDeletedNodeVersionsList>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region GetPreviousVersion

        [Fact]
        public void GetPreviousVersion() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.GetPreviousVersion(Arg.AnyLong)).Returns(FactoryRestSharp.GetPreviousVersionMock(213)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiDeletedNodeVersion>(Arg.IsAny<RestRequest>(), RequestType.GetPreviousVersion, 0))
                .Returns(FactoryNode.ApiDeletedNodeVersion).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiDeletedNodeVersion(Arg.IsAny<ApiDeletedNodeVersion>())).Returns(FactoryNode.PreviousVersion).Occurs(1);

            // ACT
            PreviousVersion actual = n.GetPreviousVersion(235);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.FromApiDeletedNodeVersion(Arg.IsAny<ApiDeletedNodeVersion>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region RestorePreviousVersion

        [Fact]
        public void RestorePreviousVersion() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<RestorePreviousVersionsRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => NodeMapper.ToApiRestorePreviousVersionsRequest(Arg.IsAny<RestorePreviousVersionsRequest>()))
                .Returns(FactoryNode.ApiRestorePreviousVersionsRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PostRestoreNodeVersion(Arg.IsAny<ApiRestorePreviousVersionsRequest>())).Returns(FactoryRestSharp.PostRestoreNodeVersionMock).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.PostRestoreNodeVersion, 0)).DoNothing().Occurs(1);

            // ACT
            n.RestorePreviousVersion(FactoryNode.RestorePreviousVersionsRequest);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<RestorePreviousVersionsRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.ToApiRestorePreviousVersionsRequest(Arg.IsAny<RestorePreviousVersionsRequest>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region DeletePreviousVersions

        [Fact]
        public void DeletePreviousVersions() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<DeletePreviousVersionsRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => NodeMapper.ToApiDeletePreviousVersionsRequest(Arg.IsAny<DeletePreviousVersionsRequest>()))
                .Returns(FactoryNode.ApiDeletePreviousVersionsRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.DeletePreviousVersion(Arg.IsAny<ApiDeletePreviousVersionsRequest>())).Returns(FactoryRestSharp.DeletePreviousVersionMock).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.DeletePreviousVersions, 0)).DoNothing().Occurs(1);

            // ACT
            n.DeletePreviousVersions(FactoryNode.DeletePreviousVersionsRequest);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<DeletePreviousVersionsRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => NodeMapper.ToApiDeletePreviousVersionsRequest(Arg.IsAny<DeletePreviousVersionsRequest>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region CreateRoom

        [Fact]
        public void CreateRoom_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateRoomRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().CheckEnumerableNullOrEmpty()).Returns(false).OccursAtLeast(1);
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString)).DoNothing().Occurs(2);
            Mock.Arrange(() => RoomMapper.ToApiCreateRoomRequest(Arg.IsAny<CreateRoomRequest>())).Returns(FactoryRoom.ApiCreateRoomRequest);
            Mock.Arrange(() => c.Builder.PostRoom(Arg.IsAny<ApiCreateRoomRequest>())).Returns(FactoryRestSharp.PostRoomMock).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PostRoom, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node);

            // ACT
            Node actual = n.CreateRoom(FactoryRoom.CreateRoomRequest);

            // ASSERT
            Assert.NotNull(actual);
            Mock.Assert(() => Arg.IsAny<CreateRoomRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<int?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<IEnumerable<long>>().CheckEnumerableNullOrEmpty());
            Mock.Assert(() => Arg.IsAny<IEnumerable<long>>().EnumerableMustNotNullOrEmpty(Arg.AnyString));
            Mock.Assert(() => RoomMapper.ToApiCreateRoomRequest(Arg.IsAny<CreateRoomRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        [Fact]
        public void CreateRoom_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateRoomRequest>().MustNotNull(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing();
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.IsAny<int?>().NullableMustNotNegative(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.IsAny<IEnumerable<long>>().CheckEnumerableNullOrEmpty()).Returns(true);

            // ACT - ASSERT
            Assert.Throws<ArgumentNullException>(() => n.CreateRoom(FactoryRoom.CreateRoomRequest));
        }

        #endregion

        #region UpdateRoom

        [Fact]
        public void UpdateRoom() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<UpdateRoomRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.Builder.PutRoom(Arg.AnyLong, Arg.IsAny<ApiUpdateRoomRequest>())).Returns(FactoryRestSharp.PutRoomMock(13)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PutRoom, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => RoomMapper.ToApiUpdateRoomRequest(Arg.IsAny<UpdateRoomRequest>())).Returns(FactoryRoom.ApiUpdateRoomRequest).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.UpdateRoom(FactoryRoom.UpdateRoomRequest);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<UpdateRoomRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustNotNegative(Arg.AnyString));
            Mock.Assert(() => RoomMapper.ToApiUpdateRoomRequest(Arg.IsAny<UpdateRoomRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region EnableRoomEncryption

        [Fact]
        public void EnableRoomEncryption_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<EnableRoomEncryptionRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<UserKeyPairAlgorithm?>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.AccountImpl.AssertUserKeyPairAlgorithmSupported(Arg.IsAny<UserKeyPairAlgorithm>())).DoNothing().Occurs(1);
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>(), Arg.IsAny<char[]>())).Returns(FactoryUser.UserKeyPair_2048).Occurs(1);
            Mock.Arrange(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>())).Returns(FactoryUser.ApiUserKeyPair_2048).Occurs(1);
            Mock.Arrange(() => RoomMapper.ToApiEnableRoomEncryptionRequest(Arg.IsAny<EnableRoomEncryptionRequest>(), Arg.IsAny<ApiUserKeyPair>()))
                .Returns(FactoryRoom.ApiEnableRoomEncryptionRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PutEnableRoomEncryption(Arg.AnyLong, Arg.IsAny<ApiEnableRoomEncryptionRequest>())).Returns(FactoryRestSharp.PutEnableRoomEncryptionMock(132)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PutEnableRoomEncryption, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.EnableRoomEncryption(FactoryRoom.EnableRoomEncryptionRequest);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<EnableRoomEncryptionRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.IsAny<UserKeyPairAlgorithm?>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>(), Arg.IsAny<char[]>()));
            Mock.Assert(() => UserMapper.ToApiUserKeyPair(Arg.IsAny<UserKeyPair>()));
            Mock.Assert(() => RoomMapper.ToApiEnableRoomEncryptionRequest(Arg.IsAny<EnableRoomEncryptionRequest>(), Arg.IsAny<ApiUserKeyPair>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            Mock.Assert(c.AccountImpl);
        }

        [Fact]
        public void EnableRoomEncryption_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<EnableRoomEncryptionRequest>().MustNotNull(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing();
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => c.AccountImpl.AssertUserKeyPairAlgorithmSupported(Arg.IsAny<UserKeyPairAlgorithm>())).DoNothing();
            Mock.Arrange(() => Crypto.Sdk.Crypto.GenerateUserKeyPair(Arg.IsAny<UserKeyPairAlgorithm>(), Arg.IsAny<char[]>())).Throws(new CryptoException());
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>())).Returns(DracoonCryptoCode.INVALID_PASSWORD_ERROR).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => n.EnableRoomEncryption(FactoryRoom.EnableRoomEncryptionRequest));
            Mock.Assert(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>()));
        }

        #endregion

        #region CreateFolder

        [Fact]
        public void CreateFolder() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<CreateFolderRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => FolderMapper.ToApiCreateFolderRequest(Arg.IsAny<CreateFolderRequest>())).Returns(FactoryFolder.ApiCreateFolderRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PostFolder(Arg.IsAny<ApiCreateFolderRequest>())).Returns(FactoryRestSharp.PostFolderMock).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PostFolder, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.CreateFolder(FactoryFolder.CreateFolderRequest);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<CreateFolderRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => FolderMapper.ToApiCreateFolderRequest(Arg.IsAny<CreateFolderRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region UpdateFolder

        [Fact]
        public void UpdateFolder() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<UpdateFolderRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, true)).DoNothing().Occurs(1);
            Mock.Arrange(() => FolderMapper.ToApiUpdateFolderRequest(Arg.IsAny<UpdateFolderRequest>())).Returns(FactoryFolder.ApiUpdateFolderRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PutFolder(Arg.AnyLong, Arg.IsAny<ApiUpdateFolderRequest>())).Returns(FactoryRestSharp.PutFolderMock(23)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PutFolder, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.UpdateFolder(FactoryFolder.UpdateFolderRequest);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<UpdateFolderRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, true));
            Mock.Assert(() => FolderMapper.ToApiUpdateFolderRequest(Arg.IsAny<UpdateFolderRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region UpdateFile

        [Fact]
        public void UpdateFile() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<UpdateFileRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, true)).DoNothing().Occurs(1);
            Mock.Arrange(() => FileMapper.ToApiUpdateFileRequest(Arg.IsAny<UpdateFileRequest>())).Returns(FactoryFile.ApiUpdateFileRequest).Occurs(1);
            Mock.Arrange(() => c.Builder.PutFile(Arg.AnyLong, Arg.IsAny<ApiUpdateFileRequest>())).Returns(FactoryRestSharp.PutFileMock(123)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiNode>(Arg.IsAny<RestRequest>(), RequestType.PutFile, 0)).Returns(FactoryNode.ApiNode).Occurs(1);
            Mock.Arrange(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>())).Returns(FactoryNode.Node).Occurs(1);

            // ACT
            Node actual = n.UpdateFile(FactoryFile.UpdateFileRequest);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<UpdateFileRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, true));
            Mock.Assert(() => FileMapper.ToApiUpdateFileRequest(Arg.IsAny<UpdateFileRequest>()));
            Mock.Assert(() => NodeMapper.FromApiNode(Arg.IsAny<ApiNode>()));
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
        }

        #endregion

        #region UploadFile

        [Fact]
        public void UploadFile_NotEncrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => n.IsNodeEncrypted(Arg.AnyLong)).Returns(false).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().RunSync()).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>())).Occurs(2);

            // ACT
            Node actual = n.UploadFile("id1", FactoryFile.UploadFileRequest, null);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString));
            Mock.Assert(() => n.IsNodeEncrypted(Arg.AnyLong));
            Mock.Assert(() => Arg.IsAny<FileUpload>().RunSync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>()));
            Mock.Assert(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>()));
        }

        [Fact]
        public void UploadFile_Encrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => n.IsNodeEncrypted(Arg.AnyLong)).Returns(true).Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetPreferredUserKeyPair()).Returns(FactoryUser.UserKeyPair_4096).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().RunSync()).Returns(FactoryNode.Node).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>())).Occurs(2);

            // ACT
            Node actual = n.UploadFile("id1", FactoryFile.UploadFileRequest, null);

            // ASSERT
            Assert.Equal(FactoryNode.Node, actual, new NodeComparer());
            Mock.Assert(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString));
            Mock.Assert(() => n.IsNodeEncrypted(Arg.AnyLong));
            Mock.Assert(() => c.AccountImpl.GetPreferredUserKeyPair());
            Mock.Assert(() => Arg.IsAny<FileUpload>().RunSync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>()));
            Mock.Assert(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>()));
        }

        [Fact]
        public void UploadFile_NotEncrypted_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing();
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => n.IsNodeEncrypted(Arg.AnyLong)).Returns(false);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().RunSync()).Returns(FactoryNode.Node);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>()));

            // ACT
            n.UploadFile("id1", FactoryFile.UploadFileRequest, null);

            // ASSERT
            Assert.Throws<ArgumentException>(() => n.UploadFile("id1", FactoryFile.UploadFileRequest, null)); // id still exists
        }

        #endregion

        #region StartUploadFileAsync

        [Fact]
        public void StartUploadFileAsync_NotEncrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => n.IsNodeEncrypted(Arg.AnyLong)).Returns(false).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().RunAsync()).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>())).Occurs(2);

            // ACT
            n.StartUploadFileAsync("id1", FactoryFile.UploadFileRequest, null);

            // ASSERT
            Mock.Assert(() => Arg.IsAny<FileUploadRequest>().MustNotNull(Arg.AnyString));
            Mock.Assert(() => Arg.AnyLong.MustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanRead(Arg.AnyString));
            Mock.Assert(() => n.IsNodeEncrypted(Arg.AnyLong));
            Mock.Assert(() => Arg.IsAny<FileUpload>().RunAsync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileUpload>>().Add(Arg.AnyString, Arg.IsAny<FileUpload>()));
            Mock.Assert(() => Arg.IsAny<FileUpload>().AddFileUploadCallback(Arg.IsAny<IFileUploadCallback>()));
        }

        #endregion

        #region CancelUploadFileAsync

        [Fact]
        public void CancelUploadFileAsync() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileUpload>>().ContainsKey(Arg.AnyString)).Returns(true);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileUpload>>()[Arg.AnyString]).Returns(new FileUpload(c, "id1", FactoryFile.UploadFileRequest, null, -1));
            Mock.Arrange(() => Arg.IsAny<FileUpload>().CancelUpload()).DoNothing().Occurs(1);

            // ACT
            n.CancelUploadFileAsync("id1");

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<FileUpload>().CancelUpload());
        }

        #endregion

        #region DownloadFile

        [Fact]
        public void DownloadFile_NotEncrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString)).DoNothing().Occurs(1);
            Node requestedNode = FactoryNode.Node;
            requestedNode.IsEncrypted = false;
            Mock.Arrange(() => n.GetNode(Arg.AnyLong)).Returns(requestedNode).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().RunSync()).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>())).Occurs(2);

            // ACT
            n.DownloadFile("id1", 1435, null, null);

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString));
            Mock.Assert(() => n.GetNode(Arg.AnyLong));
            Mock.Assert(() => Arg.IsAny<FileDownload>().RunSync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>()));
            Mock.Assert(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>()));
        }

        [Fact]
        public void DownloadFile_Encrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString)).DoNothing().Occurs(1);
            Node requestedNode = FactoryNode.Node;
            requestedNode.IsEncrypted = true;
            Mock.Arrange(() => n.GetNode(Arg.AnyLong)).Returns(requestedNode).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().RunSync()).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>())).Occurs(2);

            // ACT
            n.DownloadFile("id1", 1435, null, null);

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString));
            Mock.Assert(() => n.GetNode(Arg.AnyLong));
            Mock.Assert(() => Arg.IsAny<FileDownload>().RunSync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>()));
            Mock.Assert(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>()));
        }

        [Fact]
        public void DownloadFile_NotEncrypted_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyLong.MustPositive(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing();
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString)).DoNothing();
            Node requestedNode = FactoryNode.Node;
            requestedNode.IsEncrypted = false;
            Mock.Arrange(() => n.GetNode(Arg.AnyLong)).Returns(requestedNode);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().RunSync()).DoNothing();
            Mock.Arrange(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>()));

            // ACT
            n.DownloadFile("id1", 1435, null, null);

            // ASSERT
            Assert.Throws<ArgumentException>(() => n.DownloadFile("id1", 354, null, null));
        }

        #endregion

        #region StartDownloadFileAsync

        [Fact]
        public void StartDownloadFileAsync_NotEncrypted_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString)).DoNothing().Occurs(1);
            Node requestedNode = FactoryNode.Node;
            requestedNode.IsEncrypted = false;
            Mock.Arrange(() => n.GetNode(Arg.AnyLong)).Returns(requestedNode).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().RunAsync()).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>())).Occurs(1);
            Mock.Arrange(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>())).Occurs(2);

            // ACT
            n.StartDownloadFileAsync("id1", 1435, null, null);

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<Stream>().CheckStreamCanWrite(Arg.AnyString));
            Mock.Assert(() => n.GetNode(Arg.AnyLong));
            Mock.Assert(() => Arg.IsAny<FileDownload>().RunAsync());
            Mock.Assert(() => Arg.IsAny<Dictionary<string, FileDownload>>().Add(Arg.AnyString, Arg.IsAny<FileDownload>()));
            Mock.Assert(() => Arg.IsAny<FileDownload>().AddFileDownloadCallback(Arg.IsAny<IFileDownloadCallback>()));
        }

        #endregion

        #region CancelDownloadFileAsync

        [Fact]
        public void CancelDownloadFileAsync() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileDownload>>().ContainsKey(Arg.AnyString)).Returns(true);
            Mock.Arrange(() => Arg.IsAny<Dictionary<string, FileDownload>>()[Arg.AnyString]).Returns(new FileDownload(c, "id1", FactoryNode.Node, null));
            Mock.Arrange(() => Arg.IsAny<FileDownload>().CancelDownload()).DoNothing().Occurs(1);

            // ACT
            n.CancelDownloadFileAsync("id1");

            // ASSERT
            Mock.Assert(() => Arg.AnyString.MustNotNullOrEmptyOrWhitespace(Arg.AnyString, false));
            Mock.Assert(() => Arg.IsAny<FileDownload>().CancelDownload());
        }

        #endregion

        #region GenerateMissingFileKeys

        [Fact]
        public void GenerateMissingFileKeys_Success() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => Arg.AnyInt.MustPositive(Arg.AnyString)).DoNothing().Occurs(1);
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPairs()).Returns(FactoryUser.UserKeyPairs).Occurs(1);
            Mock.Arrange(() => c.Builder.GetMissingFileKeys(Arg.IsAny<long?>(), Arg.AnyInt, Arg.AnyInt)).Returns(FactoryRestSharp.GetMissingFileKeysMock(10, 0)).OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiMissingFileKeys>(Arg.IsAny<RestRequest>(), RequestType.GetMissingFileKeys, 0))
                    .Returns(FactoryFile.ApiMissingFileKeys).OccursAtLeast(1);
            Mock.Arrange(() => UserMapper.ConvertApiUserIdPublicKeys(Arg.IsAny<List<ApiUserIdPublicKey>>())).Returns(FactoryFile.FileUserPublicKey).OccursAtLeast(1);
            Mock.Arrange(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>())).Returns(FactoryFile.EncryptedFileKey).OccursAtLeast(1);
            Mock.Arrange(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>())).Returns(FactoryFile.ApiFileKey).OccursAtLeast(1);
            Mock.Arrange(() => c.Builder.PostMissingFileKeys(Arg.IsAny<ApiSetUserFileKeysRequest>())).Returns(FactoryRestSharp.PostMissingFileKeysMock).OccursAtLeast(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.PostMissingFileKeys, 0)).DoNothing().OccursAtLeast(1);
            Mock.Arrange(() => n.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<long?>()))
                .Returns(FactoryFile.PlainFileKey).OccursAtLeast(1);
            Mock.Arrange(() => n.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>(), Arg.IsAny<long?>()))
                .Returns(FactoryFile.EncryptedFileKey).OccursAtLeast(1);
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048).Occurs(FactoryUser.UserKeyPairs.Count);

            // ACT
            n.GenerateMissingFileKeys();

            // ASSERT
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyInt.MustPositive(Arg.AnyString));
            Mock.Assert(() => UserMapper.ConvertApiUserIdPublicKeys(Arg.IsAny<List<ApiUserIdPublicKey>>()));
            Mock.Assert(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>()));
            Mock.Assert(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>()));
            Mock.Assert(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>()));
            Mock.Assert(c.AccountImpl);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            Mock.Assert(n);
        }

        [Fact]
        public void GenerateMissingFileKeys_Success_NoKeys() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => Arg.AnyInt.MustPositive(Arg.AnyString)).DoNothing();
            Mock.Arrange(() => c.AccountImpl.GetAndCheckUserKeyPairs()).Returns(FactoryUser.UserKeyPairs).Occurs(1);
            Mock.Arrange(() => c.Builder.GetMissingFileKeys(Arg.IsAny<long?>(), Arg.AnyInt, Arg.AnyInt)).Returns(FactoryRestSharp.GetMissingFileKeysMock(10, 0));
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiMissingFileKeys>(Arg.IsAny<RestRequest>(), RequestType.GetMissingFileKeys, 0))
                    .Returns(new ApiMissingFileKeys() {
                        Items = new List<ApiUserIdFileId>()
                    });
            Mock.Arrange(() => UserMapper.ConvertApiUserIdPublicKeys(Arg.IsAny<List<ApiUserIdPublicKey>>())).Returns(FactoryFile.FileUserPublicKey).OccursNever();
            Mock.Arrange(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>())).Returns(FactoryFile.EncryptedFileKey).OccursNever();
            Mock.Arrange(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>())).Returns(FactoryFile.ApiFileKey).OccursNever();
            Mock.Arrange(() => c.Builder.PostMissingFileKeys(Arg.IsAny<ApiSetUserFileKeysRequest>())).Returns(FactoryRestSharp.PostMissingFileKeysMock).OccursNever();
            Mock.Arrange(() => c.Executor.DoSyncApiCall<VoidResponse>(Arg.IsAny<RestRequest>(), RequestType.PostMissingFileKeys, 0)).DoNothing().OccursNever();
            Mock.Arrange(() => n.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<long?>()))
                .Returns(FactoryFile.PlainFileKey).OccursNever();
            Mock.Arrange(() => n.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>(), Arg.IsAny<long?>()))
                .Returns(FactoryFile.EncryptedFileKey).OccursNever();
            Mock.Arrange(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>())).Returns(UserKeyPairAlgorithm.RSA2048).Occurs(0);

            // ACT
            n.GenerateMissingFileKeys();

            // ASSERT
            Mock.Assert(() => Arg.IsAny<long?>().NullableMustPositive(Arg.AnyString));
            Mock.Assert(() => Arg.AnyInt.MustPositive(Arg.AnyString));
            Mock.Assert(() => UserMapper.ConvertApiUserIdPublicKeys(Arg.IsAny<List<ApiUserIdPublicKey>>()));
            Mock.Assert(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>()));
            Mock.Assert(() => FileMapper.ToApiFileKey(Arg.IsAny<EncryptedFileKey>()));
            Mock.Assert(() => CryptoHelper.DetermineUserKeyPairVersion(Arg.IsAny<EncryptedFileKeyAlgorithm>()));
            Mock.Assert(c.AccountImpl);
            Mock.Assert(c.Builder);
            Mock.Assert(c.Executor);
            Mock.Assert(n);

        }

        #endregion

        #region EncryptFileKey

        [Fact]
        public void EncryptFileKey() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()))
                .Returns(FactoryFile.EncryptedFileKey).Occurs(1);

            // ACT
            EncryptedFileKey actual = n.EncryptFileKey(FactoryFile.PlainFileKey, FactoryUser.UserPublicKey_2048);

            // ASSERT
            Assert.Equal(FactoryFile.EncryptedFileKey, actual, new EncryptedFileKeyComparer());
            Mock.Assert(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>()));
        }

        [Fact]
        public void EncryptFileKey_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.EncryptFileKey(Arg.IsAny<PlainFileKey>(), Arg.IsAny<UserPublicKey>())).Throws(new CryptoException());
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>())).Returns(DracoonCryptoCode.INTERNAL_ERROR).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => n.EncryptFileKey(FactoryFile.PlainFileKey, FactoryUser.UserPublicKey_2048));
            Mock.Assert(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>()));
        }

        #endregion

        #region DecryptFileKey

        [Fact]
        public void DecryptFileKey() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<char[]>()))
                .Returns(FactoryFile.PlainFileKey).Occurs(1);

            // ACT
            PlainFileKey actual = n.DecryptFileKey(FactoryFile.EncryptedFileKey, FactoryUser.UserPrivateKey_2048);

            // ASSERT
            Assert.Equal(FactoryFile.PlainFileKey, actual, new PlainFileKeyComparer());
            Mock.Assert(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<char[]>()));
        }

        [Fact]
        public void DecryptFileKey_Fail() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => Crypto.Sdk.Crypto.DecryptFileKey(Arg.IsAny<EncryptedFileKey>(), Arg.IsAny<UserPrivateKey>(), Arg.IsAny<char[]>()))
                .Throws(new CryptoException());
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>())).Returns(DracoonCryptoCode.INTERNAL_ERROR).Occurs(1);

            // ACT - ASSERT
            Assert.Throws<DracoonCryptoException>(() => n.DecryptFileKey(FactoryFile.EncryptedFileKey, FactoryUser.UserPrivateKey_2048));
            Mock.Arrange(() => CryptoErrorMapper.ParseCause(Arg.IsAny<Exception>()));
        }

        #endregion

        #region GetEncryptedFileKey

        [Fact]
        public void GetEncryptedFileKey() {
            // ARRANGE
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonNodesImpl n = new DracoonNodesImpl(c);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiFileKey>(Arg.IsAny<RestRequest>(), RequestType.GetFileKey, 0)).Returns(FactoryFile.ApiFileKey).Occurs(1);
            Mock.Arrange(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>())).Returns(FactoryFile.EncryptedFileKey).Occurs(1);
            Mock.Arrange(() => c.Builder.GetFileKey(Arg.AnyLong)).Returns(FactoryRestSharp.GetFileKeyMock(213)).Occurs(1);

            // ACT
            EncryptedFileKey actual = n.GetEncryptedFileKey(253);

            // ASSERT
            Assert.Equal(FactoryFile.EncryptedFileKey, actual, new EncryptedFileKeyComparer());
            Mock.Assert(() => c.Executor.DoSyncApiCall<ApiFileKey>(Arg.IsAny<RestRequest>(), RequestType.GetFileKey, 0));
            Mock.Assert(() => FileMapper.FromApiFileKey(Arg.IsAny<ApiFileKey>()));
            Mock.Assert(() => c.Builder.GetFileKey(Arg.AnyLong));
        }

        #endregion
    }
}