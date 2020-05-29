using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.CustomerServiceWebApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HttpUtiityTests.CustomerServiceWebApp.Account
{
    [TestClass]
    public class CredentialTests
    {
        [TestMethod]
        public void CreateUserLoginContact()
        {
            CustomerServiceWebAppClient client = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);

            CreateLoginUserContactRequest createLoginUserContactRequest = new CreateLoginUserContactRequest
            {
                AccountIdentifier = new Guid("92de5f84-bb48-456b-b319-8e5b5dba3369"),
                AccountMasterIdentifier = new Guid("4c8577a9-b401-4dae-9570-1b47db6be529"),
                ConfirmPassword = "1234",
                Password = "1234",
                ContactEmail = "Testemail@domain.com",
                Email = "STKTestUser2@dfs.com",
                FirstName = "Softtek",
                LastName = "Test User",
                PlatformIdentifier = new Guid("55c4524c-5e8f-4b81-adf1-a2f1d38677ff"),
                PhoneNumber = "123456789"
            };
            var createLoginUserContactResponse = client.Logins.CreateContactUserLogin(createLoginUserContactRequest).Result;
            Assert.IsNotNull(createLoginUserContactResponse.Result);
            Assert.IsNotNull(createLoginUserContactResponse.Result.Contact);
            Assert.IsNotNull(createLoginUserContactResponse.Result.Login);
            Assert.IsNotNull(createLoginUserContactResponse.Result.User);
        }

        [TestMethod]
        public void GetAccountAccountMaster()
        {
            CustomerServiceWebAppClient client = new CustomerServiceWebAppClient(ServiceConstants.CustomerServiceUrl, ServiceConstants.Username, ServiceConstants.Password);

            GetAccountAccountMasterRequest getAccountAccountMasterRequest = new GetAccountAccountMasterRequest
            {
                externalId = "JhonsEXTid"
            };
            var getAccountAccountMasterResponse = client.Logins.GetAccountByAccountMasterExternalId(getAccountAccountMasterRequest).Result;
            Assert.IsNotNull(getAccountAccountMasterResponse.Result);
            Assert.IsNotNull(getAccountAccountMasterResponse.Result.AccountIdentifier);
        }
    }
}