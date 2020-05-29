using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Users;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class TestUserAccountsFactory : ITestUserAccountsFactory
    {
        readonly IIntegrationsWebAppClient _client;

        public TestUserAccountsFactory(IIntegrationsWebAppClient client)
        {
            _client = client;
        }

        public async Task AddTermsToAccount(string accountMasterExtId, string termsDescription)
        {
            var request = new
            {
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = termsDescription
                }
            };

            var response = await _client.AccountMasters.PatchEntity(accountMasterExtId, request);

            if (!response.Success)
                throw new Exception("Cannot add terms to the given account, " + accountMasterExtId);
        }

        public async Task<TestUserAccount> CreateUserAccount(TestUserAccount createUserRequest = null)
        {
            //default account
            var testUserAccount = createUserRequest ?? new TestUserAccount
            {
                Email = "testuser@dfs.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "SDET",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "9876543210",
                    Email = "company@mail.com"
                },
                AccountExternalIds = new TestExternalIdentifiers
                {
                    LoginExtId = "testLogin-123",
                    AccountMasterExtId = "testAccountMaster-123",
                    ContactExtId = "testContact-123",
                    UserExtId = "testUser-123"
                }
            };

            //validate given data
            if (createUserRequest != null)
                ValidateUserData(createUserRequest);

            //clear all account related data
            await RemoveUserAccount(testUserAccount.AccountExternalIds);

            //create account and accountmaster
            AccountMasterRequest createAccountMasterRequest = new AccountMasterRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Identifier = testUserAccount.AccountExternalIds.AccountMasterExtId,
                Name = testUserAccount.ContactInformation.CompanyName,
                CreateAccount = true,
                IsWebEnabled = true
            };
            var createAccountMasterResponse = await _client.AccountMasters.Create(createAccountMasterRequest);

            if (!createAccountMasterResponse.Success)
                throw new Exception($"Test user cannot be created, check accountmaster creation process ({createAccountMasterRequest.Identifier})");

            //create contact
            ContactRequest contactRequest = new ContactRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                ExternalIdentifier = testUserAccount.AccountExternalIds.ContactExtId,
                AccountMasterExtId = testUserAccount.AccountExternalIds.AccountMasterExtId,
                ContactEmail = testUserAccount.ContactInformation.Email,
                FirstName = testUserAccount.ContactInformation.FirstName,
                LastName = testUserAccount.ContactInformation.LastName,
                PhoneNumber = testUserAccount.ContactInformation.PhoneNumber
            };
            var createContactResponse = await _client.Contacts.Create(contactRequest);

            if (!createContactResponse.Success)
                throw new Exception("Test contact cannot be created, please check the given contact info. data");

            //create login
            LoginRequest createLoginRequest = new LoginRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                ExternalIdentifier = testUserAccount.AccountExternalIds.LoginExtId,
                IsEnabled = true,
                UserName = testUserAccount.Username,
                Email = testUserAccount.Email,
                PasswordHash = "E95FBFCD1C6EB3CA80DCE1F656633017",//password -> 1234
                PasswordSalt = "xXJnoJE="//password -> 1234
            };
            var loginResponse = await _client.Logins.Create(createLoginRequest);

            if (!loginResponse.Success)
                throw new Exception("Test login cannot be created, check logins endpoint method");

            //create user
            UserRequest userRequest = new UserRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                AccountMasterExtId = testUserAccount.AccountExternalIds.AccountMasterExtId,
                ContactExtId = testUserAccount.AccountExternalIds.ContactExtId,
                ExternalIdentifier = testUserAccount.AccountExternalIds.UserExtId,
                IsEnabled = true,
                LoginExtId = testUserAccount.AccountExternalIds.LoginExtId
            };
            var createUserResponse = await _client.Users.Create(userRequest);

            if (!createUserResponse.Success)
                throw new Exception($"Test user cannot be created, check user post endpoint method");

            return testUserAccount;
        }

        public async Task RemoveUserAccount(TestExternalIdentifiers userExternals)
        {
            //remove login
            await _client.Logins.Remove(userExternals.LoginExtId);
            //remove accountmaster
            await _client.AccountMasters.Remove(userExternals.AccountMasterExtId);
            //remove user
            await _client.Users.Remove(userExternals.UserExtId);
            //remove contact info
            await _client.Contacts.Remove(userExternals.ContactExtId);
        }

        private void ValidateUserData(TestUserAccount userData)
        {
            bool StringNullCheck(string value) => (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
            void ExceptionHandler(string entityName) => throw new Exception($"{entityName} has no value");

            //login email validation
            if (StringNullCheck(userData.Username))
                ExceptionHandler(nameof(userData.Username));

            //account external identifiers validations
            if (userData.AccountExternalIds == null)
                ExceptionHandler(nameof(userData.AccountExternalIds));

            if (StringNullCheck(userData.AccountExternalIds.AccountMasterExtId))
                ExceptionHandler(nameof(userData.AccountExternalIds.AccountMasterExtId));

            //contact information validations
            if (userData.ContactInformation == null)
                ExceptionHandler(nameof(userData.ContactInformation));

            if (StringNullCheck(userData.ContactInformation.CompanyName))
                ExceptionHandler(nameof(userData.ContactInformation.CompanyName));

            if (StringNullCheck(userData.ContactInformation.FirstName))
                ExceptionHandler(nameof(userData.ContactInformation.FirstName));

            if (StringNullCheck(userData.ContactInformation.LastName))
                ExceptionHandler(nameof(userData.ContactInformation.LastName));

            if (StringNullCheck(userData.ContactInformation.Email))
                ExceptionHandler(nameof(userData.ContactInformation.Email));

            if (StringNullCheck(userData.ContactInformation.PhoneNumber))
                ExceptionHandler(nameof(userData.ContactInformation.PhoneNumber));
        }
    }
}
