using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.Base;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Shipments
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShipmentsEndpointTest_E : ShippingServiceBaseTest<ShipmentTestData>
    {
        public ShipmentsEndpointTest_E() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task GET_Shipment_Warning310()
        {
            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = "accountM01Warning10",
                ShippingConfigExtId = "shippingConfigShipment01",
                FlatRateScheduleGroupExtId = "flatRateGroupShipment01",
                HandlingScheduleGroupExtId = "handlingGroupShipment01",
            };

            string expectedDescription = ">150 lbs";
            int expectedCode = 310;

            //create shipping configuration
            var shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Label = "sample label",
                        Amount = 2,
                        CalculationMethod = "nothing",
                        CarrierCode = "string",
                        Code = (int)ServiceLevelCodesEnum.Showroom,
                        SortOrder = 5,
                        IsEnabled = true,
                        CarrierRateDiscount = 15
                    },
                    new SCServiceLevel
                    {
                        Amount = 4,
                        CalculationMethod = "nothing",
                        CarrierCode = "nothing",                        
                        CarrierRateDiscount = 1,
                        Code = (int)ServiceLevelCodesEnum.NextDay,
                        SortOrder = 3,
                        IsEnabled = true,
                        Label = "temporal label"
                    }
                }                
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

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
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Threeday,
                        ThresholdAmount = 1
                    }
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
                    AddressLine1 = "sample street",
                    AddressLine2 = "apt 2",
                    City = "boulder",
                    Country = "us",
                    Name = "dfs",
                    Postal = "1234567",
                    StateProvinceRegion = "co"
                },
                ShipToAddress = new Address
                {
                    AddressLine1 = "sample street",
                    AddressLine2 = "apt 2",
                    City = "mexico",
                    Country = "mx",
                    Name = "stk",
                    Postal = "1234567",
                    StateProvinceRegion = "B.C"
                },
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        ProductSku = "sample string",
                        CheckoutPriceExtended = 18.33m,
                        Quantity = 2,
                        Shipping = new ShipmentProductShipping
                        {
                            Width = 180,
                            Height = 190,
                            IsFreeShip = true,
                            Length = 200,
                            IsFreightOnly = true,
                            IsQuickShip = true,
                            WeightDimensional = 19,
                            WeightActual = 19,
                            FreightClass = 15
                        }
                    }
                }
            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            //clear all test entities
            await TestScenarioCleanUp(testData);

            //generic endpoint validations
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success, $"Response status is not successful ({response.StatusCode})");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //specific validations
            Assert.AreEqual(expectedCode, response.StatusCode);
            Assert.AreEqual(expectedDescription, response.Description);
        }

        [TestMethod]
        public async Task GET_Shipment_Warning311()
        {
            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = "getShipmentAccountMaster311",
                ShippingConfigExtId = "getShipmentShippConfig311",
                FlatRateScheduleGroupExtId = "getShipmentFrGroup311",
                HandlingScheduleGroupExtId = "getShipmentHdGroup311"
            };

            string expectedDescription = "Non-Contiguous U.S";
            int expectedCode = 311;
            await TestScenarioCleanUp(testData);
            //create shipping configuration
            var shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal shipment request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Label = "sample label",
                        Amount = 2,
                        CalculationMethod = "nothing",
                        CarrierCode = "string",
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        SortOrder = 5,
                        IsEnabled = true,
                        CarrierRateDiscount = 15
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create flat rate group
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                CreatedBy = "temporal shipment request",
                Name = "shipment group"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);

            //create handling group
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                CreatedBy = "temporal shipment request",
                Name = "shipment group"
            };
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);

            //create shipping preference
            var shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal request",
                ShippingConfigurationExternalId = testData.ShippingConfigExtId,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                        ThresholdAmount = 0
                    }
                },
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };
            await Client.ShippingAccountMasterPreferences.Create(testData.AccountMasterExtId, shippingPreferenceRequest);

            ShipmentRequest request = new ShipmentRequest
            {
                OrderId = "00000000000000000001",
                ShipFromAddress = new Address
                {
                    Country = "MX",
                    AddressLine1 = "calle delante",
                    AddressLine2 = "12",
                    City = "Ensenada",
                    Name = "mexican address",
                    StateProvinceRegion = "B.C",
                    Postal = "22770"
                },
                ShipToAddress = new Address
                {
                    Country = "MX",
                    AddressLine1 = "valle dorado",
                    AddressLine2 = "349",
                    City = "Ensenada",
                    Name = "mexican address 2",
                    StateProvinceRegion = "B.C",
                    Postal = "22780"
                },
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        CheckoutPriceExtended = 12,
                        ProductSku = "temporal",
                        Quantity = 2,
                        Shipping = new ShipmentProductShipping
                        {
                            Width = 12,
                            Height = 20,
                            Length = 10,
                            IsFreeShip = false,
                            IsFreightOnly = false,
                            WeightActual = 3,
                            IsQuickShip = false,
                            WeightDimensional = 6,
                            FreightClass = 12
                        }
                    }
                }
            };
            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            Assert.IsNotNull(response, "Response object should not be null");
            Assert.IsTrue(response.Success, "Response status should be successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            Assert.AreEqual(expectedCode, response.StatusCode);
            Assert.AreEqual(expectedDescription, response.Description);

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_Shipment_Warning312()
        {
            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = "accountM01Warning12",
                ShippingConfigExtId = "shippingConfigShipment02",
                FlatRateScheduleGroupExtId = "flatRateGroupShipment02",
                FlatRateScheduleExtId = "flatRateShipment02",
                HandlingScheduleGroupExtId = "handlingGroupShipment02",
                HandlingScheduleExtId = "handlingShipment02"
            };

            string expectedDescription = "No destination address provided";
            int expectedCode = 312;

            await TestScenarioCleanUp(testData);

            //create shipping configuration
            var shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal shipmentrequest",
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Label = "Shipment 312 req",
                        Amount = 12,
                        CalculationMethod = "nothing",
                        CarrierCode = "312",
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        SortOrder = 5,
                        IsEnabled = true,
                        CarrierRateDiscount = 0.6
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create flat rate group
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                CreatedBy = "temporal shipment request",
                Name = "shipment group"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);

            //create handling group
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                CreatedBy = "temporal shipment request",
                Name = "shipment group"
            };
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);

            //create handling schedule
            var handlingScheduleRequest = new HandlingScheduleRequest
            {
                CreatedBy = "shipment request",
                OrderAmountMin = 1,
                OrderAmountMax = 2,
                Rate = 1,
                ExternalIdentifier = testData.HandlingScheduleExtId,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
            };
            await Client.HandlingSchedules.Create(handlingScheduleRequest);

            //link handling schedule with group
            var handlingScheduleConfigurationRequest = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "shipment temporal request"
            };
            await Client.HandlingScheduleConfigurations
                .Create(testData.HandlingScheduleGroupExtId, testData.HandlingScheduleExtId, handlingScheduleConfigurationRequest);

            //create shipping preference
            var shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal request",
                ShippingConfigurationExternalId = testData.ShippingConfigExtId,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                        ThresholdAmount = 0
                    }
                },
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };
            await Client.ShippingAccountMasterPreferences.Create(testData.AccountMasterExtId, shippingPreferenceRequest);

            //get shipment request
            ShipmentRequest request = new ShipmentRequest
            {
                OrderId = "00000000000000000001",
                ShipFromAddress = new Address
                {
                    AddressLine1 = "sample street",
                    AddressLine2 = "apt 2",
                    City = "boulder",
                    Country = "us",
                    Name = "dfs",
                    Postal = "1234567",
                    StateProvinceRegion = "co"
                },
                //no destination address
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        ProductSku = "sample string",
                        CheckoutPriceExtended = 18.33m,
                        Quantity = 1,
                        Shipping = new ShipmentProductShipping
                        {
                            Width = 12,
                            Height = 12,
                            IsFreeShip = false,
                            Length = 12,
                            IsFreightOnly = false,
                            IsQuickShip = false,
                            WeightDimensional = 12.5m,
                            WeightActual = 17,
                            FreightClass = 12
                        }
                    }
                }
            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            //clear test entities
            await TestScenarioCleanUp(testData);

            //generic endpoint validations
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //specific validations
            Assert.AreEqual(expectedCode, response.StatusCode);
            Assert.AreEqual(expectedDescription, response.Description);
        }

        [TestMethod]
        public async Task GET_Shipment_Warning313()
        {
            ShipmentTestData testData = new ShipmentTestData
            {
                ShippingConfigExtId = "shippingConfigShipment03",
                AccountMasterExtId = "accountM01Warning13",
                FlatRateScheduleGroupExtId = "flatRateGroupShipment03",
                HandlingScheduleGroupExtId = "hadnlingGroupShipment03"
            };

            int serviceLevelCode = (int)ServiceLevelCodesEnum.Showroom;
            int rateTypePreference = 1;//Standard = 1?

            int expectedCode = 313;
            string expectedDescription = "Use Customer Carrier";

            await TestScenarioCleanUp(testData);

            //create shipping configuration
            var shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Label = "sample label",
                        Amount = 2,
                        CalculationMethod = "nothing",
                        CarrierCode = "string",
                        Code = (int)ServiceLevelCodesEnum.Showroom,
                        SortOrder = 5,
                        IsEnabled = true,
                        CarrierRateDiscount = 15
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create a flat rate group
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                Name = "shipment group flat rate"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);

            //create a handling group
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "shipment temporal request",
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                Name = "shipment group handling"
            };
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);

            //create an accountMaster shipping preference
            var shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "shipment temporal request",
                ShippingConfigurationExternalId = testData.ShippingConfigExtId,
                UseCustomerCarrier = true,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = serviceLevelCode,
                        ThresholdAmount = 0
                    }
                },
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };
            await Client.ShippingAccountMasterPreferences.Create(testData.AccountMasterExtId, shippingPreferenceRequest);

            ShipmentRequest request = new ShipmentRequest
            {
                OrderId = "00000000000000000001",
                ShipFromAddress = new Address
                {
                    AddressLine1 = "sample street",
                    AddressLine2 = "apt 2",
                    City = "boulder",
                    Country = "us",
                    Name = "dfs",
                    Postal = "1234567",
                    StateProvinceRegion = "co"
                },
                ShipToAddress = new Address
                {
                    AddressLine1 = "any street",
                    AddressLine2 = "C",
                    City = "boulder",
                    Country = "us",
                    Name = "dfs",
                    Postal = "1234567",
                    StateProvinceRegion = "co"
                },
                Products = new List<ShipmentProduct>
                {
                    new ShipmentProduct
                    {
                        ProductSku = "sample string",
                        CheckoutPriceExtended = 18.33m,
                        Quantity = 2,
                        Shipping = new ShipmentProductShipping
                        {
                            Width = 12,
                            Height = 12,
                            IsFreeShip = true,
                            Length = 12,
                            IsFreightOnly = true,
                            IsQuickShip = true,
                            WeightDimensional = 12.5m,
                            WeightActual = 12,
                            FreightClass = 12
                        }
                    }
                }
            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            //clear test scenario
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //specific validations
            Assert.AreEqual(expectedCode, response.StatusCode);
            Assert.AreEqual(expectedDescription, response.Description);
        }

        protected override Task TestScenarioSetUp(ShipmentTestData data)
        {
            throw new NotImplementedException();
        }

        protected override async Task TestScenarioCleanUp(ShipmentTestData testData)
        {
            await Client.ShippingAccountMasterPreferences.Remove(testData.AccountMasterExtId);

            await Client.HandlingSchedules.Remove(testData.HandlingScheduleExtId);
            await Client.HandlingScheduleGroups.Remove(testData.HandlingScheduleGroupExtId);
            await Client.HandlingScheduleConfigurations.Remove(testData.HandlingScheduleGroupExtId, testData.HandlingScheduleExtId);

            await Client.FlatRateScheduleConfigurations.Remove(testData.FlatRateScheduleGroupExtId, testData.FlatRateScheduleExtId);
            await Client.FlatRateSchedules.Remove(testData.FlatRateScheduleExtId);
            await Client.FlatRateScheduleGroups.Remove(testData.FlatRateScheduleGroupExtId);

            await Client.ShippingConfigurations.Remove(testData.ShippingConfigExtId);
        }
    }
}
