using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class LoginsEndpoint : EndpointBase<LoginRequest, HttpResponseExtended<LoginResponse>>, ILoginsEndpoint
    {
        public LoginsEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }
    }
}
