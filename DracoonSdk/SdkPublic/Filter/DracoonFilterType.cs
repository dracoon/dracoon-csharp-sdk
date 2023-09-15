using System;

namespace Dracoon.Sdk.Filter {
    /// <summary>
    ///     Class which holds/builds a single specific filter.
    /// </summary>
    /// <typeparam name="T">The specific filter class definition like <see cref="Dracoon.Sdk.Filter.NodeTypeFilter"/>.</typeparam>
    public class DracoonFilterType<T> {
        internal string FilterName = "";
        internal string FilterTypeString = "";
        internal bool OperatorSet;

        private void AddOperator(string op) {
            if (!OperatorSet) {
                FilterTypeString += ":" + op;
                OperatorSet = true;
            }
        }

        private void AddValue(object value) {
            if (value is DateTime dt) {
                FilterTypeString += ":" + dt.ToString("o");
            } else {
                FilterTypeString += ":" + value.ToString().ToLower();
            }
        }

        /// <summary>
        ///     Builds the string which contains a single filter like "type:eq:room".
        /// </summary>
        /// <returns>The single filter string.</returns>
        public override string ToString() {
            return FilterTypeString;
        }

        internal void AddOperatorAndValue(object value, string filterOperator, string filterParamName) {
            if (value == null) {
                throw new ArgumentNullException(filterParamName);
            }

            AddOperator(filterOperator);
            AddValue(value);
        }

        internal void AddAnd() {
            FilterTypeString += "|" + FilterName;
            OperatorSet = false;
        }
    }
}