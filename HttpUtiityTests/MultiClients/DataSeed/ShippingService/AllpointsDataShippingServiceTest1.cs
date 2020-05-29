using HttpUtiityTests.EnvConstants;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Implementations;
using HttpUtility.Services.AutomationDataFactory.Models;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace HttpUtiityTests.MultiClients.DataSeed.ShippingService
{
    [TestClass]
    [TestCategory(TestingCategories.DataSeed)]
    public class AllpointsDataShippingServiceTest1
    {
        //new implementation
        IAutomationDataFactory DataFactoryExtended { get; set; }
        TestShippingAuthCredential AuthCredentials = new TestShippingAuthCredential
        {
            Name = ServiceConstants.AuthName,
            Password = ServiceConstants.AuthPassword,
            PublicKey = ServiceConstants.AuthPublicKey
        };

        public AllpointsDataShippingServiceTest1()
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
        public void AllPointsWithNoFlatRates()
        {
            string identifier = "noFlatRates01";

            DataFactoryExtended.Users.CreateTestUser(identifier);
            var shippingConfiguration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Nextdayam,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 1,
                            CarrierRateDiscount = 0.5,
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString(),
                            SortOrder = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Amount = 15,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            SortOrder = 3,
                            RateShopperExtId = RateShopperShipperCodesEnum.S12,
                            ErpExtId = string.Empty
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 1,
                            ErpExtId = string.Empty
                        }
                    }

                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shippingConfiguration, identifier);
        }

        [TestMethod]
        public void AllPointsWithFlatRates()
        {
            string identifier = "flatRates01";

            DataFactoryExtended.Users.CreateTestUser(identifier);

            var shippingConfig = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Showroom,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.NextDay,
                            SortOrder = 0,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01,
                            IsEnabled = true
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 1,
                            ErpExtId = string.Empty,
                            IsEnabled = true
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            Code = ServiceLevelCodesEnum.Ground,
                            SortOrder = 3,
                            CarrierRateDiscount = 0,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01,
                            IsEnabled = true
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseCustomerCarrier = false,
                    UseBestRate = false
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "schedule1",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 100,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay
                    }
                }


            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shippingConfig, identifier);
        }

        [TestMethod]
        public void AllPointsWithHandling()
        {
            string identifier = "accountHandling01";
            DataFactoryExtended.Users.CreateTestUser(identifier);

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            IsEnabled = true,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Local,
                            Label = ServiceLevelCodesEnum.Local.ToString(),
                            SortOrder = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            IsEnabled = true,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            ErpExtId = string.Empty,
                            SortOrder = 1,
                            RateShopperExtId = null
                        },
                        new TestServiceLevel
                        {
                            Amount = 1,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            SortOrder = 2,
                            RateShopperExtId = RateShopperShipperCodesEnum.S11
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
                        ServiceLevelCode = ServiceLevelCodesEnum.Local,
                        Rate = 150
                    }
                }
            };
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void AllPointsWithFlatRatesAndHandlingFees()
        {
            string identifier = "flatRatesHandling01";

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
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Amount = 0,
                            CarrierRateDiscount = 0.5,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S20
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 3,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 150,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate02",
                        OrderAmountMin = 100,
                        OrderAmountMax = 500,
                        Rate = 45,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate03",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 15,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling",
                        OrderAmountMin = 100,
                        OrderAmountMax = 200,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        Rate = 50
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling02",
                        OrderAmountMin = 100,
                        OrderAmountMax = 300,
                        Rate = 5,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay,
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling03",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 10,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }


        [TestMethod]
        public void AllPointsFreeHandlingRules()
        {
            string identifier = "allpFreeHandling01";
            DataFactoryExtended.Users.CreateTestUser(identifier);

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
                            IsEnabled = true,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Local,
                            Label = ServiceLevelCodesEnum.Local.ToString(),
                            SortOrder = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            IsEnabled = true,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            ErpExtId = string.Empty,
                            SortOrder = 1,
                            RateShopperExtId = null
                        },
                        new TestServiceLevel
                        {
                            Amount = 1,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            SortOrder = 2,
                            RateShopperExtId = RateShopperShipperCodesEnum.S11
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
                        ServiceLevelCode = ServiceLevelCodesEnum.Local,
                        Rate = 150
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    FreeHandlingRules = new List<TestFreeHandlingRule>
                    {
                        new TestFreeHandlingRule
                        {
                            ThresholdAmount = 100,
                            ServiceLevelCode = ServiceLevelCodesEnum.Local
                        }
                    }
                }
            };
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void AllPoints150Lbs310()
        {
            string identifier = "allpoints150lbs";

            var testUser = DataFactoryExtended.Users.CreateTestUser(identifier);

            var shipping = new TestShippingPreferencesConfiguration
            {
                Configuration = new TestShippingConfiguration
                {

                    DefaultServiceLevel = ServiceLevelCodesEnum.Nextdayam,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 1,
                            CarrierRateDiscount = 0.5,
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            SortOrder = 0,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString()
                        },
                        new TestServiceLevel
                        {
                            Amount = 10,
                            CarrierRateDiscount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 1
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = false,
                    FreeFreightForNonContiguousStates = false,
                    UseCustomerCarrier = true
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsWithCustomerCarrier313()
        {
            string identifier = "customerCarrier313";

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
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 2,
                            CarrierRateDiscount = 0.2,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.NextDay.ToString()
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            SortOrder = 2,
                            CarrierRateDiscount = 0.1
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0.05,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 0,
                            CarrierRateDiscount = 0
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseCustomerCarrier = true,
                    UseBestRate = false
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsTenantShippingConfiguration()
        {
            string identifier = "seedAllpConfiguration01";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Local,

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
                    UseCustomerCarrier = false,
                    UseBestRate = false
                }
            };

            DataFactoryExtended.Shipping.CreateTenantConfiguration(configuration);
        }

        [TestMethod]
        public void AllPointsFlatRatesOutsideContiguous()
        {
            string identifier = "flatRatesContiguous01";

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
                            Code = ServiceLevelCodesEnum.UPSExpress,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSExpress.ToString(),
                            SortOrder = 4,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            SortOrder = 2,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0.0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty
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
                        OrderAmountMax = 1000,
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 200,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay
                    }
                }
            };

            DataFactoryExtended.Shipping.CreateAccountConfiguration(shipping, identifier);
        }

        [TestMethod]
        public void AllPointsFreeFreight()
        {
            string identifier = "allpointsFreeFreightUser01";

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
                            Code = ServiceLevelCodesEnum.UPSExpress,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.UPSExpress.ToString(),
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            SortOrder = 2,
                            CarrierRateDiscount = 0,
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
                            ErpExtId = string.Empty
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
        public void AllPointsWithRates()
        {
            string identifier = "testRateShopperUser01";

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
                            Code = ServiceLevelCodesEnum.Threeday,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            SortOrder = 4,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.NextDay,
                            Amount = 0,
                            CarrierRateDiscount = 0,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Ground,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            SortOrder = 2,
                            CarrierRateDiscount = 0,
                            ErpExtId = string.Empty,
                            IsEnabled = true,
                            RateShopperExtId = RateShopperShipperCodesEnum.S10
                        },
                        new TestServiceLevel
                        {
                            Code = ServiceLevelCodesEnum.Showroom,
                            Amount = 0,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty
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
        public void AllPointsBestRateScenarios_1_2()
        {
            string identifier = "bestRateTestScenarios12";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Showroom,
                            Label = ServiceLevelCodesEnum.Showroom.ToString()
                        },
                        new TestServiceLevel
                        {
                            Amount = 1.5,
                            SortOrder = 1,
                            CarrierRateDiscount = 0.01,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Amount = 1,
                            SortOrder = 2,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.TwoDay,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = true,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate01",
                        Rate = 3,
                        ServiceLevelCode = ServiceLevelCodesEnum.TwoDay,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 200,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    }
                }
            };

            DataFactoryExtended.Users.CreateTestUser(identifier);
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void AllPointsBestRateScenarios_3_4()
        {
            string identifier = "bestRateScenarios34";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Ground,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
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
                            Amount = 5,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.NextDay,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        },
                        new TestServiceLevel
                        {
                            Amount = 5,
                            CarrierRateDiscount = 0,
                            SortOrder = 4,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = true,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = false
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 2,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate03",
                        Rate = 200,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling01",
                        OrderAmountMin = 0,
                        OrderAmountMax = null,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday,
                        Rate = 10
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling02",
                        OrderAmountMin = 0,
                        OrderAmountMax = null,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay,
                        Rate = 10
                    }
                }
            };

            DataFactoryExtended.Users.CreateTestUser(identifier);
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void AllPointsBestRateScenarios_5_6()
        {
            string identifier = "bestRateScenarios56";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            SortOrder = 0,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Ground,
                            Label = ServiceLevelCodesEnum.Ground.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S02
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
                            Amount = 5,
                            CarrierRateDiscount = 0,
                            SortOrder = 2,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.NextDay,
                            Label = ServiceLevelCodesEnum.NextDay.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Amount = 5,
                            CarrierRateDiscount = 0,
                            SortOrder = 3,
                            ErpExtId = string.Empty,
                            Code = ServiceLevelCodesEnum.Threeday,
                            Label = ServiceLevelCodesEnum.Threeday.ToString(),
                            RateShopperExtId = RateShopperShipperCodesEnum.S03
                        }
                    }
                },
                Preferences = new TestShippingPreferences
                {
                    UseBestRate = true,
                    UseCustomerCarrier = false,
                    FreeFreightForNonContiguousStates = true,
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
                        ExternalIdentifier = identifier + "flatRate02",
                        Rate = 2,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate03",
                        Rate = 200,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday,
                        OrderAmountMin = 0,
                        OrderAmountMax = 100
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate04",
                        Rate = 8,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        OrderAmountMin = 0,
                        OrderAmountMax = 200
                    }
                },
                HandlingSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling01",
                        OrderAmountMin = 0,
                        OrderAmountMax = null,
                        ServiceLevelCode = ServiceLevelCodesEnum.Threeday,
                        Rate = 10
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling02",
                        OrderAmountMin = 0,
                        OrderAmountMax = null,
                        ServiceLevelCode = ServiceLevelCodesEnum.NextDay,
                        Rate = 10
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling03",
                        OrderAmountMin = 0,
                        OrderAmountMax = null,
                        ServiceLevelCode = ServiceLevelCodesEnum.Ground,
                        Rate = 10
                    }
                }
            };

            DataFactoryExtended.Users.CreateTestUser(identifier);
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }

        [TestMethod]
        public void AllPointsNonPriceableRates()
        {
            string identifier = "nonPriceable01";

            var configuration = new TestShippingPreferencesConfiguration
            {
                Identifier = identifier,
                Configuration = new TestShippingConfiguration
                {
                    DefaultServiceLevel = ServiceLevelCodesEnum.Showroom,
                    ServiceLevels = new List<TestServiceLevel>
                    {
                        new TestServiceLevel
                        {
                            Amount = 0,
                            Code = ServiceLevelCodesEnum.Nextdayam,
                            SortOrder = 0,
                            Label = ServiceLevelCodesEnum.Nextdayam.ToString(),
                            IsEnabled = true,
                            ErpExtId = string.Empty,
                            CarrierRateDiscount = 0,
                            //edit this code by debugging
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            Code = ServiceLevelCodesEnum.Showroom,
                            SortOrder = 1,
                            Label = ServiceLevelCodesEnum.Showroom.ToString(),
                            ErpExtId = string.Empty,
                            CarrierRateDiscount = 0,
                            RateShopperExtId = null
                        },
                        new TestServiceLevel
                        {
                            Amount = 0,
                            Code = ServiceLevelCodesEnum.TwoDay,
                            SortOrder = 2,
                            Label = ServiceLevelCodesEnum.TwoDay.ToString(),
                            ErpExtId = string.Empty,
                            CarrierRateDiscount = 0.1,
                            //set as an invalid id
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        },
                        new TestServiceLevel
                        {
                            Amount = 15,
                            Code = ServiceLevelCodesEnum.Local,
                            Label = ServiceLevelCodesEnum.Local.ToString(),
                            ErpExtId = string.Empty,
                            CarrierRateDiscount = 0,
                            IsEnabled = true,
                            RateShopperExtId = RateShopperShipperCodesEnum.S01
                        }
                    }
                },
                FlatRateSchedules = new List<TestSchedule>
                {
                    new TestSchedule
                    {
                        ServiceLevelCode = ServiceLevelCodesEnum.Nextdayam,
                        OrderAmountMin = 100,
                        OrderAmountMax = 1500,
                        ExternalIdentifier = identifier + "flatrate01",
                        Rate = 30
                    }
                }
            };

            DataFactoryExtended.Users.CreateTestUser(identifier);
            DataFactoryExtended.Shipping.CreateAccountConfiguration(configuration, identifier);
        }
    }
}
