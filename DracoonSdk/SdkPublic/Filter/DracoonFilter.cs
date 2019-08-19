using System;
using System.Collections.Generic;

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
            string filterEndString = "";
            foreach (dynamic currentFilter in FiltersList) {
                if (filterEndString.Length != 0) {
                    filterEndString += "|";
                }
                filterEndString += currentFilter.ToString();
            }
            return filterEndString;
        }
    }
}
