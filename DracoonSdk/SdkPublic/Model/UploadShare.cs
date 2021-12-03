using System;

namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about a upload share.
    /// </summary>
    public class UploadShare {
        /// <summary>
        ///     The ID of this upload share.
        /// </summary>
        public long ShareId { get; internal set; }

        /// <summary>
        ///     The ID of the referenced node.
        /// </summary>
        public long NodeId { get; internal set; }

        /// <summary>
        ///     The name of this upload share.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     Indicates if this upload share is protected by a password.
        /// </summary>
        public bool IsProtected { get; internal set; }

        /// <summary>
        ///     The key is used to build the upload share uri.
        /// </summary>
        public string AccessKey { get; internal set; }

        /// <summary>
        ///     The creation date of this upload share.
        /// </summary>
        public DateTime CreatedAt { get; internal set; }

        /// <summary>
        ///     The user which created this upload share. See also <seealso cref="Dracoon.Sdk.Model.UserInfo"/>
        /// </summary>
        public UserInfo CreatedBy { get; internal set; }

        /// <summary>
        ///     The expiration date of this upload share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public DateTime? ExpireAt { get; internal set; }

        /// <summary>
        ///     The path of the node which is referenced by the upload share.
        /// </summary>
        public string NodePath { get; internal set; }

        /// <summary>
        ///     Indicates if the referenced node is encrypted.
        /// </summary>
        public bool IsEncrypted { get; internal set; }

        /// <summary>
        ///     The notes for this upload share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string Notes { get; internal set; }

        /// <summary>
        ///     The internal notes for this upload share.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string InternalNotes { get; internal set; }

        /// <summary>
        ///     The days after which every uploaded file (over this upload share) expires after upload date.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public int? UploadedFilesExpirationPeriod { get; internal set; }

        /// <summary>
        ///     The number of currently uploaded files over this upload share (which are still uploaded).
        /// </summary>
        public int CurrentDoneUploadsCount { get; internal set; }

        /// <summary>
        ///     The total number of uploaded files (included files which were removed later).
        /// </summary>
        public int CurrentUploadedFilesCount { get; internal set; }

        /// <summary>
        ///     If <c>true</c> the still uploaded files are publicly visible on upload share page. Otherwise <c>false</c>.
        /// </summary>
        public bool ShowUploadedFiles { get; internal set; }

        /// <summary>
        ///     The maximum number of total uploaded files.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public int? MaxAllowedUploads { get; internal set; }

        /// <summary>
        ///     The maximum size in bytes which can be uploaded over this upload share (over all uploaded files added size).
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public long? MaxAllowedTotalSizeOverAllUploadedFiles { get; internal set; }

        /// <summary>
        ///     The type of the node which this share references to.
        /// </summary>
        public NodeType Type { get; internal set; }

        /// <summary>
        ///     Upload share url.
        ///     <para>
        ///         Nullable
        ///     </para>
        /// </summary>
        public string DataUrl { get; internal set; }

        /// <summary>
        ///     The last datetime on which this shares meta datas were changed.
        ///     <para>
        ///         Nullable if never updated yet.
        ///     </para>
        /// </summary>
        public DateTime? UpdatedAt { get; internal set; }

        /// <summary>
        ///     The last user who changed this shares meta data. See also <seealso cref="Dracoon.Sdk.Model.UserInfo"/>
        ///     <para>
        ///         Nullable if never updated yet.
        ///     </para>
        /// </summary>
        public UserInfo UpdatedBy { get; internal set; }

        /// <summary>
        ///     Show creator fist and last name.
        /// </summary>
        public bool ShowCreatorName { get; internal set; }

        /// <summary>
        ///     Show creator email address.
        /// </summary>
        public bool ShowCreatorUsername { get; internal set; }
    }
}