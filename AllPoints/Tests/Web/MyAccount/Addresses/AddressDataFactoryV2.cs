using AllPoints.Features.Models;
using AllPoints.Features.MyAccount.Addresses;
using AllPoints.Tests.Web.Base.DataFactory;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace AllPoints.Tests.MyAccount.Addresses
{
    public class AddressDataFactoryV2 : BaseDataFactory
    {
        public AddressViewData UserWithAccountAddress()
        {
            string accountMasterExternalId = "9509";
            string loginEmail = "accountaddress_allpoints@dfs.com";
            AddressModel accountAddress = new AddressModel
            {
                street = "Elm street",
                country = "US",
                CompanyName = "KDA",
                postal = "22780",
                city = "Boulder",
                state = "CO",
                apartment = "apt B"
            };

            Processor.ClearUserLoginByEmail(loginEmail).Wait();

            try
            {
                //Create an Account Master and an Account asociated with it
                //CreateAccountMasterRequest request = new CreateAccountMasterRequest
                //{
                //    CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                //    CreatedUtc = DateTime.UtcNow,
                //    ExternalIdentifier = accountMasterExternalId,
                //    IsWebEnabled = true,
                //    Name = "Softtek",
                //    PlatformIdentifier = new Guid(PlatformIdentifier),
                //    TermsConfiguration = new TermsConfiguration { HasPaymentTerms = true, TermsDescription = "40 Net Days, cool Stuff" }
                //};

                //var accountMasterResponse = IntegrationsClient.AccountMaster.Create(request).Result;

                //Get the Account created by the Above Method
                GetAccountAccountMasterRequest getAccountAccountMasterRequest = new GetAccountAccountMasterRequest
                {
                    externalId = accountMasterExternalId
                };
                var getAccountAccountMasterResponse = CustomerServiceClient.Logins.GetAccountByAccountMasterExternalId(getAccountAccountMasterRequest).Result;

                //Create a Login/User/Contact providing the AccountMaster and the Account Identifier
                CreateLoginUserContactRequest createLoginUserContactRequest = new CreateLoginUserContactRequest
                {
                    AccountIdentifier = getAccountAccountMasterResponse.Result.AccountIdentifier,
                    AccountMasterIdentifier = getAccountAccountMasterResponse.Result.AccountMasterIdentifier,
                    ConfirmPassword = "1234",
                    Password = "1234",
                    ContactEmail = "testemail@gmail.com",
                    Email = loginEmail,
                    FirstName = "test1",
                    LastName = "automation",
                    PlatformIdentifier = new Guid(PlatformIdentifier),
                    PhoneNumber = "1234567"
                };
                var createLoginUserContactResponse = CustomerServiceClient.Logins.CreateContactUserLogin(createLoginUserContactRequest).Result;

                //Add an account level address to the current user
                //CreateAddressRequest createAddressRequest = new CreateAddressRequest
                //{
                //    AddressLine = accountAddress.street,
                //    AddressLine2 = string.IsNullOrEmpty(accountAddress.apartment) ? "" : accountAddress.apartment,
                //    City = accountAddress.city,
                //    Country = accountAddress.country,
                //    Identifier = Guid.NewGuid().ToString(),
                //    Name = accountAddress.CompanyName,
                //    Postal = accountAddress.postal,
                //    State = accountAddress.state,
                //    OwnerExternalId = accountMasterExternalId
                //};
                //AddressResponse createResponse = IntegrationsClient.Address.Add(createAddressRequest).Result;

                return new AddressViewData
                {
                    Email = loginEmail,
                    Password = "1234",
                    Address = accountAddress
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
