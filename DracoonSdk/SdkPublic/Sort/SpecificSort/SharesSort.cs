namespace Dracoon.Sdk.Sort {
    /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/SharesSort/*'/>
    public class SharesSort : DracoonSort {
        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/CreatedAt/*'/>
        public static CreatedAtSort<SharesSort> CreatedAt {
            get {
                return new CreatedAtSort<SharesSort>(new SharesSort());
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/ExpireAt/*'/>
        public static ExpireAtSort<SharesSort> ExpireAt {
            get {
                return new ExpireAtSort<SharesSort>(new SharesSort());
            }
        }

        /// <include file = "SpecificSort.xml" path='docs/members[@name="sharesSort"]/Name/*'/>
        public static NameSort<SharesSort> Name {
            get {
                return new NameSort<SharesSort>(new SharesSort());
            }
        }
    }
}