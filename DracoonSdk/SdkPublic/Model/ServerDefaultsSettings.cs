namespace Dracoon.Sdk.Model {
    /// <summary>
    ///     This model stores informations about the server defaults.
    /// </summary>
    public class ServerDefaultSettings {

        /// <summary>
        ///     The default language of the server.
        /// </summary>
        public string LanguageDefault { get; internal set; }

        /// <summary>
        ///     The server side default for the expiration period of a new download share.
        /// </summary>
        public int DownloadShareDefaultExpirationPeriodInDays { get; internal set; }

        /// <summary>
        ///     The server side default for the expiration period of a new upload share.
        /// </summary>
        public int UploadShareDefaultExpirationPeriodInDays { get; internal set; }

        /// <summary>
        ///     The server side default for the expiration of a new uploaded file.
        /// </summary>
        public int FileUploadDefaultExpirationPeriodInDays { get; internal set; }

        /// <summary>
        ///     Defines if new users get the role Non Member Viewer by default.
        /// </summary>
        public bool NonMemberViewerDefault { get; internal set; }

        /// <summary>
        ///     Defines if login fields should be hidden.
        /// </summary>
        public bool HideLoginInputFields { get; internal set; }
    }
}