using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.EcommerceWebApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.EcommerceWebApp.Account
{
    [TestClass]
    public class SignInTests
    {
        [TestMethod]
        public async Task Get_AuthToken_Success()
        {
            ECommerceWebAppClient client = new ECommerceWebAppClient(ServiceConstants.AllPointsUrl);
            AuthCookieRequest request = new AuthCookieRequest
            {
                LoginEmail = "AllPointsTest2@dfs.com",
                LoginPassword = "1234"
            };

            var response = await client.Authentication.GetCookie(request);
            Assert.IsNotNull(response);
        }
    }
}