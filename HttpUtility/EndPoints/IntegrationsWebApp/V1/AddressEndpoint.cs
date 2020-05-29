using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Addresses;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class AddressesEndpoint : EndPoint<AddressResponse>, IAddressesEndpoint
    {
        public AddressesEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {

        }

        public async Task<AddressResponse> Remove(string externalIdentifier)
        {
            MethodUrl = "/ext";

            var response = await Delete(externalIdentifier);

            MethodUrl = "";
            return response;
        }

        public async Task<AddressResponse> Update(string externalIdentifier, AddressRequest putRequest)
        {
            MethodUrl = "/ext";

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(putRequest));
            var response = await Put(externalIdentifier, stringPayload);

            MethodUrl = "";
            return response;
        }

        public async Task<AddressResponse> Create(string accountMasterExtId, AddressRequest postRequest)
        {
            MethodUrl = $"?ownerExtId={accountMasterExtId}";

            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(postRequest));
            var response = await Post(stringPayload);
            MethodUrl = "";

            return response;
        }
    }
}
