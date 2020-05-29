using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IHandlingSchedulesEndpoint : IGenericEndpoint<HandlingScheduleRequest, HttpEssResponse<HandlingScheduleResponse>>
    {
    }
}
