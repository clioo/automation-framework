using HttpUtility.Clients;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface IShippingConfigurationFactory
    {
        void CreateAccountConfiguration(TestShippingPreferencesConfiguration configuration, string accountMasterId);
        void CreateTenantConfiguration(TestShippingPreferencesConfiguration configuration);
        void RemoveAccountConfiguration(TestShippingPreferencesConfiguration configuration, string accountMasterId);
        void RemoveTenantConfiguration(TestShippingPreferencesConfiguration configuration);
        void Authenticate(TestShippingAuthCredential authCredentials);
    }
}
