using AllPoints.Features.BaseTest.Constants;
using AllPoints.Models;
using DatabaseUtility;
using DatabaseUtility.API;
using HttpUtility.Clients;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using System;

namespace AllPoints.Tests.DataFactory
{
    public abstract class BaseDataFactory
    {
        protected string PlatformIdentifier = EnvironmentConstants.PlatformIdentifier;
        protected string PlatformName = EnvironmentConstants.PlatformName;
        protected string WebAppUrl = EnvironmentConstants.HostUrl;

        //TODO
        //deprecate this client
        protected ECommerceProcessor Processor;

        protected IntegrationsWebAppClient IntegrationsClient;
        protected CustomerServiceWebAppClient CustomerServiceClient;
        protected ECommerceWebAppClient EcommerceClient;

        public BaseDataFactory()
        {
            string mongoConnectionString = EnvironmentConstants.MongoConnectionString;

            string integrationsApiV2Url = FormatUrl(EnvironmentConstants.IntegrationsApiUrl);
            string customerServiceApiUrl = FormatUrl(EnvironmentConstants.CustomerServiceUrl);

            bool integrationsHasHttps = EnvironmentConstants.IntegrationsApiUrl.Contains("https");
            bool customerServiceHasHttps = EnvironmentConstants.CustomerServiceUrl.Contains("https");

            Processor = new ECommerceProcessor(mongoConnectionString);
            IntegrationsClient = new IntegrationsWebAppClient(integrationsApiV2Url, PlatformName, PlatformIdentifier, integrationsHasHttps);
            CustomerServiceClient = new CustomerServiceWebAppClient(customerServiceApiUrl, @EnvironmentConstants.Username, EnvironmentConstants.UserPassword, customerServiceHasHttps);

            EcommerceClient = new ECommerceWebAppClient(WebAppUrl);
        }

        public LoginModel CreateLoginAccount(string email = "allpointstest@dfs.com", string accountExternalId = "1234")
        {
            //Remove any account matching the given email
            Processor.ClearUserLoginByEmail(email).Wait();

            string accountMasterExternalId = accountExternalId;
            string loginEmail = email;

            //Create an Account Master and an Account asociated with it
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                CreateAccount = true,
                UseAccountTermsAsDefaultPayment = true,
                TaxIdentifier = "asd",
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                ExternalData = "nada",
                GroupIdentifier = "asd",
                Identifier = accountExternalId,
                IsWebEnabled = true,
                Name = "Softtek http test",
                ProductPriceDiscount = 105,
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "29 days ;)"
                }
            };

            var resp = IntegrationsClient.AccountMasters.Create(accountMasterRequest).Result;

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
                Email = loginEmail,
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

        private string FormatUrl(string url)
        {
            if(url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }
    }
}
