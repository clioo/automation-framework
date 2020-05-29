using HttpUtility.EndPoints.ShippingService.Models;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IShippingConfigurationEndpoint
    {
        Task<HttpEssResponse<ShippingConfigurationResponse>> Create(ShippingConfigurationRequest postRequest);
        Task<HttpEssResponse<ShippingConfigurationResponse>> Update(string externalIdentifier, ShippingConfigurationRequest putRequest);
        Task<HttpEssResponse<ShippingConfigurationResponse>> GetSingle(string externalIdentifier);
        Task<HttpEssResponse<ShippingConfigurationResponse>> Remove(string externalIdentifier);
    }
}
