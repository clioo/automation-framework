using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Addresses;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class TestAddressesFactory : ITestAddressesFactory
    {
        readonly IIntegrationsWebAppClient _client;
        public TestAddressesFactory(IIntegrationsWebAppClient client)
        {
            _client = client;
        }

        public async Task AddAccountAddress(string accountMasterExtId, TestAccountAddress address = null)
        {
            string externalIdentifier = address == null ? accountMasterExtId : address.ExternalIdentifier;

            //clear account address if exist
            //await RemoveAddress(externalIdentifier);

            address = address ?? new TestAccountAddress
            {
                Apartment = "12",
                City = "denver",
                CompanyName = "dfs",
                Country = "US",
                ExternalIdentifier = accountMasterExtId,
                Postal = "80019",
                StateProvinceRegion = "CO",
                Street = "Walnut Street"
            };

            AddressRequest addressRequest = new AddressRequest
            {
                AddressLine = address.Street,
                AddressLine2 = address.Apartment,
                City = address.City,
                Country = address.Country,
                Identifier = externalIdentifier,
                Name = address.CompanyName,
                Postal = address.Postal,
                State = address.StateProvinceRegion
            };
            await _client.Addresses.Create(accountMasterExtId, addressRequest);
        }

        public async Task RemoveAddress(string addressExtId)
        {
            await _client.Addresses.Remove(addressExtId);
        }
    }
}
