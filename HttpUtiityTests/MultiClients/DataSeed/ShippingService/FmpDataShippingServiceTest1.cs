using HttpUtiityTests.EnvConstants;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HttpUtiityTests.MultiClients.DataSeed.ShippingService
{
    [TestClass]
    [TestCategory(TestingCategories.DataSeed)]
    public class FmpDataShippingServiceTest1
    {

        readonly IAutomationDataFactory DataFactory;

        public FmpDataShippingServiceTest1()
        {
            TestShippingAuthCredential testShippingAuthCredential =new TestShippingAuthCredential
            {
                Name = ServiceConstants.AuthName,
                Password = ServiceConstants.AuthPassword,
                PublicKey = ServiceConstants.AuthPublicKey
            };
            var configuration = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.FmpPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.FMPPlatformId
            };
            DataFactory = new AutomationDataFactory(configuration, testShippingAuthCredential);
        }

        [TestMethod]
        public void Fmp_BestRate_TestScenario()
        {
            string identifier = "bestRateAccount01";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 10,
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Ground,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S20
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            SortOrder = 1,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString()
                        },
                        new TestServiceLevel
                        {
                            Amount = 10,
                            SortOrder = 2,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Local,
                            Label = ServiceLevelCodesEnum.Local.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = true,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false
                }
            };

            DataFactory.Users.CreateTestUser(identifier);
            DataFactory.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void FMPAllSL()
        {
            string identifier = "FMPAllSL";

            //create user account
            var testUser = DataFactory.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.NextDay,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S20
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 7,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = null

                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false
                }
            };

            DataFactory.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void Fmp_TenantConfiguration()
        {
            string identifier = "fmpTenant06";
            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S20
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 7,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = null

                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = true,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false
                }
            };

            DataFactory.Shipping.CreateTenantConfiguration(configuration);
        }


    }
}
