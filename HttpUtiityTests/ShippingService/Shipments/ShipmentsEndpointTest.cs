using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.ShippingService.Shipments;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.Base;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Shipment
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShipmentsEndpointTest : ShippingServiceBaseTest<ShipmentTestData>
    {
        public ShipmentsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task GET_Shipment_Success()
        {
            string identifier = TestContext.TestName + "01";

            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigExtId = identifier,
                FlatRateScheduleExtId = identifier,
                HandlingScheduleExtId = identifier
            };

            //create flat rate schedule group
            FlatRateScheduleGroupsRequest flatRateScheduleGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                Name = "cheap flat rate"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupRequest);

            //create handling schedule group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                Name = "free handling"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            //create a flat rate schedule
            FlatRateScheduleRequest flatRateScheduleRequest = new FlatRateScheduleRequest
            {
                CreatedBy = "temporal request",
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay,
                ExternalIdentifier = testData.FlatRateScheduleExtId,
                Rate = 0.5m,
                OrderAmountMin = 1,
                OrderAmountMax = 1000
            };
            await Client.FlatRateSchedules.Create(flatRateScheduleRequest);

            //create a flat rate and group schedules
            FlatRateScheduleConfigurationRequest flatRateScheduleConfigurationRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "temporal request success 200"
            };
            await Client.FlatRateScheduleConfigurations.Create(testData.FlatRateScheduleGroupExtId, testData.FlatRateScheduleExtId, flatRateScheduleConfigurationRequest);

            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 200,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 1,
                        Label = ServiceLevelCodesEnum.NextDay.ToString(),
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.NextDay
                    },
                    new SCServiceLevel
                    {
                        Amount = 300,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 0,
                        IsEnabled = true,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        Code = (int)ServiceLevelCodesEnum.Local
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create shippingPreference for accountMaster
            ShippingAccountMasterPreferencesRequest shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal request",
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = testData.ShippingConfigExtId,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                },
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };
            await Client.ShippingAccountMasterPreferences.Create(testData.AccountMasterExtId, shippingPreferenceRequest);

            ShipmentRequest request = new ShipmentRequest
            {
                OrderId = "00000000000000000002",
                ShipFromAddress = new Address
                {
                    AddressLine1 = "Denver Boulder Turnpike",
                    AddressLine2 = "",
                    City = "Boulder",
                    Country = "US",
                    Name = "dfs",
                    Postal = "80014",
                    StateProvinceRegion = "CO"
                },
                ShipToAddress = new Address
                {
                    AddressLine1 = "Denver Boulder Turnpike",
                    AddressLine2 = string.Empty,
                    City = "Boulder",
                    Country = "US",
                    Name = "dfs",
                    Postal = "1234567",
                    StateProvinceRegion = "CO"
                },
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        Quantity = 1,
                        CheckoutPriceExtended = 300,
                        Shipping = new ShipmentProductShipping
                        {
                            Width = 12,
                            Height = 12,
                            IsFreeShip = false,
                            Length = 12,
                            IsFreightOnly = false,
                            IsQuickShip = false,
                            WeightDimensional = 12m,
                            WeightActual = 2,
                            FreightClass = 2
                        }
                    }
                }
            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");
        }

        [TestMethod]
        public async Task GET_Shipment_Invalid()
        {
            string accountMasterExternalId = "accountMasterExt0x";

            ShipmentRequest request = new ShipmentRequest
            {
                ShipFromAddress = new Address
                {
                    AddressLine1 = string.Empty
                },
                ShipToAddress = new Address
                {
                    AddressLine1 = string.Empty
                }
            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(accountMasterExternalId, request);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status should not be successful");
            Assert.IsNull(response.Result, "Result object should be null");
        }

        protected override Task TestScenarioSetUp(ShipmentTestData testData)
        {
            throw new NotImplementedException();
        }

        protected override async Task TestScenarioCleanUp(ShipmentTestData testData)
        {
            await Client.ShippingAccountMasterPreferences.Remove(testData.AccountMasterExtId);

            await Client.HandlingScheduleConfigurations.Remove(testData.HandlingScheduleGroupExtId, testData.HandlingScheduleExtId);
            await Client.HandlingScheduleGroups.Remove(testData.HandlingScheduleGroupExtId);
            await Client.HandlingSchedules.Remove(testData.HandlingScheduleExtId);

            await Client.FlatRateScheduleConfigurations.Remove(testData.FlatRateScheduleGroupExtId, testData.FlatRateScheduleExtId);
            await Client.FlatRateScheduleGroups.Remove(testData.FlatRateScheduleGroupExtId);
            await Client.FlatRateSchedules.Remove(testData.FlatRateScheduleExtId);

            await Client.ShippingConfigurations.Remove(testData.ShippingConfigExtId);
        }
    }
}
