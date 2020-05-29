using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IAccountMasterShippingEndpoint
    {
        Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> GetSingle(string accountMasterExtId);
        Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Create(string accountMasterExtId, ShippingAccountMasterPreferencesRequest postRequest);
        Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Update(string accountMasterExtId, ShippingAccountMasterPreferencesRequest putRequest);
        Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Remove(string accountMasterExtId);
        Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> PatchEntity(string accountMasterExtId, object patchRequest);
    }
}
