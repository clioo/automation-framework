using HttpUtility.EndPoints.Base.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface ILoginsEndpoint : ICrudEndpoint<LoginRequest, HttpResponseExtended<LoginResponse>>
    {
    }
}
