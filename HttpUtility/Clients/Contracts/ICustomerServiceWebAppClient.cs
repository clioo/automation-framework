using HttpUtility.EndPoints.CustomerServiceWebApp;

namespace HttpUtility.Clients.Contracts
{
    public interface ICustomerServiceWebAppClient
    {
        LoginEndPoint Logins { get; set; }
        UserEndpoint Users { get; set; }
    }
}