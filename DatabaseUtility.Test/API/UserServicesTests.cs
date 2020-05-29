using DatabaseUtility.API;
using DatabaseUtility.API.Models.Configuration;
using DatabaseUtility.API.Models.Membership;
using DatabaseUtility.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Threading.Tasks;

namespace DatabaseUtility.Test.API
{
    [TestClass]
    public class UserServicesTests
    {
        private ECommerceProcessor processor;

        public UserServicesTests()
        {
            string mongoConnectionString = ConfigurationManager.AppSettings["MongoConnectionString"];
            
            processor = new ECommerceProcessor(mongoConnectionString);
        }

        [TestMethod]
        public async Task CreateBlankUserWithPaymentTerms()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKBlankUserWithTerms@dfs.com",
                IsPayingWithTerms = true
            };
            await processor.CreateUserLogin(createUser);
        }

        [TestMethod]
        public void CreateBlankUserWithNoTerms()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKBlankUserNoTerms@dfs.com"
            };

            var user = processor.CreateUserLogin(createUser).Result;
        }

        [TestMethod]
        public void CreateUserWithAddress()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKAddressUser@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createAddress = new AddressCreate
            {
                AddressLine1 = "Walnut Street",
                AddressLine2 = "07",
                StateProvinceRegion = "CO",
                City = "Denver",
                Country = "US",
                Postal = "12345",
                IsInternational = false,
                setAsDefault = false
            };
            newUser.AddAddress(processor.sMember, createAddress);
        }

        [TestMethod]
        public void CreateUserWithAddressAsDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKAddressUser@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createAddress = new AddressCreate
            {
                AddressLine1 = "Walnut Street",
                AddressLine2 = "07",
                StateProvinceRegion = "CO",
                City = "Denver",
                Country = "US",
                Postal = "12345",
                IsInternational = false,
                setAsDefault = true
            };
            newUser.AddAddress(processor.sMember, createAddress);
        }

        [TestMethod]
        public void CreateUserCardToken()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenUser@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "07",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "12345",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card token id",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = false,
                CompanyName = "QA test",
                TokenId = "GPLVRBI8"
            };
            newUser.AddCardToken(processor.sMember, createCardToken);
        }

        [TestMethod]
        public void CreateUserCardTokenAsDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenUser@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "07",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "12345",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card Default",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = true
            };
            newUser.AddCardToken(processor.sMember, createCardToken);
        }

        [TestMethod]
        public void CreateUserPayWithTerms()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKTermsUser@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createCardToken = new CardTokenCreate
            {
                CompanyName = "Company two",
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "07",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "12345",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = false,
                isAccountMasterLevel = true
            };
            newUser.AddCardToken(processor.sMember, createCardToken);
            newUser.PayWithTerms(processor.sMember);
        }

        [TestMethod]
        public void CreateUserPayWithTermsAndCardDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKTermsUser@dfs.com",
                IsPayingWithTerms = true
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createCardToken = new CardTokenCreate
            {
                CompanyName = "Company one",
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "07",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "12345",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = true,
                isAccountMasterLevel = true,
                TokenId = "12345"
            };
            newUser.AddCardToken(processor.sMember, createCardToken);
        }
    }
}