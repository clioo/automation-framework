using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.Base;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Shipments
{
    [TestClass]
    public class ShipmentsEndpointTest_A : ShippingServiceBaseTest<ShipmentTestData>
    {
        public ShipmentsEndpointTest_A() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task GET_Shipment_NoDefaultServiceLevel()
        {
            ShipmentTestData testData = new ShipmentTestData
            {
                ShippingConfigExtId = "postShippingConfig03",
                AccountMasterExtId = "accountMasterA",
                FlatRateScheduleGroupExtId = "AflatRateScGroup",
                HandlingScheduleGroupExtId = "AhandlingScGroup",
                FlatRateScheduleExtId = "AflatRateSchedule",
                HandlingScheduleExtId = "AhandlingSchedule"
            };

            int expectedServiceLevel = (int)ServiceLevelCodesEnum.Showroom;
            double expectedAmount = 100;

            ShippingConfigurationRequest shippingConfigRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = testData.ShippingConfigExtId,
                CreatedBy = "C# requester",
                //No default service level code                
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = (int)ServiceLevelCodesEnum.Showroom,
                        Label = "label showroom",
                        SortOrder = 0,
                        Amount = 200,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 0.1
                    },
                    new SCServiceLevel
                    {
                        CalculationMethod = "string",
                        Amount = 100,
                        Code = (int)ServiceLevelCodesEnum.NextDay,
                        CarrierRateDiscount = .99,
                        CarrierCode = "string",
                        IsEnabled = true,
                        Label = "string",
                        SortOrder = 1
                    },
                    new SCServiceLevel
                    {
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        Label = "grounded temporal",
                        SortOrder = 3,
                        Amount = expectedAmount,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = .99,
                    }
                }
            };
            var shippingConfResponse = await Client.ShippingConfigurations.Create(shippingConfigRequest);

            //check the sort order for the given service levels
            Assert.AreEqual(expectedServiceLevel, shippingConfResponse.Result.DefaultServiceLevelCode);

            //create flat rate group
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                CreatedBy = "temporal request",
                Name = "shipment group"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);

            //create handling group
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                CreatedBy = "temporal request",
                Name = "shipment group"
            };
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);

            //create shipping preferences
            var shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal shipment request",
                ShippingConfigurationExternalId = testData.ShippingConfigExtId,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    
                },
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };
            await Client.ShippingAccountMasterPreferences.Create(testData.AccountMasterExtId, shippingPreferenceRequest);

            ShipmentRequest request = new ShipmentRequest
            {
                ShipFromAddress = new Address
                {
                    AddressLine1 = "sample street",
                    AddressLine2 = "apt 2",
                    City = "Denver",
                    Country = "US",
                    Name = "dfs",
                    Postal = "80019",
                    StateProvinceRegion = "CO"
                },
                ShipToAddress = new Address
                {
                    AddressLine1 = "any street",
                    AddressLine2 = string.Empty,
                    City = "boulder",
                    Country = "US",
                    Name = "dfs",
                    Postal = "80019",
                    StateProvinceRegion = "CO"
                },
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        Quantity = 1,
                        CheckoutPriceExtended = 100,
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

            //test scenario clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result);

            //specific validations
            Assert.AreEqual(expectedServiceLevel, response.Result.DefaultShipmentRate.ServiceLevelCode);
        }

        protected override Task TestScenarioSetUp(ShipmentTestData data)
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
