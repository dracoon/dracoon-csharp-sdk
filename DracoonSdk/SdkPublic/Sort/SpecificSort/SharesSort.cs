namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/SharesSort/*'/>
    public class SharesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/CreatedAt/*'/>
        public static SortField<SharesSort> CreatedAt {
            get {
                return new SortField<SharesSort>(new SharesSort(), "createdAt");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/ExpireAt/*'/>
        public static SortField<SharesSort> ExpireAt {
            get {
                return new SortField<SharesSort>(new SharesSort(), "expireAt");
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/Name/*'/>
        public static SortField<SharesSort> Name {
            get {
                return new SortField<SharesSort>(new SharesSort(), "name");
            }
        }
    }
}