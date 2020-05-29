using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.Base;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.Shipment;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpUtiityTests.ShippingService.Shipments
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShipmentsEndpointTest_D : ShippingServiceBaseTest<ShipmentTestData>
    {
        public ShipmentsEndpointTest_D() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task GET_Shipment_FlatRateAndHandling_Success()
        {
            string identifier = "shipmentTestD104";

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
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                Name = identifier
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
                CreatedBy = "temporal test request",
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay,
                ExternalIdentifier = testData.FlatRateScheduleExtId,
                Rate = 150,
                OrderAmountMin = 100,
                OrderAmountMax = 1000
            };
            await Client.FlatRateSchedules.Create(flatRateScheduleRequest);

            //create a flat rate and group schedules
            FlatRateScheduleConfigurationRequest flatRateScheduleConfigurationRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "temporal test request"
            };
            await Client.FlatRateScheduleConfigurations.Create(testData.FlatRateScheduleGroupExtId, testData.FlatRateScheduleExtId, flatRateScheduleConfigurationRequest);

            //create handling schedule
            HandlingScheduleRequest handlingScheduleRequest = new HandlingScheduleRequest
            {
                CreatedBy = "temporal test request",
                ExternalIdentifier = testData.HandlingScheduleExtId,
                OrderAmountMin = 100,
                OrderAmountMax = 1000,
                Rate = 50,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay
            };
            await Client.HandlingSchedules.Create(handlingScheduleRequest);

            //link handling schedule with group
            HandlingSchedulesConfigurationRequest handlingSchedulesConfigurationRequest = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "temporal test request"
            };
            await Client.HandlingScheduleConfigurations.Create(handlingScheduleGroupRequest.ExternalIdentifier, handlingScheduleRequest.ExternalIdentifier, handlingSchedulesConfigurationRequest);

            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal test request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 200,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 1,
                        Label = "temporal label",
                        CalculationMethod = "nothing",
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.NextDay
                    },
                    new SCServiceLevel
                    {
                        Amount = 300,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 0,
                        Label = "temporal label",
                        CalculationMethod = "nothing",
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.Local
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create shippingPreference for accountMaster
            ShippingAccountMasterPreferencesRequest shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal test request",
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
                        CheckoutPriceExtended = 110,
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

            //specific validations
            var shippingFlatRate = response.Result.ShipmentRates.FirstOrDefault(r => r.ServiceLevelCode == flatRateScheduleRequest.ServiceLevelCode);
            
            //this code checks all ServiceLevels, none of them is returning a handling value
            //date: 10/03/2020
            //int i = 0;
            //while(true)
            //{
            //    bool exists = Enum.IsDefined(typeof(ServiceLevelCodesEnum), i);
            //    if (exists)
            //    {
            //        flatRateScheduleRequest.ServiceLevelCode = i;
            //        shippingFlatRate = response.Result.ShipmentRates.FirstOrDefault(r => r.ServiceLevelCode == flatRateScheduleRequest.ServiceLevelCode);
            //    }
            //    i++;
            //    if (shippingFlatRate == null) continue;
            //    if (shippingFlatRate.Handling != null) break;
            //}

            //flat rate validations
            Assert.IsTrue(shippingFlatRate.IsFlatRate, $"{nameof(shippingFlatRate.IsFlatRate)} prop should be true");
            Assert.AreEqual(flatRateScheduleRequest.Rate, shippingFlatRate.Amount);

            //handling validations
            //Handlig value returned  = null
            //TO:DO  make handling don't return null
            Assert.AreEqual(null, shippingFlatRate.Handling);
            //Assert.AreEqual(handlingScheduleRequest.Rate, shippingFlatRate.Handling);
        }

        [TestMethod]
        public async Task GET_Shipment_OnlyFlatRate_Success()
        {
            string identifier = "shipmentTestD103";

            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigExtId = identifier,
                FlatRateScheduleExtId = identifier
            };

            //create flat rate schedule group
            FlatRateScheduleGroupsRequest flatRateScheduleGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal test request",
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                Name = "cheap flat rate"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupRequest);

            //create handling schedule empty group
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
                CreatedBy = "temporal test request",
                ServiceLevelCode = (int)ServiceLevelCodesEnum.NextDay,
                ExternalIdentifier = testData.FlatRateScheduleExtId,
                Rate = 150,
                OrderAmountMin = 100,
                OrderAmountMax = 1000
            };
            await Client.FlatRateSchedules.Create(flatRateScheduleRequest);

            //create a flat rate and group schedules
            FlatRateScheduleConfigurationRequest flatRateScheduleConfigurationRequest = new FlatRateScheduleConfigurationRequest
            {
                CreatedBy = "temporal test request"
            };
            await Client.FlatRateScheduleConfigurations.Create(testData.FlatRateScheduleGroupExtId, testData.FlatRateScheduleExtId, flatRateScheduleConfigurationRequest);

            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal test request",                
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 500,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 1,
                        Label = "temporal label",
                        CalculationMethod = "nothing",
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.NextDay
                    },
                    new SCServiceLevel
                    {
                        Amount = 300,
                        CarrierRateDiscount = 0.5,
                        SortOrder = 0,
                        Label = "temporal label",
                        CalculationMethod = "nothing",
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.Local
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create shippingPreference for accountMaster
            ShippingAccountMasterPreferencesRequest shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal test request",
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
                        CheckoutPriceExtended = 110,
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
                },
                OrderId = "00000000000000000002"

            };

            HttpEssResponse<ShipmentResponse> response = await Client.Shipments.GetSingle(testData.AccountMasterExtId, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //specific validations
            var shippingFlatRate = response.Result.ShipmentRates.FirstOrDefault(r => r.ServiceLevelCode == flatRateScheduleRequest.ServiceLevelCode);

            Assert.IsTrue(shippingFlatRate.IsFlatRate, $"{nameof(shippingFlatRate.IsFlatRate)} should be true");
            Assert.AreEqual(flatRateScheduleRequest.Rate, shippingFlatRate.Amount);
        }

        [TestMethod]
        public async Task GET_Shipment_OnlyHandling_Success()
        {
            string identifier = "shipmentFlatRate01";

            ShipmentTestData testData = new ShipmentTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigExtId = identifier,
                HandlingScheduleExtId = identifier
            };

            //create flat rate schedule group
            FlatRateScheduleGroupsRequest flatRateScheduleGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal test request",
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

            //create handling schedule
            HandlingScheduleRequest handlingScheduleRequest = new HandlingScheduleRequest
            {
                CreatedBy = "temporal test request",
                ExternalIdentifier = testData.HandlingScheduleExtId,
                OrderAmountMin = 0,
                OrderAmountMax = 1000,
                Rate = 50,
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Local
            };
            await Client.HandlingSchedules.Create(handlingScheduleRequest);

            //link handling schedule with group
            HandlingSchedulesConfigurationRequest handlingSchedulesConfigurationRequest = new HandlingSchedulesConfigurationRequest
            {
                CreatedBy = "temporal test request"
            };
            await Client.HandlingScheduleConfigurations.Create(handlingScheduleGroupRequest.ExternalIdentifier, handlingScheduleRequest.ExternalIdentifier, handlingSchedulesConfigurationRequest);

            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal test request",
                ExternalIdentifier = testData.ShippingConfigExtId,
                DefaultServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 200,
                        CarrierRateDiscount = 0,
                        SortOrder = 1,
                        Label = "temporal label",
                        CalculationMethod = "nothing",
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.NextDay
                    },
                    new SCServiceLevel
                    {
                        Amount = 200,
                        CarrierRateDiscount = 0,
                        SortOrder = 0,
                        Label = "temporal label",
                        CalculationMethod = "nothing",                        
                        IsEnabled = true,
                        CarrierCode = "nothing",
                        Code = (int)ServiceLevelCodesEnum.Local
                    },
                    new SCServiceLevel
                    {
                        Amount = 0,
                        SortOrder = 2,
                        Label = "ground",
                        CarrierRateDiscount = 0,
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        IsEnabled = true,
                        CalculationMethod = "nothing",
                        CarrierCode = "nothing"
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create shippingPreference for accountMaster
            ShippingAccountMasterPreferencesRequest shippingPreferenceRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal test request",
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

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result, "Result object should not be null");

            //specific validations
            var shippingRate = response.Result.ShipmentRates.FirstOrDefault(r => r.ServiceLevelCode == handlingScheduleRequest.ServiceLevelCode);

            //handling validations
            //cannot have handling without flat rate schedule
            Assert.AreEqual(handlingScheduleRequest.Rate, shippingRate.Handling);
        }

        protected override Task TestScenarioSetUp(ShipmentTestData data)
        {
            //no need to add anything here
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
