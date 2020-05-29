using DatabaseUtility.API;
using FMPOnlineTests.Constants;
using FMPOnlineTests.Models;
using HttpUtility.Clients;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace FMPOnlineTests.DataFactory
{
    public class BaseDataFactory
    {
        protected string PlatformIdentifier = EnvironmentConstants.PlatformIdentifier;
        protected string PlatformExternalId = EnvironmentConstants.PlatformExternalId;
        protected string AppUrl = EnvironmentConstants.SiteUrl;
        protected ECommerceProcessor Processor;
        protected IntegrationsWebAppClient IntegrationsClientV1;
        protected CustomerServiceWebAppClient CustomerServiceClient;
        protected ECommerceWebAppClient EcommerceClient;

        public BaseDataFactory()
        {
            string mongoConnectionString = EnvironmentConstants.MongoConnectionString;
            string integrationsApiV1Url = FormatUrl(EnvironmentConstants.IntegrationsApiV1Url);
            string customerServiceApiUrl = FormatUrl(EnvironmentConstants.CustomerServiceUrl);
            bool integrationsHasHttps = EnvironmentConstants.IntegrationsApiV1Url.Contains("https");
            bool customerServiceHasHttps = EnvironmentConstants.CustomerServiceUrl.Contains("https");

            Processor = new ECommerceProcessor(mongoConnectionString);
            IntegrationsClientV1 = new IntegrationsWebAppClient(integrationsApiV1Url, PlatformIdentifier, integrationsHasHttps);
            CustomerServiceClient = new CustomerServiceWebAppClient(customerServiceApiUrl, @EnvironmentConstants.Username, EnvironmentConstants.UserPassword, customerServiceHasHttps);
            EcommerceClient = new ECommerceWebAppClient(AppUrl);
        }

        public LoginUser CreateLoginAccount(string email = "FMPtest@dfs.com", string accountExternalId = "5383244")
        {
            //Remove any account matching the given email
            Processor.ClearUserLoginByEmail(email).Wait();

            //Create an Account Master and an Account asociated with it
            //CreateAccountMasterRequest request = new CreateAccountMasterRequest
            //{
            //    CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
            //    CreatedUtc = DateTime.UtcNow,
            //    ExternalIdentifier = accountExternalId,
            //    IsWebEnabled = true,
            //    Name = "Softtek",
            //    PlatformIdentifier = new Guid(PlatformIdentifier),
            //    TermsConfiguration = new TermsConfiguration { HasPaymentTerms = true, TermsDescription = "40 Net Days, cools Stuff" }
            //};

            //var accountMasterResponse = IntegrationsClientV1.AccountMaster.Create(request).Result;

            //Get the Account created by the Above Method
            GetAccountAccountMasterRequest getAccountAccountMasterRequest = new GetAccountAccountMasterRequest
            {
                externalId = accountExternalId
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

            return new LoginUser
            {
                Email = email,
                Password = "1234"
            };
        }

        private string FormatUrl(string url)
        {
            if (url.Contains("https")) return url.Replace("https://", "");
            if (url.Contains("http")) return url.Replace("http://", "");
            return url;
        }
    }
}