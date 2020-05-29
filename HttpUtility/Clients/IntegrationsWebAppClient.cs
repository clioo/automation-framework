using Dfsi.Utility.Requester.Https;
using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Interfaces;
using HttpUtility.EndPoints.IntegrationsWebApp.V1;

namespace HttpUtility.Clients
{
    public class IntegrationsWebAppClient : IIntegrationsWebAppClient
    {
        public IAccountMastersEndpoint AccountMasters { get; set; }
        public IPriceListsEndpoint PriceLists { get; set; }
        public ProductEndpoint Products { get; set; }
        public OfferingEndpoint Offering { get; set; }
        public IOfferingsEndpoint Offerings { get; set; }
        public ICategoriesEndpoint Categories { get; set; }
        public ILoginsEndpoint Logins { get; set; }
        public IContactsEndpoint Contacts { get; set; }
        public IUsersEndpoint Users { get; set; }
        //v1 endpoints
        public IAddressesEndpoint Addresses { get; set; }
        public PublishContentEndpoint PublishContent { get; set; }
        public BrandsEndpoint Brands { get; set; }
        public ICatalogsEndpoint Catalogs { get; set; }
        public IPublishMerchandiseEndpoint PublishMerchandise { get; set; }

        //this does not work when working with both versions at the same instance
        public IntegrationsWebAppClient(string url, string platformIdentifier, bool useHttps = false)
        {
            Requester requester = new Requester($"{url}");

            //v1 endpoint
            Addresses = new AddressesEndpoint(requester, $"/api/v1/{platformIdentifier}/addresses", useHttps);
            PublishMerchandise = new PublishMerchandiseEndpoint(requester, $"/api/v1/{platformIdentifier}/publishmerchandise", useHttps);
            PublishContent = new PublishContentEndpoint(requester, $"/api/v1/{platformIdentifier}/publishcontent", useHttps);
            Brands = new BrandsEndpoint(requester, $"/api/v1/{platformIdentifier}/brands", useHttps);
            Catalogs = new CatalogsEndpoint(requester, $"/api/v1/{platformIdentifier}/catalogs", useHttps);

            //V2 endpoint
            Products = new ProductEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/products", useHttps);
            Offering = new OfferingEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/offerings", useHttps);
            Categories = new CategoriesEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/categories", useHttps);
            AccountMasters = new AccountMastersEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/accountmasters", useHttps);
            PriceLists = new PriceListsEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/pricelists", useHttps);
            Offerings = new OfferingsEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/offerings", useHttps);
            Logins = new LoginsEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/logins", useHttps);
            Contacts = new ContactsEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/contacts", useHttps);
            Users = new UsersEndpoint(requester, $"/api/v2/platforms/{platformIdentifier}/users", useHttps);
        }

        //in order to support V1 and V2 endpoints, needs both internal and external platform ids
        public IntegrationsWebAppClient(string url, string platformExternalId, string platformInternalId, bool useHttps = false)
        {
            Requester requester = new Requester($"{url}");
            //v1 endpoint
            Addresses = new AddressesEndpoint(requester, $"/api/v1/{platformInternalId}/addresses", useHttps);
            PublishMerchandise = new PublishMerchandiseEndpoint(requester, $"/api/v1/{platformInternalId}/publishmerchandise", useHttps);
            PublishContent = new PublishContentEndpoint(requester, $"/api/v1/{platformInternalId}/publishcontent", useHttps);
            Brands = new BrandsEndpoint(requester, $"/api/v1/{platformInternalId}/brands", useHttps);
            Catalogs = new CatalogsEndpoint(requester, $"/api/v1/{platformInternalId}/catalogs", useHttps);

            //V2 endpoint
            Products = new ProductEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/products", useHttps);
            Categories = new CategoriesEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/categories", useHttps);
            AccountMasters = new AccountMastersEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/accountmasters", useHttps);
            PriceLists = new PriceListsEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/pricelists", useHttps);
            Offerings = new OfferingsEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/offerings", useHttps);
            Logins = new LoginsEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/logins", useHttps);
            Users = new UsersEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/users", useHttps);
            Contacts = new ContactsEndpoint(requester, $"/api/v2/platforms/{platformExternalId}/contacts", useHttps);
        }
    }
}