using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface IAccountAddressesFactory
    {
        void CreateUserWithAccountAddress(TestAccountAddress accountAddress, string usernameIdentifier);
        void CreateUserWithAccountAddress(TestAccountAddress accountAddress, TestUserAccount usernameIdentifier);
        void CreateUserWithAccountAddresses(List<TestAccountAddress> accountAddresses, string usernameIdentifier);
        void CreateUserWithAccountAddresses(List<TestAccountAddress> accountAddresses, TestUserAccount usernameIdentifier);
    }
}
