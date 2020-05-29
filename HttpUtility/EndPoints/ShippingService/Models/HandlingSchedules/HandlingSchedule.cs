namespace HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules
{
    public class HandlingSchedule
    {
        public string ExternalIdentifier { get; set; }
        public decimal Rate { get; set; }
        public decimal? OrderAmountMax { get; set; }
        public decimal? OrderAmountMin { get; set; }
    }
}
