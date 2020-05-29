using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class OfferingEndpoint : EndPoint<HttpResponseExtended<OfferingResponse>>
    {
        public OfferingEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {

        }

        public async Task<HttpResponseExtended<OfferingResponse>> Create(OfferingRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponseExtended<OfferingResponse> response = await Post(stringPayload);
            return response;
        }

        public async Task<HttpResponseExtended<OfferingResponse>> GetByExternalIdentifier(GetOfferingRequest request)
        {
            HttpResponseExtended<OfferingResponse> httpResponse = await Get(request.ExternalIdentifier);
            return httpResponse;
        }

        public async Task<HttpResponseExtended<OfferingResponse>> PatchByExternal(string externalId, object requestObject)
        {
            MethodUrl = $"/{externalId}";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(requestObject));
            HttpResponseExtended<OfferingResponse> response = await Patch(stringPayload);
            return response;
        }

        public async Task<HttpResponseExtended<OfferingResponse>> Remove(DeleteOfferingRequest request)
        {
            HttpResponseExtended<OfferingResponse> httpResponse = await Delete(request.ExternalIdentifier);
            return httpResponse;
        }
    }

    //Deprecated requests
    public class DeleteOfferingRequest
    {
        public string ExternalIdentifier { get; set; }
    }

    public class GetOfferingRequest
    {
        public string ExternalIdentifier { get; set; }
        public string CatalogId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
