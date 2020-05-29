using AllPoints.Features.BaseTest;
using AllPoints.Features.Models;
using AllPoints.TestDataModels;
using DatabaseUtility.API;
using DatabaseUtility.API.Models.Configuration;
using DatabaseUtility.API.Models.Membership;

namespace AllPoints.Features.MyAccount.PaymentOptions
{
    public class PaymentsDataFactory : DataFactoryBase
    {
        public PaymentsTestData AccountCardTokenView()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenAccount@dfs.com",
                IsPayingWithTerms = false
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            var cardTokenCreate = new CardTokenCreate
            {
                CompanyName = "QA Softtek",
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "07",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Denver",
                AddressCountry = "US",
                AddressPostal = "12345",
                CardType = "VISA",
                ExpirationMonth = 12,
                ExpirationYear = 22,
                NameOnCard = "Test Card",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = false,
                isAccountMasterLevel = true,
                TokenId = "fakeAccount1"
            };

            newUser.AddCardToken(processor.sMember, cardTokenCreate);

            return new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234"
            };
        }

        public PaymentsTestData NoAccountCardTokenView()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenAccount@dfs.com",
                IsPayingWithTerms = false
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            return new PaymentsTestData { Email = createUser.Email, Password = "1234" };
        }

        public PaymentsTestData AccountCardTokenSetDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenAccount1@dfs.com",
                IsPayingWithTerms = false
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate()
            {
                CompanyName = "QA Softtek",
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
                NameOnCard = "Test Card for automation",
                Email = createUser.Email,
                LastFourDigits = "1111",
                IsReadonly = false,
                setAsDefault = false,
                isAccountMasterLevel = true,
                TokenId = "fakeToken"
            };
            newUser.AddCardToken(processor.sMember, createCardToken);

            return new PaymentsTestData
            {
                Email = createUser.Email,
                Level = "user",
                Password = "1234",
                PaymentOption = new PaymentOptionModel
                {
                    CardNumber = createCardToken.LastFourDigits,
                    ExpirationMont = createCardToken.ExpirationMonth.ToString(),
                    ExpirationYear = createCardToken.ExpirationYear.ToString()
                }
            };
        }

        public PaymentsTestData UserHasTermsView()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKTermAsPaymentView@dfs.com",
                IsPayingWithTerms = true
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            return new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234"
            };
        }

        public PaymentsTestData UserCardTokenSetDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "STKCardTokenUserDef@dfs.com",
                IsPayingWithTerms = true
            };
            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                CompanyName = "QA Softtek",
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
                setAsDefault = false
            };
            newUser.AddCardToken(processor.sMember, createCardToken);

            return new PaymentsTestData
            {
                Email = createUser.Email,
                Level = "user",
                Password = "1234",
                PaymentOption = new PaymentOptionModel
                {
                    CardNumber = createCardToken.LastFourDigits,
                    ExpirationMont = createCardToken.ExpirationMonth.ToString(),
                    ExpirationYear = createCardToken.ExpirationYear.ToString(),
                    LastFourDigits = createCardToken.LastFourDigits,
                    HolderName = createCardToken.NameOnCard
                },
                PreviouslyStoredAddress = new AddressModel
                {
                    postal = createCardToken.AddressPostal,
                    CompanyName = createCardToken.CompanyName
                }
            };
        }

        public PaymentsTestData UserPrevStoredAddressAdd()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CardTokenUserAdd@dfs.com"
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

            var viewData = new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234",
                Level = "user",
                PreviouslyStoredAddress = new AddressModel
                {
                    apartment = createAddress.AddressLine2,
                    city = createAddress.City,
                    country = createAddress.Country,
                    postal = createAddress.Postal,
                    state = createAddress.StateProvinceRegion,
                    street = createAddress.AddressLine1
                }
            };

            return viewData;
        }

        public PaymentsTestData UserCardTokenAdd()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CardTokenUserAddNew@dfs.com"
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            var viewData = new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234",
                Level = "user"
            };

            return viewData;
        }

        public PaymentsTestData UserCardTokenDelete()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CardTokenUserDelete@dfs.com"
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                CompanyName = "QA Softtek",
                AddressAddressLine1 = "Walnut Street",
                AddressAddressLine2 = "10",
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
                TokenId = "trash"
            };
            newUser.AddCardToken(processor.sMember, createCardToken);

            var viewData = new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234",
                Level = "user",
                PaymentOption = new PaymentOptionModel
                {
                    LastFourDigits = createCardToken.LastFourDigits,
                    ExpirationMont = createCardToken.ExpirationMonth.ToString(),
                    ExpirationYear = createCardToken.ExpirationYear.ToString()
                }
            };

            return viewData;
        }

        public PaymentsTestData UserCardTokenEditCheckDefault()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CardTokenUserCheckDefault@dfs.com"
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                CompanyName = "QA Softtek",
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
                NameOnCard = "Test Card",
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

        public PaymentsTestData UserHasPreviouslyStoredIntlAddress()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "CardTokenUserIntlAddress@dfs.com"
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            var createCardToken = new CardTokenCreate
            {
                CompanyName = "UABC",
                AddressAddressLine1 = "carretera transpeninsular",
                AddressAddressLine2 = "",
                AddressStateProvinceRegion = "CO",
                AddressCity = "Ensenada",
                AddressCountry = "MX",
                AddressPostal = "22860",
                CardType = "VISA",
                CustomerId = newUser.Contents.Identifier.ToString(),
                ExpirationMonth = 12,
                ExpirationYear = 26,
                NameOnCard = "Test Card",
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

        public PaymentsTestData UserAccountManyItems()
        {
            var createUser = new UserCreate
            {
                AccountMasterExternalIdentifier = "5372226",
                Email = "userWithMany@dfs.com"
            };

            var newUser = processor.CreateUserLogin(createUser).Result;

            for (int i = 0; i <= 28; i++)
            {
                //add user level payments
                newUser.AddCardToken(processor.sMember, new CardTokenCreate
                {
                    CompanyName = "QA Softtek " + i,
                    AddressAddressLine1 = "Walnut Street ",
                    AddressAddressLine2 = i.ToString(),
                    AddressStateProvinceRegion = "CO",
                    AddressCity = "Denver",
                    AddressCountry = "US",
                    AddressPostal = "23480",
                    CardType = "VISA",
                    CustomerId = newUser.Contents.Identifier.ToString(),
                    ExpirationMonth = 12,
                    ExpirationYear = 25,
                    NameOnCard = "Test Card",
                    Email = createUser.Email,
                    LastFourDigits = "1111",
                    IsReadonly = false,
                    setAsDefault = false,
                    isAccountMasterLevel = false
                });

                //add account level payments
                newUser.AddCardToken(processor.sMember, new CardTokenCreate
                {
                    CompanyName = "QA Softtek " + i,
                    AddressAddressLine1 = "Walnut Street ",
                    AddressAddressLine2 = i.ToString(),
                    AddressStateProvinceRegion = "CO",
                    AddressCity = "Denver",
                    AddressCountry = "US",
                    AddressPostal = "23480",
                    CardType = "VISA",
                    CustomerId = newUser.Contents.Identifier.ToString(),
                    ExpirationMonth = 12,
                    ExpirationYear = 25,
                    NameOnCard = "Test Card " + i,
                    Email = createUser.Email,
                    LastFourDigits = "1111",
                    IsReadonly = false,
                    setAsDefault = false,
                    isAccountMasterLevel = true
                });
            }

            var data = new PaymentsTestData
            {
                Email = createUser.Email,
                Password = "1234"
            };

            return data;
        }
    }
}
