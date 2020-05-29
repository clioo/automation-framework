using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface ITestShippingConfigurationFactory
    {
        Task CreateConfiguration(TestShippingConfiguration configuration);
        Task AddAccountPreferences(TestShippingExternals externalIds, TestShippingPreferences preferences);
        Task AddTenantPreferences(TestShippingExternals externalIds, TestShippingPreferences preferences);
        Task RemoveTenantPreferences(TestShippingExternals identifiers);
        Task RemoveAccountPreferences(TestShippingExternals identifiers);
    }
}
