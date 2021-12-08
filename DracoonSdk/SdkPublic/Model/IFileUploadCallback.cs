using Dracoon.Sdk.Error;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     A listener for receiving file upload progress events.
    /// </summary>
    public interface IFileUploadCallback {

        /// <summary>
        ///     This methods gets called when a upload was started.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the upload.See also<seealso cref="INodes.UploadFile(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/> or <seealso cref = "INodes.StartUploadFileAsync(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/>
        /// </param>
        void OnStarted(string actionId);

        /// <summary>
        ///     This methods gets called at every progress update(every 250ms).
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the upload.See also <seealso cref="INodes.UploadFile(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/> or <seealso cref = "INodes.StartUploadFileAsync(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/>
        /// </param>
        /// <param name = "bytesUploaded">
        ///     The number of bytes which have been written.
        /// </param>
        /// <param name = "bytesTotal">
        ///     The total number of bytes.
        /// </param>
        void OnRunning(string actionId, long bytesUploaded, long bytesTotal);

        /// <summary>
        ///     This method gets called when a upload was finished.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the upload.See also <seealso cref="INodes.UploadFile(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/> or <seealso cref = "INodes.StartUploadFileAsync(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/>
        /// </param>
        /// <param name = "resultNode">
        ///     The result node.
        /// </param>
        void OnFinished(string actionId, Node resultNode);

        /// <summary>
        ///     This method gets called when a upload was canceled.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the upload.See also <seealso cref="INodes.UploadFile(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/> or <seealso cref = "INodes.StartUploadFileAsync(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/>
        /// </param>
        void OnCanceled(string actionId);

        /// <summary>
        ///     This method gets called when a upload failed.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the upload. See also <seealso cref = "INodes.UploadFile(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/> or <seealso cref= "INodes.StartUploadFileAsync(string, FileUploadRequest, System.IO.Stream, long, IFileUploadCallback)"/>
        /// </param>
        /// <param name= "occuredError">
        ///     The cause for the error.
        /// </param>
        void OnFailed(string actionId, DracoonException occuredError);
    }
}