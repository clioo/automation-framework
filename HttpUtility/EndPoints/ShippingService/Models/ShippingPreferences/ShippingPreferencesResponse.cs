using System.Collections.Generic;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingPreferencesResponse : ShippingPreference
    {
        public List<SFreeFreightRuleResponse> FreeFreightRules { get; set; }
        public string OwnerExternalIdentifier { get; set; }
        //this property is from another endpoint
        public ShippingConfigurationResponse ShippingConfiguration { get; set; }
        //this property is from another endpoint
        public FlatRateScheduleGroupsResponse FlatRateScheduleGroup { get; set; }
        //this property is from another endpoint
        public HandlingScheduleGroupResponse HandlingScheduleGroup { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }

    //response nested objects
    public class SFreeFreightRuleResponse : SFreeFreightRule
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
