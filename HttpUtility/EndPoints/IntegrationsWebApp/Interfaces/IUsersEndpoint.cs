using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Users;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.IntegrationsWebApp.Interfaces
{
    public interface IUsersEndpoint : IGenericEndpoint<UserRequest, HttpResponseExtended<UserResponse>>
    {
        Task<HttpResponseExtended<UserResponse>> UpdateLoginOnUser(string loginExternalId, string userExternalId);
    }
}
