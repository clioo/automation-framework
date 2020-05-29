using AllPoints.Tests.Web.Base.DataFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllPoints.Tests.Web.Cart
{
    public class CartDataFactory : BaseDataFactory
    {
        //DEPRECATED
        //public CartTestData CreateBlankUser()
        //{
        //    var createUser = new UserCreate
        //    {
        //        AccountMasterExternalIdentifier = "5372226",
        //        Email = "CotestUser4@dfs.com"
        //    };
        //    var newUser = processor.CreateUserLogin(createUser).Result;

        //    var viewData = new CartTestData
        //    {
        //        Email = createUser.Email,
        //        Password = "1234",
        //    };

        //    return viewData;
        //}
        /*
        public CartTestData CreateUserWithAddress()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CotestUserAddress5@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createAddress = new AddressCreate
            {
                AddressLine1 = "Walnut Street",
                AddressLine2 = "07",
                Name = "QA Softtek",
                StateProvinceRegion = "CO",
                City = "Denver",
                Country = "US",
                Postal = "12345",
                IsInternational = false,
                setAsDefault = false,
                IsAccountMasterLevel = false
            };

            newUser.AddAddress(processor.sMember, createAddress);
            var viewData = new CartTestData
            {
                Email = createUser.Email,
                Password = "1234",
            };

            return viewData;
        }

        public PaymentsTestData CreateUserWitPayment()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CotestUserPayment@dfs.com"
            };
            var newUser = processor.CreateUserLogin(createUser).Result;
            var createCardToken = new CardTokenCreate
            {
                CompanyName = "QA Colorado",
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "24",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "23480",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 26,
                NameOnCard = "Testing Credit Card",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = false
            };
            newUser.AddCardToken(processor.sMember, createCardToken);

            var viewData = new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234",
                PaymentOption = new PaymentOptionModel
                {
                    LastFourDigits = createCardToken.LastFourDigits,
                    ExpirationMont = createCardToken.ExpirationMonth.ToString(),
                    ExpirationYear = createCardToken.ExpirationYear.ToString()
                }
            };

            return viewData;
        }
        */
    }
}
