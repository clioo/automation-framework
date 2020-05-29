using HttpUtility.EndPoints.ShippingService.Enums;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestFreeFreightRule
    {
        public ServiceLevelCodesEnum ServiceLevelCode { get; set; }
        public decimal ThresholdAmount { get; set; }
    }
}
