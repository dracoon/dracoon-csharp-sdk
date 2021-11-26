namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/SharesSort/*'/>
    public class SharesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/CreatedAt/*'/>
        public static SortField<SharesSort> CreatedAt => new SortField<SharesSort>(new SharesSort(), "createdAt");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/ExpireAt/*'/>
        public static SortField<SharesSort> ExpireAt => new SortField<SharesSort>(new SharesSort(), "expireAt");

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/Name/*'/>
        public static SortField<SharesSort> Name => new SortField<SharesSort>(new SharesSort(), "name");
    }
}