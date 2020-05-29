using AllPoints.Models;
using AllPoints.Tests.DataFactory;
using AllPoints.Tests.MyAccount.ContactInfo;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using System;

namespace AllPoints.Features.MyAccount.ContactInfo
{
    public class ContactInfoDataFactoryV2 : BaseDataFactory
    {
        public ContactInfoViewData UserContactCreate()
        {
            string accountMasterExternalId = "9509";
            string loginEmail = "allpointstest1@dfs.com";
            ContactInfoModel contactInfo = new ContactInfoModel
            {
                FirstName = "QA",
                LastName = "Automation",
                Company = "Softtek",
                Email = "softtek@email.com",
                PhoneNumber = "0987654"
            };

            //Create an Account Master and an Account asociated with it
            CreateAccountMasterRequest request = new CreateAccountMasterRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                CreatedUtc = DateTime.UtcNow,
                ExternalIdentifier = accountMasterExternalId,
                IsWebEnabled = true,
                Name = contactInfo.Company,
                PlatformIdentifier = new Guid(PlatformIdentifier),
                TermsConfiguration = new TermsConfiguration { HasPaymentTerms = true, TermsDescription = "40 Net Days, cool Stuff" }
            };

            var accountMasterResponse = IntegrationsClient.AccountMaster.Create(request).Result;

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
                ContactEmail = contactInfo.Email,
                Email = loginEmail,
                FirstName = contactInfo.FirstName,
                LastName = contactInfo.LastName,
                PlatformIdentifier = new Guid(PlatformIdentifier),
                PhoneNumber = contactInfo.PhoneNumber
            };
            var createLoginUserContactResponse = CustomerServiceClient.Logins.CreateContactUserLogin(createLoginUserContactRequest).Result;

            return new ContactInfoViewData
            {
                Email = loginEmail,
                Password = "1234",
                Contact = contactInfo                
            };
        }
    }
}