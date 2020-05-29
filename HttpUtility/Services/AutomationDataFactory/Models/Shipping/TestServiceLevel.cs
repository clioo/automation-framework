using HttpUtility.EndPoints.ShippingService.Enums;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestServiceLevel
    {
        public ServiceLevelCodesEnum Code { get; set; }
        public string Label { get; set; }
        public int SortOrder { get; set; }
        public double Amount { get; set; }
        public bool IsEnabled { get; set; } = true;
        public string CalculationMethod { get; set; }
        public string CarrierCode { get; set; }
        public double CarrierRateDiscount { get; set; }
        public RateShopperShipperCodesEnum? RateShopperExtId { get; set; }
        public string ErpExtId { get; set; }
    }
}
