using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService.Interfaces
{
    public interface IShippingAuthEndpoint
    {
        Task<HttpEssResponse<string>> Authenticate(ShippingAuthRequest request);
    }
}
