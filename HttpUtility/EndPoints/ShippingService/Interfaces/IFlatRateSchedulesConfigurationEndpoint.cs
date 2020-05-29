using System.Threading.Tasks;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;

namespace HttpUtility.EndPoints.ShippingService
{
    public interface IFlatRateSchedulesConfigurationEndpoint
    {
        Task<HttpEssResponse<FlatRatesSchedulesConfigurationResponse>> Create(string groupId, string scheduleId, FlatRateScheduleConfigurationRequest request);
        Task<HttpEssResponse<FlatRatesSchedulesConfigurationResponse>> Remove(string groupId, string scheduleId);
    }
}