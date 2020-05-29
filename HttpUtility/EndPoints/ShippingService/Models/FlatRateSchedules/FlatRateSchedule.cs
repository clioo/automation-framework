namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateSchedule
    {
        public string ExternalIdentifier { get; set; }
        public decimal? OrderAmountMax { get; set; }
        public decimal OrderAmountMin { get; set; }
        public decimal Rate { get; set; }
    }

    public class FrServiceLevelType
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
}