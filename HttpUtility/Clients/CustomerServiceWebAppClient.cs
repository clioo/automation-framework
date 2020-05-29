using Dfsi.Utility.Requester.Https;
using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.CustomerServiceWebApp;

namespace HttpUtility.Clients
{
    public class CustomerServiceWebAppClient : ICustomerServiceWebAppClient
    {
        public LoginEndPoint Logins { get; set; }
        public UserEndpoint Users { get; set; }

        public CustomerServiceWebAppClient(string url, string user, string password, bool useHttps = false)
        {
            Requester requester = new Requester(url, @user, password);
            requester.AddBasicAuth(@user, password);
            Logins = new LoginEndPoint(requester, "/api/account", useHttps);
            Users = new UserEndpoint(requester, "/api/account/users", useHttps);
        }
    }
}