using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Schedules
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class HandlingSchedulesEndpointTest : ShippingServiceBaseTest<SchedulesTestData>
    {
        public HandlingSchedulesEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_HandlingSchedule_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "handlingSchedulePostCase01"
            };            

            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                CreatedBy = "Post_Success_Test",
                ExternalIdentifier = testData.ExternalIdentifier,
                OrderAmountMax = 0,
                OrderAmountMin = 0,
                Rate = 1,
                ServiceLevelCode = 1
            };
            
            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Create(request);

            //clear test data
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task POST_HandlingSchedule_OrderAmountMaxIsNull_Success()
        {
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "handlingSchedulePostCase03"
            };

            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                CreatedBy = "Post_Success_Test",
                ExternalIdentifier = testData.ExternalIdentifier,
                OrderAmountMax = null,
                OrderAmountMin = 0,
                Rate = 1,
                ServiceLevelCode = 1
            };

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Create(request);

            //clear test data
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
            //specific validations
            Assert.AreEqual(null, response.Result.OrderAmountMax);
        }

        [TestMethod]
        public async Task POST_HandlingSchedule_Invalid()
        {
            string externalIdentifier = "handlingSchedulePostCase02";
            
            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                //does not have the CreatedBy property
                ExternalIdentifier = externalIdentifier,
                OrderAmountMax = 100,
                OrderAmountMin = 10,
                Rate = 1,
                ServiceLevelCode = 1
            };

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Create(request);
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_HandlingSchedule_Success()
        {
            //test scenario setup
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "HandlingSchedulesGetCase01"
            };
            await TestScenarioSetUp(testData);

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.GetSingle(testData.ExternalIdentifier);

            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GET_HandlingSchedule_Not_Found()
        {
            string externalIdentifier = "HandlingSchedulesGetCase02";
            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.GetSingle(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_HandlingSchedule_Success()
        {
            //test scenario setup
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "HandlingSchedulesPutCase01"
            };
            await TestScenarioSetUp(testData);

            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                UpdatedBy = "Put success",
                ExternalIdentifier = testData.ExternalIdentifier,
                OrderAmountMax = 100,
                OrderAmountMin = 10,
                Rate = 1,
                ServiceLevelCode = 1
            };

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Update(testData.ExternalIdentifier, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_HandlingSchedule_Invalid()
        {
            //test scenario setup
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "HandlingSchedulesPutCase02"
            };

            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                //does not have an UpdatedBy prop
                ExternalIdentifier = testData.ExternalIdentifier,
                OrderAmountMax = 100,
                OrderAmountMin = 10,
                Rate = 1,
                ServiceLevelCode = 1
            };

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Update(testData.ExternalIdentifier, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_HandlingSchedule_Not_Found()
        {
            string externalIdentifier = "HandlingSchedulesPutCase03";

            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                UpdatedBy = "Put handling schedule",
                ExternalIdentifier = externalIdentifier,
                OrderAmountMax = 100,
                OrderAmountMin = 10,
                Rate = 1,
                ServiceLevelCode = 1
            };

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Update(externalIdentifier, request);
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_HandlingSchedule_Success()
        {
            //test scenario setup
            SchedulesTestData testData = new SchedulesTestData
            {
                ExternalIdentifier = "HandlingSchedulesDeleteCase01",
            };
            await TestScenarioSetUp(testData);

            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Remove(testData.ExternalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_HandlingSchedule_Not_Found()
        {
            string externalIdentifier = "HandlingSchedulesDeleteCase02";
            HttpEssResponse<HandlingScheduleResponse> response = await Client.HandlingSchedules.Remove(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        protected override async Task TestScenarioSetUp(SchedulesTestData data)
        {
            HandlingScheduleRequest request = new HandlingScheduleRequest
            {
                ExternalIdentifier = data.ExternalIdentifier,
                CreatedBy = "temporal request",
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                Rate = 0.2m,
                OrderAmountMin = 1,
                OrderAmountMax = 10
            };
            await Client.HandlingSchedules.Create(request);
        }

        protected override async Task TestScenarioCleanUp(SchedulesTestData data)
        {
            await Client.HandlingSchedules.Remove(data.ExternalIdentifier);
        }
    }
}
