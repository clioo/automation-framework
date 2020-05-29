using System.Collections.Generic;
using System.Threading.Tasks;
using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.ShippingService.Enums;
using HttpUtiityTests.ShippingService.ShippingPreferences;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpUtiityTests.ShippingService.ShippingPreference
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShippingPreferencesEndpointTest : ShippingServiceBaseTest<ShippingPreferenceTestData>
    {
        public ShippingPreferencesEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_ShippingPreference_Success()
        {
            string externalIdentifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = externalIdentifier,
                FlatRateScheduleGroupExtId = externalIdentifier,
                HandlingScheduleGroupExtId = externalIdentifier,
                ShippingConfigurationExtId = externalIdentifier
            };

            //create configuration
            ShippingConfigurationRequest shippingConfigurationRequest = new ShippingConfigurationRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = externalIdentifier,                
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
                ExternalIdentifier = externalIdentifier,
                Name = "temporal name"
            };
            await Client.FlatRateScheduleGroups.Create(flatRateScheduleGroupsRequest);

            //create handling schedule group
            HandlingScheduleGroupRequest handlingScheduleGroupRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "temporal request",
                ExternalIdentifier = externalIdentifier,
                Name = "temporal name"
            };
            await Client.HandlingScheduleGroups.Create(handlingScheduleGroupRequest);

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                CreatedBy = "C# requester",                
                FreeFreightRules = new List<SFreeFreightRule>
                {
                    new SFreeFreightRule
                    {
                        ServiceLevelCode = 1,
                        ThresholdAmount = 1
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = externalIdentifier,
                FlatRateScheduleGroupExternalId = externalIdentifier,
                HandlingScheduleGroupExternalId = externalIdentifier
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Create(request);

            await TestScenarioCleanUp(testData);

            //validations
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task POST_ShippingPreference_Invalid()
        {
            string identifier = TestContext.TestName;

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                //does not have the CreatedBy porperty
                FreeFreightRules = new List<SFreeFreightRule>
                {
                    new SFreeFreightRule
                    {
                        ServiceLevelCode = 0,
                        ThresholdAmount = 0
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = identifier,
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Create(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task GET_ShippingPreference_Success()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.GetSingle();

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //test data clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task GET_ShippingPreference_NotFound()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //make sure that the data does not exist
            await TestScenarioCleanUp(testData);

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.GetSingle();

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingPreference_Success()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test data setup
            await TestScenarioSetUp(testData);

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                UpdatedBy = "PUT request",
                UseCustomerCarrier = false,
                ShippingConfigurationExternalId = identifier,
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Update(request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //put specific validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
        }

        [TestMethod]
        public async Task PUT_ShippingPreference_Invalid()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            //Service.ShippingPreferences.CreateEntity();

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                //does not have the UpdatedBy property
                FreeFreightRules = new List<SFreeFreightRule>
                {
                    new SFreeFreightRule
                    {
                        ServiceLevelCode = 0,
                        ThresholdAmount = 0
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = identifier,
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Update(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingPreference_NotFound()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            await TestScenarioCleanUp(testData);

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                UpdatedBy = "PUT request",
                FreeFreightRules = new List<SFreeFreightRule>
                {
                    new SFreeFreightRule
                    {
                        ServiceLevelCode = 0,
                        ThresholdAmount = 0
                    }
                },
                UseCustomerCarrier = true,
                ShippingConfigurationExternalId = identifier,
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Update(request);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_ShippingPreference_Success()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Remove();

            //test data clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_ShippingPreference_NotFound()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            await TestScenarioCleanUp(testData);

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.Remove();

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PATCH_ShippingPreference_Success()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test scenario setup
            await TestScenarioSetUp(testData);

            var request = new
            {
                UpdatedBy = "Patch request",
                UseCustomerCarrier = false
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.PatchEntity(request);

            //test data clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);

            //patch specific validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.UseCustomerCarrier, response.Result.UseCustomerCarrier);
        }

        [TestMethod]
        public async Task PATCH_ShippingPreference_NotFound()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingPreferenceTestData
            {
                AccountMasterExtId = identifier,
                FlatRateScheduleGroupExtId = identifier,
                HandlingScheduleGroupExtId = identifier,
                ShippingConfigurationExtId = identifier
            };

            //test data should not exist
            await TestScenarioCleanUp(testData);

            var request = new
            {
                UpdatedBy = "Patch request"
            };

            HttpEssResponse<ShippingPreferencesResponse> response = await Client.ShippingPreferences.PatchEntity(request);

            Assert.IsNotNull(response, $"{nameof(response)} should not be null");
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
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
            ShippingPreferencesRequest shippingAccountMasterPreferencesRequest = new ShippingPreferencesRequest
            {
                CreatedBy = "temporal request",
                ShippingConfigurationExternalId = data.ShippingConfigurationExtId,
                FlatRateScheduleGroupExternalId = data.FlatRateScheduleGroupExtId,
                HandlingScheduleGroupExternalId = data.HandlingScheduleGroupExtId,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SFreeFreightRule>
                {
                    new SFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Showroom,
                        ThresholdAmount = 2
                    }
                },
            };
            await Client.ShippingPreferences.Create(shippingAccountMasterPreferencesRequest);
        }

        protected override async Task TestScenarioCleanUp(ShippingPreferenceTestData data)
        {
            await Client.ShippingPreferences.Remove();
            await Client.ShippingConfigurations.Remove(data.ShippingConfigurationExtId);
            await Client.FlatRateScheduleGroups.Remove(data.FlatRateScheduleGroupExtId);
            await Client.HandlingScheduleGroups.Remove(data.HandlingScheduleGroupExtId);
        }
    }
}
