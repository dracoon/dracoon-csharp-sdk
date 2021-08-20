using System;
using System.Collections.Generic;
using System.Text;

namespace Dracoon.Sdk.Filter {
    /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilter"]/DracoonFilter/*'/>
    public class DracoonFilter {

        internal List<dynamic> FiltersList = new List<dynamic>();

        internal void CheckFilter(dynamic filter, string filterParamName) {
            if (filter == null) {
                throw new ArgumentNullException(filterParamName);
            }

            foreach (dynamic currentFilter in FiltersList) {
                if (currentFilter.GetType() == filter.GetType()) {
                    throw new ArgumentException("Filter already set.");
                }
            }
        }

        /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilter"]/ToString/*'/>
        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (dynamic currentFilter in FiltersList) {
                if (sb.Length != 0) {
                    sb.Append("|");
                }
                sb.Append(currentFilter.ToString());
            }
            return sb.ToString();
        }
    }
}
