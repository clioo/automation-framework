using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface ITestUserAccountsFactory
    {
        Task<TestUserAccount> CreateUserAccount(TestUserAccount createUserRequest = null);
        Task AddTermsToAccount(string accountExtId, string termsDescription);
        Task RemoveUserAccount(TestExternalIdentifiers userExternals = null);
    }
}
