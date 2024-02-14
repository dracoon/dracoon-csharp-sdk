using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class RestRequestComparer : IEqualityComparer<RestRequest> {
        public bool Equals(RestRequest x, RestRequest y) {
            if (x == null && y == null) {
                return true;
            }

            if (x != null && y != null) {
                return CompareHelper.ListIsEqual(x.Parameters.ToList(), y.Parameters.ToList()) && x.Method == y.Method  &&
                       x.Timeout == y.Timeout && x.Resource == y.Resource;
            }

            return false;
        }

        public int GetHashCode(RestRequest obj) {
            throw new NotImplementedException();
        }
    }
}