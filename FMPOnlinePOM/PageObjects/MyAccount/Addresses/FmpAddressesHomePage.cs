using CommonHelper.Pages.AddressesPage;
using FMPOnlinePOM.Components;
using OpenQA.Selenium;

namespace FMPOnlinePOM.PageObjects.MyAccount.Addresses
{
    //inherits from a base address page
    public class FmpAddressesHomePage : BaseAddressesHomePage
    {
        public FmpHeader Header;
        public FmpAddressesHomePage(IWebDriver driver) : base(driver)
        {
            //TODO
            //initialize components(headers, etc)
            Header = new FmpHeader(driver);
        }
    }
}
