using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.IntegrationsWebApp.Membership.Logins;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Membership
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class LoginsEndpointTest : IntegrationsBaseTest<LoginTestData>
    {
        public LoginsEndpointTest() : base (ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {
            
        }

        [TestMethod]
        public async Task POST_Login_Success()
        {
            string externalIdentifier = "postLoginSuccess01";

            LoginTestData testData = new LoginTestData
            {
                ExternalIdentifier = externalIdentifier
            };

            LoginRequest request = new LoginRequest
            {
                CreatedBy = "post success req",
                ExternalIdentifier = externalIdentifier,
                IsEnabled = true,
                UserName = "postLoginSuccess",
                Email = "postLoginSuccess@dfs.com",
                PasswordHash = "",//todo
                PasswordSalt = ""//todo
            };

            HttpResponseExtended<LoginResponse> response = await Client.Logins.Create(request);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result, "result object should not be null");
            Assert.IsTrue(response.Success, "Repsonse status is not successful");

            //clean the entity
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //[TestMethod]
        //public async Task POST_Login_Invalid()

        [TestMethod]
        public async Task GET_Login_Success()
        {
            string externalIdentifier = "getLoginSuccess01";

            //create the entity first
            LoginTestData testData = new LoginTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<LoginResponse> response = await Client.Logins.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //clean the entity
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Login_NotFound()
        {
            string externalIdentifier = "getLoginFailed01";

            HttpResponseExtended<LoginResponse> response = await Client.Logins.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task DELETE_Login_Success()
        {
            string externalIdentifier = "deleteLogin01";

            LoginTestData testData = new LoginTestData
            {
                ExternalIdentifier = externalIdentifier
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<LoginResponse> response = await Client.Logins.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            //TODO
            //result has the deleted entity
            //Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task DELETE_Login_NotFound()
        {
            string externalIdentifier = "deleteLoginFailed01";

            HttpResponseExtended<LoginResponse> response = await Client.Logins.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        //[TestMethod]
        //public async Task PUT_Login_Success()

        [TestMethod]
        public async Task PUT_Login_NotFound()
        {
            string externalIdentifier = "putLoginFailed01";

            LoginRequest request = new LoginRequest
            {
                CreatedBy = "post success req",
                ExternalIdentifier = externalIdentifier,
                IsEnabled = true,
                UserName = "postLoginSuccess",
                Email = "postLoginSuccess@dfs.com",
                PasswordHash = "",//todo
                PasswordSalt = ""//todo
            };

            HttpResponseExtended<LoginResponse> response = await Client.Logins.Update(externalIdentifier, request);

            //TODO
            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        public override async Task TestScenarioSetUp(LoginTestData testData)
        {
            var postRequest = new LoginRequest
            {
                CreatedBy = "post success req",
                ExternalIdentifier = testData.ExternalIdentifier,
                IsEnabled = true,
                UserName = "postLoginSuccess",
                Email = "postLoginSuccess@dfs.com",
                PasswordHash = "",//todo
                PasswordSalt = ""//todo
            };

            await Client.Logins.Create(postRequest);
        }

        public override async Task TestScenarioCleanUp(LoginTestData testData)
        {
            await Client.Logins.Remove(testData.ExternalIdentifier);
        }
    }
}
