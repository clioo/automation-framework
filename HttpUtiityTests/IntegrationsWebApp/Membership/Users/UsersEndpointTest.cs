using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.IntegrationsWebApp.Membership.Users;
using HttpUtiityTests.TestBase;
using HttpUtility.Clients;
using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Logins;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Membership
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class UsersEndpointTest : IntegrationsBaseTest<UserTestData>
    {
        public UsersEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {

        }

        [TestMethod]
        public async Task POST_User_Success()
        {
            string userExtId = "postUser01";

            UserTestData testData = new UserTestData
            {
                ExternalIdentifier = userExtId,
                ContactExtId = "postUserContact01",
                AccountMasterExtId = "postUserSuccess01",
                LoginExtId = "postUserLogin01"
            };

            //create an accountmaster
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                CreateAccount = true,
                IsWebEnabled = true,
                CreatedBy = "temporal request",
                Name = "temporal Account Master"
            };
            await Client.AccountMasters.Create(accountMasterRequest);

            //create login
            LoginRequest loginRequest = new LoginRequest
            {
                CreatedBy = "temporal req",
                ExternalIdentifier = testData.LoginExtId,
                IsEnabled = true,
                UserName = "postUserSuccess",
                Email = "postUserSuccess@dfs.com",
                PasswordHash = "",//todo
                PasswordSalt = ""//todo
            };
            await Client.Logins.Create(loginRequest);

            //create contact
            ContactRequest contactRequest = new ContactRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ContactExtId,
                AccountMasterExtId = accountMasterRequest.Identifier,
                ContactEmail = "temporalEmail@dfs.com",
                FirstName = "john",
                LastName = "doe",
                PhoneNumber = "1234567890"
            };
            await Client.Contacts.Create(contactRequest);

            UserRequest userRequest = new UserRequest
            {
                CreatedBy = "post request 01",
                AccountMasterExtId = testData.AccountMasterExtId,
                ContactExtId = testData.ContactExtId,
                ExternalIdentifier = userExtId,
                IsEnabled = true,
                LoginExtId = testData.LoginExtId
            };
            HttpResponseExtended<UserResponse> response = await Client.Users.Create(userRequest);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_User_Success()
        {
            string userExtId = "getUserSuccess01";

            UserTestData testData = new UserTestData
            {
                ExternalIdentifier = userExtId,
                AccountMasterExtId = "getUserAccountSuccess01",
                LoginExtId = "getUserLoginSuccess01",
                ContactExtId = "getUserContactSuccess01"
            };
            
            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<UserResponse> response = await Client.Users.GetSingle(userExtId);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_User_NotFound()
        {
            string externalIdentifier = "getUserFailed01";

            HttpResponseExtended<UserResponse> response = await Client.Users.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task DELETE_User_Success()
        {
            string userExtId = "deleteUserSuccess01";

            UserTestData testData = new UserTestData
            {
                ExternalIdentifier = userExtId,
                AccountMasterExtId = "deleteUserAccountSuccess01",
                ContactExtId = "deleteUserContactSuccess01",
                LoginExtId = "deleteUserLoginSuccess01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<UserResponse> response = await Client.Users.Remove(userExtId);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");

            //result object has the deleted entity
            //Assert.IsNull(response.Result, "result object should be null");

            //delete entity dependencies
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_User_NotFound()
        {
            string externalIdentifier = "deleteUserFailed01";

            HttpResponseExtended<UserResponse> response = await Client.Users.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        //[TestMethod]
        //public async Task PUT_User_Success()

        //[TestMethod]
        //public async Task PUT_User_NotFound()

        public override async Task TestScenarioSetUp(UserTestData testData)
        {
            //create an accountmaster
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                CreateAccount = true,
                IsWebEnabled = true,
                CreatedBy = "temporal request",
                Name = "temporal Account Master"
            };
            await Client.AccountMasters.Create(accountMasterRequest);

            //create login
            LoginRequest loginRequest = new LoginRequest
            {
                CreatedBy = "temporal req",
                ExternalIdentifier = testData.LoginExtId,
                IsEnabled = true,
                UserName = "postUserSuccess",
                Email = "postUserSuccess@dfs.com",
                PasswordHash = "",//todo
                PasswordSalt = ""//todo
            };
            await Client.Logins.Create(loginRequest);

            //create contact
            ContactRequest contactRequest = new ContactRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ContactExtId,
                AccountMasterExtId = accountMasterRequest.Identifier,
                ContactEmail = "tempEmail@dfs.com",
                FirstName = "john",
                LastName = "doe",
                PhoneNumber = "1234567890"
            };
            await Client.Contacts.Create(contactRequest);

            //create user
            UserRequest userRequest = new UserRequest
            {
                CreatedBy = "post request 01",
                AccountMasterExtId = testData.AccountMasterExtId,
                ContactExtId = testData.ContactExtId,
                ExternalIdentifier = testData.ExternalIdentifier,
                IsEnabled = true,
                LoginExtId = testData.LoginExtId
            };
            await Client.Users.Create(userRequest);
        }

        public override async Task TestScenarioCleanUp(UserTestData testData)
        {
            await Client.AccountMasters.Remove(testData.AccountMasterExtId);
            await Client.Logins.Remove(testData.LoginExtId);
            await Client.Contacts.Remove(testData.ContactExtId);
            await Client.Users.Remove(testData.ExternalIdentifier);
        }
    }
}
