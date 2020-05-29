using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingAuthEndpoint : EndPoint<HttpEssResponse<string>>, IShippingAuthEndpoint
    {
        public ShippingAuthEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<string>> Authenticate(ShippingAuthRequest request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Post(stringPayload);
            return response;
        }
    }
}
