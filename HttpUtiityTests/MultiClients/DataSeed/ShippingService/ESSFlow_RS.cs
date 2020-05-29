using HttpUtiityTests.EnvConstants;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpUtiityTests.MultiClients.DataSeed.ShippingService
{
    [TestClass]
    [TestCategory(TestingCategories.DataSeedTest)]
    public class ESSFlow_RS
    {
        //new implementation
        IAutomationDataFactory DataFactoryExtended { get; set; }
        TestShippingAuthCredential AuthCredentials = new TestShippingAuthCredential
        {
            Name = ServiceConstants.AuthName,
            Password = ServiceConstants.AuthPassword,
            PublicKey = ServiceConstants.AuthPublicKey
        };

        public ESSFlow_RS()
        {
            var tenantConfiguration = new DataFactoryConfiguration
            {
                IntegrationsApiUrl = ServiceConstants.IntegrationsAPIUrl,
                ShippingServiceApiUrl = ServiceConstants.ShippingServiceApiUrl,
                TenantExternalIdentifier = ServiceConstants.AllPointsPlatformExtId,
                TenantInternalIdentifier = ServiceConstants.AllPointsPlatformId
            };
            DataFactoryExtended = new AutomationDataFactory(tenantConfiguration);
        }

        [TestInitialize]
        public void TestSetup()
        {
            DataFactoryExtended.Shipping.Authenticate(AuthCredentials);
        }

        [TestMethod]
        public void AllPointsDefault()
        {
            string identifier = "QAdefault1";

            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 3,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = null
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
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
            var authCredentials = new TestShippingAuthCredential
            {
                Name = ServiceConstants.AuthName,
                Password = ServiceConstants.AuthPassword,
                PublicKey = ServiceConstants.AuthPublicKey
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSL()
        {
            string identifier = "AllSL";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLnonAddress()
        {
            string identifier = "AllSLNonAddress";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLCustomerCarrier()
        {
            string identifier = "AllSLCustomerCarrier";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    UseCustomerCarrier = true,
                    FreeFreightForNonContiguousStates = false
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLCustomerCarrier2()
        {
            string identifier = "AllSLCustomerCarrier2";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S20
                        },
                        //new TestServiceLevel
                        //{
                        //    Code = ServiceLevelCodesEnum.Showroom,
                        //    Amount = 0,
                        //    Label = ServiceLevelCodesEnum.Showroom.ToString(),
                        //    SortOrder = 7,
                        //    CarrierRateDiscount = 0,
                        //    ErpExtId = string.Empty,
                        //    RateShopperExtId = null

                        //}
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = true,
                    FreeFreightForNonContiguousStates = false
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLCustomerCarrier3()
        {
            string identifier = "AllSLCustomerCarrier3";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    UseCustomerCarrier = true,
                    FreeFreightForNonContiguousStates = false,
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 100
                        }
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 50,
                        OrderAmountMax = 150,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 500,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLCustomerCarrier4()
        {
            string identifier = "AllSLCustomerCarrier4";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    UseCustomerCarrier = true,
                    FreeFreightForNonContiguousStates = false,
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 100
                        },

                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 50,
                        OrderAmountMax = 150,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 0,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 500,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 0,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLCustomerCarrier5()
        {
            string identifier = "AllSLCustomerCarrier5";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    UseCustomerCarrier = true,
                    FreeFreightForNonContiguousStates = false,
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 100
                        },

                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 50,
                        OrderAmountMax = 150,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 0,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 500,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 0,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 500,
                        ExternalIdentifier = identifier + "handling03",
                        Rate = 1,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLwithFH()
        {
            string identifier = "AllSLwithFH";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Threeday,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 2000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsFreeFreight1()
        {
            string identifier = "AllPointsFF01";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = false,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsFreeFreight2()
        {
            string identifier = "AllPointsFF02";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = true,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsProximity500()
        {
            string identifier = "AllPointsProx500";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = false,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 500
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsProximity200()
        {
            string identifier = "AllPointsProx200";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = true,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 200
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsProximity100()
        {
            string identifier = "AllPointsProx100";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = false,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 100
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsProximity100otro()
        {
            string identifier = "AllPointsProx100otro";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 10,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 10,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 10,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 10,
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
                    FreeFreightForNonContiguousStates = false,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 100
                        }
                    }
                },
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLFedex()
        {
            string identifier = "AllSLFedex";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Label = "FedEx Ground",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.NRS
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            Label = "FedEx Express Saver",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF5
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 3,
                            Label = "FedEx 2nd Day",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF3
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            Label = "FedEx Priority Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF7
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 0,
                            Label = "FedEx First Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF6
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 0,
                            Label = "FedEx Standard Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF2
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = "Hold For Pickup",
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
                    FreeFreightForNonContiguousStates = false,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSLFedex2()
        {
            string identifier = "AllSLFedex2";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Ground,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = "FedEx Ground",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF1
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            Label = "FedEx Express Saver",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF5
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 3,
                            Label = "FedEx 2nd Day",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF3
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            Label = "FedEx Priority Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF7
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 0,
                            Label = "FedEx First Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF6
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 0,
                            Label = "FedEx Standard Overright",
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF2
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = "Hold For Pickup",
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
                    FreeFreightForNonContiguousStates = true,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsExpectedMessage()
        {
            string identifier = "AllSLExpMessage";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Ground,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = "FedEx Ground",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF1
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            Label = "UPS GROUND CANADA",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S25
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 3,
                            Label = "Other SC",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S33
                        },

                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = true,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsExpectedMessage2()
        {
            string identifier = "AllSLExpMessage2";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Ground,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = "FedEx Ground",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.SF1
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            Label = "UPS GROUND CANADA",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S25
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 3,
                            Label = "Other SC",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S33
                        },

                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false,
                },

            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllSL200() //International Canada
        {
            string identifier = "AllSL200s";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.UPSStandard,
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
                            RateShopperExtId = RateShopperShipperCodesEnum.S25
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSStandard,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.UPSStandard.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S38
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSExpedited,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.UPSExpedited.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S46
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSSaver,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSSaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S48
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSExpress,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSExpress.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S39
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSExpressPlus,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSExpressPlus.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 6,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S47
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

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsAllInternationalSL200() //International Canada
        {
            string identifier = "AllSLInter";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.UPSWorldwideExpress,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSWorldwideExpedited,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.UPSWorldwideExpedited.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S46
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSWorldwideSaver,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSWorldwideSaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S48
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSWorldwideExpress,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSWorldwideExpress.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S39
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.UPSWorldwideExpressPlus,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSWorldwideExpressPlus.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S47
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

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsFreeHandling()
        {
            string identifier = "AllPointsFH1";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 0,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling01",
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        Rate = 150
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 300
                        }
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsRSDown1()
        {
            // FlatRate and Handling
            string identifier = "AllPointsRSDown1";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 0,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 0,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling01",
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        Rate = 1500
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 350
                        }
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsRSDown2()
        {
            // FlatRate=N, Free=N
            string identifier = "AllPointsRSDown2";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

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

            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsRSDown3()
        {
            string identifier = "AllPointsRSDown3";

            //create user account
            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            Amount = 2,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 3,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdaysaver,
                            Amount = 4,
                            Label = ServiceLevelCodesEnum.Nextdaysaver.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S14
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 5,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 5,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Amount = 6,
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
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 0,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "handling01",
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        Rate = 150
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = true,
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 300
                        }
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsHF()
        {
            string identifier = "handlingFlats";

            //create user account
            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Ground,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = "Ground",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            Label = "3 day",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S15
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 3,
                            Label = "2 day",
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },

                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = true,
                    FreeFreightRuless = new List<TestFreeFreightRule>
                    {
                        new TestFreeFreightRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 150
                        }
                    },
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                            ThresholdAmount = 300
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 750,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 10000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 10000,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        OrderAmountMin = 500,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "handling01",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        OrderAmountMin = 50,
                        OrderAmountMax = 90,
                        ExternalIdentifier = identifier + "handling02",
                        Rate = 1000,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }
    }
}