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
    public class ShipmentsEndpointTest_C : ShippingServiceBaseTest<ShipmentTestData>
    {
        public ShipmentsEndpointTest_C() : base(ServiceConstants.ShippingServiceApiUrl, ServiceConstants.AllPointsPlatformExtId,
                new ShippingAuthRequest { Name = ServiceConstants.AuthName, Password = ServiceConstants.AuthPassword, PublicKey = ServiceConstants.AuthPublicKey })
        { }

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
