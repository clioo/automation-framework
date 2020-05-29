using CommonHelper.BaseComponents;
using FMPOnlinePOM.Components.Constants;
using FMPOnlinePOM.PageObjects.IndexHome;
using FMPOnlinePOM.PageObjects.MyAccount.Addresses;
using FMPOnlinePOM.PageObjects.SignInRegister;
using OpenQA.Selenium;

namespace FMPOnlinePOM.Components
{
    public class FmpHeader : BaseHeader
    {
        public FmpHeader(IWebDriver driver) : base(driver)
        {
        }

        public FmpLoginPage ClickOnSignIn()
        {
            SelectSignInOption();
            return new FmpLoginPage(Driver);
        }

        public FmpIndexPage ClickOnLogOut()
        {
            SelectMyAccountMenuItem(FmpMyAccountMenuItemsLabels.SignOut);
            return new FmpIndexPage(Driver);
        }

        public FmpAddressesHomePage ClickOnAddresses()
        {
            SelectMyAccountMenuItem(FmpMyAccountMenuItemsLabels.Addresses);
            return new FmpAddressesHomePage(Driver);
        }

        public FmpContactInfoPage ClickOnContactInfoPage() 
        {
            SelectMyAccountMenuItem(FmpMyAccountMenuItemsLabels.ContactInformation);
            return new FmpContactInfoPage(Driver);
        }

        //TODO
        //orders page
        //lists page
        //dashboard page
        //payments page
        //quick orders
    }
}
