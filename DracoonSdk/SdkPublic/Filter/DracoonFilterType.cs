using System;

namespace Dracoon.Sdk.Filter {
    /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilterType"]/DracoonFilterType/*'/>
    public class DracoonFilterType<T> {
        internal string FilterTypeString = "";
        internal bool OperatorSet;

        private void AddOperator(string op) {
            if (!OperatorSet) {
                FilterTypeString += ":" + op;
                OperatorSet = true;
            }
        }

        private void AddValue(dynamic value) {
            FilterTypeString += ":" + value.ToString().ToLower();
        }

        /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilterType"]/ToString/*'/>
        public override string ToString() {
            return FilterTypeString;
        }

        internal void AddOperatorAndValue(dynamic value, string filterOperator, string filterParamName) {
            if (value == null) {
                throw new ArgumentNullException(filterParamName);
            }

            AddOperator(filterOperator);
            AddValue(value);
        }
    }
}