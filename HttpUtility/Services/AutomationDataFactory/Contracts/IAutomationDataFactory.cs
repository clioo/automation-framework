namespace HttpUtility.Services.AutomationDataFactory.Contracts
{
    public interface IAutomationDataFactory
    {
        IUserAccountsFactory Users { get; set; }
        IShippingConfigurationFactory Shipping { get; set; }
        IProductsFactory Products { get; set; }
    }
}
