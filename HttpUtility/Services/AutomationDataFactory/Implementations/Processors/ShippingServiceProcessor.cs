using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Enums;
using HttpUtility.EndPoints.ShippingService.Models;
using HttpUtility.EndPoints.ShippingService.Models.Auth;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using HttpUtility.Services.AutomationDataFactory.Models;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using HttpUtility.Services.AutomationDataFactory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class ShippingServiceProcessor
    {
        IShippingServiceClient Client;
        readonly string _essApiUrl;
        readonly string _tenantExtId;

        public ShippingServiceProcessor(IShippingServiceClient initializedClient, string essApiUrl, string tenantExtId)
        {

            Client = initializedClient;
            _essApiUrl = essApiUrl;
            _tenantExtId = tenantExtId;
        }

        public async Task CreateAccountPreferences(string identifier, string accountMasterId, TestShippingPreferences preferences)
        {
            //TODO
            //validation:
            //at this step the configuration should exist

            //create handling and  flat rate generic groups
            await CreateEmptyGroups(identifier, identifier);

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "Data factory tool",
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier,
                ShippingConfigurationExternalId = identifier,
                UseCustomerCarrier = preferences.UseCustomerCarrier,
                FreeFreightForNonContiguousStates = preferences.FreeFreightForNonContiguousStates,
                FreeFreightRules = MapAccountFreeFreightRules(preferences.FreeFreightRuless),
                FreeHandlingRules = MapFreeHandlingRules(preferences.FreeHandlingRules),
                UseBestRate = preferences.UseBestRate
            };
            var response = await Client.ShippingAccountMasterPreferences.Create(accountMasterId, request);

            if (!response.Success) throw new Exception($"Cannot add the given account shipping preferences ~> error code: {response.StatusCode}");
        }

        public async Task CreateTenantPreferences(string identifier, TestShippingPreferences preferences)
        {
            //create handling and flat rate generic groups
            await CreateEmptyGroups(identifier, identifier);

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                CreatedBy = "Data factory tool",
                ShippingConfigurationExternalId = identifier,
                FlatRateScheduleGroupExternalId = identifier,
                HandlingScheduleGroupExternalId = identifier,
                UseCustomerCarrier = preferences.UseCustomerCarrier,
                FreeFreightForNonContiguousStates = preferences.FreeFreightForNonContiguousStates,
                FreeFreightRules = MapTenantFreeFreightRules(preferences.FreeFreightRuless),
                FreeHandlingRules = MapFreeHandlingRules(preferences.FreeHandlingRules),
                UseBestRate = preferences.UseBestRate,
                UpdatedBy = "Data factory tool"
            };
            var response = await Client.ShippingPreferences.Create(request);

            if (!response.Success) throw new Exception($"Tenant shipping preferenwce cannot be added ~> error code: {response.StatusCode}");
        }

        public async Task CreateConfiguration(string identifier, TestShippingConfiguration configuration)
        {
            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                CreatedBy = "data factory tool",
                DefaultServiceLevelCode = ParseDefaultServiceLevel(configuration.DefaultServiceLevel),
                ExternalIdentifier = identifier,
                ServiceLevels = MapConfigurationServiceLevels(configuration.ServiceLevels)
            };

            var response = await Client.ShippingConfigurations.Create(request);
            if (!response.Success) throw new Exception($"Shipping configuration cannot be created ~> error code: {response.StatusCode}");
        }

        public async Task AddFlatRateSchedules(string groupIdentifier, List<TestSchedule> flatRates)
        {
            if (flatRates != null)
            {
                foreach (var schedule in flatRates)
                {
                    //create each schedule
                    var flatRateScheduleRequest = new FlatRateScheduleRequest
                    {
                        CreatedBy = "data factory tool",
                        ExternalIdentifier = schedule.ExternalIdentifier,
                        OrderAmountMax = schedule.OrderAmountMax,
                        OrderAmountMin = schedule.OrderAmountMin,
                        Rate = schedule.Rate,
                        ServiceLevelCode = (int)schedule.ServiceLevelCode
                    };

                    var flatRateScheduleResponse = await Client.FlatRateSchedules.Create(flatRateScheduleRequest);
                    if (!flatRateScheduleResponse.Success) throw new Exception($"Flat rate schedule ({schedule.ExternalIdentifier}) cannot be created ~> error code {flatRateScheduleResponse.StatusCode}");

                    //asociate the schedule with group
                    var configurationRequest = new FlatRateScheduleConfigurationRequest
                    {
                        CreatedBy = "data factory tool"
                    };

                    var configurationResponse = await Client.FlatRateScheduleConfigurations.Create(groupIdentifier, schedule.ExternalIdentifier, configurationRequest);
                    if (!configurationResponse.Success) throw new Exception($"Flat rate group {configurationResponse.StatusCode} cannot be created error ~> {configurationResponse.StatusCode}");
                }
            }
        }

        public async Task AddHandlingSchedules(string groupIdentifier, List<TestSchedule> handlingRates)
        {
            if (handlingRates != null)
            {
                foreach (var schedule in handlingRates)
                {
                    var scheduleRequest = new HandlingScheduleRequest
                    {
                        CreatedBy = "data factory tool",
                        ExternalIdentifier = schedule.ExternalIdentifier,
                        OrderAmountMax = schedule.OrderAmountMax,
                        OrderAmountMin = schedule.OrderAmountMin,
                        Rate = schedule.Rate,
                        ServiceLevelCode = (int)schedule.ServiceLevelCode
                    };
                    var scheduleResponse = await Client.HandlingSchedules.Create(scheduleRequest);
                    if (!scheduleResponse.Success) throw new Exception("Schedule cannot be created");

                    var scheduleConfigRequest = new HandlingSchedulesConfigurationRequest
                    {
                        CreatedBy = "data factory tool"
                    };
                    var scheduleConfigResponse = await Client.HandlingScheduleConfigurations.Create(groupIdentifier, schedule.ExternalIdentifier, scheduleConfigRequest);
                    if (!scheduleConfigResponse.Success) throw new Exception($"Schedule {schedule.ExternalIdentifier} in {groupIdentifier} cannot be added. error ~> {scheduleConfigResponse.StatusCode}");
                }
            }
        }

        public async Task RemoveFlatRates(string groupIdentifier, List<TestSchedule> flatRates)
        {
            if (flatRates != null)
            {
                foreach (var schedule in flatRates)
                {
                    //remove schedule configuration
                    await Client.FlatRateScheduleConfigurations.Remove(groupIdentifier, schedule.ExternalIdentifier);
                    //remove schedule
                    await Client.FlatRateSchedules.Remove(schedule.ExternalIdentifier);
                }

                //remove schedule group
                await Client.FlatRateScheduleGroups.Remove(groupIdentifier);
            }
            else
            {
                //remove schedule group
                var groupResponse = await Client.FlatRateScheduleGroups.Remove(groupIdentifier);

                if (groupResponse.StatusCode == 500)
                {
                    throw new Exception($"Flat rate group {groupIdentifier} cannot be removed. Code ~> {groupResponse.StatusCode}. Probably the group has schedules");
                }
            }
        }

        public async Task RemoveHandlingRates(string groupIdentifier, List<TestSchedule> handlingRates)
        {
            if (handlingRates != null)
            {
                foreach (var schedule in handlingRates)
                {
                    //remove schedule configuration
                    await Client.HandlingScheduleConfigurations.Remove(groupIdentifier, schedule.ExternalIdentifier);
                    //remove schedule
                    await Client.HandlingSchedules.Remove(schedule.ExternalIdentifier);
                }

                //remove group
                await Client.HandlingScheduleGroups.Remove(groupIdentifier);
            }
            else
            {
                //remove schedule group
                var groupResponse = await Client.HandlingScheduleGroups.Remove(groupIdentifier);

                if (groupResponse.StatusCode == 500)
                {
                    throw new Exception($"Handling group {groupIdentifier} cannot be removed. Code ~> {groupResponse.StatusCode}. Probably the group has schedules");
                }
            }
        }

        public async Task RemoveAccountPreferences(string accountMasterId)
        {
            await Client.ShippingAccountMasterPreferences.Remove(accountMasterId);
        }

        public async Task RemoveTenantPreferences()
        {
            await Client.ShippingPreferences.Remove();
        }

        public async Task RemoveConfiguration(string identifier)
        {
            await Client.ShippingConfigurations.Remove(identifier);
        }

        public async Task AuthenticateWithToken(TestShippingAuthCredential authCredential)
        {
            var authRequest = new ShippingAuthRequest
            {
                Name = authCredential.Name,
                Password = authCredential.Password,
                PublicKey = authCredential.PublicKey
            };

            var response = await Client.Token.Authenticate(authRequest);

            if (!response.Success)
            {
                throw new Exception($"Shipping auth request responded with: {response.StatusCode} code");
            }

            //reasign client dependency
            string essUrl = ApiUrlHelper.GetRequesterFormatUrl(_essApiUrl);
            Client = new ShippingServiceClient(essUrl, _tenantExtId, response.Result, ApiUrlHelper.UrlContainsHttps(_essApiUrl));

            //if response is null, then auth is not success
            var testResponse = await Client.ShippingConfigurations.GetSingle("string");
            if (testResponse == null)
            {
                throw new Exception("Endpoints are not authenticated");
            }
        }

        //helper methods
        private async Task CreateEmptyGroups(string flatRateGroupId, string handlingGroupId)
        {
            var flatRateRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "Data factory helper",
                ExternalIdentifier = flatRateGroupId,
                Name = "sample flat rate group name"
            };
            var handlingRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "Data factory helper",
                Name = "sample handling group name",
                ExternalIdentifier = handlingGroupId
            };

            var flatRateResponse = await Client.FlatRateScheduleGroups.Create(flatRateRequest);
            if (!flatRateResponse.Success) throw new Exception($"Flat rate group cannot be created error ~> {flatRateResponse.StatusCode}");

            var handlingResponse = await Client.HandlingScheduleGroups.Create(handlingRequest);
            if (!handlingResponse.Success) throw new Exception($"Handling group cannot be created error ~> {handlingResponse.StatusCode}");
        }

        private List<SAMFreeFreightRule> MapAccountFreeFreightRules(List<TestFreeFreightRule> freeFreightRules)
        {
            List<SAMFreeFreightRule> rules = new List<SAMFreeFreightRule>();

            //map if different to null
            if (freeFreightRules != null)
            {
                foreach (var rule in freeFreightRules)
                {
                    rules.Add(new SAMFreeFreightRule
                    {
                        ServiceLevelCode = (int)rule.ServiceLevelCode,
                        ThresholdAmount = rule.ThresholdAmount
                    });
                }
            }

            return rules;
        }

        private List<SFreeFreightRule> MapTenantFreeFreightRules(List<TestFreeFreightRule> freeFreightRules)
        {
            List<SFreeFreightRule> rules = new List<SFreeFreightRule>();

            //map if different to null
            if (freeFreightRules != null)
            {
                foreach (var rule in freeFreightRules)
                {
                    rules.Add(new SFreeFreightRule
                    {
                        ServiceLevelCode = (int)rule.ServiceLevelCode,
                        ThresholdAmount = rule.ThresholdAmount
                    });
                }
            }

            return rules;
        }

        private List<SCServiceLevel> MapConfigurationServiceLevels(List<TestServiceLevel> serviceLevels)
        {
            List<SCServiceLevel> serviceLevelsResult = new List<SCServiceLevel>();

            foreach (var serviceLevel in serviceLevels)
            {
                serviceLevelsResult.Add(new SCServiceLevel
                {
                    Amount = serviceLevel.Amount,
                    Code = (int)serviceLevel.Code,
                    Label = serviceLevel.Label,
                    SortOrder = serviceLevel.SortOrder,
                    CarrierRateDiscount = serviceLevel.CarrierRateDiscount,
                    IsEnabled = true,
                    CalculationMethod = string.Empty,
                    CarrierCode = string.Empty,
                    ErpExtId = serviceLevel.ErpExtId,
                    RateShopperExtId = ParseRateShopperExternalId(serviceLevel.RateShopperExtId)
                });
            }

            return serviceLevelsResult;
        }

        private int? ParseDefaultServiceLevel(ServiceLevelCodesEnum? serviceLevel)
        {
            if (serviceLevel != null)
            {
                return (int)serviceLevel;
            }
            return null;
        }

        private string ParseRateShopperExternalId(RateShopperShipperCodesEnum? rateShopperCode)
        {
            if (rateShopperCode == null)
                return null;

            //get the code as string
            string rateShopperCodeString = rateShopperCode.ToString();

            if (string.IsNullOrEmpty(rateShopperCodeString) || string.IsNullOrWhiteSpace(rateShopperCodeString))
                return null;

            return rateShopperCodeString;
        }

        private List<FreeHandlingRule> MapFreeHandlingRules(List<TestFreeHandlingRule> freeHandlingRules)
        {
            List<FreeHandlingRule> rules = new List<FreeHandlingRule>();

            //Do not map if null
            if (freeHandlingRules != null)
            {
                rules = freeHandlingRules.Select(testRule => new FreeHandlingRule
                {
                    ServiceLevelCode = (int)testRule.ServiceLevelCode,
                    ThresholdAmount = testRule.ThresholdAmount
                }).ToList();
            }

            return rules;
        }
    }
}
