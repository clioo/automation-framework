using HttpUtility.EndPoints.ShippingService.Models;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IShippingPreferencesEndpoint
    {
        Task<HttpEssResponse<ShippingPreferencesResponse>> Create(ShippingPreferencesRequest postRequest);
        Task<HttpEssResponse<ShippingPreferencesResponse>> GetSingle();
        Task<HttpEssResponse<ShippingPreferencesResponse>> Remove();
        Task<HttpEssResponse<ShippingPreferencesResponse>> Update(ShippingPreferencesRequest putRequest);
        Task<HttpEssResponse<ShippingPreferencesResponse>> PatchEntity(object request);
    }
}
