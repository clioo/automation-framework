namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface ITestDataFactory
    {
        ITestAddressesFactory Addresses { get; set; }
        ITestUserAccountsFactory UserAccounts { get; set; }
        ITestShippingConfigurationFactory ShippingConfigurationPreferences { get; set; }
        ITestShippingRatesFactory ShippingRates { get; set; }
    }
}
