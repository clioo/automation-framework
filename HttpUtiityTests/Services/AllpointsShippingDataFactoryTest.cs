using HttpUtiityTests.EnvConstants;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtiityTests.Services
{
    [TestClass]
    public class AllpointsShippingDataFactoryTest
    {
        readonly IAutomationDataFactory DataFactory;

        public AllpointsShippingDataFactoryTest()
        {
            var config = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.AllPointsPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.AllPointsPlatformId
            };
            DataFactory = new AutomationDataFactory(config);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_CustomShippingConfiguration_Success()
        {
            string userIdentifier = "allpointsUserCustomShipping01";

            var testShippingConfiguration = new TestShippingPreferencesConfiguration
            {
                Identifier = userIdentifier,
                //shipping configuration
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Local,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 1,
                            CarrierRateDiscount = 0,
                            SortOrder = 0,
                            Code = ServiceLevelCodesEnum.Local,
                            Label = ServiceLevelCodesEnum.Local.ToString()
                        }
                    }
                },
                //preferences
                Preferences = new TestShippingPreferences
                {

                },
                //no flat or handling rates
            };
            var testUser = DataFactory.Users.CreateTestUser(userIdentifier);

            DataFactory.Shipping.RemoveAccountConfiguration(testShippingConfiguration, userIdentifier);
            DataFactory.Shipping.CreateAccountConfiguration(testShippingConfiguration, testUser.AccountExternalIds.AccountMasterExtId);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_CustomFlatRates_Success()
        {
            string identifier = "allPointsCustomRates01";

            var testShippingConfiguration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            Label = ServiceLevelCodesEnum.Local.ToString(),
                            Code = ServiceLevelCodesEnum.Local,
                            SortOrder = 0
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 150,
                        ServiceLevelCode = ServiceLevelCodesEnum.Local,
                        ExternalIdentifier = identifier + "flatRate01"
                    }
                }
            };
            var testUser = DataFactory.Users.CreateTestUser(identifier);

            DataFactory.Shipping.RemoveAccountConfiguration(testShippingConfiguration, identifier);
            DataFactory.Shipping.CreateAccountConfiguration(testShippingConfiguration, testUser.AccountExternalIds.AccountMasterExtId);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_CustomFlatRatesWithHandling_Success()
        {
            string identifier = "allpointsShippingFlatRatesHandling";

            var testShippingConfiguration = new TestShippingPreferencesConfiguration
            {
                //shipping configuration
                //preferences
                //flat rates
                //handling rates
            };
            var testUser = DataFactory.Users.CreateTestUser(identifier);

            DataFactory.Shipping.CreateAccountConfiguration(testShippingConfiguration, testUser.AccountExternalIds.AccountMasterExtId);

            Console.WriteLine(testUser.Username);
        }

        [TestMethod]
        public void Create_Allpoints_UserAccount_FreeFreightForNonContiguous_Success()
        {
            string identifier = "allpointsShippingFreeFreightNonContiguous";

            var testShippingConfiguration = new TestShippingPreferencesConfiguration
            {
                //shipping configuration
                //preferences
                //non contiguous set to true
                Preferences = new TestShippingPreferences
                {
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Local,
                            ThresholdAmount = 100
                        }
                    }
                }
            };
            var testUser = DataFactory.Users.CreateTestUser(identifier);

            DataFactory.Shipping.CreateAccountConfiguration(testShippingConfiguration, testUser.AccountExternalIds.AccountMasterExtId);

            Console.WriteLine(testUser.Username);
        }
    }
}
