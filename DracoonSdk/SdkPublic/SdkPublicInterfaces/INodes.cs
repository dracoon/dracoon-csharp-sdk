using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.Sort;
using System;
using System.IO;

namespace Dracoon.Sdk {
    /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/INodes/*'/>
    public interface INodes {
        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetNodes/*'/>
        NodeList GetNodes(long parentNodeId = 0, long? offset = null, long? limit = null, GetNodesFilter filter = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetNodeId/*'/>
        Node GetNode(long nodeId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetNodePath/*'/>
        Node GetNode(string nodePath);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/DeleteNodes/*'/>
        void DeleteNodes(DeleteNodesRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/CopyNodes/*'/>
        Node CopyNodes(CopyNodesRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/MoveNodes/*'/>
        Node MoveNodes(MoveNodesRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/CreateRoom/*'/>
        Node CreateRoom(CreateRoomRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/UpdateRoom/*'/>
        Node UpdateRoom(UpdateRoomRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/EnableRoomEncryption/*'/>
        Node EnableRoomEncryption(EnableRoomEncryptionRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/CreateFolder/*'/>
        Node CreateFolder(CreateFolderRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/UpdateFolder/*'/>
        Node UpdateFolder(UpdateFolderRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/UpdateFile/*'/>
        Node UpdateFile(UpdateFileRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/UploadFile/*'/>
        Node UploadFile(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/StartUploadFileAsync/*'/>
        void StartUploadFileAsync(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/CancelUploadFileAsync/*'/>
        void CancelUploadFileAsync(string actionId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/DownloadFile/*'/>
        void DownloadFile(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/StartDownloadFileAsync/*'/>
        void StartDownloadFileAsync(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/CancelDownloadFileAsync/*'/>
        void CancelDownloadFileAsync(string actionId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/SearchNodes/*'/>
        NodeList SearchNodes(string searchString, long parentNodeId = 0, long offset = 0, long limit = 500, SearchNodesFilter filter = null,
            SearchNodesSort sort = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GenerateMissingFileKeys/*'/>
        void GenerateMissingFileKeys(long? nodeId = null, int limit = int.MaxValue);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/SetNodeAsFavorite/*'/>
        Node SetNodeAsFavorite(long nodeId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/DeleteNodeFromFavorites/*'/>
        void DeleteNodeFromFavorites(long nodeId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetRecycleBinItems/*'/>
        RecycleBinItemList GetRecycleBinItems(long parentRoomId, long? offset = null, long? limit = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/EmptyRecycleBin/*'/>
        void EmptyRecycleBin(long parentRoomId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetPreviousVersions/*'/>
        PreviousVersionList GetPreviousVersions(long parentId, NodeType type, string nodeName, long? offset = null, long? limit = null);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/GetPreviousVersion/*'/>
        PreviousVersion GetPreviousVersion(long previousNodeId);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/RestorePreviousVersion/*'/>
        void RestorePreviousVersion(RestorePreviousVersionsRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/DeletePreviousVersions/*'/>
        void DeletePreviousVersions(DeletePreviousVersionsRequest request);

        /// <include file = "SdkPublicInterfacesDoc.xml" path='docs/members[@name="iNodes"]/BuildMediaUrl/*'/>
        Uri BuildMediaUrl(string mediaToken, int width, int height);
    }
}