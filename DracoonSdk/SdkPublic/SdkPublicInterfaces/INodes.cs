using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.Sort;
using System;
using System.Collections.Generic;
using System.IO;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to perform node actions.
    /// </summary>
    public interface INodes {
        /// <summary>
        ///     Retrieves child nodes of a node. <para/>
        ///     Use <paramref name="parentNodeId"/> = 0 to retrieve root nodes.
        /// </summary>
        /// <param name="parentNodeId">The ID of the parent node. (ID must be 0 or positive)</param>
        /// <param name="offset">The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <param name="filter">The filter for the request result. See also <seealso cref="Dracoon.Sdk.Filter.GetNodesFilter"/></param>
        /// <param name="sort">The sort for the request result. See also <seealso cref="Dracoon.Sdk.Sort.GetNodesSort"/></param>
        /// <returns>List of nodes. See also <seealso cref="Dracoon.Sdk.Model.NodeList"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        NodeList GetNodes(long parentNodeId = 0, long? offset = null, long? limit = null, GetNodesFilter filter = null, GetNodesSort sort = null);

        /// <summary>
        ///     Retrieves a single node by id.
        /// </summary>
        /// <param name="nodeId">The ID of the node. (ID must be positive)</param>
        /// <returns>The node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node GetNode(long nodeId);

        /// <summary>
        ///     Retrieves a single node by path. <para/>
        ///     Use <paramref name="nodePath"/> = "/" to retrieve root nodes.
        /// </summary>
        /// <param name="nodePath">The path of the node.</param>
        /// <returns>The node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node GetNode(string nodePath);

        /// <summary>
        ///     Deletes nodes.
        /// </summary>
        /// <param name="request">The request with IDs of nodes which should be deleted. See also <seealso cref="Dracoon.Sdk.Model.DeleteNodesRequest"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DeleteNodes(DeleteNodesRequest request);

        /// <summary>
        ///     Copies nodes.
        /// </summary>
        /// <param name="request">The request with the nodes which should be copied. See also <seealso cref="Dracoon.Sdk.Model.CopyNodesRequest"/></param>
        /// <returns>The updated parent node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node CopyNodes(CopyNodesRequest request);

        /// <summary>
        ///     Moves nodes.
        /// </summary>
        /// <param name="request">The request with the nodes which should be moved. See also <seealso cref="Dracoon.Sdk.Model.MoveNodesRequest"/></param>
        /// <returns>The updated parent node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node MoveNodes(MoveNodesRequest request);

        /// <summary>
        ///     Creates a new room.
        /// </summary>
        /// <param name="request">The request with the informations about the new room. See also <seealso cref="Dracoon.Sdk.Model.CreateRoomRequest"/></param>
        /// <returns>The new node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node CreateRoom(CreateRoomRequest request);

        /// <summary>
        ///     Updates a room.
        /// </summary>
        /// <param name="request">The request with the updated informations about the room. See also <seealso cref="Dracoon.Sdk.Model.UpdateRoomRequest"/></param>
        /// <returns>The updated node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node UpdateRoom(UpdateRoomRequest request);

        /// <summary>
        ///     Enables the encryption for a room.
        /// </summary>
        /// <param name="request">The request with the encryption informations about the room. See also <seealso cref="Dracoon.Sdk.Model.EnableRoomEncryptionRequest"/></param>
        /// <returns>The updated room node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node EnableRoomEncryption(EnableRoomEncryptionRequest request);

        /// <summary>
        ///     Creates a new folder.
        /// </summary>
        /// <param name="request">The request with the informations about the new folder. See also <seealso cref="Dracoon.Sdk.Model.CreateFolderRequest"/></param>
        /// <returns>The new node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node CreateFolder(CreateFolderRequest request);

        /// <summary>
        ///     Updates a folder.
        /// </summary>
        /// <param name="request">The request with the updated informations about the folder. See also <seealso cref="Dracoon.Sdk.Model.UpdateFolderRequest"/></param>
        /// <returns>The updated node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node UpdateFolder(UpdateFolderRequest request);

        /// <summary>
        ///     Updates a files metadata.
        /// </summary>
        /// <param name="request">The request with the updated infromations about the file. See also <seealso cref="Dracoon.Sdk.Model.UpdateFileRequest"/></param>
        /// <returns>The updated node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node UpdateFile(UpdateFileRequest request);

        /// <summary>
        ///     Uploads a file.
        /// </summary>
        /// <param name="actionId">A ID for the upload. (This ID can be used to keep a reference)</param>
        /// <param name="request">The request with the informations about the file. See also <seealso cref="Dracoon.Sdk.Model.FileUploadRequest"/></param>
        /// <param name="input">A stream of the file.</param>
        /// <param name="fileSize">The size of the file which will be uploaded (if known).</param>
        /// <param name="callback">A informations callback if it's required. See also <seealso cref="Dracoon.Sdk.Model.IFileUploadCallback"/></param>
        /// <returns>The new node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonFileIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetInsecureException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node UploadFile(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null);

        /// <summary>
        ///     Starts an asynchronous file upload.
        /// </summary>
        /// <param name="actionId">A ID for the upload. (This ID can be used to keep a reference)</param>
        /// <param name="request">The request with the informations about the file. See also <seealso cref="Dracoon.Sdk.Model.FileUploadRequest"/></param>
        /// <param name="input">A stream of the file.</param>
        /// <param name="fileSize">The size of the file which will be uploaded (if known).</param>
        /// <param name="callback">A informations callback if it's required. See also <seealso cref="Dracoon.Sdk.Model.IFileUploadCallback"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonFileIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetInsecureException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void StartUploadFileAsync(string actionId, FileUploadRequest request, Stream input, long fileSize = -1, IFileUploadCallback callback = null);

        /// <summary>
        ///     Cancels an asynchronous file upload.
        /// </summary>
        /// <param name="actionId">The ID of the upload.</param>
        void CancelUploadFileAsync(string actionId);

        /// <summary>
        ///     Downloads a file.
        /// </summary>
        /// <param name="actionId">A ID for the download. (This ID can be used to keep a reference)</param>
        /// <param name="nodeId">The ID of the node which should be downloaded.</param>
        /// <param name="output">A stream were the file can be written.</param>
        /// <param name="callback">A informations callback if it's required. See also <seealso cref="Dracoon.Sdk.Model.IFileDownloadCallback"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonFileIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetInsecureException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DownloadFile(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null);

        /// <summary>
        ///     Starts an asynchronous file download.
        /// </summary>
        /// <param name="actionId">A ID for the download. (This ID can be used to keep a reference)</param>
        /// <param name="nodeId">The ID of the node which should be downloaded.</param>
        /// <param name="output">A stream were the file can be written.</param>
        /// <param name="callback">A informations callback if it's required. See also <seealso cref="Dracoon.Sdk.Model.IFileDownloadCallback"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonCryptoException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonFileIOException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetInsecureException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void StartDownloadFileAsync(string actionId, long nodeId, Stream output, IFileDownloadCallback callback = null);

        /// <summary>
        ///     Cancles an asynchronous file download.
        /// </summary>
        /// <param name="actionId">The ID of the upload.</param>
        void CancelDownloadFileAsync(string actionId);

        /// <summary>
        ///     Searches child nodes of <paramref name="parentNodeId"/> by their name.<para/>
        ///     Use <paramref name="parentNodeId"/> = 0 to search in all root nodes.
        /// </summary>
        /// <param name="searchString">The search string. (Search string must not null or empty)</param>
        /// <param name="parentNodeId">The ID of the parent node. (ID must be 0 or positive)</param>
        /// <param name="offset">The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <param name="filter">The filter for the request result. See also <seealso cref="Dracoon.Sdk.Filter.SearchNodesFilter"/></param>
        /// <param name="sort">The sort for the request result. See also <seealso cref="Dracoon.Sdk.Sort.SearchNodesSort"/></param>
        /// <returns>List of nodes. See also <seealso cref="Dracoon.Sdk.Model.NodeList"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        NodeList SearchNodes(string searchString, long parentNodeId = 0, long offset = 0, long limit = 500, SearchNodesFilter filter = null,
            SearchNodesSort sort = null);

        /// <summary>
        ///     Generates file keys for files with missing file keys.
        /// </summary>
        /// <param name="nodeId">The node ID for which the files keys should be generated.</param>
        /// <param name="limit">The maximum file keys which should be generated in one call.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void GenerateMissingFileKeys(long? nodeId = null, int limit = int.MaxValue);

        /// <summary>
        ///     Add a node the own favorites list.
        /// </summary>
        /// <param name="nodeId">The node which should be added to the favorites.</param>
        /// <returns>The updated node. See also <seealso cref="Dracoon.Sdk.Model.Node"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Node SetNodeAsFavorite(long nodeId);

        /// <summary>
        ///     Remove a node from the own favorites list.
        /// </summary>
        /// <param name="nodeId">The node which should be remove from the favorites.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DeleteNodeFromFavorites(long nodeId);

        /// <summary>
        ///     Retrieves the recycle bin for a given room.
        /// </summary>
        /// <param name="parentRoomId">The id of the room for which the recycle bin should be requested.</param>
        /// <param name="offset">The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <returns>List of nodes in the recycle bin. See also <seealso cref="Dracoon.Sdk.Model.RecycleBinItem"/></returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        RecycleBinItemList GetRecycleBinItems(long parentRoomId, long? offset = null, long? limit = null);

        /// <summary>
        ///     Clear the recycle bin for a given room completely.
        /// </summary>
        /// <param name="parentRoomId">The room id for which the recycle bin should be cleared.</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void EmptyRecycleBin(long parentRoomId);

        /// <summary>
        ///     Get the previous (old) versions of a requested node.
        /// </summary>
        /// <param name="parentId">The parent id of the requested node.</param>
        /// <param name="type">The type of the requested node.</param>
        /// <param name="nodeName">The name of the requested node.</param>
        /// <param name="offset">The range offset. (Zero-based index; must be 0 or positive if set)</param>
        /// <param name="limit">The range limit. (Number of returned records; must be positive if set)</param>
        /// <returns>The previous (old) versions of the requested node.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        PreviousVersionList GetPreviousVersions(long parentId, NodeType type, string nodeName, long? offset = null, long? limit = null);

        /// <summary>
        ///     Get a single previous (old) version of a node.
        /// </summary>
        /// <param name="previousNodeId">The id of the previous (old) version of the node.</param>
        /// <returns>The version info of the requrested previous (old) version.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        PreviousVersion GetPreviousVersion(long previousNodeId);

        /// <summary>
        ///     Restore previous (old) versions of a node.
        /// </summary>
        /// <param name="request">The request with the informations about the restore. See also <seealso cref="Dracoon.Sdk.Model.RestorePreviousVersionsRequest"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void RestorePreviousVersion(RestorePreviousVersionsRequest request);

        /// <summary>
        ///     Finally delete some previous (old) verisons of a node.
        /// </summary>
        /// <param name="request">The request with the informations about the deletion. See also <seealso cref="Dracoon.Sdk.Model.DeletePreviousVersionsRequest"/></param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DeletePreviousVersions(DeletePreviousVersionsRequest request);

        /// <summary>
        ///     Builds a media URL. The URL can be used to get a thumbnail or preview image for a node.
        /// </summary>
        /// <param name="mediaToken">The media token for the node.</param>
        /// <param name="width">The width of the image. (Must positive.)</param>
        /// <param name="height">The height of the image. (Must positive.)</param>
        /// <returns>The media URL.</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        Uri BuildMediaUrl(string mediaToken, int width, int height);

        /// <summary>
        ///     Generates a list of verdicts for a given file id list.
        /// </summary>
        /// <param name="fileIds">The list of the file ids.</param>
        /// <returns>A list of verdict informations for the give file ids.</returns>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        List<FileVirusProtectionInfo> GenerateVirusProtectionInfo(List<long> fileIds);

        /// <summary>
        ///     Permanently deletes a malicious file.
        /// </summary>
        /// <param name="fileId">The id of the file node which should be deleted</param>
        /// <exception cref="Dracoon.Sdk.Error.DracoonApiException"></exception>
        /// <exception cref="Dracoon.Sdk.Error.DracoonNetIOException"></exception>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        void DeleteMaliciousFile(long fileId);
    }
}