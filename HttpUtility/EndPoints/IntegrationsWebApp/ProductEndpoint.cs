using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class ProductEndpoint : EndpointBase<ProductRequest, HttpResponseExtended<ProductResponse>>, IProductsEndpoint
    {
        public ProductEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponseExtended<ProductResponse>> Create(PostProductRequest request)
        {
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponseExtended<ProductResponse> productResponse = await Post(stringPayload);
            return productResponse;
        }

        [System.Obsolete]
        public async Task<HttpResponseExtended<ProductResponse>> GetByExternalIdentifier(GetProductRequest request)
        {
            HttpResponseExtended<ProductResponse> response = await Get(request.ExternalIdentifier);
            return response;
        }

        [System.Obsolete]
        public async Task<HttpResponseExtended<ProductResponse>> Remove(DeleteProductRequest request)
        {
            HttpResponseExtended<ProductResponse> response = await Delete(request.ExternalIdentifier);
            return response;
        }
    }
}
