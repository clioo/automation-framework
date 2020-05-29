using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using System;
using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestShippingPreferences
    {
        [Obsolete]
        public string ConfigurationExtId { get; set; }
        [Obsolete]
        public List<SAMFreeFreightRule> FreeFreightRules { get; set; }

        public bool UseCustomerCarrier { get; set; } = false;
        public bool FreeFreightForNonContiguousStates { get; set; } = false;
        public List<TestFreeFreightRule> FreeFreightRuless { get; set; }
        public List<TestFreeHandlingRule> FreeHandlingRules { get; set; }
        public bool UseBestRate { get; set; } = false;
    }
}
