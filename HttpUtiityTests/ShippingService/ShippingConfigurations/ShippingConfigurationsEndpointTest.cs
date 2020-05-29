using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.TestBase;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceLevelCodesEnum = HttpUtility.EndPoints.ShippingService.Enums.ServiceLevelCodesEnum;

namespace HttpUtiityTests.ShippingService.ShippingConfigurations
{
    [TestClass]
    [TestCategory(TestingCategories.Api)]
    [TestCategory(TestingCategories.ShipingService)]
    public class ShippingConfigurationsEndpointTest : ShippingServiceBaseTest<ShippingConfigurationTestData>
    {
        public ShippingConfigurationsEndpointTest() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

        [TestMethod]
        public async Task POST_ShippingConfiguration_Success()
        {
            string externalIdentifier = "postShippingConfig01";
            int grounded = (int)ServiceLevelCodesEnum.Ground;

            ShippingConfigurationTestData testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = externalIdentifier
            };

            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                CreatedBy = "Post temporal request",
                ExternalIdentifier = externalIdentifier,
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = grounded,
                        Label = "temporal label",
                        SortOrder = 1,
                        Amount = 25,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 10
                    }
                }
            };

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Create(request);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Response status is not successful");
            Assert.IsNotNull(response.Result);

            //test scenario clean up
            await TestScenarioCleanUp(testData);
        }

        [TestMethod]
        public async Task POST_ShippingConfiguration_Invalid()
        {
            string externalIdentifier = "postShippingConfig02";

            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                ExternalIdentifier = externalIdentifier,
                //does not have the CreatedBy property
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = 0,
                        Label = "label",
                        SortOrder = 1,
                        Amount = 25,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 12
                    }
                }
            };

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Create(request);

            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task POST_ShippingConfiguration_EmptyServiceLevelsNoDefault()
        {
            string externalIdentifier = "postShippingConfig04";

            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                CreatedBy = "C# requester",
                ExternalIdentifier = externalIdentifier,                
                //No default service level code
                ServiceLevels = new List<SCServiceLevel>()
            };

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Create(request);

            //validations
            Assert.IsNotNull(response, "Response is null");
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsFalse(response.Success, "Response status is not be successful");
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task POST_ShippingConfiguration_SLsDoesNotExist_Unprocessable()
        {
            var testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = TestContext.TestName
            };

            var request = new ShippingConfigurationRequest
            {
                CreatedBy = TestContext.TestName,
                ExternalIdentifier = testData.ExternalIdentifier,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 20,
                        Label = "I do not exist",
                        CarrierRateDiscount = 0,
                        ErpExtId = "q:",
                        IsEnabled = true,
                        Code = 1222,//this SL code does not exist on ESS
                        SortOrder = 0,
                        RateShopperExtId = "q:"
                    }
                }
            };

            var response = await Client.ShippingConfigurations.Create(request);

            //validations
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public async Task POST_ShippingConfiguration_RshopperSL_Success()
        {
            string identifier = TestContext.TestName;
            var testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = identifier
            };
            var request = new ShippingConfigurationRequest
            {
                CreatedBy = identifier,
                ExternalIdentifier = identifier,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0,
                        Code = (int)ServiceLevelCodesEnum.NextDay,
                        Label = ServiceLevelCodesEnum.NextDay.ToString(),
                        SortOrder = 0,
                        IsEnabled = true,
                        CalculationMethod = string.Empty,
                        ErpExtId = "q:",
                        RateShopperExtId = RateShopperShipperCodesEnum.S01.ToString()
                    }
                }
            };
            
            var response = await Client.ShippingConfigurations.Create(request);

            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response, "response should not be null");
            Assert.AreEqual(200, response.StatusCode);
            CollectionAssert.DoesNotContain(response.Result.ServiceLevels, request.ServiceLevels.FirstOrDefault());
        }

        [TestMethod]
        public async Task GET_ShippingConfiguration_Success()
        {
            string externalIdentifier = "getShippingConfig01";

            //test scenario setup
            ShippingConfigurationTestData testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.GetSingle(testData.ExternalIdentifier);

            //test scenario clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success);
            Assert.IsNotNull(response.Result);
        }

        [TestMethod]
        public async Task GET_ShippingConfiguration_NotFound()
        {
            string externalIdentifier = "getShippConfig02";
            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.GetSingle(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsFalse(response.Success);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingConfiguration_Success()
        {
            string externalIdentifier = "putShippingConfig01";
            //test scenario setup
            ShippingConfigurationTestData testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                UpdatedBy = "PUT request",
                ExternalIdentifier = "dummyExternalIdentifier",
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = 0,
                        Label = "label",
                        SortOrder = 1,
                        Amount = 25,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 12,
                    }
                }
            };
            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Update(externalIdentifier, request);

            //test scenario clean up
            await TestScenarioCleanUp(testData);

            Assert.IsNotNull(response, "Response is null");
            Assert.IsTrue(response.Success);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsNotNull(response.Result);

            //specific validations
            Assert.AreEqual(request.UpdatedBy, response.Result.UpdatedBy);
            Assert.AreEqual(request.ServiceLevels.FirstOrDefault().Label, response.Result.ServiceLevels.FirstOrDefault().Label);
        }

        [TestMethod]
        public async Task PUT_ShippingConfiguration_Invalid()
        {
            string externalIdentifier = "putShippingConfig02";

            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                ExternalIdentifier = "dummyExternalIdentifier",
                //does not have the UpdatedBy property                
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = 0,
                        Label = "label",
                        SortOrder = 1,
                        Amount = 25,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 12
                    }
                }
            };

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Update(externalIdentifier, request);

            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.Success);
            Assert.AreEqual(422, response.StatusCode);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task PUT_ShippingConfiguration_NotFound()
        {
            string externalIdentifier = "NotFoundId";
            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                ExternalIdentifier = "dummyExternalIdentifier",
                UpdatedBy = "Dummy person",
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = 0,
                        Label = "label",
                        SortOrder = 1,
                        Amount = 25,
                        IsEnabled = true,
                        CalculationMethod = "string 1",
                        CarrierCode = "some string",
                        CarrierRateDiscount = 12
                    }
                }
            };

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Update(externalIdentifier, request);

            Assert.IsNotNull(response, "Response is null");
            Assert.IsFalse(response.Success);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result);
        }

        [TestMethod]
        public async Task DELETE_ShippingConfiguration_Success()
        {
            string externalIdentifier = "deleteShippingConfig01";

            //test scenario setup
            ShippingConfigurationTestData testData = new ShippingConfigurationTestData
            {
                ExternalIdentifier = externalIdentifier
            };
            await TestScenarioSetUp(testData);

            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Remove(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsTrue(response.Success, "Success status is false");

            //specific validations
            var getResponse = await Client.ShippingConfigurations.GetSingle(testData.ExternalIdentifier);
            Assert.IsNull(getResponse.Result, "The entity was not deleted");
            Assert.AreEqual(404, getResponse.StatusCode);
        }

        [TestMethod]
        public async Task DELETE_ShippingConfiguration_NotFound()
        {
            string externalIdentifier = "NotFoundIdentifier";
            HttpEssResponse<ShippingConfigurationResponse> response = await Client.ShippingConfigurations.Remove(externalIdentifier);

            Assert.IsNotNull(response);
            Assert.IsFalse(response.Success);
            Assert.AreEqual(404, response.StatusCode);
            Assert.IsNull(response.Result);
        }

        protected override async Task TestScenarioSetUp(ShippingConfigurationTestData data)
        {
            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                ExternalIdentifier = data.ExternalIdentifier,
                CreatedBy = "temporal request",
                DefaultServiceLevelCode = 1,
                ServiceLevels = new List<SCServiceLevel>
                {
                    new SCServiceLevel
                    {
                        Code = 1,
                        Amount = 100,
                        CarrierCode = "",
                        Label = "temporal label",
                        IsEnabled = true,
                        SortOrder = 2,
                        CarrierRateDiscount = 10,
                        CalculationMethod = "nothing"
                    }
                }
            };

            await Client.ShippingConfigurations.Create(request);
        }

        protected override async Task TestScenarioCleanUp(ShippingConfigurationTestData data)
        {
            await Client.ShippingConfigurations.Remove(data.ExternalIdentifier);
        }
    }
}
