using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.IntegrationsWebApp.Membership.Contacts;
using HttpUtiityTests.TestBase;
using HttpUtility.Clients;
using HttpUtility.Clients.Contracts;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.Contacts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Membership
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class ContactsEndpointTest : IntegrationsBaseTest<ContactTestData>
    {
        public ContactsEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {
            
        }

        [TestMethod]
        public async Task POST_Contact_Success()
        {
            string externalIdentifier = "postContactSuccess01";

            ContactTestData testData = new ContactTestData
            {
                ExternalIdentifier = externalIdentifier,
                AccountMasterExtId = "accountMasterExtId01"
            };

            //create an accountmaster first
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                CreateAccount = true,
                IsWebEnabled = true,
                CreatedBy = "post request 01",
                Name = "temporal AM"
            };
            var accountMasterResponse = await Client.AccountMasters.Create(accountMasterRequest);

            ContactRequest request = new ContactRequest
            {
                CreatedBy = "post request 01",
                ExternalIdentifier = externalIdentifier,
                AccountMasterExtId = accountMasterRequest.Identifier,
                ContactEmail = "fakeEmail@dfs.com",
                FirstName = "john",
                LastName = "doe",
                PhoneNumber = "1234567890"
            };

            HttpResponseExtended<ContactResponse> response = await Client.Contacts.Create(request);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //[TestMethod]
        //public async Task POST_Contact_Invalid() { }

        [TestMethod]
        public async Task GET_Contact_Success()
        {
            string contactExtId = "getContactSuccess01";

            ContactTestData testData = new ContactTestData
            {
                ExternalIdentifier = contactExtId,
                AccountMasterExtId = "getContactAMSuccess01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<ContactResponse> response = await Client.Contacts.GetSingle(contactExtId);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Contact_NotFound()
        {
            string externalIdentifier = "getContactFailed01";

            HttpResponseExtended<ContactResponse> response = await Client.Contacts.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task DELETE_Contact_Success()
        {
            string externalIdentifier = "deleteContactSuccess01";
            
            ContactTestData testData = new ContactTestData
            {
                ExternalIdentifier = externalIdentifier,
                AccountMasterExtId = "deleteContactAccountMaster01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpResponseExtended<ContactResponse> response = await Client.Contacts.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            //TODO
            //review this
            //Assert.IsNull(response.Result, "result object should be null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_Contact_NotFound()
        {
            string externalIdentifier = "deleteContactFailed01";

            HttpResponseExtended<ContactResponse> response = await Client.Contacts.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response, "Response object is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "result object should be null");
        }

        //TODO
        //[TestMethod]
        //public async Task UPDATE_Contact_Success() { }

        //TODO
        //[TestMethod]
        //public async Task UPDATE_Contact_NotFound() { }

        public override async Task TestScenarioCleanUp(ContactTestData testData)
        {
            await Client.Contacts.Remove(testData.ExternalIdentifier);
            await Client.AccountMasters.Remove(testData.AccountMasterExtId);
        }

        public override async Task TestScenarioSetUp(ContactTestData testData)
        {
            //create an accountmaster
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                CreateAccount = true,
                IsWebEnabled = true,
                CreatedBy = "post request 01",
                Name = "temporal AM"
            };
            await Client.AccountMasters.Create(accountMasterRequest);

            //create contact
            ContactRequest contactRequest = new ContactRequest
            {
                CreatedBy = "post request 01",
                ExternalIdentifier = testData.ExternalIdentifier,
                AccountMasterExtId = accountMasterRequest.Identifier,
                ContactEmail = "fakeEmail@dfs.com",
                FirstName = "john",
                LastName = "doe",
                PhoneNumber = "1234567890"
            };
            await Client.Contacts.Create(contactRequest);
        }
    }
}
