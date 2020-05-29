using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.ShippingService.ShippingPreferences;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpUtiityTests.ShippingService.ShippingPreference
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShippingAccountMasterTest : ShippingServiceBaseTest<ShippingPreferenceTestData>
    {
        public ShippingAccountMasterTest() : base(ServiceConstants.AllPointsPlatformExtId) { }

        [TestMethod]
        public async Task POST_ShippingAccountMaster_Success()
        {
            string accountMasterExtId = "postAccountMasterShippingPref01";

            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = accountMasterExtId,
                ShippingConfigurationExtId = "postShippingPrefShippConfig01",
                FlatRateScheduleGroupExtId = "postShippingPrefFrScheduleGroup01",
                HandlingScheduleGroupExtId = "postShippingPrefHdScheduleGroup01"
            };

            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.ShippingConfigurationExtId,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        Amount = 200,
                        CarrierCode = "nothing",
                        IsEnabled = true,
                        Label = "temporal label",
                        SortOrder = 2,
                        CarrierRateDiscount = 2
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create flat rate schedule group
            FlatRateScheduleGroupsRequest flatRateScheduleGroupsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.FlatRateScheduleGroupExtId,
                Name = "temporal name"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupsRequest);

            //create handling schedule group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = testData.HandlingScheduleGroupExtId,
                Name = "temporal name"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "post request success",
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 1
                    }
                },
                FreeHandlingRules = new List<FreeHandlingRule>
                {
                    new FreeHandlingRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 109
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = testData.ShippingConfigurationExtId,
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Create(accountMasterExtId, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task POST_ShippingAccountMaster_Invalid()
        {
            //does not need to exist
            string accountMasterExtId = "fakeAccountMasterExtId";

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                //does not have the CreatedBy property
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 1
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = "",
                FlatRateScheduleGroupExternalId = "",
                HandlingScheduleGroupExternalId = ""
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Create(accountMasterExtId, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task POST_ShippingAccountMaster_FreeFreightRulesOnResponse_Success()
        {
            string identifier = "postAmPreferenceFrRulesOnResponse01";
            string createdBy = "temporal test request";

            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };
            var expectedFrRule1 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ThresholdAmount = 150
            };
            var expectedFrRule2 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ThresholdAmount = 500
            };

            //create groups
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "flat rate group"
            };
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "handling rate group",
            };
            var configurationRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = identifier,
                CreatedBy = createdBy,
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierCode = string.Empty,
                        CarrierRateDiscount = 0.05,
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.Local,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        SortOrder = 0
                    }
                }
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);
            await Client.ShippingConfigurations.Create(configurationRequest);

            var request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = createdBy,
                FlatRateScheduleGroupExternalId = flatRateGroupRequest.ExternalIdentifier,
                HandlingScheduleGroupExternalId = handlingGroupRequest.ExternalIdentifier,
                FreeFreightForNonContiguousStates = false,
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = configurationRequest.ExternalIdentifier,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {

                    expectedFrRule1,
                    expectedFrRule2
                }
            };

            var response = await Client.ShippingAccountMasterPreferences.Create(identifier, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response, "response object should not ");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //specific validations            
            Assert.IsNotNull(response.Result.FreeFreightRules, "FreeFreightRules are not in the response object");
            Assert.AreEqual(expectedFrRule1.ServiceLevelCode, response.Result.FreeFreightRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(expectedFrRule1.ThresholdAmount, response.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
            Assert.AreEqual(expectedFrRule2.ServiceLevelCode, response.Result.FreeFreightRules.ElementAt(1).ServiceLevelCode);
            Assert.AreEqual(expectedFrRule2.ThresholdAmount, response.Result.FreeFreightRules.ElementAt(1).ThresholdAmount);
        }

        [TestMethod]
        public async Task POST_ShippingAccountMaster_DoesNotAllowDuplicateServiceLevels_Success()
        {
            string identifier = "postAmPreferenceNoDuplicateFrRules01";
            string createdBy = "temporal test request";

            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //create groups
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "flat rate group"
            };
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "handling rate group",
            };
            var configurationRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = identifier,
                CreatedBy = createdBy,
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierCode = string.Empty,
                        CarrierRateDiscount = 0.05,
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.Local,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        SortOrder = 0
                    }
                }
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);
            await Client.ShippingConfigurations.Create(configurationRequest);

            var request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = createdBy,
                FlatRateScheduleGroupExternalId = flatRateGroupRequest.ExternalIdentifier,
                HandlingScheduleGroupExternalId = handlingGroupRequest.ExternalIdentifier,
                FreeFreightForNonContiguousStates = false,
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = configurationRequest.ExternalIdentifier,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                        ThresholdAmount = 150
                    },
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                        ThresholdAmount = 500
                    }
                }
            };

            var response = await Client.ShippingAccountMasterPreferences.Create(identifier, request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response, "response object should not ");

            //specific validations
            Assert.IsNull(response.Result, "result object should not be null");
            Assert.IsFalse(response.Success);
        }

        [TestMethod]
        public async Task GET_ShippingAccountMaster_Success()
        {
            string accountMasterExtId = "getShippingAccountMaster01";
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = accountMasterExtId,
                ShippingConfigurationExtId = "getShippingAmPrefShipConfig01",
                FlatRateScheduleGroupExtId = "getShippingAmPreferencesFrGroup01",
                HandlingScheduleGroupExtId = "getShippingAmPreferencesHdGroup01"
            };

            //preconditions
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.GetSingle(accountMasterExtId);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_ShippingAccountMaster_NotFound()
        {
            string accountMasterExtId = "getCase02";

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.GetSingle(accountMasterExtId);

            Assert.IsNotNull(response, "Entire GET response object is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_ShippingAccountMaster_FreeFreightRulesOnResponse_Success()
        {
            string identifier = "getAmPreferenceFrRulesOnResponse02";
            string createdBy = "temporal test request";

            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };
            var expectedFrRule1 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ThresholdAmount = 150
            };
            var expectedFrRule2 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ThresholdAmount = 500
            };

            //create groups
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "flat rate group"
            };
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "handling rate group",
            };
            var configurationRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = identifier,
                CreatedBy = createdBy,
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierCode = string.Empty,
                        CarrierRateDiscount = 0.05,
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.Local,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        SortOrder = 0
                    }
                }
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);
            await Client.ShippingConfigurations.Create(configurationRequest);

            var request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = createdBy,
                FlatRateScheduleGroupExternalId = flatRateGroupRequest.ExternalIdentifier,
                HandlingScheduleGroupExternalId = handlingGroupRequest.ExternalIdentifier,
                FreeFreightForNonContiguousStates = false,
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = configurationRequest.ExternalIdentifier,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    expectedFrRule1,
                    expectedFrRule2
                }
            };

            var response = await Client.ShippingAccountMasterPreferences.Create(identifier, request);

            //validations
            Assert.IsNotNull(response, "response object should not ");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //GET request
            var getResponse = await Client.ShippingAccountMasterPreferences.GetSingle(testData.AccountMasterExtId);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //specific validations            
            Assert.IsNotNull(getResponse.Result.FreeFreightRules, "FreeFreightRules are not in the response object");
            Assert.AreEqual(expectedFrRule1.ServiceLevelCode, getResponse.Result.FreeFreightRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(expectedFrRule1.ThresholdAmount, getResponse.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
            Assert.AreEqual(expectedFrRule2.ServiceLevelCode, getResponse.Result.FreeFreightRules.ElementAt(1).ServiceLevelCode);
            Assert.AreEqual(expectedFrRule2.ThresholdAmount, getResponse.Result.FreeFreightRules.ElementAt(1).ThresholdAmount);
        }

        [TestMethod]
        public async Task PUT_ShippingAccountMaster_Success()
        {
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = "putShippingAm01",
                ShippingConfigurationExtId = "putShippingAmShippConf01",
                HandlingScheduleGroupExtId = "putShippingAmHdScheduleGroup01",
                FlatRateScheduleGroupExtId = "putShippingAmFrScheduleGroup01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                UpdatedBy = "PUT request",
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = testData.ShippingConfigurationExtId,
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Update(testData.AccountMasterExtId, request);

            //clear generated test data
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //specific put validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
        }

        [TestMethod]
        public async Task PUT_ShippingAccountMaster_Invalid()
        {
            string accountMasterExtId = "notFound";

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = 0,
                        ThresholdAmount = 0
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = "ShippingConfigurationExternalId",
                FlatRateScheduleGroupExternalId = "FlatRateScheduleGroupExternalId",
                HandlingScheduleGroupExternalId = "HandlingScheduleGroupExternalId"
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Update(accountMasterExtId, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingAccountMaster_NotFound()
        {
            string accountMasterExtId = "updateCase03";

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                UpdatedBy = accountMasterExtId,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 1
                    }
                },
                UseCustomerCarrier = true
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Update(accountMasterExtId, request);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingAccountMaster_FreeFreightRules_Success()
        {
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = "putShippingAm01",
                ShippingConfigurationExtId = "putShippingAmShippConf01",
                HandlingScheduleGroupExtId = "putShippingAmHdScheduleGroup01",
                FlatRateScheduleGroupExtId = "putShippingAmFrScheduleGroup01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                UpdatedBy = "PUT request",
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 10
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = testData.ShippingConfigurationExtId,
                FlatRateScheduleGroupExternalId = testData.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = testData.HandlingScheduleGroupExtId
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Update(testData.AccountMasterExtId, request);

            //clear generated test data
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //specific put validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
            Assert.AreEqual(request.FreeFreightRules.FirstOrDefault().ThresholdAmount, response.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
            //Assert.AreEqual(request.FreeFreightRules.FirstOrDefault().ThresholdAmount, response.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
        }

        [TestMethod]
        public async Task DELETE_ShippingAccountMaster_Success()
        {
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = "deleteShippingAm01",
                HandlingScheduleGroupExtId = "deleteShippingAmHandSg01",
                FlatRateScheduleGroupExtId = "deleteShippingAmFlatrSg01",
                ShippingConfigurationExtId = "deleteShippingAmShippConf01"
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Remove(testData.AccountMasterExtId);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task DELETE_ShippingAccountMaster_NotFound()
        {
            string accountMasterExtId = "deleteCase02";

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.Remove(accountMasterExtId);

            Assert.IsNotNull(response, "Response object for DELETE is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PATCH_ShippingAccountMaster_Success()
        {
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = "patchShippingAm01",
                ShippingConfigurationExtId = "patchShippingAmShippConf01",
                HandlingScheduleGroupExtId = "patchShippingAmHdScheduleGroup01",
                FlatRateScheduleGroupExtId = "patchShippingAmFrScheduleGroup"
            };
            //test scenario setup
            await TestScenarioSetUp(testData);

            var request = new
            {
                UpdatedBy = "Patch req q:",
                
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                        ThresholdAmount = 140M
                    }
                },
                FreeFreightForNonContiguousStates = true,
                UseCustomerCarrier = true
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.PatchEntity(testData.AccountMasterExtId, request);

            //clear generated test data
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response, "Response object for DELETE is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //patch specific validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
            Assert.AreEqual(request.FreeFreightForNonContiguousStates, response.Result.FreeFreightForNonContiguousStates);
            Assert.AreEqual(request.FreeFreightRules.FirstOrDefault().ServiceLevelCode, response.Result.FreeFreightRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(request.FreeFreightRules.FirstOrDefault().ThresholdAmount, response.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
        }

        [TestMethod]
        public async Task PATCH_ShippingAccountMaster_FreeHandlingRules_Success()
        {
            string identifier = "shippingAmPatchFreeHandlingRules01";
            ShippingPreferenceTestData testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                ShippingConfigurationExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                FlatRateScheduleGroupExtId = identifier
            };
            //test scenario setup
            await TestScenarioSetUp(testData);

            var request = new
            {
                UpdatedBy = "Patch req q:",

                FreeHandlingRules = new List<FreeHandlingRule>
                {
                    new FreeHandlingRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                        ThresholdAmount = 140M
                    }
                },
                FreeFreightForNonContiguousStates = true,
                UseCustomerCarrier = true
            };

            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.PatchEntity(testData.AccountMasterExtId, request);

            //clear generated test data
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response, "Response object for PATCH is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //patch specific validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
            Assert.AreEqual(request.FreeFreightForNonContiguousStates, response.Result.FreeFreightForNonContiguousStates);
            Assert.IsTrue(response.Result.FreeHandlingRules.Count > 0, $"{nameof(response.Result.FreeHandlingRules)} is empty");
            Assert.AreEqual(request.FreeHandlingRules.FirstOrDefault().ServiceLevelCode, response.Result.FreeHandlingRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(request.FreeHandlingRules.FirstOrDefault().ThresholdAmount, response.Result.FreeHandlingRules.FirstOrDefault().ThresholdAmount);
        }

        [TestMethod]
        public async Task PATCH_ShippingAccountMaster_NotFound()
        {
            //does not need to exist
            string accountMasterExtId = "blahblah";

            var request = new { };
            HttpEssResponse<ShippingAccountMasterPreferencesResponse> response = await Client.ShippingAccountMasterPreferences.PatchEntity(accountMasterExtId, request);

            Assert.IsNotNull(response, "Response object for DELETE is null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }        

        [TestMethod]
        public async Task PATCH_ShippingAccountMaster_FreeFreightRulesOnResponse_Success()
        {
            string identifier = "patchAmPreferenceFrRulesOnResponse03";
            string createdBy = "temporal test request";

            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };
            var expectedFrRule1 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ThresholdAmount = 150
            };
            var expectedFrRule2 = new SAMFreeFreightRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ThresholdAmount = 500
            };

            //create groups
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "flat rate group"
            };
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "handling rate group",
            };
            var configurationRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = identifier,
                CreatedBy = createdBy,
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierCode = string.Empty,
                        CarrierRateDiscount = 0.05,
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.Local,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        SortOrder = 0
                    }
                }
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);
            await Client.ShippingConfigurations.Create(configurationRequest);

            var request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = createdBy,
                FlatRateScheduleGroupExternalId = flatRateGroupRequest.ExternalIdentifier,
                HandlingScheduleGroupExternalId = handlingGroupRequest.ExternalIdentifier,
                FreeFreightForNonContiguousStates = false,
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = configurationRequest.ExternalIdentifier,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    expectedFrRule1,
                    expectedFrRule2
                }
            };

            var response = await Client.ShippingAccountMasterPreferences.Create(identifier, request);

            //basic post validations
            Assert.IsNotNull(response, "response object should not ");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //PATCH request
            var patchRequest = new { UpdatedBy = "patch request" };
            var patchResponse = await Client.ShippingAccountMasterPreferences.PatchEntity(testData.AccountMasterExtId, patchRequest);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //specific validations            
            Assert.IsNotNull(patchResponse.Result.FreeFreightRules, "FreeFreightRules are not in the response object");
            Assert.AreEqual(expectedFrRule1.ServiceLevelCode, patchResponse.Result.FreeFreightRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(expectedFrRule1.ThresholdAmount, patchResponse.Result.FreeFreightRules.FirstOrDefault().ThresholdAmount);
            Assert.AreEqual(expectedFrRule2.ServiceLevelCode, patchResponse.Result.FreeFreightRules.ElementAt(1).ServiceLevelCode);
            Assert.AreEqual(expectedFrRule2.ThresholdAmount, patchResponse.Result.FreeFreightRules.ElementAt(1).ThresholdAmount);
        }

        [TestMethod]
        public async Task PATCH_ShippingAccountMaster_FreeHandlingRulesOnResponse_Success()
        {
            string identifier = "patchAmPreferenceFhRulesOnResponse04";
            string createdBy = "temporal test request";

            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };
            var expectedFhRule1 = new FreeHandlingRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Local,
                ThresholdAmount = 150
            };
            var expectedFhRule2 = new FreeHandlingRule
            {
                ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                ThresholdAmount = 500
            };

            //create groups
            var flatRateGroupRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "flat rate group"
            };
            var handlingGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = createdBy,
                ExternalIdentifier = identifier,
                Name = "handling rate group",
            };
            var configurationRequest = new ShippingConfigurationRequest
            {
                ExternalIdentifier = identifier,
                CreatedBy = createdBy,
                DefaultServiceLevelCode = null,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierCode = string.Empty,
                        CarrierRateDiscount = 0.05,
                        IsEnabled = true,
                        Code = (int)ServiceLevelCodesEnum.Local,
                        Label = ServiceLevelCodesEnum.Local.ToString(),
                        SortOrder = 0
                    }
                }
            };
            await Client.FlatRateScheduleGroups.Create(flatRateGroupRequest);
            await Client.HandlingScheduleGroups.Create(handlingGroupRequest);
            await Client.ShippingConfigurations.Create(configurationRequest);

            var request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = createdBy,
                FlatRateScheduleGroupExternalId = flatRateGroupRequest.ExternalIdentifier,
                HandlingScheduleGroupExternalId = handlingGroupRequest.ExternalIdentifier,
                FreeFreightForNonContiguousStates = false,
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = configurationRequest.ExternalIdentifier,
                FreeHandlingRules = new List<FreeHandlingRule>
                {
                    expectedFhRule1,
                    expectedFhRule2
                }
            };

            var response = await Client.ShippingAccountMasterPreferences.Create(identifier, request);

            //basic post validations
            Assert.IsNotNull(response, "response object should not ");
            Assert.IsNotNull(response.Result, "result object should not be null");

            //PATCH request
            var patchRequest = new { UpdatedBy = "patch request" };
            var patchResponse = await Client.ShippingAccountMasterPreferences.PatchEntity(testData.AccountMasterExtId, patchRequest);

            //test data clean up
            await TestScenarioCleanUp(testData);

            //specific validations            
            Assert.IsNotNull(patchResponse.Result.FreeFreightRules, "FreeHandlingRules are not in the response object");
            Assert.AreEqual(expectedFhRule1.ServiceLevelCode, patchResponse.Result.FreeHandlingRules.FirstOrDefault().ServiceLevelCode);
            Assert.AreEqual(expectedFhRule1.ThresholdAmount, patchResponse.Result.FreeHandlingRules.FirstOrDefault().ThresholdAmount);
            Assert.AreEqual(expectedFhRule2.ServiceLevelCode, patchResponse.Result.FreeHandlingRules.ElementAt(1).ServiceLevelCode);
            Assert.AreEqual(expectedFhRule2.ThresholdAmount, patchResponse.Result.FreeHandlingRules.ElementAt(1).ThresholdAmount);
        }

        protected override async Task TestScenarioSetUp(ShippingPreferenceTestData data)
        {
            //create shipping configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.ShippingConfigurationExtId,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = (int)ServiceLevelCodesEnum.Ground,
                        Amount = 200,
                        CarrierCode = "nothing",
                        IsEnabled = true,
                        Label = "temporal label",
                        SortOrder = 2,
                        CarrierRateDiscount = 2
                    }
                }
            };
            await Client.ShippingConfigurations.Create(shippingConfigurationRequest);

            //create flat rate schedule group
            FlatRateScheduleGroupsRequest flatRateScheduleGroupsRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.FlatRateScheduleGroupExtId,
                Name = "temporal name"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupsRequest);

            //create handling schedule group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = data.HandlingScheduleGroupExtId,
                Name = "temporal name"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            //create account master shipping preference
            ShippingAccountMasterPreferencesRequest shippingAccountMasterPreferencesRequest = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "temporal request",
                ShippingConfigurationExternalId = data.ShippingConfigurationExtId,
                FlatRateScheduleGroupExternalId = data.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = data.HandlingScheduleGroupExtId,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Showroom,
                        ThresholdAmount = 2
                    }
                },
            };
            await Client.ShippingAccountMasterPreferences.Create(data.AccountMasterExtId, shippingAccountMasterPreferencesRequest);
        }

        protected override async Task TestScenarioCleanUp(ShippingPreferenceTestData data)
        {
            await Client.ShippingAccountMasterPreferences.Remove(data.AccountMasterExtId);
            await Client.ShippingConfigurations.Remove(data.ShippingConfigurationExtId);
            await Client.FlatRateScheduleGroups.Remove(data.FlatRateScheduleGroupExtId);
            await Client.HandlingScheduleGroups.Remove(data.HandlingScheduleGroupExtId);
        }
    }
}
