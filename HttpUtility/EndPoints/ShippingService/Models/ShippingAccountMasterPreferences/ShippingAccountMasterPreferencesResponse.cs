using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences
{
    public class ShippingAccountMasterPreferencesResponse : ShippingAccountMasterPreference
    {
        public string OwnerExternalIdentifier { get; set; }
        public List<SAMFreeFreightRuleResponse> FreeFreightRules { get; set; }
        public ShippingConfigurationResponse ShippingConfiguration { get; set; }
        public HandlingScheduleGroupResponse HandlingScheduleGroup { get; set; }
        public FlatRateScheduleGroupsResponse FlatRateScheduleGroup { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }

    public class SAMFreeFreightRuleResponse : SAMFreeFreightRule
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
