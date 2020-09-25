using System.Collections.Generic;
using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel.Requests;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class AttributeListComparer : IEqualityComparer<AttributeList> {
        public bool Equals(AttributeList x, AttributeList y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Offset == y.Offset &&
                   x.Limit == y.Limit &&
                   x.Total == y.Total &&
                   CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(AttributeList obj) {
            throw new System.NotImplementedException();
        }
    }

    internal class ApiAddOrUpdateAttributeRequestComparer : IEqualityComparer<ApiAddOrUpdateAttributeRequest> {
        public bool Equals(ApiAddOrUpdateAttributeRequest x, ApiAddOrUpdateAttributeRequest y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return CompareHelper.ListIsEqual(x.Items, y.Items);
        }

        public int GetHashCode(ApiAddOrUpdateAttributeRequest obj) {
            throw new System.NotImplementedException();
        }
    }


}