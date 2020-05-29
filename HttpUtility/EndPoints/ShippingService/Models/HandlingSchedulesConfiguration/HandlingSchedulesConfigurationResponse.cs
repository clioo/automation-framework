namespace HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration
{
    public class HandlingSchedulesConfigurationResponse : HandlingScheduleConfiguration
    {
        public HandlingScheduleGroupResponse HandlingScheduleGroup { get; set; }
        public HandlingScheduleResponse HandlingSchedule { get; set; }
        public string CreatedUtc { get; set; }
        public string UpdatedUtc { get; set; }
    }
}
