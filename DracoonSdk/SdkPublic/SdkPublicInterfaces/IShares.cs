using Dracoon.Sdk.Filter;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.Sort;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IO;
using System;

namespace Dracoon.Sdk {
    /// <summary>
    ///     Handler to maintain the shares.
    /// </summary>
    public interface IShares {

        /// <summary>
        ///     Creates a download share for a node.
        /// </summary>
        /// <param name = "request">
        ///     The request with the target node ID and the download share settings.See also <seealso cref = "CreateDownloadShareRequest"/>
        /// </param>
        /// <returns>
        ///     The download share. See also <seealso cref = "Dracoon.Sdk.Model.DownloadShare"/>
        /// </returns>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        /// <exception cref= "ArgumentNullException"/>
        DownloadShare CreateDownloadShare(CreateDownloadShareRequest request);

        /// <summary>
        ///     Retrieves the download shares which are created by the user.
        /// </summary>
        /// <param name="offset">
        ///     The range offset. (Zero-based index; must be 0 or positive if set)
        /// </param>
        /// <param name="limit">
        ///     The range limit. (Number of returned records; must be positive if set)
        /// </param>
        /// <param name="filter">
        ///     The filter for the request result. See also <seealso cref="Dracoon.Sdk.Filter.GetDownloadSharesFilter"/>
        /// </param>
        /// <param name="sort">
        ///     The sort for the request result. See also <seealso cref="Dracoon.Sdk.Sort.SharesSort"/>
        /// </param>
        /// <returns>
        ///     The result list of download shares. See also <seealso cref="DownloadShareList"/>
        /// </returns>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        DownloadShareList GetDownloadShares(long? offset = null, long? limit = null, GetDownloadSharesFilter filter = null, SharesSort sort = null);

        /// <summary>
        ///     Delete a download shares.
        /// </summary>
        /// <param name="shareId">
        ///     The ID of the download share which should be deleted.
        /// </param>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        void DeleteDownloadShare(long shareId);

        /// <summary>
        ///     Creates a upload share for a node.
        /// </summary>
        /// <param name="request">
        ///     The request with the target node ID and the upload share settings. See also <seealso cref="CreateUploadShareRequest"/>
        /// </param>
        /// <returns>
        ///     The upload share. See also <seealso cref="Dracoon.Sdk.Model.UploadShare"/>
        /// </returns>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        /// <exception cref= "ArgumentNullException"/>
        UploadShare CreateUploadShare(CreateUploadShareRequest request);

        /// <summary>
        ///     Retrieves the upload shares which are created by the user.
        /// </summary>
        /// <param name="offset">
        ///     The range offset. (Zero-based index; must be 0 or positive if set)
        /// </param>
        /// <param name="limit">
        ///     The range limit. (Number of returned records; must be positive if set)
        /// </param>
        /// <param name="filter">
        ///     The filter for the request result. See also <seealso cref="GetUploadSharesFilter"/>
        /// </param>
        /// <param name="sort">
        ///     The sort for the request result. See also <seealso cref="SharesSort"/>
        /// </param>
        /// <returns>
        ///     The result list of upload shares. See also <seealso cref="UploadShareList"/>
        /// </returns>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        UploadShareList GetUploadShares(long? offset = null, long? limit = null, GetUploadSharesFilter filter = null, SharesSort sort = null);

        /// <summary>
        ///     Delete a upload share.
        /// </summary>
        /// <param name="shareId">
        ///     The ID of the upload share which should be deleted.
        /// </param>
        /// <exception cref= "Error.DracoonApiException"/>
        /// <exception cref= "Error.DracoonNetIOException"/>
        /// <exception cref= "ArgumentException"/>
        void DeleteUploadShare(long shareId);

        /// <summary>
        ///     Send an email to specific recipients for existing download share.
        /// </summary>
        /// <param name="request">
        ///     The request with the target share id and the mail settings. See also <seealso cref="MailShareInfoRequest"/>
        /// </param>
        void SendMailForDownloadShare(MailShareInfoRequest request);

        /// <summary>
        ///     Send an email to specific recipients for existing upload share.
        /// </summary>
        /// <param name="request">
        ///     The request with the target share id and the mail settings. See also <seealso cref="MailShareInfoRequest"/>
        /// </param>
        void SendMailForUploadShare(MailShareInfoRequest request);
    }
}