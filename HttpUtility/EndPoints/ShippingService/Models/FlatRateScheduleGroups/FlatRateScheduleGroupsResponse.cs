namespace HttpUtility.EndPoints.ShippingService
{
    public class FlatRateScheduleGroupsResponse : FlatRateScheduleGroup
    {
        public string CreatedBy { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedUtc { get; set; }
    }
}