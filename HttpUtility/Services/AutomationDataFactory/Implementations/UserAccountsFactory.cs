using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class UserAccountsFactory : IUserAccountsFactory
    {
        readonly UserAccountsProcessor _processor;
        //TODO
        //readonly AccountAddressesProcessor _addressesProcessor;

        public UserAccountsFactory(UserAccountsProcessor processor)
        {
            _processor = processor;
        }

        public TestUserAccount CreateTestUser(string usernameId)
        {
            return _processor.CreateUserAccount(usernameId).Result;
        }

        public TestUserAccount CreateTestUser(TestUserAccount testUser = null)
        {
            //create default user acount
            if (testUser == null)
                return _processor.CreateDefaultUserAccount().Result;

            //create an explicit user
            return _processor.CreateUserAccount(testUser).Result;
        }

        public void RemoveUserAccount(string testUserIdentifier)
        {
            var userAccountIds = new TestExternalIdentifiers
            {
                AccountMasterExtId = testUserIdentifier,
                ContactExtId = testUserIdentifier,
                LoginExtId = testUserIdentifier,
                UserExtId = testUserIdentifier
            };
            _processor.RemoveUserAccount(userAccountIds).Wait();
            //TODO
            //remove related addresses
        }

        public void RemoveUserAccount(TestUserAccount testUser)
        {
            _processor.RemoveUserAccount(testUser.AccountExternalIds).Wait();
            //TODO
            //Addresses.RemoveAddresses
        }

        //terms related
        public TestUserAccount CreateTestUserWithTerms(string termsDescription, TestUserAccount testUser = null)
        {
            if (testUser == null)
                return _processor.CreateDefaultUserWithTerms(termsDescription).Result;

            return _processor.CreateUserWithTerms(termsDescription, testUser).Result;
        }

        //TODO
        //addresses related
        //public TestUserAccount CreateTestUserWithAccountAddress()        
    }
}
