using DatabaseUtility.Constants;
using DatabaseUtility.Models;
using DatabaseUtility.Models.OrderCapture;
using DatabaseUtility.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;

namespace DatabaseUtility.Test.Services
{
    [TestClass]
    public class GeneralProcessTest
    {
        private string env;
        private ConfigurationService sConfig;
        private MembershipService sMember;
        private OrderCaptureService sOrder;

        public GeneralProcessTest()
        {
            env = ConfigurationManager.AppSettings["MongoConnectionString"];
            sConfig = ServiceGenerator<ConfigurationService>.GetInstance(ConfigurationConstants.ConfigurationDatabase, env);
            sMember = ServiceGenerator<MembershipService>.GetInstance(ConfigurationConstants.MembershipDatabase, env);
            sOrder = ServiceGenerator<OrderCaptureService>.GetInstance(ConfigurationConstants.OrderCaptureDatabase, env);
        }

        [TestMethod]
        public void CreateUser()
        {
            var login = sConfig.CreateLogin("DevTestBlank3@Gmail.com").Result;
            var accountMaster = sMember.CreateAccountMaster("Softtek QA Test", true).Result;
            var account = sMember.CreateAccount(accountMaster.Contents.Identifier).Result;
            var contact = sMember.CreateContact(account.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var user = sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier).Result;
        }

        [TestMethod]
        public void CreateUserWithAddress()
        {
            var login = sConfig.CreateLogin("DevTestAddress@Gmail.com").Result;
            var accountMaster = sMember.CreateAccountMaster("Softtek QA Test", true).Result;
            var account = sMember.CreateAccount(accountMaster.Contents.Identifier).Result;
            var contact = sMember.CreateContact(account.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var user = sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier).Result;
            var newAddress = new AddressContent
            {
                AddressLine1 = "Walnut Street",
                AddressLine2 = "07",
                Name = "QA Softtek",
                StateProvinceRegion = "CO",
                City = "Denver",
                Country = "US",
                Postal = "12345",
                IsInternational = false,
                Owner = new Owner { Collection = "User", Identifier = user.Contents.Identifier }
            };
            var address = sMember.CreateAddress(newAddress).Result;
        }

        [TestMethod]
        public void CreateUserWithCardToken()
        {
            var login = sConfig.CreateLogin("DevTestCardToken@Gmail.com").Result;
            var accountMaster = sMember.CreateAccountMaster("Softtek QA Test", true).Result;
            var account = sMember.CreateAccount(accountMaster.Contents.Identifier).Result;
            var contact = sMember.CreateContact(account.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var user = sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier).Result;
            var cardTokenAddress = new CardTokenAddress
            {
                Name = "QA Softtek",
                AddressLine1 = "Walnut Street",
                AddressLine2 = "07",
                StateProvinceRegion = "CO",
                City = "Denver",
                Country = "US",
                Postal = "12345"
            };
            var newCardToken = new CardTokenContent("CardToken123")
            {
                Address = cardTokenAddress,
                CardType = "VISA",
                CustomerId = user.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card",
                Email = login.Contents.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                Owner = new Owner { Collection = "User", Identifier = user.Contents.Identifier }
            };
            var address = sMember.CreateCardToken(newCardToken).Result;
        }

        [TestMethod]
        public void CreateUserWithOrder()
        {
            var login = sConfig.CreateLogin("DevTestOrder@Gmail.com").Result;
            var accountMaster = sMember.CreateAccountMaster("Softtek QA Test", true).Result;
            var account = sMember.CreateAccount(accountMaster.Contents.Identifier).Result;
            var contact = sMember.CreateContact(account.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var user = sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier).Result;
            var newOrder = new OrderContent
            {
                ExternalIdentifier = "51492783",
                IsPayingWithTerms = true,
                WebOrderNumber = "1234",
                Owner = new Owner { Collection = "User", Identifier = user.Contents.Identifier }
            };
            var order = sOrder.CreateOrder(newOrder).Result;
        }

        [TestMethod]
        public void CreateUserWithShopper()
        {
            var login = sConfig.CreateLogin("DevTestShopper@Gmail.com").Result;
            var accountMaster = sMember.CreateAccountMaster("Softtek QA Test", true).Result;
            var account = sMember.CreateAccount(accountMaster.Contents.Identifier).Result;
            var contact = sMember.CreateContact(account.Contents.Identifier, "test", "test", "1278023", "test@gmail.com").Result;
            var user = sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier).Result;
            var newShopper = new ShopperContent
            {
                UserIdentifier = user.Contents.Identifier
            };
            var shopper = sOrder.CreateShopper(newShopper).Result;
        }
    }
}