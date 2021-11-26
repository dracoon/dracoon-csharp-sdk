using Dracoon.Sdk.Model;
using Dracoon.Sdk.SdkInternal;
using Dracoon.Sdk.SdkInternal.ApiModel;
using Dracoon.Sdk.SdkInternal.Mapper;
using Dracoon.Sdk.UnitTest.Factory;
using Dracoon.Sdk.UnitTest.XUnitComparer;
using RestSharp;
using Telerik.JustMock;
using Xunit;
using static Dracoon.Sdk.SdkInternal.DracoonRequestExecutor;

namespace Dracoon.Sdk.UnitTest.Test.PublicInterfaceImpl {
    public class DracoonServerPoliciesImplTest {
        #region GetEncryptionPasswordPolicies

        [Fact]
        public void GetEncryptionPasswordPolicies() {
            // ARRANGE
            PasswordEncryptionPolicies expected = FactoryPolicies.PasswordEncryptionPolicies;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerPoliciesImpl ss = new DracoonServerPoliciesImpl(c);
            Mock.Arrange(() => c.Builder.GetPasswordPolicies()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPasswordPolicies, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiPasswordSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetPasswordPolicies, 0)).Returns(FactoryPolicies.ApiPasswordSettings).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiPasswordEncryptionPolicies(Arg.IsAny<ApiEncryptionPasswordSettings>())).Returns(FactoryPolicies.PasswordEncryptionPolicies).Occurs(1);

            // ACT
            PasswordEncryptionPolicies actual = ss.GetEncryptionPasswordPolicies();

            // ASSERT
            Assert.Equal(expected, actual, new PasswordEncryptionPolicyComparer());
            Mock.Assert(() => SettingsMapper.FromApiPasswordEncryptionPolicies(Arg.IsAny<ApiEncryptionPasswordSettings>()));
            Mock.Assert(c.Executor);
            Mock.Assert(c.Builder);
        }

        #endregion

        #region GetSharesPasswordPolicies

        [Fact]
        public void GetSharesPasswordPolicies() {
            // ARRANGE
            PasswordSharePolicies expected = FactoryPolicies.PasswordSharePolicies;
            IInternalDracoonClient c = FactoryClients.InternalDracoonClientMock(true);
            DracoonServerPoliciesImpl ss = new DracoonServerPoliciesImpl(c);
            Mock.Arrange(() => c.Builder.GetPasswordPolicies()).Returns(FactoryRestSharp.RestRequestWithAuth(ApiConfig.ApiGetPasswordPolicies, Method.GET)).Occurs(1);
            Mock.Arrange(() => c.Executor.DoSyncApiCall<ApiPasswordSettings>(Arg.IsAny<IRestRequest>(), RequestType.GetPasswordPolicies, 0)).Returns(FactoryPolicies.ApiPasswordSettings).Occurs(1);
            Mock.Arrange(() => SettingsMapper.FromApiPasswordSharePolicies(Arg.IsAny<ApiSharePasswordSettings>())).Returns(FactoryPolicies.PasswordSharePolicies).Occurs(1);

            // ACT
            PasswordSharePolicies actual = ss.GetSharesPasswordPolicies();

            // ASSERT
            Assert.Equal(expected, actual, new PasswordSharePolicyComparer());
            Mock.Assert(() => SettingsMapper.FromApiPasswordSharePolicies(Arg.IsAny<ApiSharePasswordSettings>()));
            Mock.Assert(c.Executor);
            Mock.Assert(c.Builder);
        }

        #endregion
    }
}
