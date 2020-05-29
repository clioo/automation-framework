using HttpUtility.EndPoints.Base.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IHandlingScheduleGroupsEndpoint : ICrudEndpoint<HandlingScheduleGroupRequest, HttpEssResponse<HandlingScheduleGroupResponse>>
    {
        
    }
}
