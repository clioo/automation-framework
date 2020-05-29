using System.Collections.Generic;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingConfigurationRequest : ShippingConfiguration
    {
        public List<SCServiceLevel> ServiceLevels { get; set; }
    }

    #region nested objects
    public class SCServiceLevel
    {
        public int Code { get; set; }
        public string Label { get; set; }
        public int SortOrder { get; set; }
        public double? Amount { get; set; }
        public bool IsEnabled { get; set; }
        public string CalculationMethod { get; set; }
        public string CarrierCode { get; set; }
        public double? CarrierRateDiscount { get; set; }
        public string RateShopperExtId { get; set; }
        public string ErpExtId { get; set; }
    }
    #endregion nested objects
}
