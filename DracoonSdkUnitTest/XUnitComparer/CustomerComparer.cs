using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using System;
using System.Collections.Generic;

namespace Dracoon.Sdk.UnitTest.XUnitComparer {
    internal class CustomerAccountComparer : IEqualityComparer<CustomerAccount> {
        public bool Equals(CustomerAccount x, CustomerAccount y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Id == y.Id &&
                string.Equals(x.Name, y.Name) &&
                x.AccountsUsed == y.AccountsUsed &&
                x.AccountsLimit == y.AccountsLimit &&
                x.SpaceUsed == y.SpaceUsed &&
                x.SpaceLimit == y.SpaceLimit &&
                x.HasEncryptionEnabled == y.HasEncryptionEnabled;
        }

        public int GetHashCode(CustomerAccount obj) {
            throw new NotImplementedException();
        }
    }

    internal class ApiCustomerAccountComparer : IEqualityComparer<ApiCustomerAccount> {
        public bool Equals(ApiCustomerAccount x, ApiCustomerAccount y) {
            if (x == null && y == null) {
                return true;
            }
            if ((x == null && y != null) || (x != null && y == null)) {
                return false;
            }
            return x.Id == y.Id &&
                string.Equals(x.Name, y.Name) &&
                x.AccountsUsed == y.AccountsUsed &&
                x.AccountsLimit == y.AccountsLimit &&
                x.SpaceUsed == y.SpaceUsed &&
                x.SpaceLimit == y.SpaceLimit &&
                x.CustomerEncryptionEnabled == y.CustomerEncryptionEnabled &&
                x.IsProviderCustomer == y.IsProviderCustomer;
        }

        public int GetHashCode(ApiCustomerAccount obj) {
            throw new NotImplementedException();
        }
    }
}
