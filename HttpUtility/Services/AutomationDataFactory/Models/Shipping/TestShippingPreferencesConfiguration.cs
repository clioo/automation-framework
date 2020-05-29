using System.Collections.Generic;

namespace HttpUtility.Services.AutomationDataFactory.Models.Shipping
{
    public class TestShippingPreferencesConfiguration
    {
        public string Identifier { get; set; }        
        public TestShippingConfiguration Configuration { get; set; }
        public TestShippingPreferences Preferences { get; set; } = new TestShippingPreferences();
        public List<TestSchedule> HandlingSchedules { get; set; }
        public List<TestSchedule> FlatRateSchedules { get; set; }
    }
}
