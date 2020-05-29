using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.ShippingService.Schedules;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.FlatRateSchedules
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class FlatRateSchedulesEndpointTest : ShippingServiceBaseTest<SchedulesTestData>
    {
        public FlatRateSchedulesEndpointTest() : base(ServiceConstants.AllPointsPlatformExtId) { }

        [TestMethod]
        public async Task POST_FlatRateSchedule_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRatePostCase01"
            };

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                CreatedBy = "post success",
                ExternalIdentifier = testData.ExternalIdentifier,
                Rate = 1,
                OrderAmountMax = 29,
                OrderAmountMin = 24,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Create(request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task POST_FlatRateSchedule_OrderAmountMaxIsNull_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRatePostCase03"
            };

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                CreatedBy = "post success",
                ExternalIdentifier = testData.ExternalIdentifier,
                Rate = 1,
                OrderAmountMax = null,
                OrderAmountMin = 24,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Create(request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //specific validations
            Assert.IsNull(response.Result.OrderAmountMax, "OrderAmountMax should be null");
        }

        [TestMethod]
        public async Task POST_FlatRateSchedule_Invalid()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRatePostCase02"
            };

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                //Does not have a CreatedBy prop
                ExternalIdentifier = testData.ExternalIdentifier,
                Rate = 1,
                OrderAmountMax = 29,
                OrderAmountMin = 24,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Create(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_FlatRateSchedule_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRateGetCase01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.GetSingle(testData.ExternalIdentifier);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_FlatRateSchedule_Not_Found()
        {
            string externalIdentifier = "flatRateGetCase02";
            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.GetSingle(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.IsNull(response.Result);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task PUT_FlatRateSchedule_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRatePutCase01"
            };

            //preconditions
            await TestScenarioSetUp(testData);

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                UpdatedBy = "put success",
                ExternalIdentifier = testData.ExternalIdentifier,
                Rate = 11,
                OrderAmountMax = 69,
                OrderAmountMin = 4,
                ServiceLevelCode = 1
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Update(testData.ExternalIdentifier, request);
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task PUT_FlatRateSchedule_Invalid()
        {
            string externalIdentifier = "flatRatePutCase02";

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                //does not have the UpadtedBy property
                ExternalIdentifier = externalIdentifier,
                Rate = 11,
                OrderAmountMax = 69,
                OrderAmountMin = 4,
                ServiceLevelCode = 1
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Update(externalIdentifier, request);
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_FlatRateSchedule_Not_Found()
        {
            string externalIdentifier = "flatRatePutCase03";

            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                UpdatedBy = "put not found",
                ExternalIdentifier = externalIdentifier,
                Rate = 1,
                OrderAmountMax = 90,
                OrderAmountMin = 24,
                ServiceLevelCode = 0
            };

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Update(externalIdentifier, request);
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_FlatRateSchedule_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "flatRateDeleteCase01"
            };

            //preconditions
            await TestScenarioSetUp(testData);

            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Remove(testData.ExternalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_FlatRateSchedule_Not_Found()
        {
            string externalIdentifier = "flatRateDeleteCase02";
            HttpEssResponse<FlatRateScheduleResponse> response = await Client.FlatRateSchedules.Remove(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        protected override async Task TestScenarioSetUp(SchedulesTestData data)
        {
            FlatRateScheduleRequest request = new FlatRateScheduleRequest
            {
                ExternalIdentifier = data.ExternalIdentifier,
                CreatedBy = "temporal request",
                Rate = 1,
                ServiceLevelCode = 0,
                OrderAmountMin = 1,
                OrderAmountMax = 10
            };

            await Client.FlatRateSchedules.Create(request);
        }

        protected override async Task TestScenarioCleanUp(SchedulesTestData data)
        {
            await Client.FlatRateSchedules.Remove(data.ExternalIdentifier);
        }
    }
}
