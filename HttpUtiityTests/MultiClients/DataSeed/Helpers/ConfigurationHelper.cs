using HttpUtiityTests.EnvConstants;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using System;

namespace HttpUtiityTests.MultiClients.DataSeed.Helpers
{
    static public class ConfigurationHelper
    {
        static public ITestDataFactory SetPlatform(TenantsEnum tenant)
        {
            DataFactoryConfiguration configuration = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl
            };

            switch (tenant)
            {
                case TenantsEnum.AllPoints:
                    configuration.TenantExternalIdentifier = ServiceConstants.AllPointsPlatformExtId;
                    configuration.TenantInternalIdentifier = ServiceConstants.AllPointsPlatformId;
                    break;

                case TenantsEnum.Fmp:
                    configuration.TenantExternalIdentifier = ServiceConstants.FmpPlatformExtId;
                    configuration.TenantInternalIdentifier = ServiceConstants.FMPPlatformId;
                    break;

                default:
                    throw new ArgumentException($"{tenant} is invalid");
            }

            //initialize the factory tool (unit of work)
            return new DataFactory(configuration);
        }
    }
}
