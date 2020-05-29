using System.Threading.Tasks;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;

namespace HttpUtility.EndPoints.ShippingService
{
    public interface IHandlingSchedulesConfigurationEndpoint
    {
        Task<HttpEssResponse<HandlingSchedulesConfigurationResponse>> Create(string groupId, string scheduleId, HandlingSchedulesConfigurationRequest request);
        Task<HttpEssResponse<HandlingSchedulesConfigurationResponse>> Remove(string groupId, string scheduleId);
    }
}