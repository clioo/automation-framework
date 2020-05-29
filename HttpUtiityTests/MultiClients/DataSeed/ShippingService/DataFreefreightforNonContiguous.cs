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
    public class DataFreefreightforNonContiguous
    {
        ITestDataFactory DataFactoryAllPoints { get; set; }
        ITestDataFactory DataFactoryFmp { get; set; }

        public DataFreefreightforNonContiguous()
        {
            DataFactoryAllPoints = ConfigurationHelper.SetPlatform(TenantsEnum.AllPoints);
            DataFactoryFmp = ConfigurationHelper.SetPlatform(TenantsEnum.Fmp);
        }

        [TestMethod]
        public async Task AllPointsFreeFreight()
        {
            string identifier = "freeFreight15";

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
            //Tiene todos los identifiers 
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
                    },
                    new TestServiceLevel
                    {
                        Amount = 1,
                        CarrierRateDiscount = 0.5,
                        Code = ServiceLevelCodesEnum.Ground,
                        SortOrder = 1,
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