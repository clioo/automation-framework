using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences
{
    public class ShippingAccountMasterPreference
    {
        public bool UseCustomerCarrier { get; set; }
        public bool FreeFreightForNonContiguousStates { get; set; }
        public bool UseBestRate { get; set; }
        public List<FreeHandlingRule> FreeHandlingRules { get; set; }
    }
}
