using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.PriceLists;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class PriceListsEndpoint : EndpointBase<PriceListRequest, HttpResponseExtended<PriceListResponse>>, IPriceListsEndpoint
    {
        public PriceListsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}
