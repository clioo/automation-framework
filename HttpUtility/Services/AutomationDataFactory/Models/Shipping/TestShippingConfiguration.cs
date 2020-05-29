using HttpUtility.EndPoints.ShippingService.Enums;
using System;
using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestShippingConfiguration
    {
        [Obsolete]
        public string Identifier { get; set; }
        public ServiceLevelCodesEnum? DefaultServiceLevel { get; set; }
        [Obsolete]
        public double? FreeParcelShipProximityMessageDollar { get; set; }
        [Obsolete]
        public double? FreeParcelShipProximityMessagePercentage { get; set; }
        public List<TestServiceLevel> ServiceLevels { get; set; }
    }
}
