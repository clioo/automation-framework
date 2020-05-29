using Dfsi.Utility.Requester.Https;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Interfaces;

namespace HttpUtility.Clients
{
    public class ShippingServiceClient : IShippingServiceClient
    {
        public IShippingConfigurationEndpoint ShippingConfigurations { get; set; }
        public IAccountMasterShippingEndpoint ShippingAccountMasterPreferences { get; set; }
        public IShippingPreferencesEndpoint ShippingPreferences { get; set; }
        public IFlatRateSchedulesEndpoint FlatRateSchedules { get; set; }
        public IHandlingSchedulesEndpoint HandlingSchedules { get; set; }
        public IFlatRateScheduleGroupsEndpoint FlatRateScheduleGroups { get; set; }
        public IHandlingScheduleGroupsEndpoint HandlingScheduleGroups { get; set; }
        public IShipmentEndpoint Shipments { get; set; }
        public IHandlingSchedulesConfigurationEndpoint HandlingScheduleConfigurations { get; set; }
        public IFlatRateSchedulesConfigurationEndpoint FlatRateScheduleConfigurations { get; set; }
        public IShippingAuthEndpoint Token { get; set; }

        //constuctor
        public ShippingServiceClient(string url, string platformExternalId, bool useHttps = false)
        {
            Requester requester = new Requester(url);

            ShippingConfigurations = new ShippingConfigurationsEndpoint(requester, $"/tenants/{platformExternalId}/shippingConfigurations", useHttps);
            ShippingPreferences = new ShippingPreferencesEndpoint(requester, $"/tenants/{platformExternalId}/ShippingPreferences", useHttps);
            ShippingAccountMasterPreferences = new AccountMasterShippingEndpoint(requester, $"/tenants/{platformExternalId}/accountMasters", useHttps);
            FlatRateSchedules = new FlatRateSchedulesEndpoint(requester, $"/tenants/{platformExternalId}/flatRateSchedules", useHttps);
            HandlingSchedules = new HandlingSchedulesEndpoint(requester, $"/tenants/{platformExternalId}/handlingSchedules", useHttps);
            FlatRateScheduleGroups = new FlatRateScheduleGroupsEndpoint(requester, $"/tenants/{platformExternalId}/flatRateScheduleGroups", useHttps);
            HandlingScheduleGroups = new HandlingScheduleGroupsEndpoint(requester, $"/tenants/{platformExternalId}/handlingScheduleGroups", useHttps);
            FlatRateScheduleConfigurations = new FlatRateSchedulesConfigurationEndpoint(requester, $"/tenants/{platformExternalId}/configurations", useHttps);
            HandlingScheduleConfigurations = new HandlingSchedulesConfigurationEndpoint(requester, $"/tenants/{platformExternalId}/configurations", useHttps);
            Shipments = new ShipmentEndpoint(requester, $"/tenants/{platformExternalId}/accountmasters", useHttps);
            Token = new ShippingAuthEndpoint(requester, $"/token/authenticate", useHttps);
        }

        public ShippingServiceClient(string url, string platformExternalId, string bearerToken, bool useHttps)
        { 
            Requester requester = new Requester(url);
            //authentication via token
            requester.AddBearerAuth(bearerToken);

            ShippingConfigurations = new ShippingConfigurationsEndpoint(requester, $"/tenants/{platformExternalId}/shippingConfigurations", useHttps);
            ShippingPreferences = new ShippingPreferencesEndpoint(requester, $"/tenants/{platformExternalId}/ShippingPreferences", useHttps);
            ShippingAccountMasterPreferences = new AccountMasterShippingEndpoint(requester, $"/tenants/{platformExternalId}/accountMasters", useHttps);
            FlatRateSchedules = new FlatRateSchedulesEndpoint(requester, $"/tenants/{platformExternalId}/flatRateSchedules", useHttps);
            HandlingSchedules = new HandlingSchedulesEndpoint(requester, $"/tenants/{platformExternalId}/handlingSchedules", useHttps);
            FlatRateScheduleGroups = new FlatRateScheduleGroupsEndpoint(requester, $"/tenants/{platformExternalId}/flatRateScheduleGroups", useHttps);
            HandlingScheduleGroups = new HandlingScheduleGroupsEndpoint(requester, $"/tenants/{platformExternalId}/handlingScheduleGroups", useHttps);
            FlatRateScheduleConfigurations = new FlatRateSchedulesConfigurationEndpoint(requester, $"/tenants/{platformExternalId}/configurations", useHttps);
            HandlingScheduleConfigurations = new HandlingSchedulesConfigurationEndpoint(requester, $"/tenants/{platformExternalId}/configurations", useHttps);
            Shipments = new ShipmentEndpoint(requester, $"/tenants/{platformExternalId}/accountmasters", useHttps);
            Token = new ShippingAuthEndpoint(requester, $"/token/authenticate", useHttps);
        }
    }
}
