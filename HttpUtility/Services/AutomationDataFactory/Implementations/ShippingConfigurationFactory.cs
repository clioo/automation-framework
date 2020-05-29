using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class ShippingConfigurationFactory : IShippingConfigurationFactory
    {
        readonly ShippingServiceProcessor _processor;

        public ShippingConfigurationFactory(ShippingServiceProcessor processor)
        {
            _processor = processor;
        }

        public ShippingConfigurationFactory(ShippingServiceProcessor processor, TestShippingAuthCredential authCredentials)
        {
            _processor = processor;
            Authenticate(authCredentials);
        }

        public void CreateAccountConfiguration(TestShippingPreferencesConfiguration configuration, string accountMasterId)
        {
            if (string.IsNullOrEmpty(configuration.Identifier) || string.IsNullOrWhiteSpace(configuration.Identifier))
            {
                throw new System.Exception($"{nameof(configuration.Identifier)} is mandatory");
            }

            //remove existing test data before create new
            RemoveAccountConfiguration(configuration, accountMasterId);

            _processor.CreateConfiguration(configuration.Identifier, configuration.Configuration).Wait();
            _processor.CreateAccountPreferences(configuration.Identifier, accountMasterId, configuration.Preferences).Wait();
            _processor.AddFlatRateSchedules(configuration.Identifier, configuration.FlatRateSchedules).Wait();
            _processor.AddHandlingSchedules(configuration.Identifier, configuration.HandlingSchedules).Wait();
        }

        public void CreateTenantConfiguration(TestShippingPreferencesConfiguration configuration)
        {
            //remove existing test data before create new
            RemoveTenantConfiguration(configuration);

            _processor.CreateConfiguration(configuration.Identifier, configuration.Configuration).Wait();
            _processor.CreateTenantPreferences(configuration.Identifier, configuration.Preferences).Wait();
            _processor.AddFlatRateSchedules(configuration.Identifier, configuration.FlatRateSchedules).Wait();
            _processor.AddHandlingSchedules(configuration.Identifier, configuration.HandlingSchedules).Wait();
        }

        public void RemoveAccountConfiguration(TestShippingPreferencesConfiguration configuration, string accountMasterId)
        {
            _processor.RemoveAccountPreferences(accountMasterId).Wait();
            _processor.RemoveFlatRates(configuration.Identifier, configuration.FlatRateSchedules).Wait();
            _processor.RemoveHandlingRates(configuration.Identifier, configuration.HandlingSchedules).Wait();
            _processor.RemoveConfiguration(configuration.Identifier).Wait();
        }

        public void RemoveTenantConfiguration(TestShippingPreferencesConfiguration configuration)
        {
            _processor.RemoveTenantPreferences().Wait();
            _processor.RemoveFlatRates(configuration.Identifier, configuration.FlatRateSchedules).Wait();
            _processor.RemoveHandlingRates(configuration.Identifier, configuration.HandlingSchedules).Wait();
            _processor.RemoveConfiguration(configuration.Identifier).Wait();
        }

        public void Authenticate(TestShippingAuthCredential authCredentials)
        {
            _processor.AuthenticateWithToken(authCredentials).Wait();
        }
    }
}
