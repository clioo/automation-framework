using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.ScheduleConfigurations
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class HandlingScheduleConfigurationsEndpointTest : ShippingServiceBaseTest<ScheduleConfigurationTestData>
    {
        public HandlingScheduleConfigurationsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_HandlingSchedulesConfiguration_Success()
        {
            //test scenario setup
            ScheduleConfigurationTestData testData = new ScheduleConfigurationTestData
            {
                GroupExtId = "postGroupConfig01",
                ScheduleExtId = "postScheduleConfig01"
            };

            //create handling schedule
            HandlingScheduleRequest handlingScheduleRequest = new HandlingScheduleRequest
            {
                ExternalIdentifier = testData.ScheduleExtId,
                CreatedBy = "temporal request",
                Rate = 0.2m,
                ServiceLevelCode = 0,
                OrderAmountMax = 10,
                OrderAmountMin = 1
            };
            await Client.HandlingSchedules.Create(handlingScheduleRequest);

            //create handling group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.GroupExtId,
                Name = "temporal name"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            HandlingSchedulesConfigurationRequest request = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "post success request"
            };

            HttpEssResponse<HandlingSchedulesConfigurationResponse> response = await Client
                .HandlingScheduleConfigurations.Create(testData.GroupExtId, testData.ScheduleExtId, request);

            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response, "Response object should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not success");
            Assert.IsNotNull(response.Result, "Result object should not be null");
        }

        [TestMethod]
        public async Task POST_HandlingSchedulesConfiguration_NotFound()
        {
            string groupId = "notFound";
            string scheduleId = "notFound";

            HandlingSchedulesConfigurationRequest request = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "post success request"
            };

            HttpEssResponse<HandlingSchedulesConfigurationResponse> response = await Client.HandlingScheduleConfigurations.Create(groupId, scheduleId, request);

            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result, "result object should be null");
        }

        [TestMethod]
        public async Task DELETE_HandlingSchedulesConfiguration_Success()
        {
            ScheduleConfigurationTestData testData = new ScheduleConfigurationTestData
            {
                GroupExtId = "deleteGroup01",
                ScheduleExtId = "deleteSchedule01"
            };
            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<HandlingSchedulesConfigurationResponse> response = await Client
                .HandlingScheduleConfigurations.Remove(testData.GroupExtId, testData.ScheduleExtId);

            await TestScenarioCleanUp(testData);
            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Status is not successful");
            Assert.IsNull(response.Result, "Result object should be null");
        }

        [TestMethod]
        public async Task DELETE_HandlingSchedulesConfiguration_Not_Found()
        {
            string groupId = "notfound";
            string scheduleId = "notfound";

            HttpEssResponse<HandlingSchedulesConfigurationResponse> response = await Client.HandlingScheduleConfigurations.Remove(groupId, scheduleId);

            //validations
            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result, "Result object should be null");
        }

        protected override async Task TestScenarioSetUp(ScheduleConfigurationTestData data)
        {
            //create handling schedule
            HandlingScheduleRequest handlingScheduleRequest = new HandlingScheduleRequest
            {
                ExternalIdentifier = data.ScheduleExtId,
                CreatedBy = "temporal request",
                Rate = 0.2m,
                ServiceLevelCode = 0,
                OrderAmountMax = 10,
                OrderAmountMin = 1
            };
            await Client.HandlingSchedules.Create(handlingScheduleRequest);

            //create handling group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.GroupExtId,
                Name = "temporal name"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            //create handling configuration
            HandlingSchedulesConfigurationRequest handlingSchedulesConfigurationRequest = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "temporal request"
            };
            await Client.HandlingScheduleConfigurations.Create(data.GroupExtId, data.ScheduleExtId, handlingSchedulesConfigurationRequest);
        }

        protected override async Task TestScenarioCleanUp(ScheduleConfigurationTestData data)
        {
            await Client.HandlingScheduleConfigurations.Remove(data.GroupExtId, data.ScheduleExtId);
            await Client.HandlingScheduleGroups.Remove(data.GroupExtId);
            await Client.HandlingSchedules.Remove(data.ScheduleExtId);
        }
    }
}
