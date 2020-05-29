using HttpUtility.EndPoints.ShippingService.Enums;

namespace HttpUtility.Services.AutomationDataFactory.Models
{
    public class TestFreeHandlingRule
    {
        public ServiceLevelCodesEnum ServiceLevelCode { get; set; }
        public decimal ThresholdAmount { get; set; }
    }
}
