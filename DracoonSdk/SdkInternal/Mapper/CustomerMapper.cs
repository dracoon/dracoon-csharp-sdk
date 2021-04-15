using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;

namespace Dracoon.Sdk.SdkInternal.Mapper {
    internal static class CustomerMapper {
        internal static CustomerAccount FromApiCustomerAccount(ApiCustomerAccount apiCustomerAccount) {
            if (apiCustomerAccount == null) {
                return null;
            }

            CustomerAccount customerAccount = new CustomerAccount {
                Id = apiCustomerAccount.Id,
                Name = apiCustomerAccount.Name,
                AccountsUsed = apiCustomerAccount.AccountsUsed,
                AccountsLimit = apiCustomerAccount.AccountsLimit,
                SpaceUsed = apiCustomerAccount.SpaceUsed,
                SpaceLimit = apiCustomerAccount.SpaceLimit,
                HasEncryptionEnabled = apiCustomerAccount.CustomerEncryptionEnabled
            };
            return customerAccount;
        }
    }
}