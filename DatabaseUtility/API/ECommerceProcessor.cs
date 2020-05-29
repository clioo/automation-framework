using DatabaseUtility.API.Models.Configuration;
using DatabaseUtility.Constants;
using DatabaseUtility.Models;
using DatabaseUtility.Services;
using System.Threading.Tasks;

namespace DatabaseUtility.API
{
    public class ECommerceProcessor
    {
        private string env;
        private ConfigurationService sConfig;
        public MembershipService sMember;
        private OrderCaptureService sOrder;

        public ECommerceProcessor(string connectionString)
        {
            env = connectionString;
            sConfig = ServiceGenerator<ConfigurationService>.GetInstance(ConfigurationConstants.ConfigurationDatabase, env);
            sMember = ServiceGenerator<MembershipService>.GetInstance(ConfigurationConstants.MembershipDatabase, env);
            sOrder = ServiceGenerator<OrderCaptureService>.GetInstance(ConfigurationConstants.OrderCaptureDatabase, env);
        }

        public async Task<User> CreateUserLogin(UserCreate user)
        {
            await sConfig.ClearLoginsByEmail(user.Email);
            var login = await sConfig.CreateLogin(user.Email);
            var accountMaster = await sMember.CreateAccountMaster("Softtek QA Test", user.IsPayingWithTerms, user.AccountMasterExternalIdentifier);
            var account = await sMember.CreateAccount(accountMaster.Contents.Identifier);
            var contact = await sMember.CreateContact(account.Contents.Identifier, "Sofftek", "Quality", "1278023", user.Email);
            var newUser = await sMember.CreateUser(account.Contents.Identifier, login.Contents.Identifier, contact.Contents.Identifier);

            return newUser;
        }

        public async Task ClearUserLoginByEmail(string email)
        {
            await sConfig.ClearLoginsByEmail(email);
        }
    }
}