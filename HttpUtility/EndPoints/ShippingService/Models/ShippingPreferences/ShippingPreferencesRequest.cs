using System.Collections.Generic;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingPreferencesRequest : ShippingPreference
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SFreeFreightRule> FreeFreightRules { get; set; }
        public string ShippingConfigurationExternalId { get; set; }
        public string FlatRateScheduleGroupExternalId { get; set; }
        public string HandlingScheduleGroupExternalId { get; set; }
    }

    public class SFreeFreightRule
    {
        public int ServiceLevelCode { get; set; }
        public decimal ThresholdAmount { get; set; }
    }
}
