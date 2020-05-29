using System.Threading.Tasks;
using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.ShippingService.Interfaces;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Newtonsoft.Json;

namespace HttpUtility.EndPoints.ShippingService
{
    public class AccountMasterShippingEndpoint : EndPoint<HttpEssResponse<ShippingAccountMasterPreferencesResponse>>, IAccountMasterShippingEndpoint
    {
        public AccountMasterShippingEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> GetSingle(string accountMasterExtId)
        {
            MethodUrl = $"/{accountMasterExtId}/shippingPreferences";
            var response = await Get("");

            return response;
        }

        public async Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Create(string accountMasterExtId, ShippingAccountMasterPreferencesRequest request)
        {
            MethodUrl = $"/{accountMasterExtId}/shippingPreferences";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Post(stringPayload);

            return response;
        }

        public async Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Update(string accountMasterExtId, ShippingAccountMasterPreferencesRequest request)
        {
            MethodUrl = $"/{accountMasterExtId}/shippingPreferences";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Put("", stringPayload);

            return response;
        }

        public async Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> Remove(string accountMasterExtId)
        {
            MethodUrl = $"/{accountMasterExtId}/shippingPreferences";
            var response = await Delete("");
            return response;
        }

        public async Task<HttpEssResponse<ShippingAccountMasterPreferencesResponse>> PatchEntity(string accountMasterExtId, object request)
        {
            MethodUrl = $"/{accountMasterExtId}/shippingPreferences/";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Patch(stringPayload);
            return response;
        }
    }
}
