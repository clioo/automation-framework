using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.ScheduleConfigurations
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class FlatRateScheduleConfigurationsEndpointTest : ShippingServiceBaseTest<ScheduleConfigurationTestData>
    {
        public FlatRateScheduleConfigurationsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_FlatRateScheduleConfiguration_Success()
        {
            ScheduleConfigurationTestData testData = new ScheduleConfigurationTestData
            {
                GroupExtId = "flatRateGroupConfigPost01",
                ScheduleExtId = "flatRateScheduleConfigPost01"
            };

            //create group
            FlatRateScheduleGroupsRequest groupsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                Name = "temporal name",
                ExternalIdentifier = testData.GroupExtId
            };
            await Client.FlatRateScheduleGroups.Create(groupsRequest);

            //create schedule
            FlatRateScheduleRequest scheduleRequest = new FlatRateScheduleRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ScheduleExtId,
                Rate = 10,
                OrderAmountMin = 1,
                OrderAmountMax = 10,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground
            };
            await Client.FlatRateSchedules.Create(scheduleRequest);


            FlatRateScheduleConfigurationRequest request = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "automated post request"
            };

            HttpEssResponse<FlatRatesSchedulesConfigurationResponse> response = await Client
                .FlatRateScheduleConfigurations.Create(testData.GroupExtId, testData.ScheduleExtId, request);

            Assert.IsNotNull(response, "Response is not null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task POST_FlatRateScheduleConfiguration_NotFound()
        {
            string groupExtId = "notFound";
            string scheduleExtId = "notFound";

            FlatRateScheduleConfigurationRequest request = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "automated post request"
            };

            HttpEssResponse<FlatRatesSchedulesConfigurationResponse> response = await Client.FlatRateScheduleConfigurations.Create(groupExtId, scheduleExtId, request);

            Assert.IsNotNull(response, "Response is not null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status is not successful");
            Assert.IsNull(response.Result, "Result object should not be null");
        }

        [TestMethod]
        public async Task DELETE_FlatRateScheduleConfiguration_Success()
        {
            ScheduleConfigurationTestData testData = new ScheduleConfigurationTestData
            {
                GroupExtId = "flatRateGroupConfigDelete01",
                ScheduleExtId = "flatRateScheduleConfigDelete01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<FlatRatesSchedulesConfigurationResponse> response = await Client.
                FlatRateScheduleConfigurations.Remove(testData.GroupExtId, testData.ScheduleExtId);

            Assert.IsNotNull(response, "Response is not null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "Result object should be null");

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_FlatRateScheduleConfiguration_Not_Found()
        {
            string groupId = "flatRateGroupConfigDelete02";
            string scheduleId = "flatRateScheduleConfigDelete02";

            HttpEssResponse<FlatRatesSchedulesConfigurationResponse> response = await Client.FlatRateScheduleConfigurations.Remove(groupId, scheduleId);

            Assert.IsNotNull(response, "Response should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "Result object should be null");
        }

        protected override async Task TestScenarioSetUp(ScheduleConfigurationTestData data)
        {
            //create group
            FlatRateScheduleGroupsRequest groupsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                Name = "temporal name",
                ExternalIdentifier = data.GroupExtId
            };
            await Client.FlatRateScheduleGroups.Create(groupsRequest);

            //create schedule
            FlatRateScheduleRequest scheduleRequest = new FlatRateScheduleRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.ScheduleExtId,
                Rate = 10,
                OrderAmountMin = 1,
                OrderAmountMax = 10,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground
            };
            await Client.FlatRateSchedules.Create(scheduleRequest);

            //create schedule configuration
            FlatRateScheduleConfigurationRequest flatRatesSchedulesConfigurationRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "temporal request"
            };
            await Client.FlatRateScheduleConfigurations.Create(data.GroupExtId, data.ScheduleExtId, flatRatesSchedulesConfigurationRequest);
        }

        protected override async Task TestScenarioCleanUp(ScheduleConfigurationTestData data)
        {
            await Client.FlatRateScheduleConfigurations.Remove(data.GroupExtId, data.ScheduleExtId);
            await Client.FlatRateScheduleGroups.Remove(data.GroupExtId);
            await Client.FlatRateSchedules.Remove(data.ScheduleExtId);
        }
    }
}
