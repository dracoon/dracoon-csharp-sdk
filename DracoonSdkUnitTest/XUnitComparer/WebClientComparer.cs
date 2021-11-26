using System;
using System.Collections.Generic;
using System.Net;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class WebClientComparer : IEqualityComparer<WebClient> {
        public bool Equals(WebClient x, WebClient y) {
            return CompareHelper.ListIsEqual(x.Headers, y.Headers);
        }

        public int GetHashCode(WebClient obj) {
            throw new NotImplementedException();
        }
    }
}
