using Dfsi.Utility.Requester.Https;
using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.EcommerceWebApp;

namespace HttpUtility.Clients
{
    public class ECommerceWebAppClient : IECommerceWebAppClient
    {
        public AddressEndPoint Addresses { get; set; }
        public AuthEndPoint Authentication { get; set; }

        public ECommerceWebAppClient(string url, bool useHttps = false)
        {
            Requester requester = new Requester(url);
            Addresses = new AddressEndPoint(requester, "/api/myaccount/addresses", useHttps);
            Authentication = new AuthEndPoint(requester, "/SignInAttempt/CheckCredentials", useHttps);
        }
    }
}