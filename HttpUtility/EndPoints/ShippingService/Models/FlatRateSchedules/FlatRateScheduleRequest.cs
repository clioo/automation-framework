namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateScheduleRequest : FlatRateSchedule
    {
        public int ServiceLevelCode { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
