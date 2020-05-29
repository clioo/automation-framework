using Dfsi.Utility.Requester;
using Dfsi.Utility.Requester.Https;
using Dfsi.Utility.Requester.Https.Contracts;
using HttpUtility.EndPoints.CustomerServiceWebApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.EndPoints.CustomerServiceWebApp
{
    public class LoginEndPoint : EndPoint<HttpResponse<LoginResponse>>
    {
        public LoginEndPoint(IRequester requester, string url, bool useHttps) : base(requester, url, useHttps)
        {
        }

        public async Task<HttpResponse<LoginResponse>> CreateContactUserLogin(CreateLoginUserContactRequest request)
        {
            MethodUrl = "/contactsuserslogins";
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse<LoginResponse> response = await Post(stringPayload);
            return response;
        }

        public async Task<HttpResponse<LoginResponse>> GetAccountByAccountMasterExternalId(GetAccountAccountMasterRequest request)
        {
            MethodUrl = $"/linkaccount/{request.id}";
            ICollection<IFilter> apiFilters = new List<IFilter>();
            apiFilters.Add(new Filter("externalId", request.externalId));
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(request));
            HttpResponse<LoginResponse> response = await Get(apiFilters);
            return response;
        }
    }

    public class CreateLoginUserContactRequest
    {
        public Guid AccountIdentifier { get; set; }

        public Guid AccountMasterIdentifier { get; set; }

        public string ContactEmail { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
        public Guid PlatformIdentifier { get; set; }

        public string PhoneNumber { get; set; }
    }

    public class GetAccountAccountMasterRequest
    {
        public GetAccountAccountMasterRequest()
        {
            id = new Guid().ToString();
        }

        public string id { get; }
        public string externalId { get; set; }
    }

    public class LoginResponse : AccountAccountMaster
    {
        public Contact Contact { get; set; }

        public Login Login { get; set; }

        public User User { get; set; }
    }
}