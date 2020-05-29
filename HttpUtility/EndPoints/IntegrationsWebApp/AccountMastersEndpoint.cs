using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class AccountMastersEndpoint : EndpointBase<AccountMasterRequest, HttpResponseExtended<AccountMasterResponse>>, IAccountMastersEndpoint
    {
        public AccountMastersEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }        
    }
}
