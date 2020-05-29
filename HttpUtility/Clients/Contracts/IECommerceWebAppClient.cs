using HttpUtility.EndPoints.EcommerceWebApp;

namespace HttpUtility.Clients.Contracts
{
    public interface IECommerceWebAppClient
    {
        AddressEndPoint Addresses { get; set; }
        AuthEndPoint Authentication { get; set; }
    }
}