using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences
{
    public class ShippingAccountMasterPreferencesRequest : ShippingAccountMasterPreference
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public List<SAMFreeFreightRule> FreeFreightRules { get; set; }
        public string ShippingConfigurationExternalId { get; set; }
        public string FlatRateScheduleGroupExternalId { get; set; }
        public string HandlingScheduleGroupExternalId { get; set; }
    }

    public class SAMFreeFreightRule
    {
        public int ServiceLevelCode { get; set; }
        public decimal ThresholdAmount { get;set; }
    }
}
