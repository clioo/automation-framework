using System.Threading.Tasks;
using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using Newtonsoft.Json;

namespace HttpUtility.EndPoints.ShippingService
{
    public class ShippingPreferencesEndpoint : EndPoint<HttpEssResponse<ShippingPreferencesResponse>>, IShippingPreferencesEndpoint
    {
        public ShippingPreferencesEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<ShippingPreferencesResponse>> Create(ShippingPreferencesRequest request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Post(stringPayload);

            return response;
        }

        public async Task<HttpEssResponse<ShippingPreferencesResponse>> GetSingle()
        {
            var response = await Get("");
            return response;
        }

        public async Task<HttpEssResponse<ShippingPreferencesResponse>> Update(ShippingPreferencesRequest request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Put("", stringPayload);

            return response;
        }

        public async Task<HttpEssResponse<ShippingPreferencesResponse>> Remove()
        {
            var response = await Delete("");
            return response;
        }

        public async Task<HttpEssResponse<ShippingPreferencesResponse>> PatchEntity(object request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Patch(stringPayload);

            return response;
        }
    }
}
