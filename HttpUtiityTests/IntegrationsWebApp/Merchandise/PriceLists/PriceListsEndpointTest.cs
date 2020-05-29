using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.IntegrationsWebApp.Merchandise.PriceLists;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.IntegrationsWebApp.Models;
using HttpUtility.EndPoints.IntegrationsWebApp.Models.PriceLists;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.IntegrationsWebApp.Membership.PriceLists
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.Integrations)]
    public class PriceListsEndpointTest : IntegrationsBaseTest<PriceListTestData>
    {
        public PriceListsEndpointTest() : base(ServiceConstants.IntegrationsAPIUrl, ServiceConstants.AllPointsPlatformExtId, ServiceConstants.AllPointsPlatformId)
        {

        }

        [TestMethod]
        public async Task POST_PriceList_Success()
        {
            string externalIdentifier = "postPricelistCase01";
            PriceListTestData testData = new PriceListTestData
            {
                ExternalIdentifier = externalIdentifier
            };

            PriceListRequest request = new PriceListRequest
            {
                Name = "Pricelist name",
                Identifier = externalIdentifier,
                CreatedBy = "post request"
            };

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //validate that all props are assigned in the response
            Assert.AreEqual(request.Name, response.Result.Name);
            Assert.AreEqual(request.CreatedBy, response.Result.CreatedBy);
            Assert.AreEqual(request.Identifier, response.Result.ExternalIdentifier);

            //scenario clean up
            await TestScenarioCleanUp(testData);
        }

        //TODO
        //pending
        //[TestMethod]
        public async Task POST_PriceList_Invalid()
        {
            string externalIdentifier = "postPricelistCase02";

            PriceListRequest request = new PriceListRequest
            {
                Identifier = externalIdentifier
            };

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(500, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_PriceList_Success()
        {
            string externalIdentifier = "getPricelistCase01";
            //scenario setup
            PriceListTestData testData = new PriceListTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_PriceList_Not_Found()
        {
            string externalIdentifier = "getPricelistCase02";

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_PriceList_Success()
        {
            string externalIdentifier = "putCase01";
            //scenario setup
            PriceListTestData testData = new PriceListTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            PriceListRequest request = new PriceListRequest
            {
                Name = "patched name ;)",
                UpdatedBy = "put automated request",
                Identifier = "identifier_updated"
            };

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Update(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //validate that all properties has been updated
            Assert.AreEqual(request.Name, response.Result.Name);
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(externalIdentifier, response.Result.ExternalIdentifier);
            Assert.AreNotEqual(request.Identifier, response.Result.ExternalIdentifier);

            //test scenario clean up
            await TestScenarioSetUp(testData);
        }

        //TODO:
        //pending
        //[TestMethod]
        //public async Task PUT_Invalid()

        [TestMethod]
        public async Task PUT_PriceList_Not_Found()
        {
            string externalIdentifier = "putNotFoundPriceList01";

            PriceListRequest request = new PriceListRequest
            {
                Name = "patched name ;)",
                UpdatedBy = "put automated request",
                Identifier = externalIdentifier
            };

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Update(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_PriceList_Success()
        {
            string externalIdentifier = "deletePricelist01";
            //scenario setup
            PriceListTestData testData = new PriceListTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //no need to clean up due to the request is a delete and has no cross references
        }

        [TestMethod]
        public async Task DELETE_PriceList_Not_Found()
        {
            string externalIdentifier = "deletePricelist02";

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);

            var getResponse = await Client.PriceLists.GetSingle(externalIdentifier);

            Assert.IsFalse(getResponse.Success, "PriceList is still existing");
        }

        [TestMethod]
        public async Task PATCH_PriceList_Success()
        {
            string externalIdentifier = "patchPriceListCase01";
            //scenario setup
            PriceListTestData testData = new PriceListTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            var request = new
            {
                Name = "patched q:",
                UpdatedBy = "patch request"
            };

            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //check that the prop has been patched
            Assert.AreEqual(request.Name, response.Result.Name);
            //TODO
            //review this property is not patched
            //Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PATCH_PriceList_Not_Found()
        {
            string externalIdentifier = "patchPriceList02";
            var request = new { };
            HttpResponseExtended<PriceListResponse> response = await Client.PriceLists.PatchEntity(externalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        public override async Task TestScenarioSetUp(PriceListTestData testData)
        {
            PriceListRequest request = new PriceListRequest
            {
                CreatedBy = "temporal request",
                Identifier = testData.ExternalIdentifier,
                Name = "Temporal name",
                UpdatedBy = "temporal request"
            };

            await Client.PriceLists.Create(request);
        }

        public override async Task TestScenarioCleanUp(PriceListTestData testData)
        {
            await Client.PriceLists.Remove(testData.ExternalIdentifier);
        }
    }
}
