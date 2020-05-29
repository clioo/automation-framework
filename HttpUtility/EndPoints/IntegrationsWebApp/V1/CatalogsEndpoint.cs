using System.Threading.Tasks;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Catalogs;

namespace HttpUtility.EndPoints.IntegrationsWebApp.V1
{
    public class CatalogsEndpoint : EndpointBase<CatalogRequest, HttpResponseExtended<CatalogResponse>>, ICatalogsEndpoint
    {
        public CatalogsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public override async Task<HttpResponseExtended<CatalogResponse>> GetSingle(string externalIdentifier)
        {
            MethodUrl = "/ext/" + externalIdentifier;
            var response = await Get("");
            return response;
        }

        public override async Task<HttpResponseExtended<CatalogResponse>> Remove(string externalIdentifier)
        {
            MethodUrl = "/ext/" + externalIdentifier;
            var response = await Delete("");
            return response;
        }

        public override async Task<HttpResponseExtended<CatalogResponse>> Create(CatalogRequest postRequest)
        {
            MethodUrl = "";
            return await base.Create(postRequest);
        }

        //TODO
        //handle put by internal
    }
}
