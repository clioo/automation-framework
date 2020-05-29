using HttpUtility.EndPoints.ShippingService.Enums;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestSchedule
    {
        public ServiceLevelCodesEnum ServiceLevelCode { get; set; }
        public string ExternalIdentifier { get; set; }
        public decimal Rate { get; set; }
        public int OrderAmountMin { get; set; }
        public int? OrderAmountMax {get;set;}
    }
}
