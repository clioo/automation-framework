using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestShippingExternals
    {
        public string AccountMasterExtId { get; set; }
        public string ConfigurationExtId { get; set; }
        public string HandlingGroupExtId { get; set; }
        public string FlatRateGroupExtId { get; set; }

        public List<TestSchedule> HandlingSchedules { get; set; }
        public List<TestSchedule> FlatRateSchedules { get; set; }
    }
}
