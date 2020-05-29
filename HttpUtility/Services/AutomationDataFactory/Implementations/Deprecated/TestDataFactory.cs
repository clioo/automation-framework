using HttpUtility.Clients;
using HttpUtility.Clients.Contracts;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using System;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class DataFactory : ITestDataFactory
    {
        public ITestAddressesFactory Addresses { get; set; }
        public ITestUserAccountsFactory UserAccounts { get; set; }
        public ITestShippingConfigurationFactory ShippingConfigurationPreferences { get; set; }
        public ITestShippingRatesFactory ShippingRates { get; set; }

        public DataFactory(DataFactoryConfiguration configuration)
        {
            IIntegrationsWebAppClient integrationsClient = null;
            IShippingServiceClient shippingServiceClient = null;

            //empties and nulls validation
            UrlExist(configuration.IntegrationsApiUrl, nameof(configuration.IntegrationsApiUrl));
            UrlExist(configuration.ShippingServiceApiUrl, nameof(configuration.ShippingServiceApiUrl));

            string integrationsApiUrl = FixHttpOnUrl(configuration.IntegrationsApiUrl);
            string shippingServiceApiUrl = FixHttpOnUrl(configuration.ShippingServiceApiUrl);

            //services initialization
            integrationsClient = new IntegrationsWebAppClient(integrationsApiUrl, configuration.TenantExternalIdentifier, configuration.TenantInternalIdentifier, configuration.IntegrationsApiUrl.Contains("https"));
            shippingServiceClient = new ShippingServiceClient(shippingServiceApiUrl, configuration.TenantExternalIdentifier, configuration.ShippingServiceApiUrl.Contains("https"));

            //dependencies initialization
            UserAccounts = new TestUserAccountsFactory(integrationsClient);
            Addresses = new TestAddressesFactory(integrationsClient);
            ShippingConfigurationPreferences = new TestShippingConfigurationFactory(shippingServiceClient);
            ShippingRates = new TestShippingRatesFactory(shippingServiceClient);
        }

        private string FixHttpOnUrl(string url)
        {
            if (url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }

        private void UrlExist(string url, string name)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
                throw new Exception($"{name} has an invalid url value: {url}");
        }
    }
}
