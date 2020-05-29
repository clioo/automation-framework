using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;

namespace HttpUtility.EndPoints.ShippingService
{
    public class HandlingScheduleResponse : HandlingSchedule
    {
        public int ServiceLevelTypeId { get; set; }
        public HsServiceLevelType ServiceLevelType { get; set; }
    }
}
