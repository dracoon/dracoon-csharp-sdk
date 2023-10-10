using Dracoon.Sdk.Error;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     A listener for receiving file download progress events.
    /// </summary>
    public interface IFileDownloadCallback {

        /// <summary>
        ///     This methods gets called when a download was started.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the download.See also <seealso cref="INodes.DownloadFile(string, long, System.IO.Stream, IFileDownloadCallback)"/> or <seealso cref = "INodes.StartDownloadFileAsync(string, long, System.IO.Stream, IFileDownloadCallback)"/>
        /// </param>
        void OnStarted(string actionId);

        /// <summary>
        ///     This methods gets called at every progress update(every 250ms).
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the download.See also <seealso cref="INodes.DownloadFile(string, long, System.IO.Stream, IFileDownloadCallback)"/> or <seealso cref = "INodes.StartDownloadFileAsync(string, long, System.IO.Stream, IFileDownloadCallback)"/>
        /// </param>
        /// <param name = "bytesDownloaded">
        ///     The number of bytes which have been read.
        /// </param>
        /// <param name = "bytesTotal">
        ///     The total number of bytes.
        /// </param>
        void OnRunning(string actionId, long bytesDownloaded, long bytesTotal);

        /// <summary>
        ///     This method gets called when a download was finished.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the download.See also <seealso cref="INodes.DownloadFile(string, long, System.IO.Stream, IFileDownloadCallback)"/> or <seealso cref = "INodes.StartDownloadFileAsync(string, long, System.IO.Stream, IFileDownloadCallback)"/>
        /// </param>
        void OnFinished(string actionId);

        /// <summary>
        ///     This method gets called when a download was canceled.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the download.See also <seealso cref="INodes.DownloadFile(string, long, System.IO.Stream, IFileDownloadCallback)"/> or <seealso cref = "INodes.StartDownloadFileAsync(string, long, System.IO.Stream, IFileDownloadCallback)"/>
        /// </param>
        void OnCanceled(string actionId);

        /// <summary>
        ///     This method gets called when a download failed.
        /// </summary>
        /// <param name = "actionId">
        ///     The id of the download. See also <seealso cref = "INodes.DownloadFile(string, long, System.IO.Stream, IFileDownloadCallback)"/> or <seealso cref= "INodes.StartDownloadFileAsync(string, long, System.IO.Stream, IFileDownloadCallback)"/>
        /// </param>
        /// <param name = "occuredError">
        ///     The cause for the error.
        /// </param>
        void OnFailed(string actionId, DracoonException occuredError);
    }
}