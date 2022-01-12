namespace Dracoon.Sdk.Sort {
    /// <summary>
    ///     This class provides filters for <see cref="Dracoon.Sdk.IShares.GetDownloadShares(long?, long?, Filter.GetDownloadSharesFilter, SharesSort)"/> or <see cref="Dracoon.Sdk.IShares.GetUploadShares(long?, long?, Filter.GetUploadSharesFilter, SharesSort)"/>.
    /// </summary>
    public class SharesSort : DracoonSort {

        /// <summary>
        ///     Gets a new sort for the field 'CreatedAt' of a <see cref="Dracoon.Sdk.Model.DownloadShare"/> or <see cref="Dracoon.Sdk.Model.UploadShare"/>.
        /// </summary>
        public static SortField<SharesSort> CreatedAt => new SortField<SharesSort>(new SharesSort(), "createdAt");

        /// <summary>
        ///     Gets a new sort for the field 'Name' of a <see cref="Dracoon.Sdk.Model.DownloadShare"/> or <see cref="Dracoon.Sdk.Model.UploadShare"/>.
        /// </summary>
        public static SortField<SharesSort> ExpireAt => new SortField<SharesSort>(new SharesSort(), "expireAt");

        /// <summary>
        ///     Gets a new sort for the field 'ExpireAt' of a <see cref="Dracoon.Sdk.Model.DownloadShare"/> or <see cref="Dracoon.Sdk.Model.UploadShare"/>.
        /// </summary>
        public static SortField<SharesSort> Name => new SortField<SharesSort>(new SharesSort(), "name");
    }
}