namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateScheduleResponse : FlatRateSchedule
    {
        public int ServiceLevelTypeId { get; set; }
        public FrServiceLevelType ServiceLevelType { get; set; }
    }
}
