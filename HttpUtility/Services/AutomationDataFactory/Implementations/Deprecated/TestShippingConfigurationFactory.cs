using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models.ShippingAccountMasterPreferences;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class TestShippingConfigurationFactory : ITestShippingConfigurationFactory
    {
        public IShippingServiceClient _client { get; set; }

        public TestShippingConfigurationFactory(IShippingServiceClient client)
        {
            _client = client;
        }

        public async Task RemoveTenantPreferences(TestShippingExternals identifiers)
        {
            await _client.ShippingPreferences.Remove();

            await RemoveHandlings(identifiers.HandlingSchedules, identifiers.HandlingGroupExtId);            
            await RemoveFlatRates(identifiers.FlatRateSchedules, identifiers.FlatRateGroupExtId);            

            await _client.ShippingConfigurations.Remove(identifiers.ConfigurationExtId);
        }

        public async Task RemoveAccountPreferences(TestShippingExternals identifiers)
        {
            await _client.ShippingAccountMasterPreferences.Remove(identifiers.AccountMasterExtId);

            await RemoveHandlings(identifiers.HandlingSchedules, identifiers.HandlingGroupExtId);
            await RemoveFlatRates(identifiers.FlatRateSchedules, identifiers.FlatRateGroupExtId);

            await _client.ShippingConfigurations.Remove(identifiers.ConfigurationExtId);
        }

        public async Task AddAccountPreferences(TestShippingExternals externalIds, TestShippingPreferences preferences)
        {
            //create handling and  flat rate generic groups
            await CreateEmptyGroups(externalIds.FlatRateGroupExtId, externalIds.HandlingGroupExtId);

            ShippingAccountMasterPreferencesRequest request = new ShippingAccountMasterPreferencesRequest
            {
                CreatedBy = "Data factory tool",
                FlatRateScheduleGroupExternalId = externalIds.FlatRateGroupExtId,
                HandlingScheduleGroupExternalId = externalIds.HandlingGroupExtId,
                UseCustomerCarrier = preferences.UseCustomerCarrier,
                ShippingConfigurationExternalId = preferences.ConfigurationExtId,
                FreeFreightRules = preferences.FreeFreightRules,
                FreeFreightForNonContiguousStates = preferences.FreeFreightForNonContiguousStates,
                UseBestRate = preferences.UseBestRate

            };
            var response = await _client.ShippingAccountMasterPreferences.Create(externalIds.AccountMasterExtId, request);

            if (!response.Success) throw new Exception("Cannot add the given account shipping preferences");
        }

        public async Task AddTenantPreferences(TestShippingExternals externalIds, TestShippingPreferences preferences)
        {
            //create handling and flat rate generic groups
            await CreateEmptyGroups(externalIds.FlatRateGroupExtId, externalIds.HandlingGroupExtId);

            ShippingPreferencesRequest request = new ShippingPreferencesRequest
            {
                CreatedBy = "Data factory tool",
                UseCustomerCarrier = preferences.UseCustomerCarrier,
                FreeFreightRules = GetFreefreightRules(preferences.FreeFreightRules),
                ShippingConfigurationExternalId = preferences.ConfigurationExtId,
                FlatRateScheduleGroupExternalId = externalIds.FlatRateGroupExtId,
                HandlingScheduleGroupExternalId = externalIds.HandlingGroupExtId,
                FreeFreightForNonContiguousStates = preferences.FreeFreightForNonContiguousStates,
                UseBestRate = preferences.UseBestRate
            };
            var response = await _client.ShippingPreferences.Create(request);

            if (!response.Success) throw new Exception("Tenant shipping preference cannot be added");
        }

        public async Task CreateConfiguration(TestShippingConfiguration configuration)
        {
            ShippingConfigurationRequest request = new ShippingConfigurationRequest
            {
                CreatedBy = "data factory tool",
                DefaultServiceLevelCode = (int)configuration.DefaultServiceLevel,
                ExternalIdentifier = configuration.Identifier,
                ServiceLevels = GetServiceLevels(configuration.ServiceLevels)
            };

            var response = await _client.ShippingConfigurations.Create(request);
            if (!response.Success) throw new Exception("Shipping configuration cannot be created");
        }

        //helper methods
        private async Task CreateEmptyGroups(string flatRateExtId, string handlingExtId)
        {
            var flatRateRequest = new FlatRateScheduleGroupsRequest
            {
                CreatedBy = "Data factory helper",
                ExternalIdentifier = flatRateExtId,
                Name = "sample flat rate group name"
            };
            var handlingRequest = new HandlingScheduleGroupRequest
            {
                CreatedBy = "Data factory helper",
                Name = "sample handling group name",
                ExternalIdentifier = handlingExtId
            };

            var flatRateResponse = await _client.FlatRateScheduleGroups.Create(flatRateRequest);
            if (!flatRateResponse.Success) throw new Exception("Flat rate group cannot be created");

            var handlingResponse = await _client.HandlingScheduleGroups.Create(handlingRequest);
            if (!handlingResponse.Success) throw new Exception("Handling group cannot be created");
        }

        private List<SCServiceLevel> GetServiceLevels(List<TestServiceLevel> serviceLevels)
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
                    CarrierCode = string.Empty
                });
            }

            return serviceLevelsResult;
        }

        private List<SFreeFreightRule> GetFreefreightRules(List<SAMFreeFreightRule> rules)
        {
            List<SFreeFreightRule> freeFreightRules = new List<SFreeFreightRule>();

            //do nothing if rules came null
            if (rules == null) return null;
            
            foreach (var rule in rules)
            {
                freeFreightRules.Add(new SFreeFreightRule
                {
                    ServiceLevelCode = rule.ServiceLevelCode,
                    ThresholdAmount = rule.ThresholdAmount
                });
            }

            return freeFreightRules;
        }

        private async Task RemoveHandlings(List<TestSchedule> schedules, string groupId)
        {
            if (schedules != null)
            {
                foreach (var schedule in schedules)
                {
                    //remove schedule configuration
                    await _client.HandlingScheduleConfigurations.Remove(groupId, schedule.ExternalIdentifier);                    
                    //remove schedule
                    await _client.HandlingSchedules.Remove(schedule.ExternalIdentifier);
                }

                //remove group
                await _client.HandlingScheduleGroups.Remove(groupId);
            }
            else
            {
                //remove schedule group
                await _client.HandlingScheduleGroups.Remove(groupId);
            }
        }

        private async Task RemoveFlatRates(List<TestSchedule> schedules, string groupId)
        {
            if (schedules != null)
            {
                foreach (var schedule in schedules)
                {
                    //remove schedule configuration
                    await _client.FlatRateScheduleConfigurations.Remove(groupId, schedule.ExternalIdentifier);
                    //remove schedule
                    await _client.FlatRateSchedules.Remove(schedule.ExternalIdentifier);
                }

                //remove schedule group
                await _client.FlatRateScheduleGroups.Remove(groupId);
            }
            else
            {
                //remove schedule group
                await _client.FlatRateScheduleGroups.Remove(groupId);
            }
        }
    }
}
