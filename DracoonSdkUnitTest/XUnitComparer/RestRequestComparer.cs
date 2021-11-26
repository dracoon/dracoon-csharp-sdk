using RestSharp;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class RestRequestComparer : IEqualityComparer<IRestRequest> {
        public bool Equals(IRestRequest x, IRestRequest y) {
            if (x == null && y == null) {
                return true;
            }

            if (x != null && y != null) {
                return CompareHelper.ListIsEqual(x.Parameters, y.Parameters) && x.Method == y.Method && x.ReadWriteTimeout == y.ReadWriteTimeout &&
                       x.Timeout == y.Timeout && x.Resource == y.Resource;
            }

            return false;
        }

        public int GetHashCode(IRestRequest obj) {
            throw new NotImplementedException();
        }
    }
}