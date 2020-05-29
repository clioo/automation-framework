using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface IUserAccountsFactory
    {
        TestUserAccount CreateTestUser(string usernameIdentifier);
        TestUserAccount CreateTestUser(TestUserAccount userAccount = null);
        void RemoveUserAccount(TestUserAccount testUser);
        void RemoveUserAccount(string testUserIdentifier);
        TestUserAccount CreateTestUserWithTerms(string termsDescription, TestUserAccount testUser = null);
    }
}
