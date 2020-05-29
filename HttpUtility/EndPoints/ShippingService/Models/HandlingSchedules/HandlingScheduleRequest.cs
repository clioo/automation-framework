namespace HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules
{
    public class HandlingScheduleRequest : HandlingSchedule
    {
        public int ServiceLevelCode { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
