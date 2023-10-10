using System;

namespace Dracoon.Sdk {
    /// <summary>
    ///     ILog is the interface for custom logger implementations of the DRACOON SDK.
    ///     <para>
    ///         A custom logger can be set via the DracoonClient constructor(<see cref = "Dracoon.Sdk.DracoonClient.DracoonClient(Uri, DracoonAuth, string, ILog, DracoonHttpConfig)" />).
    ///     </para>
    /// </summary>
    public interface ILog {
        /// <summary>
        ///     Writes a DEBUG log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        void Debug(string tag, string message);

        /// <summary>
        ///     Writes a DEBUG log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        /// <param name="e">An exception cause for this log.</param>
        void Debug(string tag, string message, Exception e);

        /// <summary>
        ///     Writes a INFO log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        void Info(string tag, string message);

        /// <summary>
        ///     Writes a INFO log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        /// <param name="e">An exception cause for this log.</param>
        void Info(string tag, string message, Exception e);

        /// <summary>
        ///     Writes a WARN log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        void Warn(string tag, string message);

        /// <summary>
        ///     Writes a WARN log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        /// <param name="e">An exception cause for this log.</param>
        void Warn(string tag, string message, Exception e);

        /// <summary>
        ///     Writes a Error log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        void Error(string tag, string message);

        /// <summary>
        ///     Writes a Error log message.
        /// </summary>
        /// <param name="tag">Used to identify the source of the log message (Class name).</param>
        /// <param name="message">The message you can log.</param>
        /// <param name="e">An exception cause for this log.</param>
        void Error(string tag, string message, Exception e);
    }
}