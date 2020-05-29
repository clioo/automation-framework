using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IAccountMastersEndpoint : IGenericEndpoint<AccountMasterRequest, HttpResponseExtended<AccountMasterResponse>>
    {
    }
}
