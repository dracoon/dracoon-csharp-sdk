namespace Dracoon.Sdk.Sort {

    /// <summary>
    ///     Sort for a given field.
    /// </summary>
    /// <typeparam name="T">Is the specific definition for a request like <see cref="Dracoon.Sdk.Sort.SharesSort"/></typeparam>
    public class SortField<T> : DracoonSortOrder<T> where T : DracoonSort {

        /// <summary>
        ///     Constructs a new sort for the given field.
        /// </summary>
        /// <param name="p">The parent instance like <see cref="Dracoon.Sdk.Sort.SharesSort"/></param>
        /// <param name="sortField">The field name which should be used for the sort. E.g. 'createdAt' or 'updatedAt'.</param>
        public SortField(T p, string sortField) : base(p) {
            Parent.SortString += sortField;
        }
    }
}