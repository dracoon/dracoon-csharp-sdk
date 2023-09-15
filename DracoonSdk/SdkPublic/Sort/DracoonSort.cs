namespace Dracoon.Sdk.Sort {
    /// <summary>
    ///     Super class for all specific sorts.
    /// </summary>
    public class DracoonSort {
        internal string SortString;

        /// <summary>
        ///     Builds the string which contains the sort definition like "updatedAt:desc".
        /// </summary>
        /// <returns>The sort string.</returns>
        public override string ToString() {
            return SortString;
        }
    }
}