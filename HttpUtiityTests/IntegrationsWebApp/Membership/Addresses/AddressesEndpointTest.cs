using System.Threading.Tasks;
using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.IntegrationsWebApp.Membership.Addresses;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using HttpUtility.EndPoints.IntegrationsWebApp.V1.Models.Addresses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpUtiityTests.IntegrationsWebApp.Addresses
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class AddressesEndpointTest : IntegrationsBaseTest<AddressTestData>
    {
        public AddressesEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {
        }

        [TestMethod]
        public async Task POST_Address_Success()
        {
            //test scenario setup
            AddressTestData testData = new AddressTestData
            {
                ExternalIdentifier = "postAddress01",
                AccountMasterExtId = "postAddressAccountMaster01"
            };
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                CreateAccount = false,
                Name = "temporal account master",
                IsWebEnabled = true,
                CreatedBy = "temporal request"
            };
            //create accountmaster
            await Client.AccountMasters.Create(accountMasterRequest);

            AddressRequest addressRequest = new AddressRequest
            {
                AddressLine = "Elm street, 23",
                AddressLine2 = "",
                City = "boulder",
                Country = "US",
                Identifier = testData.ExternalIdentifier,
                Name = "Temporal name",
                Postal = "28790",
                State = "colorado"
            };

            AddressResponse response = await Client.Addresses.Create(testData.AccountMasterExtId, addressRequest);

            Assert.IsNotNull(response, "Response should not be null");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PUT_Address_Success()
        {
            AddressTestData testData = new AddressTestData
            {
                ExternalIdentifier = "putAddress01",
                AccountMasterExtId = "putAddressAccountMaster01"
            };
            await TestScenarioSetUp(testData);

            AddressRequest addressRequest = new AddressRequest
            {
                AddressLine = "address updated",
                AddressLine2 = "1212",
                City = "denver",
                Country = "US",
                Name = "name updated",
                Postal = "0987654321",
                State = "co"
            };

            AddressResponse response = await Client.Addresses.Update(testData.ExternalIdentifier, addressRequest);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");

            //specific validations
            Assert.AreEqual(addressRequest.AddressLine, response.AddressLine);
            Assert.AreEqual(addressRequest.Name, response.Name);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //when not found the response is a string
        //[TestMethod]
        //public async Task PUT_Address_NotFound()

        [TestMethod]
        public async Task DELETE_Address_Success()
        {
            //test scenario setup
            AddressTestData testData = new AddressTestData
            {
                ExternalIdentifier = "deleteAddress01",
                AccountMasterExtId = "deleteAddressAccountMaster01"
            };
            await TestScenarioSetUp(testData);

            AddressResponse response = await Client.Addresses.Remove(testData.ExternalIdentifier);

            //validations
            Assert.IsNull(response, "Response object is not null");

            //test scenario does not need to clean up
        }

        //TODO
        //when not found the response is a string
        //[TestMethod]
        //public async Task DELETE_Address_NotFound()

        public override async Task TestScenarioSetUp(AddressTestData testData)
        {
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Identifier = testData.AccountMasterExtId,
                Name = "temporal request helper",
                CreateAccount = false,
                IsWebEnabled = true,
                CreatedBy = "temporal request",
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "Net 15 days"
                }
            };
            AddressRequest addressRequest = new AddressRequest
            {
                Identifier = testData.ExternalIdentifier,
                Name = "temporal address",
                AddressLine = "elm street",
                AddressLine2 = "1",
                City = "denver",
                Country = "US",
                State = "Colorado",
                Postal = "1234567890"
            };

            await Client.AccountMasters.Create(accountMasterRequest);
            await Client.Addresses.Create(testData.AccountMasterExtId, addressRequest);
        }

        public override async Task TestScenarioCleanUp(AddressTestData testData)
        {
            await Client.AccountMasters.Remove(testData.AccountMasterExtId);
            await Client.Addresses.Remove(testData.ExternalIdentifier);
        }
    }
}
