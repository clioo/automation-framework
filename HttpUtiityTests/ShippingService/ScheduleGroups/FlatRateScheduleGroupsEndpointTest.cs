using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.ScheduleGroups
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class FlatRateScheduleGroupsEndpointTest : ShippingServiceBaseTest<ScheduleGroupTestData>
    {
        public FlatRateScheduleGroupsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_FlatRateScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "postFlatRateScheduleGroupCase01"
            };
            //test scenario setup
            await TestScenarioCleanUp(testData);

            FlatRateScheduleGroupsRequest request = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.ExternalIdentifier,
                CreatedBy = "post success test method",
                Name = "flat rate group"
            }; 

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }


        [TestMethod]
        public async Task POST_FlatRateScheduleGroup_Invalid()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "postFlatRateScheduleGroupCase02"
            };

            FlatRateScheduleGroupsRequest request = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "post success test method",
                Name = "flat rate group"
            };

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_FlatRateScheduleGroup_Success()
        {
            //test scenario setup
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "getFlatRateScheduleGroupCase01"
            };
            await TestScenarioSetUp(testData);

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.GetSingle(testData.ExternalIdentifier);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GET_FlatRateScheduleGroup_Not_Found()
        {
            string externalIdentifier = "getFlatRateScheduleGroupCase02";

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_FlatRateScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "deleteFlatRateScheduleGroupCase01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Remove(testData.ExternalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);

            //specific validation
            var getResponse = await Client.FlatRateScheduleGroups.GetSingle(testData.ExternalIdentifier);
            Assert.AreEqual(404, getResponse.StatusCode);
        }

        [TestMethod]
        public async Task DELETE_FlatRateScheduleGroup_Not_Found()
        {
            string externalIdentifier = "deleteFlatRateScheduleGroupCase02";
            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_FlatRateScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "putFlatRateScheduleGroup01"
            };

            //test scenario set up
            await TestScenarioSetUp(testData);

            FlatRateScheduleGroupsRequest request = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.ExternalIdentifier,
                Name = "any value",
                UpdatedBy = "temporal request"
            };

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Update(testData.ExternalIdentifier, request);

            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status code should not be successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PUT_FlatRateScheduleGroup_Not_Found()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "putFlatRateScheduleGroup02"
            };            

            FlatRateScheduleGroupsRequest request = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.ExternalIdentifier,
                Name = "any value",
                UpdatedBy = "temporal request"
            };

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Update(testData.ExternalIdentifier, request);

            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status code should not be successful");
            Assert.IsNull(response.Result, "Result object should be null");
        }

        [TestMethod]
        public async Task PUT_FlatRateScheduleGroup_Invalid()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "putFlatRateScheduleGroup03"
            };
            //test scenario setup
            await TestScenarioSetUp(testData);

            FlatRateScheduleGroupsRequest request = new FlatRateScheduleGroupsRequest
            {
                
            };

            HttpEssResponse<FlatRateScheduleGroupsResponse> response = await Client.FlatRateScheduleGroups.Update(testData.ExternalIdentifier, request);

            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status code should not be successful");

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        protected override async Task TestScenarioSetUp(ScheduleGroupTestData data)
        {
            FlatRateScheduleGroupsRequest flatRateScheduleGroupsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.ExternalIdentifier,
                Name = "temporal name"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupsRequest);
        }

        protected override async Task TestScenarioCleanUp(ScheduleGroupTestData data)
        {
            await Client.FlatRateScheduleGroups.Remove(data.ExternalIdentifier);
        }
    }
}
