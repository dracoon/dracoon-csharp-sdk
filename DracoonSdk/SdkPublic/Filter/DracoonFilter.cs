using System;
using System.Collections.Generic;
using System.Text;

namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     Super class for all specific filters which holds the single added filters and checks if they are only added once.
    /// </summary>
    public class DracoonFilter {

        internal List<dynamic> FiltersList = new List<dynamic>();

        internal void CheckFilter(object filter, string filterParamName) {
            if (filter == null) {
                throw new ArgumentNullException(filterParamName);
            }

            foreach (object currentFilter in FiltersList) {
                if (currentFilter.GetType() == filter.GetType()) {
                    throw new ArgumentException("Filter already set.");
                }
            }
        }

        /// <summary>
        ///     Builds the string which contains all filters separated with '|'.
        /// </summary>
        /// <returns>The string with all single filter strings.</returns>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (object currentFilter in FiltersList) {
                if (sb.Length != 0) {
                    sb.Append("|");
                }
                sb.Append(currentFilter.ToString());
            }
            return sb.ToString();
        }
    }
}
