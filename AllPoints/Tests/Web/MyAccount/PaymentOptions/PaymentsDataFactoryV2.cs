using AllPoints.Features.BaseTest.Constants;
using AllPoints.Models;
using AllPoints.Tests.Web.Base.DataFactory;
using HttpUtility.Clients;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace AllPoints.Tests.MyAccount.PaymentOptions
{
    public class PaymentsDataFactoryV2 : BaseDataFactory
    {
        public LoginModel UserWithNoTermsAsPaymentOption()
        {
            string email = "notermsuser@dfs.com";
            string accountMasterExternalId = "5372225";

            Processor.ClearUserLoginByEmail(email).Wait();

            //Create an Account Master and an Account asociated with it
            //CreateAccountMasterRequest request = new CreateAccountMasterRequest
            //{
            //    CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
            //    CreatedUtc = DateTime.UtcNow,
            //    ExternalIdentifier = accountMasterExternalId,
            //    IsWebEnabled = true,
            //    Name = "Softtek",
            //    PlatformIdentifier = new Guid(PlatformIdentifier),
            //    TermsConfiguration = new TermsConfiguration { HasPaymentTerms = false, TermsDescription = "40 Net Days, cool Stuff" }
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
                ContactEmail = "Testemail@domain.com",
                Email = email,
                FirstName = "Softtek",
                LastName = "Test User",
                PlatformIdentifier = new Guid(PlatformIdentifier),
                PhoneNumber = "123456789"
            };
            var createLoginUserContactResponse = CustomerServiceClient.Logins.CreateContactUserLogin(createLoginUserContactRequest).Result;

            return new LoginModel
            {
                Email = email,
                Password = "1234"
            };
        }
    }
}