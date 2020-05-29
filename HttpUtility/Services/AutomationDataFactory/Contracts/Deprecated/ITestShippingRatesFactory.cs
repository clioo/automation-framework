using HttpUtility.Services.AutomationDataFactory.Models.Shipping;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface ITestShippingRatesFactory
    {
        Task AddFlatRatesToGroup(string groupExtId, List<TestSchedule> schedules);
        Task AddHandlingsToGroup(string groupExtId, List<TestSchedule> schedules);
    }
}
