using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Interfaces;

namespace HttpUtility.Clients
{
    public interface IShippingServiceClient
    {
        IFlatRateSchedulesEndpoint FlatRateSchedules { get; set; }
        IHandlingSchedulesEndpoint HandlingSchedules { get; set; }
        IShippingConfigurationEndpoint ShippingConfigurations { get; set; }
        IShippingPreferencesEndpoint ShippingPreferences { get; set; }
        IAccountMasterShippingEndpoint ShippingAccountMasterPreferences { get; set; }
        IFlatRateScheduleGroupsEndpoint FlatRateScheduleGroups { get; set; }
        IHandlingScheduleGroupsEndpoint HandlingScheduleGroups { get; set; }
        IShipmentEndpoint Shipments { get; set; }
        IHandlingSchedulesConfigurationEndpoint HandlingScheduleConfigurations { get; set; }
        IFlatRateSchedulesConfigurationEndpoint FlatRateScheduleConfigurations { get; set; }
        IShippingAuthEndpoint Token { get; set; }
    }
}