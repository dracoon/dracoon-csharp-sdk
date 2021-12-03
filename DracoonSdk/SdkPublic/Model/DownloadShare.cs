using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about a download share.
    /// </summary>
    public class DownloadShare {
        /// <summary>
        ///     The ID of this download share.
        /// </summary>
        public long ShareId { get; internal set; }

        /// <summary>
        ///     The ID of the referenced node.
        /// </summary>
        public long NodeId { get; internal set; }

        /// <summary>
        ///     The path of the referenced node.
        /// </summary>
        public string NodePath { get; internal set; }

        /// <summary>
        ///     The key is used to build the download share uri.
        /// </summary>
        public string AccessKey { get; internal set; }

        /// <summary>
        ///     The current number of using this download share.
        /// </summary>
        public int CurrentDownloadsCount { get; internal set; }

        /// <summary>
        ///     The creation date of this download share.
        /// </summary>
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        ///     The user which created this download share.
        /// </summary>
        public UserInfo CreatedBy { get; internal set; }

        /// <summary>
        ///     The name of this download share.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     The notes for this download share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Notes { get; internal set; }

        /// <summary>
        ///     The internal notes for this download share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string InternalNotes { get; internal set; }

        /// <summary>
        ///     Indicates if the name of the creator is shown by using this download share.
        /// </summary>
        public bool ShowCreatorName { get; internal set; }

        /// <summary>
        ///     Indicates if the user name of the creator is shown by using this download share.
        /// </summary>
        public bool ShowCreatorUserName { get; internal set; }

        /// <summary>
        ///     Indicates if this download share is protected by a password.
        /// </summary>
        public bool IsProtected { get; internal set; }

        /// <summary>
        ///     The expiration date of this download share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public DateTime? ExpireAt { get; internal set; }

        /// <summary>
        ///     The maximum usage number for this download share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public int? MaxAllowedDownloads { get; internal set; }

        /// <summary>
        ///     Indicates if the referenced node is encrypted.
        ///     <para>
        ///         Nullable if <see cref="Type"/> is <see cref="NodeType.Folder"/>
        ///     </para>
        /// </summary>
        public bool? IsEncrypted { get; internal set; }

        /// <summary>
        ///     Indicates type of the shared node.
        /// </summary>
        public NodeType Type { get; internal set; }

        /// <summary>
        ///     Download share url.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string DataUrl { get; internal set; }

        /// <summary>
        ///     The last datetime on which this shares meta datas were changed.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        ///     The last user who changed this shares meta data. See also <seealso cref="Dracoon.Sdk.Model.UserInfo"/>
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }
    }
}