using HttpUtility.Clients;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations.Processors;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using HttpUtility.Services.AutomationDataFactory.Utils;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class AutomationDataFactory : IAutomationDataFactory
    {
        public IUserAccountsFactory Users { get; set; }
        public IShippingConfigurationFactory Shipping { get; set; }
        public IProductsFactory Products { get; set; }

        public AutomationDataFactory(DataFactoryConfiguration configuration)
        {
            //empties and nulls validation
            ApiUrlHelper.ValidateUrl(configuration.IntegrationsApiUrl, nameof(configuration.IntegrationsApiUrl));
            ApiUrlHelper.ValidateUrl(configuration.ShippingServiceApiUrl, nameof(configuration.ShippingServiceApiUrl));

            //clean service api url
            string integrationsApiUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.IntegrationsApiUrl);
            string shippingServiceApiUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.ShippingServiceApiUrl);
            string tenantUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.TenantSiteUrl);

            //clients initialization
            var integrationsClient = new IntegrationsWebAppClient(integrationsApiUrl, configuration.TenantExternalIdentifier, configuration.TenantInternalIdentifier, configuration.IntegrationsApiUrl.Contains("https"));
            var shippingServiceClient = new ShippingServiceClient(shippingServiceApiUrl, configuration.TenantExternalIdentifier, configuration.ShippingServiceApiUrl.Contains("https"));

            //dependencies setup
            var usersProcessor = new UserAccountsProcessor(integrationsClient);
            Users = new UserAccountsFactory(usersProcessor);

            var shippingProcessor = new ShippingServiceProcessor(shippingServiceClient, configuration.ShippingServiceApiUrl, configuration.TenantExternalIdentifier);
            Shipping = new ShippingConfigurationFactory(shippingProcessor);

            var productsProcessor = new MerchandiseProcessor(integrationsClient);
            Products = new ProductsFactory(productsProcessor, tenantUrl);
        }


        public AutomationDataFactory(DataFactoryConfiguration configuration, TestShippingAuthCredential testShippingAuthCredential)
        {
            //empties and nulls validation
            ApiUrlHelper.ValidateUrl(configuration.IntegrationsApiUrl, nameof(configuration.IntegrationsApiUrl));
            ApiUrlHelper.ValidateUrl(configuration.ShippingServiceApiUrl, nameof(configuration.ShippingServiceApiUrl));

            //clean service api url
            string integrationsApiUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.IntegrationsApiUrl);
            string shippingServiceApiUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.ShippingServiceApiUrl);
            string tenantUrl = ApiUrlHelper.GetRequesterFormatUrl(configuration.TenantSiteUrl);

            //clients initialization
            var integrationsClient = new IntegrationsWebAppClient(integrationsApiUrl, configuration.TenantExternalIdentifier, configuration.TenantInternalIdentifier, configuration.IntegrationsApiUrl.Contains("https"));
            var shippingServiceClient = new ShippingServiceClient(shippingServiceApiUrl, configuration.TenantExternalIdentifier, configuration.ShippingServiceApiUrl.Contains("https"));

            //dependencies setup
            var usersProcessor = new UserAccountsProcessor(integrationsClient);
            Users = new UserAccountsFactory(usersProcessor);

            var shippingProcessor = new ShippingServiceProcessor(shippingServiceClient, configuration.ShippingServiceApiUrl, configuration.TenantExternalIdentifier);
            Shipping = new ShippingConfigurationFactory(shippingProcessor, testShippingAuthCredential);

            var productsProcessor = new MerchandiseProcessor(integrationsClient);
            Products = new ProductsFactory(productsProcessor, tenantUrl);
        }

    }
}
