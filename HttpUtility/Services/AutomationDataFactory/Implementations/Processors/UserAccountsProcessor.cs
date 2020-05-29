using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Users;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class UserAccountsProcessor
    {
        readonly IIntegrationsWebAppClient _client;

        public UserAccountsProcessor(IIntegrationsWebAppClient client)
        {
            _client = client;
        }

        public async Task<TestUserAccount> CreateDefaultUserAccount()
        {
            string externalIdentifier = "defaultUser-123";
            var userAccount = new TestUserAccount
            {
                Email = "defaultuser@dfs.com",
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
                    LoginExtId = externalIdentifier,
                    AccountMasterExtId = externalIdentifier,
                    ContactExtId = externalIdentifier,
                    UserExtId = externalIdentifier
                }
            };

            //does not need to validate test user data
            //clear all account related data
            await RemoveUserAccount(userAccount.AccountExternalIds);

            await CreateUserFullPath(userAccount);

            return userAccount;
        }

        public async Task<TestUserAccount> CreateUserAccount(TestUserAccount userAccount)
        {
            //validate user data
            ValidateUserData(userAccount);
            //clear all account related data
            await RemoveUserAccount(userAccount.AccountExternalIds);
            await CreateUserFullPath(userAccount);

            return userAccount;
        }

        public async Task<TestUserAccount> CreateUserAccount(string userIdentifier)
        {
            var userAccount = new TestUserAccount
            {
                Email = userIdentifier + "@dfs.com",
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
                    LoginExtId = userIdentifier,
                    AccountMasterExtId = userIdentifier,
                    ContactExtId = userIdentifier,
                    UserExtId = userIdentifier
                }
            };

            //validate user data
            if (string.IsNullOrEmpty(userIdentifier) || string.IsNullOrWhiteSpace(userIdentifier))
                throw new ArgumentException($"{nameof(userIdentifier)} cannot be empty or null");

            //clear all account related data
            await RemoveUserAccount(userAccount.AccountExternalIds);
            await CreateUserFullPath(userAccount);

            return userAccount;
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

        public async Task<TestUserAccount> CreateDefaultUserWithTerms(string termsDescription)
        {
            string externalIdentifier = "accountTerm01-abc";

            var userAccount = new TestUserAccount
            {
                Email = "defaultAccountTerm@mail.com",
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = externalIdentifier,
                    ContactExtId = externalIdentifier,
                    LoginExtId = externalIdentifier,
                    UserExtId = externalIdentifier
                },
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "DFS",
                    Email = "allpoints@mail.com",
                    FirstName = "firstName",
                    LastName = "lastName",
                    PhoneNumber = "0123456789"
                }
            };

            //basic term validation
            if (string.IsNullOrEmpty(termsDescription) || string.IsNullOrWhiteSpace(termsDescription))
                throw new ArgumentException($"{nameof(termsDescription)} should not be empty or null");

            //clear all user account related data
            await RemoveUserAccount(userAccount.AccountExternalIds);
            //TODO
            //remove addresses

            await CreateUserFullPath(userAccount);

            //add terms
            await AddTermsToUserAccount(termsDescription, userAccount);

            return userAccount;
        }

        public async Task<TestUserAccount> CreateUserWithTerms(string termsDescription, TestUserAccount userAccount)
        {
            //basic term validation
            if (string.IsNullOrEmpty(termsDescription) || string.IsNullOrWhiteSpace(termsDescription))
                throw new ArgumentException($"{nameof(termsDescription)} should not be empty or null");

            //clear all user account related data
            await RemoveUserAccount(userAccount.AccountExternalIds);
            //TODO
            //remove addresses
            //remove payments?

            //create user account
            await CreateUserFullPath(userAccount);
            //add terms
            await AddTermsToUserAccount(termsDescription, userAccount);

            return userAccount;
        }

        //generic helper methods
        private void ValidateUserData(TestUserAccount user)
        {
            bool StringNullCheck(string value) => (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value));
            void ExceptionHandler(string entityName) => throw new Exception($"{entityName} has no value");

            //login email validation
            if (StringNullCheck(user.Username))
                ExceptionHandler(nameof(user.Username));

            //account external identifiers validations
            if (user.AccountExternalIds == null)
                ExceptionHandler(nameof(user.AccountExternalIds));

            if (StringNullCheck(user.AccountExternalIds.AccountMasterExtId))
                ExceptionHandler(nameof(user.AccountExternalIds.AccountMasterExtId));

            //contact information validations
            if (user.ContactInformation == null)
                ExceptionHandler(nameof(user.ContactInformation));

            if (StringNullCheck(user.ContactInformation.CompanyName))
                ExceptionHandler(nameof(user.ContactInformation.CompanyName));

            if (StringNullCheck(user.ContactInformation.FirstName))
                ExceptionHandler(nameof(user.ContactInformation.FirstName));

            if (StringNullCheck(user.ContactInformation.LastName))
                ExceptionHandler(nameof(user.ContactInformation.LastName));

            if (StringNullCheck(user.ContactInformation.Email))
                ExceptionHandler(nameof(user.ContactInformation.Email));

            if (StringNullCheck(user.ContactInformation.PhoneNumber))
                ExceptionHandler(nameof(user.ContactInformation.PhoneNumber));
        }

        private async Task CreateUserFullPath(TestUserAccount userAccountData)
        {
            //create account and accountmaster
            AccountMasterRequest createAccountMasterRequest = new AccountMasterRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                Identifier = userAccountData.AccountExternalIds.AccountMasterExtId,
                Name = userAccountData.ContactInformation.CompanyName,
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
                ExternalIdentifier = userAccountData.AccountExternalIds.ContactExtId,
                AccountMasterExtId = userAccountData.AccountExternalIds.AccountMasterExtId,
                ContactEmail = userAccountData.ContactInformation.Email,
                FirstName = userAccountData.ContactInformation.FirstName,
                LastName = userAccountData.ContactInformation.LastName,
                PhoneNumber = userAccountData.ContactInformation.PhoneNumber
            };
            var createContactResponse = await _client.Contacts.Create(contactRequest);

            if (!createContactResponse.Success)
                throw new Exception("Test contact cannot be created, please check the given contact info. data");

            //create login
            LoginRequest createLoginRequest = new LoginRequest
            {
                CreatedBy = System.Security.Principal.WindowsIdentity.GetCurrent().Name,
                ExternalIdentifier = userAccountData.AccountExternalIds.LoginExtId,
                IsEnabled = true,
                UserName = userAccountData.Username,
                Email = userAccountData.Email,
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
                AccountMasterExtId = userAccountData.AccountExternalIds.AccountMasterExtId,
                ContactExtId = userAccountData.AccountExternalIds.ContactExtId,
                ExternalIdentifier = userAccountData.AccountExternalIds.UserExtId,
                IsEnabled = true,
                LoginExtId = userAccountData.AccountExternalIds.LoginExtId
            };
            var createUserResponse = await _client.Users.Create(userRequest);

            if (!createUserResponse.Success)
                throw new Exception($"Test user cannot be created, check user post endpoint method");
        }

        private async Task AddTermsToUserAccount(string termsDescription, TestUserAccount userAccountData)
        {
            var request = new
            {
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = termsDescription
                }
            };

            var response = await _client.AccountMasters.PatchEntity(userAccountData.AccountExternalIds.AccountMasterExtId, request);

            if (!response.Success)
                throw new Exception("Cannot add terms to the given account, " + userAccountData.AccountExternalIds.AccountMasterExtId);
        }
    }
}
