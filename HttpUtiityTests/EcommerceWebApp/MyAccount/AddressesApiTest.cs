using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.EcommerceWebApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.EcommerceWebApp.MyAccount
{
    [TestClass]
    public class AddressesApiTest
    {
        string siteUrl = ServiceConstants.AllPointsUrl;

        [TestMethod]
        public async Task Get_AllUserAddresses_Success()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(siteUrl);

            //Performing a Login to get the cookie into the client
            AuthCookieRequest loginRequest = new AuthCookieRequest
            {
                LoginEmail = "AllPointsTest2@dfs.com",
                LoginPassword = "1234"
            };
            await client.Authentication.GetCookie(loginRequest);

            var getAllUserAddressesResponse = client.Addresses.GetAddresses("User").Result;
            Assert.IsNotNull(getAllUserAddressesResponse);
        }

        [TestMethod]
        public async Task GetDefaultUserAddress_Success()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(siteUrl);

            AuthCookieRequest loginRequest = new AuthCookieRequest
            {
                LoginEmail = "AllPointsTest2@dfs.com",
                LoginPassword = "1234"
            };

            await client.Authentication.GetCookie(loginRequest);

            var userAddressResponse = await client.Addresses.GetDefaultAddress();

            Assert.IsNotNull(userAddressResponse);
        }

        [TestMethod]
        public async Task Post_CreateAddress_Success()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(siteUrl);

            //Performing a Login to get the cookie into the client
            AuthCookieRequest loginRequest = new AuthCookieRequest
            {
                LoginEmail = "AllPointsTest2@dfs.com",
                LoginPassword = "1234"
            };
            var cookie = await client.Authentication.GetCookie(loginRequest);

            var addressRequest = new AddressRequest
            {
                AddressLine1 = "Irish Road",
                AddressLine2 = "",
                City = "Marlenburgh",
                Country = "US",
                IsInternational = false,
                Name = "My address",
                Postal = "48139",
                StateProvinceRegion = "CT"
            };
            var createUserAddressResponse = client.Addresses.Create(addressRequest).Result;
            Assert.IsNotNull(createUserAddressResponse, "Response is null");
            Assert.IsNotNull(createUserAddressResponse.Entity, $"{nameof(createUserAddressResponse.Entity)} is null");
        }

        [TestMethod]
        public async Task Delete_RemoveAddress_Success()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(siteUrl);

            AuthCookieRequest loginRequest = new AuthCookieRequest
            {
                LoginEmail = "STKtest2@etundra.com",
                LoginPassword = "1234"
            };
            await client.Authentication.GetCookie(loginRequest);

            var addressRequest = new AddressRequest
            {
                AddressLine1 = "Jhonnatans Road 2",
                AddressLine2 = "",
                City = "Marlenburgh",
                Country = "US",
                IsInternational = false,
                Name = null,
                Postal = "48139",
                StateProvinceRegion = "CT"
            };

            var getUserAddressResponse = client.Addresses.Create(addressRequest).Result;

            var createdAddres = getUserAddressResponse.Entity as AddressResponse;
            string addressIdentifier = createdAddres.Identifier.ToString();
            var deleteAddressResponse = client.Addresses.Remove(addressIdentifier).Result;

            Assert.IsNotNull(deleteAddressResponse);
        }

        [TestMethod]
        public async Task UpdateAddress()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(siteUrl);

            AuthCookieRequest loginRequest = new AuthCookieRequest
            {
                LoginEmail = "STKtest2@etundra.com",
                LoginPassword = "1234"
            };
            await client.Authentication.GetCookie(loginRequest);

            var createAddressRequest = new AddressRequest
            {
                AddressLine1 = "I will be updated",
                AddressLine2 = "",
                City = "Marlenburgh",
                Country = "US",
                IsInternational = false,
                Name = "My address name",
                Postal = "48139",
                StateProvinceRegion = "CT"
            };
            var httpAddressCreateResponse = await client.Addresses.Create(createAddressRequest);

            var createdAddressResponse = httpAddressCreateResponse.Entity as AddressResponse;

            var updateAddressRequest = new AddressRequest
            {
                AddressLine1 = "Edited address",
                AddressLine2 = "",
                City = "Boulder",
                Country = "US",
                ExternalIdentifier = createdAddressResponse.ExternalIdentifier,
                Identifier = createdAddressResponse.Identifier,
                IsInternational = false,
                Name = "My address name",
                PlatformIdentifier = createAddressRequest.PlatformIdentifier,
                Postal = "48139",
                StateProvinceRegion = "CO"
            };

            var updateResponse = client.Addresses.Update(updateAddressRequest).Result;
        }
    }
}