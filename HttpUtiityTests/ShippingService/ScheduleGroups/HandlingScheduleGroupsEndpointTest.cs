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
    public class HandlingScheduleGroupsEndpointTest : ShippingServiceBaseTest<ScheduleGroupTestData>
    {
        public HandlingScheduleGroupsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_HandlingScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "postHandlingScheduleGroupCase01"
            };

            HandlingScheduleGroupRequest request = new HandlingScheduleGroupRequest
            {
                ExternalIdentifier = testData.ExternalIdentifier,
                CreatedBy = "post success test method",
                Name = "handling schedule group generic name"
            };

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Create(request);

            //clean test data
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task POST_HandlingScheduleGroup_Invalid()
        {
            string externalIdentifier = "postHandlingScheduleGroupCase02";

            HandlingScheduleGroupRequest request = new HandlingScheduleGroupRequest
            {
                //does not have the CreatedBy property
                ExternalIdentifier = externalIdentifier,
                Name = "handling schedule group generic name"
            };

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Create(request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_HandlingScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "getHandlingScheduleGroupCase01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.GetSingle(testData.ExternalIdentifier);

            //clean test data
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GET_HandlingScheduleGroup_Not_Found()
        {
            string externalIdentifier = "getHandlingScheduleGroupCase02";

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.GetSingle(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_HandlingScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "putHandlingScheduleG01"
            };

            //test data setup
            await TestScenarioSetUp(testData);

            HandlingScheduleGroupRequest request = new HandlingScheduleGroupRequest
            {
                UpdatedBy = "temporal request",
                Name = "temporal group",
                ExternalIdentifier = testData.ExternalIdentifier
            };

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Update(testData.ExternalIdentifier, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //TODO
            //specific validations
        }

        [TestMethod]
        public async Task PUT_HandlingScheduleGroup_Invalid()
        {
            string externalId = "invalidExternalId";

            HandlingScheduleGroupRequest request = new HandlingScheduleGroupRequest
            {
                CreatedBy = "string",
                ExternalIdentifier = externalId,
                Name = "nothing"
            };

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Update(externalId, request);

            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsNull(response.Result, "Result object should be null");
        }

        [TestMethod]
        public async Task PUT_HandlingScheduleGroup_Not_Found()
        {
            string externalId = "notFoundExternalId";

            HandlingScheduleGroupRequest request = new HandlingScheduleGroupRequest
            {
                UpdatedBy = "string",
                ExternalIdentifier = externalId,
                Name = "nothing"
            };

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Update(externalId, request);

            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result, "Result object should be null");
        }

        [TestMethod]
        public async Task DELETE_HandlingScheduleGroup_Success()
        {
            ScheduleGroupTestData testData = new ScheduleGroupTestData
            {
                ExternalIdentifier = "deleteHandlingScheduleGroupCase01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Remove(testData.ExternalIdentifier);

            //clear test data
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_HandlingScheduleGroup_Not_Found()
        {
            string externalIdentifier = "deleteHandlingScheduleGroupCase02";
            HttpEssResponse<HandlingScheduleGroupResponse> response = await Client.HandlingScheduleGroups.Remove(externalIdentifier);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        protected override async Task TestScenarioSetUp(ScheduleGroupTestData data)
        {
            var request = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                Name = "temporal name",
                ExternalIdentifier = data.ExternalIdentifier
            };
            await Client.HandlingScheduleGroups.Create(request);
        }

        protected override async Task TestScenarioCleanUp(ScheduleGroupTestData data)
        {
            await Client.HandlingScheduleGroups.Remove(data.ExternalIdentifier);
        }
    }
}
