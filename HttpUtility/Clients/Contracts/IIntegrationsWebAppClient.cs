using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.V1;

namespace HttpUtility.Clients.Contracts
{
    public interface IIntegrationsWebAppClient
    {
        IAccountMastersEndpoint AccountMasters { get; set; }
        IPriceListsEndpoint PriceLists { get; set; }
        ProductEndpoint Products { get; set; }
        IOfferingsEndpoint Offerings { get; set; }
        ICategoriesEndpoint Categories { get; set; }
        ICatalogsEndpoint Catalogs { get; set; }
        BrandsEndpoint Brands { get; set; }
        IAddressesEndpoint Addresses { get; set; }
        ILoginsEndpoint Logins { get; set; }
        IContactsEndpoint Contacts { get; set; }
        IUsersEndpoint Users { get; set; }
        IPublishMerchandiseEndpoint PublishMerchandise { get; set; }
    }
}