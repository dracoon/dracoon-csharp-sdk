using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using Xunit;

namespace Dracoon.Sdk.UnitTest.Test.Mapper {
    public class CustomerMapperTest {
        #region FromApiCustomerAccount

        [Fact]
        public void FromApiCustomerAccount() {
            // ARRANGE
            CustomerAccount expected = FactoryCustomer.CustomerAccount;

            ApiCustomerAccount param = new ApiCustomerAccount {
                Id = expected.Id,
                Name = expected.Name,
                AccountsUsed = expected.AccountsUsed,
                AccountsLimit = expected.AccountsLimit,
                SpaceUsed = expected.SpaceUsed,
                SpaceLimit = expected.SpaceLimit,
                CountRooms = expected.CountRooms,
                CountFolders = expected.CountFolders,
                CountFiles = expected.CountFiles,
                CustomerEncryptionEnabled = expected.HasEncryptionEnabled
            };

            // ACT
            CustomerAccount actual = CustomerMapper.FromApiCustomerAccount(param);

            // ASSERT
            Assert.Equal(expected, actual, new CustomerAccountComparer());
        }

        [Fact]
        public void FromApiCustomerAccount_Null() {
            // ARRANGE
            CustomerAccount expected = null;
            ApiCustomerAccount param = null;

            // ACT
            CustomerAccount actual = CustomerMapper.FromApiCustomerAccount(param);

            // ASSERT
            Assert.Equal(expected, actual, new CustomerAccountComparer());
        }

        #endregion
    }
}