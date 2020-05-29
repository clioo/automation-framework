using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface ITestAddressesFactory
    {
        Task AddAccountAddress(string accountMasterExtId, TestAccountAddress address = null);
        Task RemoveAddress(string addressExtId);
    }
}
