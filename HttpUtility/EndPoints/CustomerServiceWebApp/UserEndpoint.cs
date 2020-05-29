using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.CustomerServiceWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.CustomerServiceWebApp
{
    public class UserEndpoint : EndPoint<HttpResponse<UserResponse>>
    {
        public UserEndpoint(IRequester requester, string url, bool useHttps = true) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponse<UserResponse>> GetByInternalIdentifier(GetUserRequest request)
        {
            HttpResponse<UserResponse> response = await Get(request.Id.ToString());

            return response;
        }

        public async Task<HttpResponse<UserResponse>> Create(CreateUserRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse<UserResponse> response = await Post(stringPayload);
            return response;
        }

        public async Task<HttpResponse<UserResponse>> Remove(DeleteUserRequest request)
        {
            HttpResponse<UserResponse> response = await Delete(request.Id);
            return response;
        }

        public async Task<HttpResponse<UserResponse>> Update(UpdateUserRequest request)
        {
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse<UserResponse> response = await Put(request.Identifier.ToString(), stringPayload);

            return response;
        }
    }

    public class UpdateUserRequest : User
    {
        public Guid DefaultAddressIdentifier { get; set; }
        public Guid DefaultCreditCardPaymentIdentifier { get; set; }
        public string CookieString { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> RoleGuids { get; set; }
        public bool UseAccountTermsAsDefaultPayment { get; set; }
    }

    public class DeleteUserRequest
    {
        public string Id { get; set; }
    }

    public class CreateUserRequest : User
    {
        public Guid DefaultAddressIdentifier { get; set; }
        public Guid DefaultCreditCardPaymentIdentifier { get; set; }
        public string CookieString { get; set; }
        public bool IsAnonymous { get; set; }
        public bool IsEnabled { get; set; }
        public List<string> RoleGuids { get; set; }
        public bool UseAccountTermsAsDefaultPayment { get; set; }
    }

    public class GetUserRequest
    {
        public Guid Id { get; set; }
    }

    public class UserResponse : User
    {
        public Guid DefaultAddressIdentifier { get; set; }
        public Guid DefaultCreditCardPaymentIdentifier { get; set; }
    }
}
