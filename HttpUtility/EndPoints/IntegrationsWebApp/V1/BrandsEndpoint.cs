using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Extensions;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Brands;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.V1
{
    public class BrandsEndpoint : EndpointExtended<HttpResponse<BrandResponse>>
    {
        public BrandsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {

        }

        public async Task<HttpResponse<BrandResponse>> Create(PostBrandRequest request)
        {
            MethodUrl = "";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Post(stringPayload);

            return response;
        }

        public async Task<HttpResponse<BrandResponse>> GetByExternalIdentifier(string externalIdentifier)
        {
            MethodUrl = "/ext/" + externalIdentifier;
            var response = await Get("");

            return response;
        }

        //experimental
        public async Task<HttpMultiResponse<BrandResponse>> GetMulti(string baseUrl)
        {
            //handle filters
            //IFilter
            var response = await GetMany<HttpMultiResponse<BrandResponse>>(baseUrl);
            return response;
        }

        public async Task<HttpResponse<BrandResponse>> Remove(string externalIdentifier)
        {
            MethodUrl = "/ext/" + externalIdentifier;

            var response = await Delete("");

            return response;
        }

        public async Task<HttpResponse<BrandResponse>> Update(string internalIdentifier, PutBrandRequest request)
        {
            MethodUrl = "";
            string stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            var response = await Put(internalIdentifier, stringPayload);

            return response;
        }
    }

    public class HttpMultiResponse<TEntity> : HttpResponse<TEntity> where TEntity : class
    {
        public new ResultCollection<TEntity> Result { get; set; }
    }

    public class ResultCollection<TEntity> where TEntity : class
    {
        public List<TEntity> Collection { get; set; }
    }
}
