using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class CategoriesEndpoint : EndpointBase<CategoryRequest, HttpResponseExtended<CategoryResponse>>, ICategoriesEndpoint
    {
        public CategoriesEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}