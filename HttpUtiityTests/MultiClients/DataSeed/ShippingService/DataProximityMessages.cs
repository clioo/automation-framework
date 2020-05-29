using HttpUtiityTests.EnvConstants;
using HttpUtiityTests.MultiClients.DataSeed.Helpers;
using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using HttpUtility.Services.AutomationDataFactory;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using HttpUtility.Services.AutomationDataFactory.Models.UserAccount;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtiityTests.MultiClients.DataSeed.ShippingService
{
    [TestClass]
    [TestCategory(TestingCategories.DataSeed)]
    public class DataProximityMessages
    {
        ITestDataFactory DataFactoryAllPoints { get; set; }
        ITestDataFactory DataFactoryFmp { get; set; }

        public DataProximityMessages()
        {
            DataFactoryAllPoints = ConfigurationHelper.SetPlatform(TenantsEnum.AllPoints);
            DataFactoryFmp = ConfigurationHelper.SetPlatform(TenantsEnum.Fmp);
        }

        [TestMethod]
        public async Task AllPointsProximityMessage()
        {
            string identifier = "ProxMessage05";

            var testUser = new TestUserAccount
            {
                Email = identifier + "@mail.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "dfs",
                    Email = "samplemail@mail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                },
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = identifier,
                    ContactExtId = identifier,
                    LoginExtId = identifier,
                    UserExtId = identifier
                }
            };
            testUser = await DataFactoryAllPoints.UserAccounts.CreateUserAccount(testUser);

            TestShippingExternals customerCarrierAccount = new TestShippingExternals
            {
                AccountMasterExtId = identifier,
                ConfigurationExtId = identifier,
                FlatRateGroupExtId = identifier,
                HandlingGroupExtId = identifier
            };

            await DataFactoryAllPoints.ShippingConfigurationPreferences.RemoveAccountPreferences(customerCarrierAccount);

            var configuration = new TestShippingConfiguration
            {
                FreeParcelShipProximityMessageDollar = 500,
                FreeParcelShipProximityMessagePercentage = 0.5,
                Identifier = identifier,
                DefaultServiceLevel = ServiceLevelCodesEnum.Nextdayam,
                ServiceLevels = new List<TestServiceLevel>
                {
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Nextdayam,
                        SortOrder = 7,
                        Label = "Next Day AM",
                        RateShopperExtId = RateShopperShipperCodesEnum.S20,
                        ErpExtId = ""
                    },
                    new TestServiceLevel
                    {
                        Amount = 1,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Ground,
                        SortOrder = 1,
                        Label = "Ground",
                        RateShopperExtId = RateShopperShipperCodesEnum.S41,
                        ErpExtId = ""
                    },
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Nextdaysaver,
                        SortOrder = 5,
                        Label = "Next Day Air Saver",
                        RateShopperExtId = RateShopperShipperCodesEnum.S21,
                        ErpExtId = ""
                    },
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0,
                        Code = ServiceLevelCodesEnum.Showroom,
                        Label = "Showroom",
                        SortOrder = 8,
                        RateShopperExtId = RateShopperShipperCodesEnum.S01,
                        ErpExtId = ""
                    }
                }
            };
            await DataFactoryAllPoints.ShippingConfigurationPreferences.CreateConfiguration(configuration);

            var preferences = new TestShippingPreferences
            {
                ConfigurationExtId = identifier,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                        ThresholdAmount = 500

                    }
                },
                //agregar de la imagen
                FreeFreightForNonContiguousStates = true
            };

            await DataFactoryAllPoints.ShippingConfigurationPreferences.AddAccountPreferences(customerCarrierAccount, preferences);

        }

        [TestMethod]
        public async Task AllPointsBestRate()
        {
            string identifier = "BestRate01";

            var testUser = new TestUserAccount
            {
                Email = identifier + "@mail.com",
                ContactInformation = new TestContactInformation
                {
                    CompanyName = "dfs",
                    Email = "samplemail@mail.com",
                    FirstName = "John",
                    LastName = "Doe",
                    PhoneNumber = "1234567890"
                },
                AccountExternalIds = new TestExternalIdentifiers
                {
                    AccountMasterExtId = identifier,
                    ContactExtId = identifier,
                    LoginExtId = identifier,
                    UserExtId = identifier
                }
            };
            testUser = await DataFactoryAllPoints.UserAccounts.CreateUserAccount(testUser);

            TestShippingExternals customerCarrierAccount = new TestShippingExternals
            {
                AccountMasterExtId = identifier,
                ConfigurationExtId = identifier,
                FlatRateGroupExtId = identifier,
                HandlingGroupExtId = identifier,
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
                        ServiceLevelCode = ServiceLevelCodesEnum.Nextdayam
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "flatRate03",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 15,
                        ServiceLevelCode = ServiceLevelCodesEnum.Nextdaysaver
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
                        ServiceLevelCode = ServiceLevelCodesEnum.Nextdayam,
                    },
                    new TestSchedule
                    {
                        ExternalIdentifier = identifier + "handling03",
                        OrderAmountMin = 100,
                        OrderAmountMax = 1000,
                        Rate = 10,
                        ServiceLevelCode = ServiceLevelCodesEnum.Nextdaysaver
                    }
                }
            };

            await DataFactoryAllPoints.ShippingConfigurationPreferences.RemoveAccountPreferences(customerCarrierAccount);

            var configuration = new TestShippingConfiguration
            {
                FreeParcelShipProximityMessageDollar = 500,
                FreeParcelShipProximityMessagePercentage = 0.5,
                Identifier = identifier,
                DefaultServiceLevel = ServiceLevelCodesEnum.Nextdayam,
                ServiceLevels = new List<TestServiceLevel>
                {
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Nextdayam,
                        SortOrder = 1,
                        Label = "Next Day AM",
                    },
                    new TestServiceLevel
                    {
                        Amount = 1,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Ground,
                        SortOrder = 7,
                        Label = "Ground",
                    },
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Nextdaysaver,
                        SortOrder = 5,
                        Label = "Next Day Air Saver",
                    },
                    new TestServiceLevel
                    {
                        Amount = 0,
                        CarrierRateDiscount = 0,
                        Code = ServiceLevelCodesEnum.Showroom,
                        Label = "Showroom",
                        SortOrder = 8
                    }
                }
            };
            await DataFactoryAllPoints.ShippingConfigurationPreferences.CreateConfiguration(configuration);

            var preferences = new TestShippingPreferences
            {
                UseBestRate = true,
                ConfigurationExtId = identifier,
                UseCustomerCarrier = false,
                FreeFreightRules = new List<SAMFreeFreightRule>
                {
                    new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)ServiceLevelCodesEnum.Ground,
                        ThresholdAmount = 500

                    }
                },
                //agregar de la imagen
                FreeFreightForNonContiguousStates = true
            };

            await DataFactoryAllPoints.ShippingConfigurationPreferences.AddAccountPreferences(customerCarrierAccount, preferences);


        }
    }
}
