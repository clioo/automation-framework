using System.Threading.Tasks;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.Base.Implementations;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Users;

namespace HttpUtility.EndPoints.IntegrationsWebApp
{
    public class UsersEndpoint : EndpointBase<UserRequest, HttpResponseExtended<UserResponse>>, IUsersEndpoint
    {
        public UsersEndpoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponseExtended<UserResponse>> UpdateLoginOnUser(string loginExternalId, string userExternalId)
        {
            MethodUrl = $"/{loginExternalId}/user/{userExternalId}";

            var response = await Post("");
            return response;
        }
    }
}
