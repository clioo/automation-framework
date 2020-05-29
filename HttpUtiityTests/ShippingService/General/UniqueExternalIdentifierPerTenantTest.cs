using HttpUtiityTests.BaseTest;
using HttpUtiityTests.EnvConstants;
using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.General
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class UniqueExternalIdentifierPerTenantTest : ApiBaseTest
    {
        IShippingServiceClient AllPointsClient;
        IShippingServiceClient FmpClient;

        public UniqueExternalIdentifierPerTenantTest()
        {
            bool shippingApiHasHttps = ServiceConstants.ShippingServiceApiUrl.Contains("https");
            //Gets bearer token
            string bearerToken = GetBearerToken(authName: ServiceConstants.AuthName,
                password: ServiceConstants.AuthPassword,
                publicKey: ServiceConstants.AuthPublicKey,
                shippingApiHasHttps: shippingApiHasHttps);

            AllPointsClient = new ShippingServiceClient(FixUrl(ServiceConstants.ShippingServiceApiUrl), ServiceConstants.AllPointsPlatformExtId, bearerToken, shippingApiHasHttps);
            FmpClient = new ShippingServiceClient(FixUrl(ServiceConstants.ShippingServiceApiUrl), ServiceConstants.FmpPlatformExtId, bearerToken, shippingApiHasHttps);
        }

        [TestMethod]
        public async Task CreateConfigurationBothTenants()
        {
            string identifier = TestContext.TestName;

            var allpointsRequest = new ShippingConfigurationRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 10,
                        CarrierRateDiscount = 0,
                        ErpExtId = string.Empty,
                        RateShopperExtId = RateShopperShipperCodesEnum.S01.ToString(),
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        Code = (int)ServiceLevelCodesEnum.Local,
                        SortOrder = 0
                    }
                }
            };
            var fmpRequest = new ShippingConfigurationRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0,
                        ErpExtId = string.Empty,
                        RateShopperExtId = RateShopperShipperCodesEnum.S01.ToString(),
                        Label = ServiceLevelCodesEnum.Ground.ToString(),
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        SortOrder = 0
                    }
                }
            };

            var allpointsResponse = await AllPointsClient.ShippingConfigurations.Create(allpointsRequest);
            var fmpResponse = await FmpClient.ShippingConfigurations.Create(fmpRequest);

            //test data clean up
            await AllPointsClient.ShippingConfigurations.Remove(identifier);
            await FmpClient.ShippingConfigurations.Remove(identifier);

            Assert.IsNotNull(allpointsResponse);
            Assert.IsNotNull(allpointsResponse.Result);
            Assert.IsTrue(allpointsResponse.Success);
            Assert.AreEqual(identifier, allpointsResponse.Result.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpResponse);
            Assert.IsNotNull(fmpResponse.Result);
            Assert.IsTrue(fmpResponse.Success);
            Assert.AreEqual(identifier, fmpResponse.Result.ExternalIdentifier);
        }

        [TestMethod]
        public async Task CreateFlatSchedulesBothTenants()
        {
            string identifier = TestContext.TestName;

            var allpointsRequest = new FlatRateScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };
            var fmpRequest = new FlatRateScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };

            var allpointsResponse = await AllPointsClient.FlatRateSchedules.Create(allpointsRequest);
            var fmpResponse = await FmpClient.FlatRateSchedules.Create(fmpRequest);

            //clear test data
            await AllPointsClient.FlatRateSchedules.Remove(identifier);
            await FmpClient.FlatRateSchedules.Remove(identifier);

            //allpoints validations
            Assert.IsNotNull(allpointsResponse);
            Assert.IsNotNull(allpointsResponse.Result);
            Assert.IsTrue(allpointsResponse.Success);
            Assert.AreEqual(identifier, allpointsResponse.Result.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpResponse);
            Assert.IsNotNull(fmpResponse.Result);
            Assert.IsTrue(fmpResponse.Success);
            Assert.AreEqual(identifier, fmpResponse.Result.ExternalIdentifier);
        }

        [TestMethod]
        public async Task CreateHandlingScheduleBothTenants()
        {
            string identifier = TestContext.TestName;

            var allpointsRequest = new HandlingScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };
            var fmpRequest = new HandlingScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };

            var allpointsResponse = await AllPointsClient.HandlingSchedules.Create(allpointsRequest);
            var fmpResponse = await FmpClient.HandlingSchedules.Create(fmpRequest);

            //clear test data
            await AllPointsClient.HandlingSchedules.Remove(identifier);
            await FmpClient.HandlingSchedules.Remove(identifier);

            //allpoints validations
            Assert.IsNotNull(allpointsResponse);
            Assert.IsNotNull(allpointsResponse.Result);
            Assert.IsTrue(allpointsResponse.Success);
            Assert.AreEqual(identifier, allpointsResponse.Result.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpResponse);
            Assert.IsNotNull(fmpResponse.Result);
            Assert.IsTrue(fmpResponse.Success);
            Assert.AreEqual(identifier, fmpResponse.Result.ExternalIdentifier);
        }

        [TestMethod]
        public async Task CreateFlatGroupsBothTenants()
        {
            string identifier = TestContext.TestName + "01";

            var allpointsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };
            var fmpRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };

            var allpointsResponse = await AllPointsClient.FlatRateScheduleGroups.Create(allpointsRequest);
            var fmpResponse = await FmpClient.FlatRateScheduleGroups.Create(fmpRequest);

            //clear test data
            await AllPointsClient.FlatRateScheduleGroups.Remove(identifier);
            await FmpClient.FlatRateScheduleGroups.Remove(identifier);

            //allpoints validations
            Assert.IsNotNull(allpointsResponse);
            Assert.IsNotNull(allpointsResponse.Result);
            Assert.IsTrue(allpointsResponse.Success);
            Assert.AreEqual(identifier, allpointsResponse.Result.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpResponse);
            Assert.IsNotNull(fmpResponse.Result);
            Assert.IsTrue(fmpResponse.Success);
            Assert.AreEqual(identifier, fmpResponse.Result.ExternalIdentifier);
        }

        [TestMethod]
        public async Task CreateHandlingGroupsBothTenants()
        {
            string identifier = TestContext.TestName + "01";

            var allpointsRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };
            var fmpRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };

            var allpointsResponse = await AllPointsClient.HandlingScheduleGroups.Create(allpointsRequest);
            var fmpResponse = await FmpClient.HandlingScheduleGroups.Create(fmpRequest);

            //clear test data
            await AllPointsClient.HandlingScheduleGroups.Remove(identifier);
            await FmpClient.HandlingScheduleGroups.Remove(identifier);

            //allpoints validations
            Assert.IsNotNull(allpointsResponse);
            Assert.IsNotNull(allpointsResponse.Result);
            Assert.IsTrue(allpointsResponse.Success);
            Assert.AreEqual(identifier, allpointsResponse.Result.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpResponse);
            Assert.IsNotNull(fmpResponse.Result);
            Assert.IsTrue(fmpResponse.Success);
            Assert.AreEqual(identifier, fmpResponse.Result.ExternalIdentifier);
        }

        [TestMethod]
        public async Task CreateFlatScheduleConfigurationBothTenants()
        {
            string identifier = TestContext.TestName + "01";

            //create schedule groups
            var allpointsGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };
            var fmpGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
            };

            await AllPointsClient.FlatRateScheduleGroups.Create(allpointsGroupRequest);
            await FmpClient.FlatRateScheduleGroups.Create(fmpGroupRequest);

            //create schedules
            var allpointsScheduleRequest = new FlatRateScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };
            var fmpScheduleRequest = new FlatRateScheduleRequest
            {
                CreatedBy = identifier,
                Rate = 0,
                ExternalIdentifier = identifier,
                OrderAmountMin = 130,
                OrderAmountMax = 300,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };

            await AllPointsClient.FlatRateSchedules.Create(allpointsScheduleRequest);
            await FmpClient.FlatRateSchedules.Create(fmpScheduleRequest);

            //create configuration groups
            var allpointsRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = identifier
            };
            var fmpRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = identifier
            };

            var allpointsGroupConfigResponse = await AllPointsClient.FlatRateScheduleConfigurations.Create(identifier, identifier, allpointsRequest);
            var fmpGroupConfigResponse = await FmpClient.FlatRateScheduleConfigurations.Create(identifier, identifier, fmpRequest);

            //clear test data
            await AllPointsClient.FlatRateSchedules.Remove(identifier);
            await AllPointsClient.FlatRateScheduleGroups.Remove(identifier);
            await AllPointsClient.FlatRateScheduleConfigurations.Remove(identifier, identifier);

            await FmpClient.FlatRateSchedules.Remove(identifier);
            await FmpClient.FlatRateScheduleGroups.Remove(identifier);
            await FmpClient.FlatRateScheduleConfigurations.Remove(identifier, identifier);

            //allpoints validations
            Assert.IsNotNull(allpointsGroupConfigResponse);
            Assert.IsNotNull(allpointsGroupConfigResponse.Result);
            Assert.IsTrue(allpointsGroupConfigResponse.Success);
            Assert.AreEqual(identifier, allpointsGroupConfigResponse.Result.FlatRateSchedule.ExternalIdentifier);

            //fmp validations
            Assert.IsNotNull(fmpGroupConfigResponse);
            Assert.IsNotNull(fmpGroupConfigResponse.Result);
            Assert.IsTrue(fmpGroupConfigResponse.Success);
            Assert.AreEqual(identifier, fmpGroupConfigResponse.Result.FlatRateScheduleGroup.ExternalIdentifier);
        }
    }
}
