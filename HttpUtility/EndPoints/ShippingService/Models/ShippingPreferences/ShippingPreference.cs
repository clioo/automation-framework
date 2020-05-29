using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService.Models
{
    public class ShippingPreference
    {
        public bool UseCustomerCarrier { get; set; }
        public bool FreeFreightForNonContiguousStates { get; set; }
        public bool UseBestRate { get; set; }
        public List<FreeHandlingRule> FreeHandlingRules { get; set; }
    }

    public class FreeHandlingRule
    {
        public int ServiceLevelCode { get; set; }
        public decimal ThresholdAmount { get; set; }
    }
}

