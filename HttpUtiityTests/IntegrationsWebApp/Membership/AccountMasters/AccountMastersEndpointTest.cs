using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.AccountMasters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Credentials
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class AccountMastersEndpointTest : IntegrationsBaseTest<AccountMasterTestData>
    {
        public AccountMastersEndpointTest(): base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {
            
        }

        [TestMethod]
        public async Task POST_AccountMaster_Success()
        {
            string externalIdentifier = "postAccountMaster01";

            //test scenario setup
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };

            AccountMasterRequest request = new AccountMasterRequest
            {
                CreateAccount = true,
                UseAccountTermsAsDefaultPayment = true,
                TaxIdentifier = "asd",
                CreatedBy = "http test",
                ExternalData = "nada",
                GroupIdentifier = "asd",
                Identifier = externalIdentifier,
                IsWebEnabled = true,
                Name = "http test",
                ProductPriceDiscount = 105,
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "29 days ;)"
                }
            };

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //pending
        //[TestMethod]
        public async Task POST_Verify_PriceList_Order()
        {
            string externalIdentifier = "postAccountMasterCase01";
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            //TODO:
            //review this validation
            IDictionary<int, string> priceListItems = new Dictionary<int, string>
            {
                { 0, "priceList01" },
                { 1, "priceList04" },
                { 2, "priceList02" },
                { 3, "priceList03" }
            };

            AccountMasterRequest request = new AccountMasterRequest
            {
                CreateAccount = true,
                UseAccountTermsAsDefaultPayment = true,
                TaxIdentifier = "asd",
                CreatedBy = "http test",
                ExternalData = "nada",
                GroupIdentifier = "asd",
                Identifier = externalIdentifier,
                IsWebEnabled = true,
                Name = "http test",
                ProductPriceDiscount = 105,
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "29 days ;)"
                },
                PriceListIds = new List<string>
                {
                    ""
                }
            };

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //NOTE
            //the result.PriceLists will return a list of external ids

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //pending
        //[TestMethod]
        //public async Task POST_AccountMaster_Invalid()


        [TestMethod]
        public async Task GET_AccountMaster_Success()
        {
            string externalIdentifier = "getAccountMasterCase01";
            //test scenario setup
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_AccountMaster_Not_Found()
        {
            string externalIdentifier = "getAccountMaster02";
            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_AccountMaster_Success()
        {
            string externalIdentifier = "putAccountMaster01";
            //test scenario setup
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            AccountMasterRequest request = new AccountMasterRequest
            {
                CreateAccount = true,
                UseAccountTermsAsDefaultPayment = true,
                TaxIdentifier = "asd",
                UpdatedBy = "http test",
                ExternalData = "nada",
                GroupIdentifier = "asd",
                Identifier = externalIdentifier,
                IsWebEnabled = true,
                Name = "http test",
                ProductPriceDiscount = 105,
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "29 days ;)"
                }
            };

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Update(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
            //TODO
            //add updated properties
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //pending
        //[TestMethod]
        //public async Task PUT_AccountMaster_Invalid()

        [TestMethod]
        public async Task PUT_AccountMaster_Not_Found()
        {
            string externalIdentifier = "putAccountMasterCase03";

            AccountMasterRequest request = new AccountMasterRequest
            {
                CreateAccount = true,
                UseAccountTermsAsDefaultPayment = true,
                TaxIdentifier = "asd",
                UpdatedBy = "http test",
                ExternalData = "nada",
                GroupIdentifier = "asd",
                Identifier = externalIdentifier,
                IsWebEnabled = true,
                Name = "http test",
                ProductPriceDiscount = 105,
                TermsConfiguration = new AccountMasterTermConfiguration
                {
                    HasPaymentTerms = true,
                    TermsDescription = "29 days ;)"
                }
            };

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Update(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_AccountMaster_Success()
        {
            string externalIdentifier = "deleteAccountMasterCase01";
            //test scenario setup
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_AccountMaster_Not_Found()
        {
            string externalIdentifier = "deleteAccountMasterCase02";

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PATCH_AccountMaster_Success()
        {
            string externalIdentifier = "patchAccountMaster01";
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            //test scenario setup
            await TestScenarioSetUp(testData);

            //TODO:
            //properies to patch
            var request = new
            {
                UpdatedBy = "patch request",
                Name = "name patched"
            };

            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //patch specific validations
            Assert.AreEqual(request.Name, response.Result.Name);
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_AccountMaster_NotFound()
        {
            string externalIdentifier = "patchAccountMaster02";
            var request = new { Name = "patched property" };
            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        //TODO
        //pending
        //[TestMethod]
        public async Task PATCH_PriceLists()
        {
            string externalIdentifier = "patchPriceList02";
            AccountMasterTestData testData = new AccountMasterTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);
            //TODO:
            //precondition
            //create priceLists to add
            //string item1 = "priceList01";
            //string item2 = "priceList02";
            //string item3 = "priceList03";
            //var request = new { PriceListIds = new List<string> { item3, item1, item2 } };
            var request = new { Name = "asdasd" };
            HttpResponseExtended<AccountMasterResponse> response = await Client.AccountMasters.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //specific validations
            //Assert.IsNotNull(response.Result.PriceLists);
            //Assert.AreEqual(response.Result.PriceLists.IndexOf(item3), 0);
            //Assert.AreEqual(response.Result.PriceLists.IndexOf(item1), 1);
            //Assert.AreEqual(response.Result.PriceLists.IndexOf(item2), 2);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        public override async Task TestScenarioSetUp(AccountMasterTestData testData)
        {
            AccountMasterRequest accountMasterRequest = new AccountMasterRequest
            {
                Name = "temporal name",
                IsWebEnabled = true,
                CreateAccount = false,
                Identifier = testData.ExternalIdentifier,
                CreatedBy = "temporal request"
            };

            await Client.AccountMasters.Create(accountMasterRequest);
        }

        public override async Task TestScenarioCleanUp(AccountMasterTestData testData)
        {
            await Client.AccountMasters.Remove(testData.ExternalIdentifier);
        }
    }
}
