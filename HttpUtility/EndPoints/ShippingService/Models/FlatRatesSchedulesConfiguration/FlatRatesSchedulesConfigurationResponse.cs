namespace HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration
{
    public class FlatRatesSchedulesConfigurationResponse : FlatRatesSchedulesConfiguration
    {
        public FlatRateScheduleGroupsResponse FlatRateScheduleGroup { get; set; }
        public FlatRateScheduleResponse FlatRateSchedule { get; set; }
        public string CreatedUtc { get; set; }        
        public string UpdatedUtc { get; set; }
    }
}
