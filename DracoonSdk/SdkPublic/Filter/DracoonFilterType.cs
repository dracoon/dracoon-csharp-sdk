using System;

namespace Dracoon.Sdk.Filter {

    /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilterType"]/DracoonFilterType/*'/>
    public class DracoonFilterType<T> {

        internal string filterTypeString = "";
        internal bool operatorSet = false;

        private void AddOperator(string op) {
            if (!operatorSet) {
                filterTypeString += ":" + op;
                operatorSet = true;
            }
        }

        private void AddValue(dynamic value) {
            filterTypeString += ":" + value.ToString().ToLower();
        }

        /// <include file="FilterDoc.xml" path='docs/members[@name="dracoonFilterType"]/ToString/*'/>
        public override string ToString() {
            return filterTypeString;
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
