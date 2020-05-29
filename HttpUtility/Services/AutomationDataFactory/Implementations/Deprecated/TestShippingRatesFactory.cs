using HttpUtility.Clients;
using HttpUtility.EndPoints.ShippingService;
using HttpUtility.EndPoints.ShippingService.Models.FlatRatesSchedulesConfiguration;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedules;
using HttpUtility.EndPoints.ShippingService.Models.HandlingSchedulesConfiguration;
using HttpUtility.Services.AutomationDataFactory.Contracts;
using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Implementations
{
    public class TestShippingRatesFactory : ITestShippingRatesFactory
    {
        IShippingServiceClient _client;

        public TestShippingRatesFactory(IShippingServiceClient client)
        {
            _client = client;
        }

        public async Task AddFlatRatesToGroup(string groupExtId, List<TestSchedule> schedules)
        {

            foreach (var schedule in schedules)
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

                var flatRateScheduleResponse = await _client.FlatRateSchedules.Create(flatRateScheduleRequest);
                if (!flatRateScheduleResponse.Success) throw new Exception("Flat rate schedule cannot be created, " + schedule.ExternalIdentifier);

                //asociate the schedule with group
                var configurationRequest = new FlatRateScheduleConfigurationRequest
                {
                    CreatedBy = "data factory tool"
                };

                var configurationResponse = await _client.FlatRateScheduleConfigurations.Create(groupExtId, schedule.ExternalIdentifier, configurationRequest);
                if (!configurationResponse.Success) throw new Exception("Flat rate group request cannot be created");
            }
        }

        public async Task AddHandlingsToGroup(string groupExtId, List<TestSchedule> schedules)
        {

            foreach (var schedule in schedules)
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
                var scheduleResponse = await _client.HandlingSchedules.Create(scheduleRequest);
                if (!scheduleResponse.Success) throw new Exception("Schedule cannot be created");

                var scheduleConfigRequest = new HandlingSchedulesConfigurationRequest
                {
                    CreatedBy = "data factory tool"
                };
                var scheduleConfigResponse = await _client.HandlingScheduleConfigurations.Create(groupExtId, schedule.ExternalIdentifier, scheduleConfigRequest);
                if (!scheduleConfigResponse.Success) throw new Exception($"Schedule cannot be added into {groupExtId}");
            }
        }
    }
}
